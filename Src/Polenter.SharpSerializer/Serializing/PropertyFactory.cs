
// Type: Polenter.Serialization.Serializing.PropertyFactory
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced;
using Polenter.Serialization.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace Polenter.Serialization.Serializing
{
  public sealed class PropertyFactory
  {
    private readonly object[] _emptyObjectArray = new object[0];
    private readonly PropertyProvider _propertyProvider;
    private IDictionary<object, ComplexProperty> nonDuplicateValues = (IDictionary<object, ComplexProperty>) new Dictionary<object, ComplexProperty>();
    private int nextReferenceId = 1;

    public PropertyFactory(PropertyProvider propertyProvider)
    {
      this._propertyProvider = propertyProvider;
    }

    public Property CreateProperty(string name, object value)
    {
      if (value == null)
        return (Property) new NullProperty(name);
      TypeInfo typeInfo = TypeInfo.GetTypeInfo(value);
      Property property1 = PropertyFactory.createSimpleProperty(name, typeInfo, value);
      if (property1 != null)
        return property1;
      if (typeInfo.IsArray)
        property1 = typeInfo.ArrayDimensionCount >= 2 ? this.createMultiDimensionalArrayProperty(name, typeInfo, value) : this.createSingleDimensionalArrayProperty(name, typeInfo, value);
      else if (typeInfo.IsDictionary)
        property1 = this.createDictionaryProperty(name, typeInfo, value);
      else if (typeInfo.IsCollection)
        property1 = this.createCollectionProperty(name, typeInfo, value);
      else if (typeInfo.IsEnumerable)
        property1 = this.createCollectionProperty(name, typeInfo, value);
      if (property1 == null)
      {
        ComplexProperty referenceTarget;
        if (this.nonDuplicateValues.TryGetValue(value, out referenceTarget))
        {
          if (!referenceTarget.IsReferencedMoreThanOnce)
            referenceTarget.ComplexReferenceId = this.nextReferenceId++;
          return (Property) new ComplexReferenceProperty(name, referenceTarget);
        }
        property1 = (Property) new ComplexProperty(name, typeInfo.Type, value);
      }
      if (property1 is ComplexProperty complexProperty)
      {
        this.nonDuplicateValues.Add(value, complexProperty);
        foreach (PropertyInfo property2 in (IEnumerable<PropertyInfo>) this._propertyProvider.GetProperties(typeInfo))
        {
          object obj = property2.GetValue(value, this._emptyObjectArray);
          Property property3 = this.CreateProperty(property2.Name, obj);
          complexProperty.Properties.Add(property3);
        }
      }
      return property1;
    }

    private Property createCollectionProperty(string name, TypeInfo info, object value)
    {
      CollectionProperty collectionProperty = new CollectionProperty(name, info.Type);
      collectionProperty.ElementType = info.ElementType;
      foreach (object obj in (IEnumerable) value)
      {
        Property property = this.CreateProperty((string) null, obj);
        collectionProperty.Items.Add(property);
      }
      return (Property) collectionProperty;
    }

    private Property createDictionaryProperty(string name, TypeInfo info, object value)
    {
      DictionaryProperty dictionaryProperty = new DictionaryProperty(name, info.Type);
      dictionaryProperty.KeyType = info.KeyType;
      dictionaryProperty.ValueType = info.ElementType;
      foreach (DictionaryEntry dictionaryEntry in (IDictionary) value)
      {
        Property property1 = this.CreateProperty((string) null, dictionaryEntry.Key);
        Property property2 = this.CreateProperty((string) null, dictionaryEntry.Value);
        dictionaryProperty.Items.Add(new KeyValueItem(property1, property2));
      }
      return (Property) dictionaryProperty;
    }

    private Property createMultiDimensionalArrayProperty(string name, TypeInfo info, object value)
    {
      MultiDimensionalArrayProperty dimensionalArrayProperty = new MultiDimensionalArrayProperty(name, info.Type);
      dimensionalArrayProperty.ElementType = info.ElementType;
      ArrayAnalyzer arrayAnalyzer = new ArrayAnalyzer(value);
      dimensionalArrayProperty.DimensionInfos = arrayAnalyzer.ArrayInfo.DimensionInfos;
      foreach (int[] index in arrayAnalyzer.GetIndexes())
      {
        Property property = this.CreateProperty((string) null, ((Array) value).GetValue(index));
        dimensionalArrayProperty.Items.Add(new MultiDimensionalArrayItem(index, property));
      }
      return (Property) dimensionalArrayProperty;
    }

    private Property createSingleDimensionalArrayProperty(string name, TypeInfo info, object value)
    {
      SingleDimensionalArrayProperty dimensionalArrayProperty = new SingleDimensionalArrayProperty(name, info.Type);
      dimensionalArrayProperty.ElementType = info.ElementType;
      ArrayAnalyzer arrayAnalyzer = new ArrayAnalyzer(value);
      DimensionInfo dimensionInfo = arrayAnalyzer.ArrayInfo.DimensionInfos[0];
      dimensionalArrayProperty.LowerBound = dimensionInfo.LowerBound;
      foreach (object obj in arrayAnalyzer.GetValues())
      {
        Property property = this.CreateProperty((string) null, obj);
        dimensionalArrayProperty.Items.Add(property);
      }
      return (Property) dimensionalArrayProperty;
    }

    private static Property createSimpleProperty(string name, TypeInfo typeInfo, object value)
    {
      if (!typeInfo.IsSimple)
        return (Property) null;
      return (Property) new SimpleProperty(name, typeInfo.Type)
      {
        Value = value
      };
    }
  }
}

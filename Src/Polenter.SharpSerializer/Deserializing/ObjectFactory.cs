
// Type: Polenter.Serialization.Deserializing.ObjectFactory
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Core;
using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace Polenter.Serialization.Deserializing
{
  public sealed class ObjectFactory
  {
    private readonly object[] _emptyObjectArray = new object[0];

    public object CreateObject(Property property)
    {
      if (property == null)
        throw new ArgumentNullException(nameof (property));
      if (property is NullProperty)
        return (object) null;
      if (property is ComplexReferenceProperty property1)
        return this.createObjectFromComplexReferenceProperty(property1);
      if ((object) property.Type == null)
        throw new InvalidOperationException(string.Format("Property type is not defined. Property: \"{0}\"", (object) property.Name));
      switch (property)
      {
        case SimpleProperty property2:
          return ObjectFactory.createObjectFromSimpleProperty(property2);
        case MultiDimensionalArrayProperty property3:
          return this.createObjectFromMultidimensionalArrayProperty(property3);
        case SingleDimensionalArrayProperty property4:
          return this.createObjectFromSingleDimensionalArrayProperty(property4);
        case DictionaryProperty property5:
          return this.createObjectFromDictionaryProperty(property5);
        case CollectionProperty property6:
          return this.createObjectFromCollectionProperty(property6);
        case ComplexProperty property7:
          return this.createObjectFromComplexProperty(property7);
        default:
          throw new InvalidOperationException(string.Format("Unknown Property type: {0}", (object) property.GetType().Name));
      }
    }

    private static object createObjectFromSimpleProperty(SimpleProperty property) => property.Value;

    private object createObjectFromComplexProperty(ComplexProperty property)
    {
      object instance = Tools.CreateInstance(property.Type);
      property.Value = instance;
      this.fillProperties(instance, (IEnumerable<Property>) property.Properties);
      return instance;
    }

    private object createObjectFromComplexReferenceProperty(ComplexReferenceProperty property)
    {
      return property.ReferenceTarget.Value;
    }

    private object createObjectFromCollectionProperty(CollectionProperty property)
    {
      object instance = Tools.CreateInstance(property.Type);
      this.fillProperties(instance, (IEnumerable<Property>) property.Properties);
      MethodInfo method = instance.GetType().GetMethod("Add");
      if ((object) method != null && method.GetParameters().Length == 1)
      {
        foreach (Property property1 in (IEnumerable<Property>) property.Items)
        {
          object obj = this.CreateObject(property1);
          method.Invoke(instance, new object[1]{ obj });
        }
      }
      return instance;
    }

    private object createObjectFromDictionaryProperty(DictionaryProperty property)
    {
      object instance = Tools.CreateInstance(property.Type);
      this.fillProperties(instance, (IEnumerable<Property>) property.Properties);
      MethodInfo method = instance.GetType().GetMethod("Add");
      if ((object) method != null && method.GetParameters().Length == 2)
      {
        foreach (KeyValueItem keyValueItem in (IEnumerable<KeyValueItem>) property.Items)
        {
          object obj1 = this.CreateObject(keyValueItem.Key);
          object obj2 = this.CreateObject(keyValueItem.Value);
          method.Invoke(instance, new object[2]
          {
            obj1,
            obj2
          });
        }
      }
      return instance;
    }

    private void fillProperties(object obj, IEnumerable<Property> properties)
    {
      foreach (Property property1 in properties)
      {
        PropertyInfo property2 = obj.GetType().GetProperty(property1.Name);
        if ((object) property2 != null)
        {
          object obj1 = this.CreateObject(property1);
          if (obj1 != null)
            property2.SetValue(obj, obj1, this._emptyObjectArray);
        }
      }
    }

    private object createObjectFromSingleDimensionalArrayProperty(
      SingleDimensionalArrayProperty property)
    {
      int count = property.Items.Count;
      Array arrayInstance = ObjectFactory.createArrayInstance(property.ElementType, new int[1]
      {
        count
      }, new int[1]{ property.LowerBound });
      for (int lowerBound = property.LowerBound; lowerBound < property.LowerBound + count; ++lowerBound)
      {
        object obj = this.CreateObject(property.Items[lowerBound]);
        if (obj != null)
          arrayInstance.SetValue(obj, lowerBound);
      }
      return (object) arrayInstance;
    }

    private object createObjectFromMultidimensionalArrayProperty(
      MultiDimensionalArrayProperty property)
    {
      ObjectFactory.MultiDimensionalArrayCreatingInfo arrayCreatingInfo = ObjectFactory.getMultiDimensionalArrayCreatingInfo((IEnumerable<DimensionInfo>) property.DimensionInfos);
      Array arrayInstance = ObjectFactory.createArrayInstance(property.ElementType, arrayCreatingInfo.Lengths, arrayCreatingInfo.LowerBounds);
      foreach (MultiDimensionalArrayItem dimensionalArrayItem in (IEnumerable<MultiDimensionalArrayItem>) property.Items)
      {
        object obj = this.CreateObject(dimensionalArrayItem.Value);
        if (obj != null)
          arrayInstance.SetValue(obj, dimensionalArrayItem.Indexes);
      }
      return (object) arrayInstance;
    }

    private static Array createArrayInstance(Type elementType, int[] lengths, int[] lowerBounds)
    {
      return Array.CreateInstance(elementType, lengths, lowerBounds);
    }

    private static ObjectFactory.MultiDimensionalArrayCreatingInfo getMultiDimensionalArrayCreatingInfo(
      IEnumerable<DimensionInfo> infos)
    {
      List<int> intList1 = new List<int>();
      List<int> intList2 = new List<int>();
      foreach (DimensionInfo info in infos)
      {
        intList1.Add(info.Length);
        intList2.Add(info.LowerBound);
      }
      return new ObjectFactory.MultiDimensionalArrayCreatingInfo()
      {
        Lengths = intList1.ToArray(),
        LowerBounds = intList2.ToArray()
      };
    }

    private class MultiDimensionalArrayCreatingInfo
    {
      public int[] Lengths { get; set; }

      public int[] LowerBounds { get; set; }
    }
  }
}

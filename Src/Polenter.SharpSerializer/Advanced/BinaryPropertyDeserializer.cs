
// Type: Polenter.Serialization.Advanced.BinaryPropertyDeserializer
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced.Binary;
using Polenter.Serialization.Advanced.Deserializing;
using Polenter.Serialization.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

#nullable disable
namespace Polenter.Serialization.Advanced
{
  public sealed class BinaryPropertyDeserializer : IPropertyDeserializer
  {
    private readonly IBinaryReader _reader;
    private IDictionary<int, ComplexProperty> complexItems = (IDictionary<int, ComplexProperty>) new Dictionary<int, ComplexProperty>();

    public BinaryPropertyDeserializer(IBinaryReader reader)
    {
      this._reader = reader != null ? reader : throw new ArgumentNullException(nameof (reader));
    }

    public void Open(Stream stream) => this._reader.Open(stream);

    public Property Deserialize() => this.deserialize(this._reader.ReadElementId(), (Type) null);

    public void Close() => this._reader.Close();

    private Property deserialize(byte elementId, Type expectedType)
    {
      string propertyName = this._reader.ReadName();
      return this.deserialize(elementId, propertyName, expectedType);
    }

    private Property deserialize(byte elementId, string propertyName, Type expectedType)
    {
      Type propertyType = this._reader.ReadType();
      if ((object) propertyType == null)
        propertyType = expectedType;
      Property property1 = this.createProperty(elementId, propertyName, propertyType);
      switch (property1)
      {
        case null:
          return (Property) null;
        case NullProperty nullProperty:
          return (Property) nullProperty;
        case SimpleProperty property2:
          this.parseSimpleProperty(property2);
          return (Property) property2;
        case MultiDimensionalArrayProperty property3:
          this.parseMultiDimensionalArrayProperty(property3);
          return (Property) property3;
        case SingleDimensionalArrayProperty property4:
          this.parseSingleDimensionalArrayProperty(property4);
          return (Property) property4;
        case DictionaryProperty property5:
          this.parseDictionaryProperty(property5);
          return (Property) property5;
        case CollectionProperty property6:
          this.parseCollectionProperty(property6);
          return (Property) property6;
        case ComplexProperty property7:
          this.parseComplexProperty(property7, elementId == (byte) 8);
          return (Property) property7;
        case ComplexReferenceProperty property8:
          this.parseComplexReferenceProperty(property8);
          return (Property) property8;
        default:
          return property1;
      }
    }

    private void parseComplexReferenceProperty(ComplexReferenceProperty property)
    {
      int key = this._reader.ReadNumber();
      ComplexProperty complexProperty;
      if (this.complexItems.TryGetValue(key, out complexProperty))
        property.ReferenceTarget = complexProperty;
      else
        throw new FormatException(string.Format("{0}-parser : Cannot find <{6} {4}='{5}'/>  when resolving <{1} {2} ='{3}' {4}='{5}'/>", (object) this.GetType().Name, (object) "ComplexReference", (object) "name", (object) property.Name, (object) "id", (object) key, (object) "Complex"));
    }

    private void parseComplexProperty(ComplexProperty property, bool withReferenceId)
    {
      if (withReferenceId)
      {
        property.ComplexReferenceId = this._reader.ReadNumber();
        this.complexItems.Add(property.ComplexReferenceId, property);
      }
      this.readProperties(property.Properties, property.Type);
    }

    private void readProperties(PropertyCollection properties, Type ownerType)
    {
      int num = this._reader.ReadNumber();
      for (int index = 0; index < num; ++index)
      {
        byte elementId = this._reader.ReadElementId();
        string str = this._reader.ReadName();
        PropertyInfo property1 = ownerType.GetProperty(str);
        Property property2 = this.deserialize(elementId, str, property1.PropertyType);
        properties.Add(property2);
      }
    }

    private void parseCollectionProperty(CollectionProperty property)
    {
      property.ElementType = this._reader.ReadType();
      this.readProperties(property.Properties, property.Type);
      this.readItems((ICollection<Property>) property.Items, property.ElementType);
    }

    private void parseDictionaryProperty(DictionaryProperty property)
    {
      property.KeyType = this._reader.ReadType();
      property.ValueType = this._reader.ReadType();
      this.readProperties(property.Properties, property.Type);
      this.readDictionaryItems(property.Items, property.KeyType, property.ValueType);
    }

    private void readDictionaryItems(
      IList<KeyValueItem> items,
      Type expectedKeyType,
      Type expectedValueType)
    {
      int num = this._reader.ReadNumber();
      for (int index = 0; index < num; ++index)
        this.readDictionaryItem(items, expectedKeyType, expectedValueType);
    }

    private void readDictionaryItem(
      IList<KeyValueItem> items,
      Type expectedKeyType,
      Type expectedValueType)
    {
      KeyValueItem keyValueItem = new KeyValueItem(this.deserialize(this._reader.ReadElementId(), expectedKeyType), this.deserialize(this._reader.ReadElementId(), expectedValueType));
      items.Add(keyValueItem);
    }

    private void parseSingleDimensionalArrayProperty(SingleDimensionalArrayProperty property)
    {
      property.ElementType = this._reader.ReadType();
      property.LowerBound = this._reader.ReadNumber();
      this.readItems((ICollection<Property>) property.Items, property.ElementType);
    }

    private void readItems(ICollection<Property> items, Type expectedElementType)
    {
      int num = this._reader.ReadNumber();
      for (int index = 0; index < num; ++index)
      {
        Property property = this.deserialize(this._reader.ReadElementId(), expectedElementType);
        items.Add(property);
      }
    }

    private void parseMultiDimensionalArrayProperty(MultiDimensionalArrayProperty property)
    {
      property.ElementType = this._reader.ReadType();
      this.readDimensionInfos(property.DimensionInfos);
      this.readMultiDimensionalArrayItems(property.Items, property.ElementType);
    }

    private void readMultiDimensionalArrayItems(
      IList<MultiDimensionalArrayItem> items,
      Type expectedElementType)
    {
      int num = this._reader.ReadNumber();
      for (int index = 0; index < num; ++index)
        this.readMultiDimensionalArrayItem(items, expectedElementType);
    }

    private void readMultiDimensionalArrayItem(
      IList<MultiDimensionalArrayItem> items,
      Type expectedElementType)
    {
      MultiDimensionalArrayItem dimensionalArrayItem = new MultiDimensionalArrayItem(this._reader.ReadNumbers(), this.deserialize(this._reader.ReadElementId(), expectedElementType));
      items.Add(dimensionalArrayItem);
    }

    private void readDimensionInfos(IList<DimensionInfo> dimensionInfos)
    {
      int num = this._reader.ReadNumber();
      for (int index = 0; index < num; ++index)
        this.readDimensionInfo(dimensionInfos);
    }

    private void readDimensionInfo(IList<DimensionInfo> dimensionInfos)
    {
      dimensionInfos.Add(new DimensionInfo()
      {
        Length = this._reader.ReadNumber(),
        LowerBound = this._reader.ReadNumber()
      });
    }

    private void parseSimpleProperty(SimpleProperty property)
    {
      property.Value = this._reader.ReadValue(property.Type);
    }

    private Property createProperty(byte elementId, string propertyName, Type propertyType)
    {
      switch (elementId)
      {
        case 1:
          return (Property) new CollectionProperty(propertyName, propertyType);
        case 2:
        case 8:
          return (Property) new ComplexProperty(propertyName, propertyType);
        case 3:
          return (Property) new DictionaryProperty(propertyName, propertyType);
        case 4:
          return (Property) new MultiDimensionalArrayProperty(propertyName, propertyType);
        case 5:
          return (Property) new NullProperty(propertyName);
        case 6:
          return (Property) new SimpleProperty(propertyName, propertyType);
        case 7:
          return (Property) new SingleDimensionalArrayProperty(propertyName, propertyType);
        case 9:
          return (Property) new ComplexReferenceProperty(propertyName);
        default:
          return (Property) null;
      }
    }
  }
}

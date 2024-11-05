
// Type: Polenter.Serialization.Advanced.XmlPropertyDeserializer
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced.Deserializing;
using Polenter.Serialization.Advanced.Xml;
using Polenter.Serialization.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

#nullable disable
namespace Polenter.Serialization.Advanced
{
  public sealed class XmlPropertyDeserializer : IPropertyDeserializer
  {
    private readonly IXmlReader _reader;
    private IDictionary<int, ComplexProperty> complexItems = (IDictionary<int, ComplexProperty>) new Dictionary<int, ComplexProperty>();

    public XmlPropertyDeserializer(IXmlReader reader) => this._reader = reader;

    public void Open(Stream stream) => this._reader.Open(stream);

    public Property Deserialize()
    {
      XmlPropertyDeserializer.PropertyTag propertyTag = XmlPropertyDeserializer.getPropertyTag(this._reader.ReadElement());
      return propertyTag == XmlPropertyDeserializer.PropertyTag.Unknown ? (Property) null : this.deserialize(propertyTag, (Type) null);
    }

    public void Close() => this._reader.Close();

    private Property deserialize(XmlPropertyDeserializer.PropertyTag propertyTag, Type expectedType)
    {
      string attributeAsString = this._reader.GetAttributeAsString("name");
      Type propertyType = this._reader.GetAttributeAsType("type");
      if ((object) propertyType == null)
        propertyType = expectedType;
      Property property1 = XmlPropertyDeserializer.createProperty(propertyTag, attributeAsString, propertyType);
      switch (property1)
      {
        case null:
          return (Property) null;
        case NullProperty nullProperty:
          return (Property) nullProperty;
        case SimpleProperty property2:
          this.parseSimpleProperty(this._reader, property2);
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
        case ComplexReferenceProperty property7:
          this.parseComplexReferenceProperty(property7);
          return (Property) property7;
        case ComplexProperty property8:
          this.parseComplexProperty(property8);
          return (Property) property8;
        default:
          return property1;
      }
    }

    private void parseCollectionProperty(CollectionProperty property)
    {
      property.ElementType = (object) property.Type != null ? Polenter.Serialization.Serializing.TypeInfo.GetTypeInfo(property.Type).ElementType : (Type) null;
      foreach (string readSubElement in this._reader.ReadSubElements())
      {
        switch (readSubElement)
        {
          case "Properties":
            this.readProperties(property.Properties, property.Type);
            continue;
          case "Items":
            this.readItems((ICollection<Property>) property.Items, property.ElementType);
            continue;
          default:
            continue;
        }
      }
    }

    private void parseDictionaryProperty(DictionaryProperty property)
    {
      if ((object) property.Type != null)
      {
        Polenter.Serialization.Serializing.TypeInfo typeInfo = Polenter.Serialization.Serializing.TypeInfo.GetTypeInfo(property.Type);
        property.KeyType = typeInfo.KeyType;
        property.ValueType = typeInfo.ElementType;
      }
      foreach (string readSubElement in this._reader.ReadSubElements())
      {
        switch (readSubElement)
        {
          case "Properties":
            this.readProperties(property.Properties, property.Type);
            continue;
          case "Items":
            this.readDictionaryItems(property.Items, property.KeyType, property.ValueType);
            continue;
          default:
            continue;
        }
      }
    }

    private void readDictionaryItems(
      IList<KeyValueItem> items,
      Type expectedKeyType,
      Type expectedValueType)
    {
      foreach (string readSubElement in this._reader.ReadSubElements())
      {
        if (readSubElement == "Item")
          this.readDictionaryItem(items, expectedKeyType, expectedValueType);
      }
    }

    private void readDictionaryItem(
      IList<KeyValueItem> items,
      Type expectedKeyType,
      Type expectedValueType)
    {
      Property key = (Property) null;
      Property property = (Property) null;
      foreach (string readSubElement in this._reader.ReadSubElements())
      {
        if (key != null)
        {
          if (property != null)
            break;
        }
        XmlPropertyDeserializer.PropertyTag propertyTag = XmlPropertyDeserializer.getPropertyTag(readSubElement);
        if (propertyTag != XmlPropertyDeserializer.PropertyTag.Unknown)
        {
          if (key == null)
            key = this.deserialize(propertyTag, expectedKeyType);
          else
            property = this.deserialize(propertyTag, expectedValueType);
        }
      }
      KeyValueItem keyValueItem = new KeyValueItem(key, property);
      items.Add(keyValueItem);
    }

    private void parseMultiDimensionalArrayProperty(MultiDimensionalArrayProperty property)
    {
      property.ElementType = (object) property.Type != null ? Polenter.Serialization.Serializing.TypeInfo.GetTypeInfo(property.Type).ElementType : (Type) null;
      foreach (string readSubElement in this._reader.ReadSubElements())
      {
        if (readSubElement == "Dimensions")
          this.readDimensionInfos(property.DimensionInfos);
        if (readSubElement == "Items")
          this.readMultiDimensionalArrayItems(property.Items, property.ElementType);
      }
    }

    private void readMultiDimensionalArrayItems(
      IList<MultiDimensionalArrayItem> items,
      Type expectedElementType)
    {
      foreach (string readSubElement in this._reader.ReadSubElements())
      {
        if (readSubElement == "Item")
          this.readMultiDimensionalArrayItem(items, expectedElementType);
      }
    }

    private void readMultiDimensionalArrayItem(
      IList<MultiDimensionalArrayItem> items,
      Type expectedElementType)
    {
      int[] attributeAsArrayOfInt = this._reader.GetAttributeAsArrayOfInt("indexes");
      foreach (string readSubElement in this._reader.ReadSubElements())
      {
        XmlPropertyDeserializer.PropertyTag propertyTag = XmlPropertyDeserializer.getPropertyTag(readSubElement);
        if (propertyTag != XmlPropertyDeserializer.PropertyTag.Unknown)
        {
          Property property = this.deserialize(propertyTag, expectedElementType);
          MultiDimensionalArrayItem dimensionalArrayItem = new MultiDimensionalArrayItem(attributeAsArrayOfInt, property);
          items.Add(dimensionalArrayItem);
        }
      }
    }

    private void readDimensionInfos(IList<DimensionInfo> dimensionInfos)
    {
      foreach (string readSubElement in this._reader.ReadSubElements())
      {
        if (readSubElement == "Dimension")
          this.readDimensionInfo(dimensionInfos);
      }
    }

    private void readDimensionInfo(IList<DimensionInfo> dimensionInfos)
    {
      dimensionInfos.Add(new DimensionInfo()
      {
        Length = this._reader.GetAttributeAsInt("length"),
        LowerBound = this._reader.GetAttributeAsInt("lowerBound")
      });
    }

    private void parseSingleDimensionalArrayProperty(SingleDimensionalArrayProperty property)
    {
      property.ElementType = (object) property.Type != null ? Polenter.Serialization.Serializing.TypeInfo.GetTypeInfo(property.Type).ElementType : (Type) null;
      property.LowerBound = this._reader.GetAttributeAsInt("lowerBound");
      foreach (string readSubElement in this._reader.ReadSubElements())
      {
        if (readSubElement == "Items")
          this.readItems((ICollection<Property>) property.Items, property.ElementType);
      }
    }

    private void readItems(ICollection<Property> items, Type expectedElementType)
    {
      foreach (string readSubElement in this._reader.ReadSubElements())
      {
        XmlPropertyDeserializer.PropertyTag propertyTag = XmlPropertyDeserializer.getPropertyTag(readSubElement);
        if (propertyTag != XmlPropertyDeserializer.PropertyTag.Unknown)
        {
          Property property = this.deserialize(propertyTag, expectedElementType);
          items.Add(property);
        }
      }
    }

    private void parseComplexReferenceProperty(ComplexReferenceProperty property)
    {
      int attributeAsInt = this._reader.GetAttributeAsInt("id");
      ComplexProperty complexProperty;
      if (this.complexItems.TryGetValue(attributeAsInt, out complexProperty))
        property.ReferenceTarget = complexProperty;
      else
        throw new FormatException(string.Format("{0}-parser : Cannot find <{6} {4}='{5}'/>  when resolving <{1} {2} ='{3}' {4}='{5}'/>", (object) this.GetType().Name, (object) "ComplexReference", (object) "name", (object) property.Name, (object) "id", (object) attributeAsInt, (object) "Complex"));
    }

    private void parseComplexProperty(ComplexProperty property)
    {
      property.ComplexReferenceId = this._reader.GetAttributeAsInt("id");
      if (property.IsReferencedMoreThanOnce)
        this.complexItems.Add(property.ComplexReferenceId, property);
      foreach (string readSubElement in this._reader.ReadSubElements())
      {
        if (readSubElement == "Properties")
          this.readProperties(property.Properties, property.Type);
      }
    }

    private void readProperties(PropertyCollection properties, Type ownerType)
    {
      foreach (string readSubElement in this._reader.ReadSubElements())
      {
        XmlPropertyDeserializer.PropertyTag propertyTag = XmlPropertyDeserializer.getPropertyTag(readSubElement);
        if (propertyTag != XmlPropertyDeserializer.PropertyTag.Unknown)
        {
          string attributeAsString = this._reader.GetAttributeAsString("name");
          if (!string.IsNullOrEmpty(attributeAsString))
          {
            PropertyInfo property1 = ownerType.GetProperty(attributeAsString);
            Property property2 = this.deserialize(propertyTag, property1.PropertyType);
            properties.Add(property2);
          }
        }
      }
    }

    private void parseSimpleProperty(IXmlReader reader, SimpleProperty property)
    {
      property.Value = this._reader.GetAttributeAsObject("value", property.Type);
    }

    private static Property createProperty(
      XmlPropertyDeserializer.PropertyTag tag,
      string propertyName,
      Type propertyType)
    {
      switch (tag)
      {
        case XmlPropertyDeserializer.PropertyTag.Simple:
          return (Property) new SimpleProperty(propertyName, propertyType);
        case XmlPropertyDeserializer.PropertyTag.Complex:
          return (Property) new ComplexProperty(propertyName, propertyType);
        case XmlPropertyDeserializer.PropertyTag.Collection:
          return (Property) new CollectionProperty(propertyName, propertyType);
        case XmlPropertyDeserializer.PropertyTag.Dictionary:
          return (Property) new DictionaryProperty(propertyName, propertyType);
        case XmlPropertyDeserializer.PropertyTag.SingleArray:
          return (Property) new SingleDimensionalArrayProperty(propertyName, propertyType);
        case XmlPropertyDeserializer.PropertyTag.MultiArray:
          return (Property) new MultiDimensionalArrayProperty(propertyName, propertyType);
        case XmlPropertyDeserializer.PropertyTag.Null:
          return (Property) new NullProperty(propertyName);
        case XmlPropertyDeserializer.PropertyTag.ComplexReference:
          return (Property) new ComplexReferenceProperty(propertyName);
        default:
          return (Property) null;
      }
    }

    private static XmlPropertyDeserializer.PropertyTag getPropertyTag(string name)
    {
      switch (name)
      {
        case "Simple":
          return XmlPropertyDeserializer.PropertyTag.Simple;
        case "Complex":
          return XmlPropertyDeserializer.PropertyTag.Complex;
        case "Collection":
          return XmlPropertyDeserializer.PropertyTag.Collection;
        case "SingleArray":
          return XmlPropertyDeserializer.PropertyTag.SingleArray;
        case "Null":
          return XmlPropertyDeserializer.PropertyTag.Null;
        case "Dictionary":
          return XmlPropertyDeserializer.PropertyTag.Dictionary;
        case "MultiArray":
          return XmlPropertyDeserializer.PropertyTag.MultiArray;
        case "ComplexReference":
          return XmlPropertyDeserializer.PropertyTag.ComplexReference;
        default:
          return XmlPropertyDeserializer.PropertyTag.Unknown;
      }
    }

    private enum PropertyTag
    {
      Unknown,
      Simple,
      Complex,
      Collection,
      Dictionary,
      SingleArray,
      MultiArray,
      Null,
      ComplexReference,
    }
  }
}

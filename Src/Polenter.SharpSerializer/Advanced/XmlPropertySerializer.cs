
// Type: Polenter.Serialization.Advanced.XmlPropertySerializer
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced.Serializing;
using Polenter.Serialization.Advanced.Xml;
using Polenter.Serialization.Core;
using Polenter.Serialization.Serializing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

#nullable disable
namespace Polenter.Serialization.Advanced
{
  public sealed class XmlPropertySerializer : PropertySerializer
  {
    private readonly IXmlWriter _writer;

    public XmlPropertySerializer(IXmlWriter writer)
    {
      this._writer = writer != null ? writer : throw new ArgumentNullException(nameof (writer));
    }

    protected override void SerializeNullProperty(PropertyTypeInfo<NullProperty> property)
    {
      this.writeStartProperty("Null", property.Name, property.ValueType);
      this.writeEndProperty();
    }

    protected override void SerializeSimpleProperty(PropertyTypeInfo<SimpleProperty> property)
    {
      if (property.Property.Value == null)
        return;
      this.writeStartProperty("Simple", property.Name, property.ValueType);
      this._writer.WriteAttribute("value", property.Property.Value);
      this.writeEndProperty();
    }

    private void writeEndProperty() => this._writer.WriteEndElement();

    private void writeStartProperty(string elementId, string propertyName, Type propertyType)
    {
      this._writer.WriteStartElement(elementId);
      if (!string.IsNullOrEmpty(propertyName))
        this._writer.WriteAttribute("name", propertyName);
      if ((object) propertyType == null)
        return;
      this._writer.WriteAttribute("type", propertyType);
    }

    protected override void SerializeMultiDimensionalArrayProperty(
      PropertyTypeInfo<MultiDimensionalArrayProperty> property)
    {
      this.writeStartProperty("MultiArray", property.Name, property.ValueType);
      this.writeDimensionInfos((IEnumerable<DimensionInfo>) property.Property.DimensionInfos);
      this.writeMultiDimensionalArrayItems((IEnumerable<MultiDimensionalArrayItem>) property.Property.Items, property.Property.ElementType);
      this.writeEndProperty();
    }

    private void writeMultiDimensionalArrayItems(
      IEnumerable<MultiDimensionalArrayItem> items,
      Type defaultItemType)
    {
      this._writer.WriteStartElement("Items");
      foreach (MultiDimensionalArrayItem dimensionalArrayItem in items)
        this.writeMultiDimensionalArrayItem(dimensionalArrayItem, defaultItemType);
      this._writer.WriteEndElement();
    }

    private void writeMultiDimensionalArrayItem(
      MultiDimensionalArrayItem item,
      Type defaultTypeOfItemValue)
    {
      this._writer.WriteStartElement("Item");
      this._writer.WriteAttribute("indexes", item.Indexes);
      this.SerializeCore(new PropertyTypeInfo<Property>(item.Value, defaultTypeOfItemValue));
      this._writer.WriteEndElement();
    }

    private void writeDimensionInfos(IEnumerable<DimensionInfo> infos)
    {
      this._writer.WriteStartElement("Dimensions");
      foreach (DimensionInfo info in infos)
        this.writeDimensionInfo(info);
      this._writer.WriteEndElement();
    }

    protected override void SerializeSingleDimensionalArrayProperty(
      PropertyTypeInfo<SingleDimensionalArrayProperty> property)
    {
      this.writeStartProperty("SingleArray", property.Name, property.ValueType);
      if (property.Property.LowerBound != 0)
        this._writer.WriteAttribute("lowerBound", property.Property.LowerBound);
      this.writeItems((IEnumerable<Property>) property.Property.Items, property.Property.ElementType);
      this.writeEndProperty();
    }

    private void writeDimensionInfo(DimensionInfo info)
    {
      this._writer.WriteStartElement("Dimension");
      if (info.Length != 0)
        this._writer.WriteAttribute("length", info.Length);
      if (info.LowerBound != 0)
        this._writer.WriteAttribute("lowerBound", info.LowerBound);
      this._writer.WriteEndElement();
    }

    protected override void SerializeDictionaryProperty(
      PropertyTypeInfo<DictionaryProperty> property)
    {
      this.writeStartProperty("Dictionary", property.Name, property.ValueType);
      this.writeProperties((ICollection<Property>) property.Property.Properties, property.Property.Type);
      this.writeDictionaryItems((IEnumerable<KeyValueItem>) property.Property.Items, property.Property.KeyType, property.Property.ValueType);
      this.writeEndProperty();
    }

    private void writeDictionaryItems(
      IEnumerable<KeyValueItem> items,
      Type defaultKeyType,
      Type defaultValueType)
    {
      this._writer.WriteStartElement("Items");
      foreach (KeyValueItem keyValueItem in items)
        this.writeDictionaryItem(keyValueItem, defaultKeyType, defaultValueType);
      this._writer.WriteEndElement();
    }

    private void writeDictionaryItem(KeyValueItem item, Type defaultKeyType, Type defaultValueType)
    {
      this._writer.WriteStartElement("Item");
      this.SerializeCore(new PropertyTypeInfo<Property>(item.Key, defaultKeyType));
      this.SerializeCore(new PropertyTypeInfo<Property>(item.Value, defaultValueType));
      this._writer.WriteEndElement();
    }

    private void writeValueType(Type type)
    {
      if ((object) type == null)
        return;
      this._writer.WriteAttribute("valueType", type);
    }

    private void writeKeyType(Type type)
    {
      if ((object) type == null)
        return;
      this._writer.WriteAttribute("keyType", type);
    }

    protected override void SerializeCollectionProperty(
      PropertyTypeInfo<CollectionProperty> property)
    {
      this.writeStartProperty("Collection", property.Name, property.ValueType);
      this.writeProperties((ICollection<Property>) property.Property.Properties, property.Property.Type);
      this.writeItems((IEnumerable<Property>) property.Property.Items, property.Property.ElementType);
      this.writeEndProperty();
    }

    private void writeItems(IEnumerable<Property> properties, Type defaultItemType)
    {
      this._writer.WriteStartElement("Items");
      foreach (Property property in properties)
        this.SerializeCore(new PropertyTypeInfo<Property>(property, defaultItemType));
      this._writer.WriteEndElement();
    }

    private void writeProperties(ICollection<Property> properties, Type ownerType)
    {
      if (properties.Count == 0)
        return;
      this._writer.WriteStartElement("Properties");
      foreach (Property property1 in (IEnumerable<Property>) properties)
      {
        PropertyInfo property2 = ownerType.GetProperty(property1.Name);
        if ((object) property2 != null)
          this.SerializeCore(new PropertyTypeInfo<Property>(property1, property2.PropertyType));
        else
          this.SerializeCore(new PropertyTypeInfo<Property>(property1, (Type) null));
      }
      this._writer.WriteEndElement();
    }

    protected override void SerializeComplexReferenceProperty(
      PropertyTypeInfo<ComplexReferenceProperty> property)
    {
      this.writeStartProperty("ComplexReference", property.Name, (Type) null);
      this._writer.WriteAttribute("id", property.Property.ReferenceTarget.ComplexReferenceId);
      this.writeEndProperty();
    }

    protected override void SerializeComplexProperty(PropertyTypeInfo<ComplexProperty> property)
    {
      this.writeStartProperty("Complex", property.Name, property.ValueType);
      int complexReferenceId = property.Property.ComplexReferenceId;
      if (complexReferenceId != 0)
        this._writer.WriteAttribute("id", complexReferenceId);
      this.writeProperties((ICollection<Property>) property.Property.Properties, property.Property.Type);
      this.writeEndProperty();
    }

    public override void Open(Stream stream) => this._writer.Open(stream);

    public override void Close() => this._writer.Close();
  }
}

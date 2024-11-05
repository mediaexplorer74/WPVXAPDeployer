
// Type: Polenter.Serialization.Advanced.BinaryPropertySerializer
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced.Binary;
using Polenter.Serialization.Advanced.Serializing;
using Polenter.Serialization.Core;
using Polenter.Serialization.Serializing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

#nullable disable
namespace Polenter.Serialization.Advanced
{
  public sealed class BinaryPropertySerializer : PropertySerializer
  {
    private readonly IBinaryWriter _writer;

    public BinaryPropertySerializer(IBinaryWriter writer)
    {
      this._writer = writer != null ? writer : throw new ArgumentNullException(nameof (writer));
    }

    public override void Open(Stream stream) => this._writer.Open(stream);

    public override void Close() => this._writer.Close();

    private void writePropertyHeader(byte elementId, string name, Type valueType)
    {
      this._writer.WriteElementId(elementId);
      this._writer.WriteName(name);
      this._writer.WriteType(valueType);
    }

    protected override void SerializeNullProperty(PropertyTypeInfo<NullProperty> property)
    {
      this.writePropertyHeader((byte) 5, property.Name, property.ValueType);
    }

    protected override void SerializeSimpleProperty(PropertyTypeInfo<SimpleProperty> property)
    {
      this.writePropertyHeader((byte) 6, property.Name, property.ValueType);
      this._writer.WriteValue(property.Property.Value);
    }

    protected override void SerializeMultiDimensionalArrayProperty(
      PropertyTypeInfo<MultiDimensionalArrayProperty> property)
    {
      this.writePropertyHeader((byte) 4, property.Name, property.ValueType);
      this._writer.WriteType(property.Property.ElementType);
      this.writeDimensionInfos(property.Property.DimensionInfos);
      this.writeMultiDimensionalArrayItems(property.Property.Items, property.Property.ElementType);
    }

    private void writeMultiDimensionalArrayItems(
      IList<MultiDimensionalArrayItem> items,
      Type defaultItemType)
    {
      this._writer.WriteNumber(items.Count);
      foreach (MultiDimensionalArrayItem dimensionalArrayItem in (IEnumerable<MultiDimensionalArrayItem>) items)
        this.writeMultiDimensionalArrayItem(dimensionalArrayItem, defaultItemType);
    }

    private void writeMultiDimensionalArrayItem(
      MultiDimensionalArrayItem item,
      Type defaultItemType)
    {
      this._writer.WriteNumbers(item.Indexes);
      this.SerializeCore(new PropertyTypeInfo<Property>(item.Value, defaultItemType));
    }

    private void writeDimensionInfos(IList<DimensionInfo> dimensionInfos)
    {
      this._writer.WriteNumber(dimensionInfos.Count);
      foreach (DimensionInfo dimensionInfo in (IEnumerable<DimensionInfo>) dimensionInfos)
        this.writeDimensionInfo(dimensionInfo);
    }

    private void writeDimensionInfo(DimensionInfo info)
    {
      this._writer.WriteNumber(info.Length);
      this._writer.WriteNumber(info.LowerBound);
    }

    protected override void SerializeSingleDimensionalArrayProperty(
      PropertyTypeInfo<SingleDimensionalArrayProperty> property)
    {
      this.writePropertyHeader((byte) 7, property.Name, property.ValueType);
      this._writer.WriteType(property.Property.ElementType);
      this._writer.WriteNumber(property.Property.LowerBound);
      this.writeItems((ICollection<Property>) property.Property.Items, property.Property.ElementType);
    }

    private void writeItems(ICollection<Property> items, Type defaultItemType)
    {
      this._writer.WriteNumber(items.Count);
      foreach (Property property in (IEnumerable<Property>) items)
        this.SerializeCore(new PropertyTypeInfo<Property>(property, defaultItemType));
    }

    protected override void SerializeDictionaryProperty(
      PropertyTypeInfo<DictionaryProperty> property)
    {
      this.writePropertyHeader((byte) 3, property.Name, property.ValueType);
      this._writer.WriteType(property.Property.KeyType);
      this._writer.WriteType(property.Property.ValueType);
      this.writeProperties(property.Property.Properties, property.Property.Type);
      this.writeDictionaryItems(property.Property.Items, property.Property.KeyType, property.Property.ValueType);
    }

    private void writeDictionaryItems(
      IList<KeyValueItem> items,
      Type defaultKeyType,
      Type defaultValueType)
    {
      this._writer.WriteNumber(items.Count);
      foreach (KeyValueItem keyValueItem in (IEnumerable<KeyValueItem>) items)
        this.writeDictionaryItem(keyValueItem, defaultKeyType, defaultValueType);
    }

    private void writeDictionaryItem(KeyValueItem item, Type defaultKeyType, Type defaultValueType)
    {
      this.SerializeCore(new PropertyTypeInfo<Property>(item.Key, defaultKeyType));
      this.SerializeCore(new PropertyTypeInfo<Property>(item.Value, defaultValueType));
    }

    protected override void SerializeCollectionProperty(
      PropertyTypeInfo<CollectionProperty> property)
    {
      this.writePropertyHeader((byte) 1, property.Name, property.ValueType);
      this._writer.WriteType(property.Property.ElementType);
      this.writeProperties(property.Property.Properties, property.Property.Type);
      this.writeItems((ICollection<Property>) property.Property.Items, property.Property.ElementType);
    }

    protected override void SerializeComplexReferenceProperty(
      PropertyTypeInfo<ComplexReferenceProperty> property)
    {
      this.writePropertyHeader((byte) 9, property.Name, property.ValueType);
      this._writer.WriteNumber(property.Property.ReferenceTarget.ComplexReferenceId);
    }

    protected override void SerializeComplexProperty(PropertyTypeInfo<ComplexProperty> property)
    {
      if (property.Property.IsReferencedMoreThanOnce)
      {
        this.writePropertyHeader((byte) 8, property.Name, property.ValueType);
        this._writer.WriteNumber(property.Property.ComplexReferenceId);
      }
      else
        this.writePropertyHeader((byte) 2, property.Name, property.ValueType);
      this.writeProperties(property.Property.Properties, property.Property.Type);
    }

    private void writeProperties(PropertyCollection properties, Type ownerType)
    {
      this._writer.WriteNumber((int) Convert.ToInt16(properties.Count));
      foreach (Property property1 in (Collection<Property>) properties)
      {
        PropertyInfo property2 = ownerType.GetProperty(property1.Name);
        this.SerializeCore(new PropertyTypeInfo<Property>(property1, property2.PropertyType));
      }
    }
  }
}

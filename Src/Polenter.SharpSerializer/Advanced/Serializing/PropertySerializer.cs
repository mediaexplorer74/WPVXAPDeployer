
// Type: Polenter.Serialization.Advanced.Serializing.PropertySerializer
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Core;
using Polenter.Serialization.Serializing;
using System;
using System.IO;

#nullable disable
namespace Polenter.Serialization.Advanced.Serializing
{
  public abstract class PropertySerializer : IPropertySerializer
  {
    public void Serialize(Property property)
    {
      this.SerializeCore(new PropertyTypeInfo<Property>(property, (Type) null));
    }

    public abstract void Open(Stream stream);

    public abstract void Close();

    protected void SerializeCore(PropertyTypeInfo<Property> property)
    {
      if (property == null)
        throw new ArgumentNullException(nameof (property));
      if (property.Property is NullProperty property8)
      {
        this.SerializeNullProperty(new PropertyTypeInfo<NullProperty>(property8, property.ExpectedPropertyType, property.ValueType));
      }
      else
      {
        if ((object) property.ExpectedPropertyType != null && (object) property.ExpectedPropertyType == (object) property.ValueType)
          property.ValueType = (Type) null;
        if (property.Property is SimpleProperty property7)
          this.SerializeSimpleProperty(new PropertyTypeInfo<SimpleProperty>(property7, property.ExpectedPropertyType, property.ValueType));
        else if (property.Property is MultiDimensionalArrayProperty property6)
          this.SerializeMultiDimensionalArrayProperty(new PropertyTypeInfo<MultiDimensionalArrayProperty>(property6, property.ExpectedPropertyType, property.ValueType));
        else if (property.Property is SingleDimensionalArrayProperty property5)
          this.SerializeSingleDimensionalArrayProperty(new PropertyTypeInfo<SingleDimensionalArrayProperty>(property5, property.ExpectedPropertyType, property.ValueType));
        else if (property.Property is DictionaryProperty property4)
          this.SerializeDictionaryProperty(new PropertyTypeInfo<DictionaryProperty>(property4, property.ExpectedPropertyType, property.ValueType));
        else if (property.Property is CollectionProperty property3)
          this.SerializeCollectionProperty(new PropertyTypeInfo<CollectionProperty>(property3, property.ExpectedPropertyType, property.ValueType));
        else if (property.Property is ComplexProperty property2)
        {
          this.SerializeComplexProperty(new PropertyTypeInfo<ComplexProperty>(property2, property.ExpectedPropertyType, property.ValueType));
        }
        else
        {
          if (!(property.Property is ComplexReferenceProperty property1))
            throw new InvalidOperationException(string.Format("Unknown Property: {0}", (object) property.Property.GetType()));
          this.SerializeComplexReferenceProperty(new PropertyTypeInfo<ComplexReferenceProperty>(property1, (Type) null, (Type) null));
        }
      }
    }

    protected abstract void SerializeNullProperty(PropertyTypeInfo<NullProperty> property);

    protected abstract void SerializeSimpleProperty(PropertyTypeInfo<SimpleProperty> property);

    protected abstract void SerializeMultiDimensionalArrayProperty(
      PropertyTypeInfo<MultiDimensionalArrayProperty> property);

    protected abstract void SerializeSingleDimensionalArrayProperty(
      PropertyTypeInfo<SingleDimensionalArrayProperty> property);

    protected abstract void SerializeDictionaryProperty(
      PropertyTypeInfo<DictionaryProperty> property);

    protected abstract void SerializeCollectionProperty(
      PropertyTypeInfo<CollectionProperty> property);

    protected abstract void SerializeComplexProperty(PropertyTypeInfo<ComplexProperty> property);

    protected abstract void SerializeComplexReferenceProperty(
      PropertyTypeInfo<ComplexReferenceProperty> property);
  }
}

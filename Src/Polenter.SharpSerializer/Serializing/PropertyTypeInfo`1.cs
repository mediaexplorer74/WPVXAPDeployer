
// Type: Polenter.Serialization.Serializing.PropertyTypeInfo`1
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;

#nullable disable
namespace Polenter.Serialization.Serializing
{
  public sealed class PropertyTypeInfo<TProperty> where TProperty : Polenter.Serialization.Core.Property
  {
    public PropertyTypeInfo(TProperty property, Type valueType)
    {
      this.Property = property;
      this.ExpectedPropertyType = valueType;
      this.ValueType = property.Type;
      this.Name = property.Name;
    }

    public PropertyTypeInfo(TProperty property, Type expectedPropertyType, Type valueType)
    {
      this.Property = property;
      this.ExpectedPropertyType = expectedPropertyType;
      this.ValueType = valueType;
      this.Name = property.Name;
    }

    public Type ExpectedPropertyType { get; set; }

    public Type ValueType { get; set; }

    public string Name { get; set; }

    public TProperty Property { get; set; }
  }
}

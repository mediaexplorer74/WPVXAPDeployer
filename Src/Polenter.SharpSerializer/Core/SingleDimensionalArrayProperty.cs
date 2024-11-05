
// Type: Polenter.Serialization.Core.SingleDimensionalArrayProperty
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;

#nullable disable
namespace Polenter.Serialization.Core
{
  public sealed class SingleDimensionalArrayProperty : Property
  {
    private PropertyCollection _items;

    public SingleDimensionalArrayProperty(string name, Type type)
      : base(name, type)
    {
    }

    public PropertyCollection Items
    {
      get
      {
        if (this._items == null)
          this._items = new PropertyCollection()
          {
            Parent = (Property) this
          };
        return this._items;
      }
      set => this._items = value;
    }

    public int LowerBound { get; set; }

    public Type ElementType { get; set; }
  }
}

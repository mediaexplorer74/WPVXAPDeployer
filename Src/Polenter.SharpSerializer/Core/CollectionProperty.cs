
// Type: Polenter.Serialization.Core.CollectionProperty
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Polenter.Serialization.Core
{
  public sealed class CollectionProperty : ComplexProperty
  {
    private IList<Property> _items;

    public CollectionProperty(string name, Type type)
      : base(name, type)
    {
    }

    public IList<Property> Items
    {
      get
      {
        if (this._items == null)
          this._items = (IList<Property>) new List<Property>();
        return this._items;
      }
      set => this._items = value;
    }

    public Type ElementType { get; set; }
  }
}

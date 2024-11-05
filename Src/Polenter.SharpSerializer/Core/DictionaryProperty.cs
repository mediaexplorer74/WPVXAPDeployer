
// Type: Polenter.Serialization.Core.DictionaryProperty
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Polenter.Serialization.Core
{
  public sealed class DictionaryProperty : ComplexProperty
  {
    private IList<KeyValueItem> _items;

    public DictionaryProperty(string name, Type type)
      : base(name, type)
    {
    }

    public IList<KeyValueItem> Items
    {
      get
      {
        if (this._items == null)
          this._items = (IList<KeyValueItem>) new List<KeyValueItem>();
        return this._items;
      }
      set => this._items = value;
    }

    public Type KeyType { get; set; }

    public Type ValueType { get; set; }
  }
}

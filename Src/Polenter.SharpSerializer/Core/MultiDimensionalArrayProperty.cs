
// Type: Polenter.Serialization.Core.MultiDimensionalArrayProperty
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Polenter.Serialization.Core
{
  public sealed class MultiDimensionalArrayProperty : Property
  {
    private IList<DimensionInfo> _dimensionInfos;
    private IList<MultiDimensionalArrayItem> _items;

    public MultiDimensionalArrayProperty(string name, Type type)
      : base(name, type)
    {
    }

    public IList<MultiDimensionalArrayItem> Items
    {
      get
      {
        if (this._items == null)
          this._items = (IList<MultiDimensionalArrayItem>) new List<MultiDimensionalArrayItem>();
        return this._items;
      }
      set => this._items = value;
    }

    public IList<DimensionInfo> DimensionInfos
    {
      get
      {
        if (this._dimensionInfos == null)
          this._dimensionInfos = (IList<DimensionInfo>) new List<DimensionInfo>();
        return this._dimensionInfos;
      }
      set => this._dimensionInfos = value;
    }

    public Type ElementType { get; set; }
  }
}

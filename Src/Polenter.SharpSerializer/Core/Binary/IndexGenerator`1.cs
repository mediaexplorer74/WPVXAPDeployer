
// Type: Polenter.Serialization.Core.Binary.IndexGenerator`1
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System.Collections.Generic;

#nullable disable
namespace Polenter.Serialization.Core.Binary
{
  internal sealed class IndexGenerator<T>
  {
    private readonly List<T> _items = new List<T>();

    public IList<T> Items => (IList<T>) this._items;

    public int GetIndexOfItem(T item)
    {
      int indexOfItem = this._items.IndexOf(item);
      if (indexOfItem > -1)
        return indexOfItem;
      this._items.Add(item);
      return this._items.Count - 1;
    }
  }
}

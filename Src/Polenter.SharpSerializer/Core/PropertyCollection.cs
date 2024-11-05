
// Type: Polenter.Serialization.Core.PropertyCollection
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Polenter.Serialization.Core
{
  public sealed class PropertyCollection : Collection<Property>
  {
    public Property Parent { get; set; }

    protected override void ClearItems()
    {
      foreach (Property property in (IEnumerable<Property>) this.Items)
        property.Parent = (Property) null;
      base.ClearItems();
    }

    protected override void InsertItem(int index, Property item)
    {
      base.InsertItem(index, item);
      item.Parent = this.Parent;
    }

    protected override void RemoveItem(int index)
    {
      this.Items[index].Parent = (Property) null;
      base.RemoveItem(index);
    }

    protected override void SetItem(int index, Property item)
    {
      this.Items[index].Parent = (Property) null;
      base.SetItem(index, item);
      item.Parent = this.Parent;
    }
  }
}

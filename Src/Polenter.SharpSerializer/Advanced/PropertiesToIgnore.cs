
// Type: Polenter.Serialization.Advanced.PropertiesToIgnore
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Polenter.Serialization.Advanced
{
  public sealed class PropertiesToIgnore
  {
    private readonly PropertiesToIgnore.TypePropertiesToIgnoreCollection _propertiesToIgnore = new PropertiesToIgnore.TypePropertiesToIgnoreCollection();

    public void Add(Type type, string propertyName)
    {
      PropertiesToIgnore.TypePropertiesToIgnore propertiesToIgnore = this.getPropertiesToIgnore(type);
      if (propertiesToIgnore.PropertyNames.Contains(propertyName))
        return;
      propertiesToIgnore.PropertyNames.Add(propertyName);
    }

    private PropertiesToIgnore.TypePropertiesToIgnore getPropertiesToIgnore(Type type)
    {
      PropertiesToIgnore.TypePropertiesToIgnore propertiesToIgnore = this._propertiesToIgnore.TryFind(type);
      if (propertiesToIgnore == null)
      {
        propertiesToIgnore = new PropertiesToIgnore.TypePropertiesToIgnore(type);
        this._propertiesToIgnore.Add(propertiesToIgnore);
      }
      return propertiesToIgnore;
    }

    public bool Contains(Type type, string propertyName)
    {
      return this._propertiesToIgnore.ContainsProperty(type, propertyName);
    }

    private sealed class TypePropertiesToIgnore
    {
      private IList<string> _propertyNames;

      public TypePropertiesToIgnore(Type type) => this.Type = type;

      public Type Type { get; set; }

      public IList<string> PropertyNames
      {
        get
        {
          if (this._propertyNames == null)
            this._propertyNames = (IList<string>) new List<string>();
          return this._propertyNames;
        }
        set => this._propertyNames = value;
      }
    }

    private sealed class TypePropertiesToIgnoreCollection : 
      KeyedCollection<Type, PropertiesToIgnore.TypePropertiesToIgnore>
    {
      protected override Type GetKeyForItem(PropertiesToIgnore.TypePropertiesToIgnore item)
      {
        return item.Type;
      }

      public int IndexOf(Type type)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if ((object) this[index].Type == (object) type)
            return index;
        }
        return -1;
      }

      public PropertiesToIgnore.TypePropertiesToIgnore TryFind(Type type)
      {
        foreach (PropertiesToIgnore.TypePropertiesToIgnore propertiesToIgnore in (IEnumerable<PropertiesToIgnore.TypePropertiesToIgnore>) this.Items)
        {
          if ((object) propertiesToIgnore.Type == (object) type)
            return propertiesToIgnore;
        }
        return (PropertiesToIgnore.TypePropertiesToIgnore) null;
      }

      public bool ContainsProperty(Type type, string propertyName)
      {
        PropertiesToIgnore.TypePropertiesToIgnore propertiesToIgnore = this.TryFind(type);
        return propertiesToIgnore != null && propertiesToIgnore.PropertyNames.Contains(propertyName);
      }
    }
  }
}

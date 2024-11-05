
// Type: Polenter.Serialization.Serializing.TypeInfoCollection
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Polenter.Serialization.Serializing
{
  public sealed class TypeInfoCollection : KeyedCollection<Type, TypeInfo>
  {
    public TypeInfo TryGetTypeInfo(Type type)
    {
      foreach (TypeInfo typeInfo in (IEnumerable<TypeInfo>) this.Items)
      {
        if ((object) typeInfo.Type == (object) type)
          return typeInfo;
      }
      return (TypeInfo) null;
    }

    protected override Type GetKeyForItem(TypeInfo item) => item.Type;
  }
}

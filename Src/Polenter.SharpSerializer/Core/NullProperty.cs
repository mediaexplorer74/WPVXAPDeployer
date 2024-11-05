
// Type: Polenter.Serialization.Core.NullProperty
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;

#nullable disable
namespace Polenter.Serialization.Core
{
  public sealed class NullProperty : Property
  {
    public NullProperty()
      : base((string) null, (Type) null)
    {
    }

    public NullProperty(string name)
      : base(name, (Type) null)
    {
    }
  }
}

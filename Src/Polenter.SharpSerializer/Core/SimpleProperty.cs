
// Type: Polenter.Serialization.Core.SimpleProperty
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;

#nullable disable
namespace Polenter.Serialization.Core
{
  public sealed class SimpleProperty : Property
  {
    public SimpleProperty(string name, Type type)
      : base(name, type)
    {
    }

    public object Value { get; set; }

    public override string ToString()
    {
      string str = base.ToString();
      return this.Value == null ? string.Format("{0}, (null)", (object) str) : string.Format("{0}, ({1})", (object) str, this.Value);
    }
  }
}

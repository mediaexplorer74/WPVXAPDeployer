
// Type: Polenter.Serialization.Core.ComplexReferenceProperty
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;

#nullable disable
namespace Polenter.Serialization.Core
{
  public class ComplexReferenceProperty : Property
  {
    public ComplexProperty ReferenceTarget { get; set; }

    public ComplexReferenceProperty(string name, ComplexProperty referenceTarget)
      : base(name, (Type) null)
    {
      this.ReferenceTarget = referenceTarget;
    }

    public ComplexReferenceProperty(string name)
      : this(name, (ComplexProperty) null)
    {
    }
  }
}

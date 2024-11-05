
// Type: Polenter.Serialization.Core.ComplexProperty
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;

#nullable disable
namespace Polenter.Serialization.Core
{
  public class ComplexProperty : Property
  {
    private PropertyCollection _properties;

    public ComplexProperty(string name, Type type, object value)
      : base(name, type)
    {
      this.Value = value;
      this.ComplexReferenceId = 0;
    }

    public ComplexProperty(string name, Type type)
      : this(name, type, (object) null)
    {
    }

    public object Value { get; set; }

    public int ComplexReferenceId { get; set; }

    public bool IsReferencedMoreThanOnce => this.ComplexReferenceId != 0;

    public PropertyCollection Properties
    {
      get
      {
        if (this._properties == null)
          this._properties = new PropertyCollection()
          {
            Parent = (Property) this
          };
        return this._properties;
      }
      set => this._properties = value;
    }
  }
}

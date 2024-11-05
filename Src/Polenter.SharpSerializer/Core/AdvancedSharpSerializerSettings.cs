
// Type: Polenter.Serialization.Core.AdvancedSharpSerializerSettings
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced;
using Polenter.Serialization.Advanced.Serializing;
using System;
using System.Collections.Generic;

#nullable disable
namespace Polenter.Serialization.Core
{
  public class AdvancedSharpSerializerSettings
  {
    private PropertiesToIgnore _propertiesToIgnore;
    private IList<Type> _attributesToIgnore;

    public AdvancedSharpSerializerSettings()
    {
      this.AttributesToIgnore.Add(typeof (ExcludeFromSerializationAttribute));
      this.RootName = "Root";
    }

    public PropertiesToIgnore PropertiesToIgnore
    {
      get
      {
        if (this._propertiesToIgnore == null)
          this._propertiesToIgnore = new PropertiesToIgnore();
        return this._propertiesToIgnore;
      }
      set => this._propertiesToIgnore = value;
    }

    public IList<Type> AttributesToIgnore
    {
      get
      {
        if (this._attributesToIgnore == null)
          this._attributesToIgnore = (IList<Type>) new List<Type>();
        return this._attributesToIgnore;
      }
      set => this._attributesToIgnore = value;
    }

    public string RootName { get; set; }

    public ITypeNameConverter TypeNameConverter { get; set; }
  }
}

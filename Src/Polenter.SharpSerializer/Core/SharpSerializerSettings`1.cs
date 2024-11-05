
// Type: Polenter.Serialization.Core.SharpSerializerSettings`1
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

#nullable disable
namespace Polenter.Serialization.Core
{
  public abstract class SharpSerializerSettings<T> where T : AdvancedSharpSerializerSettings, new()
  {
    private T _advancedSettings;

    public T AdvancedSettings
    {
      get
      {
        if ((object) this._advancedSettings == (object) default (T))
          this._advancedSettings = new T();
        return this._advancedSettings;
      }
      set => this._advancedSettings = value;
    }

    public bool IncludeAssemblyVersionInTypeName { get; set; }

    public bool IncludeCultureInTypeName { get; set; }

    public bool IncludePublicKeyTokenInTypeName { get; set; }
  }
}

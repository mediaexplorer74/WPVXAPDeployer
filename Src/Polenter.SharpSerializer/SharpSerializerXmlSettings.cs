
// Type: Polenter.Serialization.SharpSerializerXmlSettings
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Core;
using System.Globalization;
using System.Text;

#nullable disable
namespace Polenter.Serialization
{
  public sealed class SharpSerializerXmlSettings : 
    SharpSerializerSettings<AdvancedSharpSerializerXmlSettings>
  {
    public SharpSerializerXmlSettings()
    {
      this.Culture = CultureInfo.InvariantCulture;
      this.Encoding = Encoding.UTF8;
    }

    public CultureInfo Culture { get; set; }

    public Encoding Encoding { get; set; }
  }
}

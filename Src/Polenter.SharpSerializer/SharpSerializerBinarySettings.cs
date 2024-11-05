
// Type: Polenter.Serialization.SharpSerializerBinarySettings
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Core;
using System.Text;

#nullable disable
namespace Polenter.Serialization
{
  public sealed class SharpSerializerBinarySettings : 
    SharpSerializerSettings<AdvancedSharpSerializerBinarySettings>
  {
    public SharpSerializerBinarySettings() => this.Encoding = Encoding.UTF8;

    public SharpSerializerBinarySettings(BinarySerializationMode mode)
    {
      this.Encoding = Encoding.UTF8;
      this.Mode = mode;
    }

    public Encoding Encoding { get; set; }

    public BinarySerializationMode Mode { get; set; }
  }
}

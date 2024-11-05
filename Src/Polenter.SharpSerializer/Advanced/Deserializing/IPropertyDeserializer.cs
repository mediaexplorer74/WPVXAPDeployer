
// Type: Polenter.Serialization.Advanced.Deserializing.IPropertyDeserializer
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Core;
using System.IO;

#nullable disable
namespace Polenter.Serialization.Advanced.Deserializing
{
  public interface IPropertyDeserializer
  {
    void Open(Stream stream);

    Property Deserialize();

    void Close();
  }
}

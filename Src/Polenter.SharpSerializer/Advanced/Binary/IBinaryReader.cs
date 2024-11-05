
// Type: Polenter.Serialization.Advanced.Binary.IBinaryReader
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.IO;

#nullable disable
namespace Polenter.Serialization.Advanced.Binary
{
  public interface IBinaryReader
  {
    byte ReadElementId();

    Type ReadType();

    int ReadNumber();

    int[] ReadNumbers();

    string ReadName();

    object ReadValue(Type expectedType);

    void Open(Stream stream);

    void Close();
  }
}

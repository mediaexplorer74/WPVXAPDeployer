
// Type: Polenter.Serialization.Advanced.Xml.IXmlReader
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Polenter.Serialization.Advanced.Xml
{
  public interface IXmlReader
  {
    string ReadElement();

    IEnumerable<string> ReadSubElements();

    string GetAttributeAsString(string attributeName);

    Type GetAttributeAsType(string attributeName);

    int GetAttributeAsInt(string attributeName);

    int[] GetAttributeAsArrayOfInt(string attributeName);

    object GetAttributeAsObject(string attributeName, Type expectedType);

    void Open(Stream stream);

    void Close();
  }
}

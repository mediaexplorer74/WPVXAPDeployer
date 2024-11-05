
// Type: Polenter.Serialization.Advanced.Xml.IXmlWriter
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.IO;

#nullable disable
namespace Polenter.Serialization.Advanced.Xml
{
  public interface IXmlWriter
  {
    void WriteStartElement(string elementId);

    void WriteEndElement();

    void WriteAttribute(string attributeId, string text);

    void WriteAttribute(string attributeId, Type type);

    void WriteAttribute(string attributeId, int number);

    void WriteAttribute(string attributeId, int[] numbers);

    void WriteAttribute(string attributeId, object value);

    void Open(Stream stream);

    void Close();
  }
}


// Type: Polenter.Serialization.Advanced.DefaultXmlWriter
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced.Serializing;
using Polenter.Serialization.Advanced.Xml;
using System;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Polenter.Serialization.Advanced
{
  public sealed class DefaultXmlWriter : IXmlWriter
  {
    private readonly XmlWriterSettings _settings;
    private readonly ISimpleValueConverter _simpleValueConverter;
    private readonly ITypeNameConverter _typeNameProvider;
    private XmlWriter _writer;

    public DefaultXmlWriter(
      ITypeNameConverter typeNameProvider,
      ISimpleValueConverter simpleValueConverter,
      XmlWriterSettings settings)
    {
      if (typeNameProvider == null)
        throw new ArgumentNullException(nameof (typeNameProvider));
      if (simpleValueConverter == null)
        throw new ArgumentNullException(nameof (simpleValueConverter));
      if (settings == null)
        throw new ArgumentNullException(nameof (settings));
      this._simpleValueConverter = simpleValueConverter;
      this._settings = settings;
      this._typeNameProvider = typeNameProvider;
    }

    public void WriteStartElement(string elementId) => this._writer.WriteStartElement(elementId);

    public void WriteEndElement() => this._writer.WriteEndElement();

    public void WriteAttribute(string attributeId, string text)
    {
      if (text == null)
        return;
      this._writer.WriteAttributeString(attributeId, text);
    }

    public void WriteAttribute(string attributeId, Type type)
    {
      if ((object) type == null)
        return;
      string typeName = this._typeNameProvider.ConvertToTypeName(type);
      this.WriteAttribute(attributeId, typeName);
    }

    public void WriteAttribute(string attributeId, int number)
    {
      this._writer.WriteAttributeString(attributeId, number.ToString());
    }

    public void WriteAttribute(string attributeId, int[] numbers)
    {
      string arrayOfIntAsText = DefaultXmlWriter.getArrayOfIntAsText(numbers);
      this._writer.WriteAttributeString(attributeId, arrayOfIntAsText);
    }

    public void WriteAttribute(string attributeId, object value)
    {
      if (value == null)
        return;
      string str = this._simpleValueConverter.ConvertToString(value);
      this._writer.WriteAttributeString(attributeId, str);
    }

    public void Open(Stream stream)
    {
      this._writer = XmlWriter.Create(stream, this._settings);
      this._writer.WriteStartDocument(true);
    }

    public void Close()
    {
      this._writer.WriteEndDocument();
      this._writer.Close();
    }

    private static string getArrayOfIntAsText(int[] values)
    {
      if (values.Length == 0)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (int num in values)
      {
        stringBuilder.Append(num.ToString());
        stringBuilder.Append(",");
      }
      return stringBuilder.ToString().TrimEnd(',');
    }
  }
}

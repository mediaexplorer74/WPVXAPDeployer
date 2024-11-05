
// Type: Polenter.Serialization.Advanced.DefaultXmlReader
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced.Serializing;
using Polenter.Serialization.Advanced.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

#nullable disable
namespace Polenter.Serialization.Advanced
{
  public sealed class DefaultXmlReader : IXmlReader
  {
    private readonly XmlReaderSettings _settings;
    private readonly ITypeNameConverter _typeNameConverter;
    private readonly ISimpleValueConverter _valueConverter;
    private XmlReader _currentReader;
    private Stack<XmlReader> _readerStack;

    public DefaultXmlReader(
      ITypeNameConverter typeNameConverter,
      ISimpleValueConverter valueConverter,
      XmlReaderSettings settings)
    {
      if (typeNameConverter == null)
        throw new ArgumentNullException(nameof (typeNameConverter));
      if (valueConverter == null)
        throw new ArgumentNullException(nameof (valueConverter));
      if (settings == null)
        throw new ArgumentNullException(nameof (settings));
      this._typeNameConverter = typeNameConverter;
      this._valueConverter = valueConverter;
      this._settings = settings;
    }

    public string ReadElement()
    {
      while (this._currentReader.Read())
      {
        if (this._currentReader.NodeType == XmlNodeType.Element)
          return this._currentReader.Name;
      }
      return (string) null;
    }

    public IEnumerable<string> ReadSubElements()
    {
      this._currentReader.MoveToElement();
      XmlReader subReader = this._currentReader.ReadSubtree();
      subReader.Read();
      this.pushCurrentReader(subReader);
      try
      {
        for (string name = this.ReadElement(); !string.IsNullOrEmpty(name); name = this.ReadElement())
          yield return name;
      }
      finally
      {
        subReader.Close();
        this.popCurrentReader();
      }
    }

    public string GetAttributeAsString(string attributeName)
    {
      return !this._currentReader.MoveToAttribute(attributeName) ? (string) null : this._currentReader.Value;
    }

    public Type GetAttributeAsType(string attributeName)
    {
      return this._typeNameConverter.ConvertToType(this.GetAttributeAsString(attributeName));
    }

    public int GetAttributeAsInt(string attributeName)
    {
      return !this._currentReader.MoveToAttribute(attributeName) ? 0 : this._currentReader.ReadContentAsInt();
    }

    public int[] GetAttributeAsArrayOfInt(string attributeName)
    {
      return !this._currentReader.MoveToAttribute(attributeName) ? (int[]) null : DefaultXmlReader.getArrayOfIntFromText(this._currentReader.Value);
    }

    public object GetAttributeAsObject(string attributeName, Type expectedType)
    {
      return this._valueConverter.ConvertFromString(this.GetAttributeAsString(attributeName), expectedType);
    }

    public void Open(Stream stream)
    {
      this._readerStack = new Stack<XmlReader>();
      this.pushCurrentReader(XmlReader.Create(stream, this._settings));
    }

    public void Close() => this._currentReader.Close();

    private void popCurrentReader()
    {
      if (this._readerStack.Count > 0)
        this._readerStack.Pop();
      if (this._readerStack.Count > 0)
        this._currentReader = this._readerStack.Peek();
      else
        this._currentReader = (XmlReader) null;
    }

    private void pushCurrentReader(XmlReader reader)
    {
      this._readerStack.Push(reader);
      this._currentReader = reader;
    }

    private static int[] getArrayOfIntFromText(string text)
    {
      if (string.IsNullOrEmpty(text))
        return (int[]) null;
      string[] strArray = text.Split(',');
      if (strArray.Length == 0)
        return (int[]) null;
      List<int> intList = new List<int>();
      foreach (string s in strArray)
      {
        int num = int.Parse(s);
        intList.Add(num);
      }
      return intList.ToArray();
    }
  }
}


// Type: Polenter.Serialization.Advanced.BurstBinaryWriter
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced.Binary;
using Polenter.Serialization.Advanced.Serializing;
using Polenter.Serialization.Core.Binary;
using System;
using System.IO;
using System.Text;

#nullable disable
namespace Polenter.Serialization.Advanced
{
  public sealed class BurstBinaryWriter : IBinaryWriter
  {
    private readonly Encoding _encoding;
    private readonly ITypeNameConverter _typeNameConverter;
    private BinaryWriter _writer;

    public BurstBinaryWriter(ITypeNameConverter typeNameConverter, Encoding encoding)
    {
      if (typeNameConverter == null)
        throw new ArgumentNullException(nameof (typeNameConverter));
      this._encoding = encoding != null ? encoding : throw new ArgumentNullException(nameof (encoding));
      this._typeNameConverter = typeNameConverter;
    }

    public void WriteElementId(byte id) => this._writer.Write(id);

    public void WriteNumber(int number) => BinaryWriterTools.WriteNumber(number, this._writer);

    public void WriteNumbers(int[] numbers)
    {
      BinaryWriterTools.WriteNumbers(numbers, this._writer);
    }

    public void WriteType(Type type)
    {
      if ((object) type == null)
      {
        this._writer.Write(false);
      }
      else
      {
        this._writer.Write(true);
        this._writer.Write(this._typeNameConverter.ConvertToTypeName(type));
      }
    }

    public void WriteName(string name) => BinaryWriterTools.WriteString(name, this._writer);

    public void WriteValue(object value) => BinaryWriterTools.WriteValue(value, this._writer);

    public void Open(Stream stream) => this._writer = new BinaryWriter(stream, this._encoding);

    public void Close() => this._writer.Flush();
  }
}

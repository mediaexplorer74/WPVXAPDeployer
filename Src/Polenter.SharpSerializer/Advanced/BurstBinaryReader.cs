
// Type: Polenter.Serialization.Advanced.BurstBinaryReader
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
  public sealed class BurstBinaryReader : IBinaryReader
  {
    private readonly Encoding _encoding;
    private readonly ITypeNameConverter _typeNameConverter;
    private BinaryReader _reader;

    public BurstBinaryReader(ITypeNameConverter typeNameConverter, Encoding encoding)
    {
      if (typeNameConverter == null)
        throw new ArgumentNullException(nameof (typeNameConverter));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      this._typeNameConverter = typeNameConverter;
      this._encoding = encoding;
    }

    public string ReadName() => BinaryReaderTools.ReadString(this._reader);

    public byte ReadElementId() => this._reader.ReadByte();

    public Type ReadType()
    {
      return !this._reader.ReadBoolean() ? (Type) null : this._typeNameConverter.ConvertToType(this._reader.ReadString());
    }

    public int ReadNumber() => BinaryReaderTools.ReadNumber(this._reader);

    public int[] ReadNumbers() => BinaryReaderTools.ReadNumbers(this._reader);

    public object ReadValue(Type expectedType)
    {
      return BinaryReaderTools.ReadValue(expectedType, this._reader);
    }

    public void Open(Stream stream) => this._reader = new BinaryReader(stream, this._encoding);

    public void Close()
    {
    }
  }
}

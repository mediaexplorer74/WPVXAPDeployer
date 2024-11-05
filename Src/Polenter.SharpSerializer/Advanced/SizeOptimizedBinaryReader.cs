
// Type: Polenter.Serialization.Advanced.SizeOptimizedBinaryReader
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced.Binary;
using Polenter.Serialization.Advanced.Serializing;
using Polenter.Serialization.Core.Binary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace Polenter.Serialization.Advanced
{
  public sealed class SizeOptimizedBinaryReader : IBinaryReader
  {
    private readonly Encoding _encoding;
    private readonly IList<string> _names = (IList<string>) new List<string>();
    private readonly ITypeNameConverter _typeNameConverter;
    private readonly IList<Type> _types = (IList<Type>) new List<Type>();
    private BinaryReader _reader;

    public SizeOptimizedBinaryReader(ITypeNameConverter typeNameConverter, Encoding encoding)
    {
      if (typeNameConverter == null)
        throw new ArgumentNullException(nameof (typeNameConverter));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      this._typeNameConverter = typeNameConverter;
      this._encoding = encoding;
    }

    public byte ReadElementId() => this._reader.ReadByte();

    public Type ReadType() => this._types[BinaryReaderTools.ReadNumber(this._reader)];

    public int ReadNumber() => BinaryReaderTools.ReadNumber(this._reader);

    public int[] ReadNumbers() => BinaryReaderTools.ReadNumbers(this._reader);

    public string ReadName() => this._names[BinaryReaderTools.ReadNumber(this._reader)];

    public object ReadValue(Type expectedType)
    {
      return BinaryReaderTools.ReadValue(expectedType, this._reader);
    }

    public void Open(Stream stream)
    {
      this._reader = new BinaryReader(stream, this._encoding);
      SizeOptimizedBinaryReader.readHeader<string>(this._reader, this._names, (SizeOptimizedBinaryReader.HeaderCallback<string>) (text => text));
      SizeOptimizedBinaryReader.readHeader<Type>(this._reader, this._types, new SizeOptimizedBinaryReader.HeaderCallback<Type>(this._typeNameConverter.ConvertToType));
    }

    public void Close()
    {
    }

    private static void readHeader<T>(
      BinaryReader reader,
      IList<T> items,
      SizeOptimizedBinaryReader.HeaderCallback<T> readCallback)
    {
      int num = BinaryReaderTools.ReadNumber(reader);
      for (int index = 0; index < num; ++index)
      {
        string text = BinaryReaderTools.ReadString(reader);
        T obj = readCallback(text);
        items.Add(obj);
      }
    }

    private delegate T HeaderCallback<T>(string text);
  }
}

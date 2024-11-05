
// Type: Polenter.Serialization.Advanced.SizeOptimizedBinaryWriter
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
  public sealed class SizeOptimizedBinaryWriter : IBinaryWriter
  {
    private readonly Encoding _encoding;
    private readonly ITypeNameConverter _typeNameConverter;
    private List<SizeOptimizedBinaryWriter.WriteCommand> _cache;
    private IndexGenerator<string> _names;
    private Stream _stream;
    private IndexGenerator<Type> _types;

    public SizeOptimizedBinaryWriter(ITypeNameConverter typeNameConverter, Encoding encoding)
    {
      if (typeNameConverter == null)
        throw new ArgumentNullException(nameof (typeNameConverter));
      this._encoding = encoding != null ? encoding : throw new ArgumentNullException(nameof (encoding));
      this._typeNameConverter = typeNameConverter;
    }

    public void WriteElementId(byte id)
    {
      this._cache.Add((SizeOptimizedBinaryWriter.WriteCommand) new SizeOptimizedBinaryWriter.ByteWriteCommand(id));
    }

    public void WriteType(Type type)
    {
      this._cache.Add((SizeOptimizedBinaryWriter.WriteCommand) new SizeOptimizedBinaryWriter.NumberWriteCommand(this._types.GetIndexOfItem(type)));
    }

    public void WriteName(string name)
    {
      this._cache.Add((SizeOptimizedBinaryWriter.WriteCommand) new SizeOptimizedBinaryWriter.NumberWriteCommand(this._names.GetIndexOfItem(name)));
    }

    public void WriteValue(object value)
    {
      this._cache.Add((SizeOptimizedBinaryWriter.WriteCommand) new SizeOptimizedBinaryWriter.ValueWriteCommand(value));
    }

    public void WriteNumber(int number)
    {
      this._cache.Add((SizeOptimizedBinaryWriter.WriteCommand) new SizeOptimizedBinaryWriter.NumberWriteCommand(number));
    }

    public void WriteNumbers(int[] numbers)
    {
      this._cache.Add((SizeOptimizedBinaryWriter.WriteCommand) new SizeOptimizedBinaryWriter.NumbersWriteCommand(numbers));
    }

    public void Open(Stream stream)
    {
      this._stream = stream;
      this._cache = new List<SizeOptimizedBinaryWriter.WriteCommand>();
      this._types = new IndexGenerator<Type>();
      this._names = new IndexGenerator<string>();
    }

    public void Close()
    {
      BinaryWriter writer = new BinaryWriter(this._stream, this._encoding);
      this.writeNamesHeader(writer);
      this.writeTypesHeader(writer);
      SizeOptimizedBinaryWriter.writeCache(this._cache, writer);
      writer.Flush();
    }

    private static void writeCache(
      List<SizeOptimizedBinaryWriter.WriteCommand> cache,
      BinaryWriter writer)
    {
      foreach (SizeOptimizedBinaryWriter.WriteCommand writeCommand in cache)
        writeCommand.Write(writer);
    }

    private void writeNamesHeader(BinaryWriter writer)
    {
      BinaryWriterTools.WriteNumber(this._names.Items.Count, writer);
      foreach (string text in (IEnumerable<string>) this._names.Items)
        BinaryWriterTools.WriteString(text, writer);
    }

    private void writeTypesHeader(BinaryWriter writer)
    {
      BinaryWriterTools.WriteNumber(this._types.Items.Count, writer);
      foreach (Type type in (IEnumerable<Type>) this._types.Items)
        BinaryWriterTools.WriteString(this._typeNameConverter.ConvertToTypeName(type), writer);
    }

    private abstract class WriteCommand
    {
      public abstract void Write(BinaryWriter writer);
    }

    private sealed class ByteWriteCommand : SizeOptimizedBinaryWriter.WriteCommand
    {
      public ByteWriteCommand(byte data) => this.Data = data;

      public byte Data { get; set; }

      public override void Write(BinaryWriter writer) => writer.Write(this.Data);
    }

    private sealed class NumberWriteCommand : SizeOptimizedBinaryWriter.WriteCommand
    {
      public NumberWriteCommand(int data) => this.Data = data;

      public int Data { get; set; }

      public override void Write(BinaryWriter writer)
      {
        BinaryWriterTools.WriteNumber(this.Data, writer);
      }
    }

    private sealed class NumbersWriteCommand : SizeOptimizedBinaryWriter.WriteCommand
    {
      public NumbersWriteCommand(int[] data) => this.Data = data;

      public int[] Data { get; set; }

      public override void Write(BinaryWriter writer)
      {
        BinaryWriterTools.WriteNumbers(this.Data, writer);
      }
    }

    private sealed class ValueWriteCommand : SizeOptimizedBinaryWriter.WriteCommand
    {
      public ValueWriteCommand(object data) => this.Data = data;

      public object Data { get; set; }

      public override void Write(BinaryWriter writer)
      {
        BinaryWriterTools.WriteValue(this.Data, writer);
      }
    }
  }
}

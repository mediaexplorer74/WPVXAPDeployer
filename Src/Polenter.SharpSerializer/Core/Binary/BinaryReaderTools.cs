
// Type: Polenter.Serialization.Core.Binary.BinaryReaderTools
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Polenter.Serialization.Core.Binary
{
  public static class BinaryReaderTools
  {
    public static string ReadString(BinaryReader reader)
    {
      return !reader.ReadBoolean() ? (string) null : reader.ReadString();
    }

    public static int ReadNumber(BinaryReader reader)
    {
      switch (reader.ReadByte())
      {
        case 0:
          return 0;
        case 1:
          return (int) reader.ReadByte();
        case 2:
          return (int) reader.ReadInt16();
        default:
          return reader.ReadInt32();
      }
    }

    public static int[] ReadNumbers(BinaryReader reader)
    {
      int num = BinaryReaderTools.ReadNumber(reader);
      if (num == 0)
        return new int[0];
      List<int> intList = new List<int>();
      for (int index = 0; index < num; ++index)
        intList.Add(BinaryReaderTools.ReadNumber(reader));
      return intList.ToArray();
    }

    public static object ReadValue(Type expectedType, BinaryReader reader)
    {
      return !reader.ReadBoolean() ? (object) null : BinaryReaderTools.readValueCore(expectedType, reader);
    }

    private static object readValueCore(Type type, BinaryReader reader)
    {
      try
      {
        if ((object) type == (object) typeof (byte[]))
          return (object) BinaryReaderTools.readArrayOfByte(reader);
        if ((object) type == (object) typeof (string))
          return (object) reader.ReadString();
        if ((object) type == (object) typeof (bool))
          return (object) reader.ReadBoolean();
        if ((object) type == (object) typeof (byte))
          return (object) reader.ReadByte();
        if ((object) type == (object) typeof (char))
          return (object) reader.ReadChar();
        if ((object) type == (object) typeof (DateTime))
          return (object) new DateTime(reader.ReadInt64());
        if ((object) type == (object) typeof (Guid))
          return (object) new Guid(reader.ReadBytes(16));
        if ((object) type == (object) typeof (Decimal))
          return (object) reader.ReadDecimal();
        if ((object) type == (object) typeof (double))
          return (object) reader.ReadDouble();
        if ((object) type == (object) typeof (short))
          return (object) reader.ReadInt16();
        if ((object) type == (object) typeof (int))
          return (object) reader.ReadInt32();
        if ((object) type == (object) typeof (long))
          return (object) reader.ReadInt64();
        if ((object) type == (object) typeof (sbyte))
          return (object) reader.ReadSByte();
        if ((object) type == (object) typeof (float))
          return (object) reader.ReadSingle();
        if ((object) type == (object) typeof (ushort))
          return (object) reader.ReadUInt16();
        if ((object) type == (object) typeof (uint))
          return (object) reader.ReadUInt32();
        if ((object) type == (object) typeof (ulong))
          return (object) reader.ReadUInt64();
        if ((object) type == (object) typeof (TimeSpan))
          return (object) new TimeSpan(reader.ReadInt64());
        return type.IsEnum ? BinaryReaderTools.readEnumeration(type, reader) : throw new InvalidOperationException(string.Format("Unknown simple type: {0}", (object) type.FullName));
      }
      catch (Exception ex)
      {
        throw new SimpleValueParsingException(string.Format("Invalid type: {0}. See details in the inner exception.", (object) type), ex);
      }
    }

    private static object readEnumeration(Type expectedType, BinaryReader reader)
    {
      int num = reader.ReadInt32();
      return Enum.ToObject(expectedType, num);
    }

    private static byte[] readArrayOfByte(BinaryReader reader)
    {
      int count = BinaryReaderTools.ReadNumber(reader);
      return count == 0 ? (byte[]) null : reader.ReadBytes(count);
    }
  }
}

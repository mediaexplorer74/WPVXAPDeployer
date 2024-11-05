
// Type: Polenter.Serialization.Core.Binary.BinaryWriterTools
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.IO;

#nullable disable
namespace Polenter.Serialization.Core.Binary
{
  public static class BinaryWriterTools
  {
    public static void WriteNumber(int number, BinaryWriter writer)
    {
      byte numberSize = NumberSize.GetNumberSize(number);
      writer.Write(numberSize);
      if (numberSize <= (byte) 0)
        return;
      switch (numberSize)
      {
        case 1:
          writer.Write((byte) number);
          break;
        case 2:
          writer.Write((short) number);
          break;
        default:
          writer.Write(number);
          break;
      }
    }

    public static void WriteNumbers(int[] numbers, BinaryWriter writer)
    {
      BinaryWriterTools.WriteNumber(numbers.Length, writer);
      foreach (int number in numbers)
        BinaryWriterTools.WriteNumber(number, writer);
    }

    public static void WriteValue(object value, BinaryWriter writer)
    {
      if (value == null)
      {
        writer.Write(false);
      }
      else
      {
        writer.Write(true);
        BinaryWriterTools.writeValueCore(value, writer);
      }
    }

    public static void WriteString(string text, BinaryWriter writer)
    {
      if (string.IsNullOrEmpty(text))
      {
        writer.Write(false);
      }
      else
      {
        writer.Write(true);
        writer.Write(text);
      }
    }

    private static void writeValueCore(object value, BinaryWriter writer)
    {
      Type type = value != null ? value.GetType() : throw new ArgumentNullException(nameof (value), "Written data can not be null.");
      if ((object) type == (object) typeof (byte[]))
        BinaryWriterTools.writeArrayOfByte((byte[]) value, writer);
      else if ((object) type == (object) typeof (string))
        writer.Write((string) value);
      else if ((object) type == (object) typeof (bool))
        writer.Write((bool) value);
      else if ((object) type == (object) typeof (byte))
        writer.Write((byte) value);
      else if ((object) type == (object) typeof (char))
        writer.Write((char) value);
      else if ((object) type == (object) typeof (DateTime))
        writer.Write(((DateTime) value).Ticks);
      else if ((object) type == (object) typeof (Guid))
        writer.Write(((Guid) value).ToByteArray());
      else if ((object) type == (object) typeof (Decimal))
        writer.Write((Decimal) value);
      else if ((object) type == (object) typeof (double))
        writer.Write((double) value);
      else if ((object) type == (object) typeof (short))
        writer.Write((short) value);
      else if ((object) type == (object) typeof (int))
        writer.Write((int) value);
      else if ((object) type == (object) typeof (long))
        writer.Write((long) value);
      else if ((object) type == (object) typeof (sbyte))
        writer.Write((sbyte) value);
      else if ((object) type == (object) typeof (float))
        writer.Write((float) value);
      else if ((object) type == (object) typeof (ushort))
        writer.Write((ushort) value);
      else if ((object) type == (object) typeof (uint))
        writer.Write((uint) value);
      else if ((object) type == (object) typeof (ulong))
        writer.Write((ulong) value);
      else if ((object) type == (object) typeof (TimeSpan))
      {
        writer.Write(((TimeSpan) value).Ticks);
      }
      else
      {
        if (!type.IsEnum)
          throw new InvalidOperationException(string.Format("Unknown simple type: {0}", (object) type.FullName));
        writer.Write(Convert.ToInt32(value));
      }
    }

    private static void writeArrayOfByte(byte[] data, BinaryWriter writer)
    {
      BinaryWriterTools.WriteNumber(data.Length, writer);
      writer.Write(data);
    }
  }
}

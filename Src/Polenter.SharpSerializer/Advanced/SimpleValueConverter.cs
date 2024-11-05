
// Type: Polenter.Serialization.Advanced.SimpleValueConverter
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced.Xml;
using Polenter.Serialization.Core;
using System;
using System.Globalization;

#nullable disable
namespace Polenter.Serialization.Advanced
{
  public sealed class SimpleValueConverter : ISimpleValueConverter
  {
    private readonly CultureInfo _cultureInfo;

    public SimpleValueConverter() => this._cultureInfo = CultureInfo.InvariantCulture;

    public SimpleValueConverter(CultureInfo cultureInfo) => this._cultureInfo = cultureInfo;

    public string ConvertToString(object value)
    {
      return value == null ? string.Empty : Convert.ToString(value, (IFormatProvider) this._cultureInfo);
    }

    public object ConvertFromString(string text, Type type)
    {
      try
      {
        if ((object) type == (object) typeof (string))
          return (object) text;
        if ((object) type == (object) typeof (bool))
          return (object) Convert.ToBoolean(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (byte))
          return (object) Convert.ToByte(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (char))
          return (object) Convert.ToChar(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (DateTime))
          return (object) Convert.ToDateTime(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (Decimal))
          return (object) Convert.ToDecimal(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (double))
          return (object) Convert.ToDouble(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (short))
          return (object) Convert.ToInt16(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (int))
          return (object) Convert.ToInt32(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (long))
          return (object) Convert.ToInt64(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (sbyte))
          return (object) Convert.ToSByte(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (float))
          return (object) Convert.ToSingle(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (ushort))
          return (object) Convert.ToUInt16(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (uint))
          return (object) Convert.ToUInt32(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (ulong))
          return (object) Convert.ToUInt64(text, (IFormatProvider) this._cultureInfo);
        if ((object) type == (object) typeof (TimeSpan))
          return (object) TimeSpan.Parse(text);
        if ((object) type == (object) typeof (Guid))
          return (object) new Guid(text);
        return type.IsEnum ? Enum.Parse(type, text, true) : throw new InvalidOperationException(string.Format("Unknown simple type: {0}", (object) type.FullName));
      }
      catch (Exception ex)
      {
        throw new SimpleValueParsingException(string.Format("Invalid value: {0}. See details in the inner exception.", (object) text), ex);
      }
    }
  }
}

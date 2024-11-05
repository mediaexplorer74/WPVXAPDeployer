
// Type: Polenter.Serialization.Core.DefaultInitializer
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced;
using Polenter.Serialization.Advanced.Serializing;
using Polenter.Serialization.Advanced.Xml;
using System.Globalization;
using System.Text;
using System.Xml;

#nullable disable
namespace Polenter.Serialization.Core
{
  internal static class DefaultInitializer
  {
    public static XmlWriterSettings GetXmlWriterSettings()
    {
      return DefaultInitializer.GetXmlWriterSettings(Encoding.UTF8);
    }

    public static XmlWriterSettings GetXmlWriterSettings(Encoding encoding)
    {
      return new XmlWriterSettings()
      {
        Encoding = encoding,
        Indent = true,
        OmitXmlDeclaration = true
      };
    }

    public static XmlReaderSettings GetXmlReaderSettings()
    {
      return new XmlReaderSettings()
      {
        IgnoreComments = true,
        IgnoreWhitespace = true
      };
    }

    public static ITypeNameConverter GetTypeNameConverter()
    {
      return DefaultInitializer.GetTypeNameConverter(false, false, false);
    }

    public static ITypeNameConverter GetTypeNameConverter(
      bool includeAssemblyVersion,
      bool includeCulture,
      bool includePublicKeyToken)
    {
      return (ITypeNameConverter) new TypeNameConverter(includeAssemblyVersion, includeCulture, includePublicKeyToken);
    }

    public static ISimpleValueConverter GetSimpleValueConverter()
    {
      return DefaultInitializer.GetSimpleValueConverter(CultureInfo.InvariantCulture);
    }

    public static ISimpleValueConverter GetSimpleValueConverter(CultureInfo cultureInfo)
    {
      return (ISimpleValueConverter) new SimpleValueConverter(cultureInfo);
    }
  }
}

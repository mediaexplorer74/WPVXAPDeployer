
// Type: Polenter.Serialization.SharpSerializer
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced;
using Polenter.Serialization.Advanced.Binary;
using Polenter.Serialization.Advanced.Deserializing;
using Polenter.Serialization.Advanced.Serializing;
using Polenter.Serialization.Advanced.Xml;
using Polenter.Serialization.Core;
using Polenter.Serialization.Deserializing;
using Polenter.Serialization.Serializing;
using System;
using System.IO;
using System.Xml;

#nullable disable
namespace Polenter.Serialization
{
  public sealed class SharpSerializer
  {
    private IPropertyDeserializer _deserializer;
    private PropertyProvider _propertyProvider;
    private string _rootName;
    private IPropertySerializer _serializer;

    public SharpSerializer() => this.initialize(new SharpSerializerXmlSettings());

    public SharpSerializer(bool binarySerialization)
    {
      if (binarySerialization)
        this.initialize(new SharpSerializerBinarySettings());
      else
        this.initialize(new SharpSerializerXmlSettings());
    }

    public SharpSerializer(BinarySerializationMode binaryMode)
    {
      this.initialize(new SharpSerializerBinarySettings(binaryMode));
    }

    public SharpSerializer(SharpSerializerXmlSettings settings)
    {
      if (settings == null)
        throw new ArgumentNullException(nameof (settings));
      this.initialize(settings);
    }

    public SharpSerializer(SharpSerializerBinarySettings settings)
    {
      if (settings == null)
        throw new ArgumentNullException(nameof (settings));
      this.initialize(settings);
    }

    public SharpSerializer(IPropertySerializer serializer, IPropertyDeserializer deserializer)
    {
      if (serializer == null)
        throw new ArgumentNullException(nameof (serializer));
      if (deserializer == null)
        throw new ArgumentNullException(nameof (deserializer));
      this._serializer = serializer;
      this._deserializer = deserializer;
    }

    public PropertyProvider PropertyProvider
    {
      get
      {
        if (this._propertyProvider == null)
          this._propertyProvider = new PropertyProvider();
        return this._propertyProvider;
      }
      set => this._propertyProvider = value;
    }

    public string RootName
    {
      get
      {
        if (this._rootName == null)
          this._rootName = "Root";
        return this._rootName;
      }
      set => this._rootName = value;
    }

    private void initialize(SharpSerializerXmlSettings settings)
    {
      this.PropertyProvider.PropertiesToIgnore = settings.AdvancedSettings.PropertiesToIgnore;
      this.PropertyProvider.AttributesToIgnore = settings.AdvancedSettings.AttributesToIgnore;
      this.RootName = settings.AdvancedSettings.RootName;
      ISimpleValueConverter simpleValueConverter = settings.AdvancedSettings.SimpleValueConverter ?? DefaultInitializer.GetSimpleValueConverter(settings.Culture);
      ITypeNameConverter typeNameConverter = settings.AdvancedSettings.TypeNameConverter ?? DefaultInitializer.GetTypeNameConverter(settings.IncludeAssemblyVersionInTypeName, settings.IncludeCultureInTypeName, settings.IncludePublicKeyTokenInTypeName);
      XmlWriterSettings xmlWriterSettings = DefaultInitializer.GetXmlWriterSettings(settings.Encoding);
      XmlReaderSettings xmlReaderSettings = DefaultInitializer.GetXmlReaderSettings();
      DefaultXmlReader reader = new DefaultXmlReader(typeNameConverter, simpleValueConverter, xmlReaderSettings);
      this._serializer = (IPropertySerializer) new XmlPropertySerializer((IXmlWriter) new DefaultXmlWriter(typeNameConverter, simpleValueConverter, xmlWriterSettings));
      this._deserializer = (IPropertyDeserializer) new XmlPropertyDeserializer((IXmlReader) reader);
    }

    private void initialize(SharpSerializerBinarySettings settings)
    {
      this.PropertyProvider.PropertiesToIgnore = settings.AdvancedSettings.PropertiesToIgnore;
      this.PropertyProvider.AttributesToIgnore = settings.AdvancedSettings.AttributesToIgnore;
      this.RootName = settings.AdvancedSettings.RootName;
      ITypeNameConverter typeNameConverter = settings.AdvancedSettings.TypeNameConverter ?? DefaultInitializer.GetTypeNameConverter(settings.IncludeAssemblyVersionInTypeName, settings.IncludeCultureInTypeName, settings.IncludePublicKeyTokenInTypeName);
      IBinaryWriter writer;
      IBinaryReader reader;
      if (settings.Mode == BinarySerializationMode.Burst)
      {
        writer = (IBinaryWriter) new BurstBinaryWriter(typeNameConverter, settings.Encoding);
        reader = (IBinaryReader) new BurstBinaryReader(typeNameConverter, settings.Encoding);
      }
      else
      {
        writer = (IBinaryWriter) new SizeOptimizedBinaryWriter(typeNameConverter, settings.Encoding);
        reader = (IBinaryReader) new SizeOptimizedBinaryReader(typeNameConverter, settings.Encoding);
      }
      this._deserializer = (IPropertyDeserializer) new BinaryPropertyDeserializer(reader);
      this._serializer = (IPropertySerializer) new BinaryPropertySerializer(writer);
    }

    public void Serialize(object data, string filename)
    {
      using (Stream stream = (Stream) new FileStream(filename, FileMode.Create, FileAccess.Write))
        this.Serialize(data, stream);
    }

    public void Serialize(object data, Stream stream)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      Property property = new PropertyFactory(this.PropertyProvider).CreateProperty(this.RootName, data);
      try
      {
        this._serializer.Open(stream);
        this._serializer.Serialize(property);
      }
      finally
      {
        this._serializer.Close();
      }
    }

    public object Deserialize(string filename)
    {
      using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
        return this.Deserialize((Stream) fileStream);
    }

    public object Deserialize(Stream stream)
    {
      try
      {
        this._deserializer.Open(stream);
        Property property = this._deserializer.Deserialize();
        this._deserializer.Close();
        return new ObjectFactory().CreateObject(property);
      }
      catch (Exception ex)
      {
        throw new DeserializingException("An error occured during the deserialization. Details are in the inner exception.", ex);
      }
    }
  }
}

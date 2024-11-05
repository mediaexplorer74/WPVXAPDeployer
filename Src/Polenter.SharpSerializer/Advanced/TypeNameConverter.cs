
// Type: Polenter.Serialization.Advanced.TypeNameConverter
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Advanced.Serializing;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace Polenter.Serialization.Advanced
{
  public sealed class TypeNameConverter : ITypeNameConverter
  {
    private readonly Dictionary<Type, string> _cache = new Dictionary<Type, string>();

    public TypeNameConverter()
    {
    }

    public TypeNameConverter(
      bool includeAssemblyVersion,
      bool includeCulture,
      bool includePublicKeyToken)
    {
      this.IncludeAssemblyVersion = includeAssemblyVersion;
      this.IncludeCulture = includeCulture;
      this.IncludePublicKeyToken = includePublicKeyToken;
    }

    public bool IncludeAssemblyVersion { get; private set; }

    public bool IncludeCulture { get; private set; }

    public bool IncludePublicKeyToken { get; private set; }

    public string ConvertToTypeName(Type type)
    {
      if ((object) type == null)
        return string.Empty;
      if (this._cache.ContainsKey(type))
        return this._cache[type];
      string typename = type.AssemblyQualifiedName;
      if (!this.IncludeAssemblyVersion)
        typename = TypeNameConverter.removeAssemblyVersion(typename);
      if (!this.IncludeCulture)
        typename = TypeNameConverter.removeCulture(typename);
      if (!this.IncludePublicKeyToken)
        typename = TypeNameConverter.removePublicKeyToken(typename);
      this._cache.Add(type, typename);
      return typename;
    }

    public Type ConvertToType(string typeName)
    {
      return string.IsNullOrEmpty(typeName) ? (Type) null : Type.GetType(typeName, true);
    }

    private static string removePublicKeyToken(string typename)
    {
      return Regex.Replace(typename, ", PublicKeyToken=\\w+", string.Empty);
    }

    private static string removeCulture(string typename)
    {
      return Regex.Replace(typename, ", Culture=\\w+", string.Empty);
    }

    private static string removeAssemblyVersion(string typename)
    {
      return Regex.Replace(typename, ", Version=\\d+.\\d+.\\d+.\\d+", string.Empty);
    }
  }
}

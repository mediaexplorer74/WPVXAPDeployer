
// Type: Polenter.Serialization.Advanced.PropertyProvider
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace Polenter.Serialization.Advanced
{
  public class PropertyProvider
  {
    private PropertiesToIgnore _propertiesToIgnore;
    private IList<Type> _attributesToIgnore;
    private static readonly Dictionary<Type, IList<PropertyInfo>> _cache = new Dictionary<Type, IList<PropertyInfo>>();

    public PropertiesToIgnore PropertiesToIgnore
    {
      get
      {
        if (this._propertiesToIgnore == null)
          this._propertiesToIgnore = new PropertiesToIgnore();
        return this._propertiesToIgnore;
      }
      set => this._propertiesToIgnore = value;
    }

    public IList<Type> AttributesToIgnore
    {
      get
      {
        if (this._attributesToIgnore == null)
          this._attributesToIgnore = (IList<Type>) new List<Type>();
        return this._attributesToIgnore;
      }
      set => this._attributesToIgnore = value;
    }

    public IList<PropertyInfo> GetProperties(Polenter.Serialization.Serializing.TypeInfo typeInfo)
    {
      if (PropertyProvider._cache.ContainsKey(typeInfo.Type))
        return PropertyProvider._cache[typeInfo.Type];
      PropertyInfo[] allProperties = this.GetAllProperties(typeInfo.Type);
      List<PropertyInfo> properties = new List<PropertyInfo>();
      foreach (PropertyInfo property in allProperties)
      {
        if (!this.IgnoreProperty(typeInfo, property))
          properties.Add(property);
      }
      PropertyProvider._cache.Add(typeInfo.Type, (IList<PropertyInfo>) properties);
      return (IList<PropertyInfo>) properties;
    }

    protected virtual bool IgnoreProperty(Polenter.Serialization.Serializing.TypeInfo info, PropertyInfo property)
    {
      return this.PropertiesToIgnore.Contains(info.Type, property.Name) || this.ContainsExcludeFromSerializationAttribute((ICustomAttributeProvider) property) || !property.CanRead || !property.CanWrite || property.GetIndexParameters().Length > 0;
    }

    protected bool ContainsExcludeFromSerializationAttribute(ICustomAttributeProvider property)
    {
      foreach (Type attributeType in (IEnumerable<Type>) this.AttributesToIgnore)
      {
        if (property.GetCustomAttributes(attributeType, false).Length > 0)
          return true;
      }
      return false;
    }

    protected virtual PropertyInfo[] GetAllProperties(Type type)
    {
      return type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
    }
  }
}

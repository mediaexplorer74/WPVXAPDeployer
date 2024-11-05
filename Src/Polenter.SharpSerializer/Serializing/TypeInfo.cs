
// Type: Polenter.Serialization.Serializing.TypeInfo
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using Polenter.Serialization.Core;
using System;

#nullable disable
namespace Polenter.Serialization.Serializing
{
  public sealed class TypeInfo
  {
    private static readonly TypeInfoCollection Cache = new TypeInfoCollection();

    public bool IsSimple { get; set; }

    public bool IsArray { get; set; }

    public bool IsEnumerable { get; set; }

    public bool IsCollection { get; set; }

    public bool IsDictionary { get; set; }

    public Type ElementType { get; set; }

    public Type KeyType { get; set; }

    public int ArrayDimensionCount { get; set; }

    public Type Type { get; set; }

    public static TypeInfo GetTypeInfo(object obj)
    {
      return obj != null ? TypeInfo.GetTypeInfo(obj.GetType()) : throw new ArgumentNullException(nameof (obj));
    }

    public static TypeInfo GetTypeInfo(Type type)
    {
      TypeInfo typeInfo = TypeInfo.Cache.TryGetTypeInfo(type);
      if (typeInfo == null)
      {
        typeInfo = new TypeInfo();
        typeInfo.Type = type;
        typeInfo.IsSimple = Tools.IsSimple(type);
        if (!typeInfo.IsSimple)
        {
          typeInfo.IsArray = Tools.IsArray(type);
          if (typeInfo.IsArray)
          {
            if (type.HasElementType)
              typeInfo.ElementType = type.GetElementType();
            typeInfo.ArrayDimensionCount = type.GetArrayRank();
          }
          else
          {
            typeInfo.IsEnumerable = Tools.IsEnumerable(type);
            if (typeInfo.IsEnumerable)
            {
              typeInfo.IsCollection = Tools.IsCollection(type);
              if (typeInfo.IsCollection)
              {
                typeInfo.IsDictionary = Tools.IsDictionary(type);
                Type type1 = type;
                bool flag;
                do
                {
                  flag = TypeInfo.fillKeyAndElementType(typeInfo, type1);
                  type1 = type1.BaseType;
                }
                while (!flag && (object) type1 != null && (object) type1 != (object) typeof (object));
              }
            }
          }
        }
        TypeInfo.Cache.Add(typeInfo);
      }
      return typeInfo;
    }

    private static bool fillKeyAndElementType(TypeInfo typeInfo, Type type)
    {
      if (!type.IsGenericType)
        return false;
      Type[] genericArguments = type.GetGenericArguments();
      if (typeInfo.IsDictionary)
      {
        typeInfo.KeyType = genericArguments[0];
        typeInfo.ElementType = genericArguments[1];
      }
      else
        typeInfo.ElementType = genericArguments[0];
      return genericArguments.Length > 0;
    }
  }
}

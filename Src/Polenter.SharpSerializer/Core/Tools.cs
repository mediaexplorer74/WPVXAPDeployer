
// Type: Polenter.Serialization.Core.Tools
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.Collections;

#nullable disable
namespace Polenter.Serialization.Core
{
  internal static class Tools
  {
    public static bool IsSimple(Type type)
    {
      return (object) type == (object) typeof (string) || (object) type == (object) typeof (DateTime) || (object) type == (object) typeof (TimeSpan) || (object) type == (object) typeof (Decimal) || (object) type == (object) typeof (Guid) || type.IsEnum || type.IsPrimitive;
    }

    public static bool IsEnumerable(Type type) => typeof (IEnumerable).IsAssignableFrom(type);

    public static bool IsCollection(Type type) => typeof (ICollection).IsAssignableFrom(type);

    public static bool IsDictionary(Type type) => typeof (IDictionary).IsAssignableFrom(type);

    public static bool IsArray(Type type) => type.IsArray;

    public static object CreateInstance(Type type)
    {
      if ((object) type == null)
        return (object) null;
      try
      {
        return Activator.CreateInstance(type);
      }
      catch (Exception ex)
      {
        throw new CreatingInstanceException(string.Format("Error during creating an object. Please check if the type \"{0}\" has public parameterless constructor. Details are in the inner exception.", (object) type.AssemblyQualifiedName), ex);
      }
    }
  }
}

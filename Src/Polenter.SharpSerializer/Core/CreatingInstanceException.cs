
// Type: Polenter.Serialization.Core.CreatingInstanceException
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Polenter.Serialization.Core
{
  [Serializable]
  public class CreatingInstanceException : Exception
  {
    public CreatingInstanceException()
    {
    }

    public CreatingInstanceException(string message)
      : base(message)
    {
    }

    public CreatingInstanceException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    protected CreatingInstanceException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}

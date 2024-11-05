
// Type: Polenter.Serialization.Core.DeserializingException
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Polenter.Serialization.Core
{
  [Serializable]
  public class DeserializingException : Exception
  {
    public DeserializingException()
    {
    }

    public DeserializingException(string message)
      : base(message)
    {
    }

    public DeserializingException(string message, Exception inner)
      : base(message, inner)
    {
    }

    protected DeserializingException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}


// Type: Polenter.Serialization.Core.Binary.NumberSize
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

#nullable disable
namespace Polenter.Serialization.Core.Binary
{
  public static class NumberSize
  {
    public const byte Zero = 0;
    public const byte B1 = 1;
    public const byte B2 = 2;
    public const byte B4 = 4;

    public static byte GetNumberSize(int value)
    {
      if (value == 0)
        return 0;
      if (value > (int) short.MaxValue || value < (int) short.MinValue)
        return 4;
      return value < 0 || value > (int) byte.MaxValue ? (byte) 2 : (byte) 1;
    }
  }
}


// Type: Polenter.Serialization.Core.KeyValueItem
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

#nullable disable
namespace Polenter.Serialization.Core
{
  public sealed class KeyValueItem
  {
    public KeyValueItem(Property key, Property value)
    {
      this.Key = key;
      this.Value = value;
    }

    public Property Key { get; set; }

    public Property Value { get; set; }
  }
}

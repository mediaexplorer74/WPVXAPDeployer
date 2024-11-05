
// Type: WPV_XAP_Deployer.Properties.Resources




using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace WPV_XAP_Deployer.Properties
{
  [DebuggerNonUserCode]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) WPV_XAP_Deployer.Properties.Resources.resourceMan, (object) null))
          WPV_XAP_Deployer.Properties.Resources.resourceMan = new ResourceManager("WPV_XAP_Deployer.Properties.Resources", typeof (WPV_XAP_Deployer.Properties.Resources).Assembly);
        return WPV_XAP_Deployer.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => WPV_XAP_Deployer.Properties.Resources.resourceCulture;
      set => WPV_XAP_Deployer.Properties.Resources.resourceCulture = value;
    }

    internal static Bitmap mango
    {
      get
      {
        return (Bitmap) WPV_XAP_Deployer.Properties.Resources.ResourceManager.GetObject(nameof (mango), WPV_XAP_Deployer.Properties.Resources.resourceCulture);
      }
    }
  }
}


// Type: WPV_XAP_Deployer.Setting




using Polenter.Serialization;
using System;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace WPV_XAP_Deployer
{
  internal class Setting
  {
    private string AppPath = "";

    public bool IsForceUninstall { get; set; }

    public bool IsShutdown { get; set; }

    public Setting()
    {
      this.AppPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WPVXAP.setting";
      this.IsForceUninstall = true;
      this.IsShutdown = false;
      if (File.Exists(this.AppPath))
        return;
      this.Save();
    }

    public void Save()
    {
      try
      {
        if (File.Exists(this.AppPath))
          new FileInfo(this.AppPath).Attributes = FileAttributes.Normal;
        new SharpSerializer(true).Serialize((object) this, this.AppPath);
        new FileInfo(this.AppPath).Attributes = FileAttributes.Hidden;
      }
      catch (Exception ex)
      {
         Debug.WriteLine("[ex] Settings error: " + ex.Message);
      }
    }

    public void Load()
    {
      try
      {
        Setting setting = new SharpSerializer(true).Deserialize(this.AppPath) as Setting;
        this.IsForceUninstall = setting.IsForceUninstall;
        this.IsShutdown = setting.IsShutdown;
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] Settings error: " + ex.Message);
      }
    }
  }
}

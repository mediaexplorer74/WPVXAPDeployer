
// Type: WPV_XAP_Deployer.Program




using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace WPV_XAP_Deployer
{
  internal static class Program
  {
    public static string defaultFile = "";
    public static Setting AppSettings = new Setting();

    [STAThread]
    private static void Main(string[] args)
    {
      if (((IEnumerable<string>) args).Count<string>() > 0)
        Program.defaultFile = args[0];
      Program.AppSettings.Load();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Form1());
    }
  }
}

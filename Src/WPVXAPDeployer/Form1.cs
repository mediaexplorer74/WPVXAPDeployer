
// Type: WPV_XAP_Deployer.Form1




using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Ionic.Zip;
using Microsoft.SmartDevice.Connectivity;


#nullable disable
namespace WPV_XAP_Deployer
{
  public partial class Form1 : Form
  {
    private DatastoreManager dsmgrObj = default;      
        
    private Platform WP7SDK;
    private Device WP7Device;
    private List<XAPInfo> xapFiles = new List<XAPInfo>();
    private Thread thread;
    private bool error;
    
    public Form1()
    {
      this.InitializeComponent();

      try
      {
         dsmgrObj = new DatastoreManager(1033);
      }
      catch (Exception ex) 
      {
         Debug.WriteLine("[ex] new DatastoreManager error: " + ex.Message);
      }

      this.ckUninstall.Checked = Program.AppSettings.IsForceUninstall;
      this.ckShutdown.Checked = Program.AppSettings.IsShutdown;
      this.cbTarget.SelectedIndex = 0;
      Version version = Assembly.GetExecutingAssembly().GetName().Version;
      this.Text = "WPV XAP Deployer " + (object) version.Major
                + "." + (object) version.Minor + "." + (object) version.Build + " - Hà Mai Tùng [tdiddy.2]";
      this.xAPInfoBindingSource.DataSource = (object) this.xapFiles;
      this.FindTargetDevices();
    }

    public void FindTargetDevices()
    {
      try
      {
        this.cbTarget.Items.Clear();

        if (this.dsmgrObj != null)
        {
            this.WP7SDK = this.dsmgrObj.GetPlatforms().First<Platform>();

            foreach (object device in this.WP7SDK.GetDevices())
            {
                this.cbTarget.Items.Add(device);
            }

                    this.cbTarget.SelectedIndex = 0;
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show((IWin32Window) this, ex.Message,
            "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    public void CreateConnection()
    {
      this.WP7Device = this.WP7SDK.GetDevices().ElementAt<Device>(this.cbTarget.SelectedIndex);
    }

    private void btnDeploy_Click(object sender, EventArgs e)
    {
      if (this.btnDeploy.Text == "DEPLOY")
      {
        for (int rowIndex = 0; rowIndex < this.xapFiles.Count; ++rowIndex)
          this.ChangeRowStatus(rowIndex, 0);
        this.CreateConnection();
        this.thread = new Thread(new ThreadStart(this.Start));
        this.thread.Start();
        this.btnDeploy.Text = "STOP";
        this.error = false;
      }
      else
      {
        this.lblStatus.Text = "Stopping...";
        this.progressBar.Style = ProgressBarStyle.Marquee;
        this.thread.Abort();
      }
    }

    public void Start()
    {
      try
      {
        if (this.ckShutdown.Checked)
        {
          DialogResult dlgResult = DialogResult.None;
          this.Invoke((Delegate) (() => dlgResult = MessageBox.Show((IWin32Window) this,
              "Your computer will shut down after deployment complete\nAre you sure want to do this?",
              "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)));
          if (dlgResult == DialogResult.No)
            return;
        }
        this.Invoke((Delegate) (() =>
        {
          this.dgvFiles.ClearSelection();
          this.ckShutdown.Enabled = this.ckUninstall.Enabled
            = this.btnRemove.Enabled = this.btnAddFile.Enabled 
            = this.cbTarget.Enabled = this.dgvFiles.Enabled = this.btnOpenFolder.Enabled 
            = this.tabPage2.Enabled = false;

          this.lblStatus.Text = "Connecting to " + this.cbTarget.Text + "...";
          this.progressBar.Visible = true;
          this.progressBar.Style = ProgressBarStyle.Marquee;
        }));
        this.Connect();
        this.Invoke((Delegate) (() =>
        {
          this.progressBar.Style = ProgressBarStyle.Blocks;
          this.progressBar.Value = 0;
        }));
        for (int i = 0; i < this.xapFiles.Count; ++i)
        {
          try
          {
            this.Invoke((Delegate) (() =>
            {
              this.ChangeRowStatus(i, 1);
              this.dgvFiles.Rows[i].Selected = true;
              this.dgvFiles.FirstDisplayedScrollingRowIndex = i;
              this.lblStatus.Text = "Deploying (" + (object) (i + 1) + "/" + (object) this.xapFiles.Count + ")..." + this.xapFiles[i].Title;
              this.progressBar.Value = (int) ((double) (i + 1) / (double) this.xapFiles.Count * 100.0);
            }));
            this.Deploy(this.xapFiles[i]);
            this.Invoke((Delegate) (() => this.ChangeRowStatus(i, 2)));
          }
          catch (Exception ex)
          {
            this.Invoke((Delegate) (() =>
            {
              this.ChangeRowStatus(i, 3);
              this.dgvFiles[i, 0].ToolTipText = ex.Message;
              this.error = true;
            }));
          }
        }
        this.Invoke((Delegate) (() =>
        {
          this.lblStatus.Text = "Completed";
          if (!this.error)
            return;
          this.lblStatus.Text += " with error, click the error icon to view message";
        }));
        if (this.ckShutdown.Checked)
        {
          this.Invoke((Delegate) (() =>
          {
            ManagementClass managementClass = new ManagementClass("Win32_OperatingSystem");
            managementClass.Get();
            managementClass.Scope.Options.EnablePrivileges = true;
            ManagementBaseObject methodParameters = managementClass.GetMethodParameters("Win32Shutdown");
            methodParameters["Flags"] = (object) "1";
            methodParameters["Reserved"] = (object) "0";
            foreach (ManagementObject instance in managementClass.GetInstances())
              instance.InvokeMethod("Win32Shutdown", methodParameters, (InvokeMethodOptions) null);
          }));
        }
        else
        {
          int num;
          this.Invoke((Delegate) (() => num = (int) MessageBox.Show((IWin32Window) this, 
              "Deployment completed!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)));
        }
      }
      catch (ThreadAbortException ex)
      {
        Debug.WriteLine("[ex] Deployment or Management Process error: " + ex.Message);
        this.Invoke((Delegate) (() => this.lblStatus.Text = "Stopped by user"));
      }
      catch (Exception ex)
      {
        Form1 owner = this;
        int num;
        this.Invoke((Delegate) (() => num = (int) MessageBox.Show((IWin32Window) owner, 
            ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand)));
      }
      finally
      {
        this.Invoke((Delegate) (() =>
        {
          this.btnDeploy.Text = "DEPLOY";
          this.progressBar.Visible = false;
          this.ckShutdown.Enabled = this.ckUninstall.Enabled 
            = this.btnRemove.Enabled = this.btnAddFile.Enabled 
            = this.cbTarget.Enabled = this.dgvFiles.Enabled 
            = this.tabPage2.Enabled = this.btnOpenFolder.Enabled = true;
          this.lblCount.Text = "";
          this.dgvFiles.ClearSelection();
        }));
      }
    }

    public void Connect()
    {
      try
      {
        this.WP7Device.Connect();
      }
      catch (Exception ex)
      {
        this.Invoke((Delegate) (() => this.lblStatus.Text += "Failed"));
        throw new Exception(ex.Message);
      }
    }

    public void Deploy(XAPInfo xap)
    {
      if (this.WP7Device.IsApplicationInstalled(xap.ProductID))
      {
        RemoteApplication application = this.WP7Device.GetApplication(xap.ProductID);
        if (this.ckUninstall.Checked)
        {
          application.Uninstall();
        }
        else
        {
          application.UpdateApplication(xap.ProductID.ToString(), (string) null, xap.Path);
          return;
        }
      }
      this.WP7Device.InstallApplication(xap.ProductID, xap.ProductID, "NormalApp", (string) null, xap.Path);
    }

    public XAPInfo GetDetails(string item)
    {
      XAPInfo details = new XAPInfo();
      ZipFile zipFile = ZipFile.Read(item);
      MemoryStream memoryStream = new MemoryStream();
      zipFile["WMAppManifest.xml"].Extract((Stream) memoryStream);
      StreamReader streamReader = new StreamReader((Stream) memoryStream);
      memoryStream.Seek(0L, SeekOrigin.Begin);
      XElement xelement = XDocument.Load((TextReader) streamReader).Descendants((XName) "App").First<XElement>();
      details.Path = item;
      details.Title = xelement.Attributes((XName) "Title").First<XAttribute>().Value;
      details.ProductID = new Guid(xelement.Attributes((XName) "ProductID").First<XAttribute>().Value);
      details.Version = xelement.Attributes((XName) "Version").First<XAttribute>().Value;
      details.Author = xelement.Attributes((XName) "Author").First<XAttribute>().Value;
      details.FileSize = Math.Round((double) new FileInfo(item).Length / 1048576.0, 2).ToString() + " Mb";
      streamReader.Close();
      memoryStream.Close();
      return details;
    }

    private void btnAddFile_Click(object sender, EventArgs e)
    {
      if (this.openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      foreach (string fileName in this.openFileDialog.FileNames)
      {
        try
        {
          this.xAPInfoBindingSource.Add((object) this.GetDetails(fileName));
          this.ChangeRowStatus(this.xapFiles.Count - 1, 0);
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show((IWin32Window) this, ex.Message, "Error", 
              MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      this.btnDeploy.Enabled = true;
      this.lblStatus.Text = "Total: " + (object) this.xapFiles.Count + " file(s)";
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://winphoneviet.com");
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
      foreach (DataGridViewBand selectedRow in (BaseCollection) this.dgvFiles.SelectedRows)
        this.xAPInfoBindingSource.RemoveAt(selectedRow.Index);
      this.btnDeploy.Enabled = this.xapFiles.Count > 0;
      this.lblStatus.Text = "Total: " + (object) this.xapFiles.Count + " file(s)";
    }

    private void dgvFiles_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop))
        e.Effect = DragDropEffects.All;
      else
        e.Effect = DragDropEffects.None;
    }

    private void dgvFiles_DragDrop(object sender, DragEventArgs e)
    {
      foreach (string fileName in (string[]) e.Data.GetData(DataFormats.FileDrop, false))
      {
        if (new FileInfo(fileName).Extension.ToLower() == ".xap")
        {
          this.xAPInfoBindingSource.Add((object) this.GetDetails(fileName));
          this.ChangeRowStatus(this.xapFiles.Count - 1, 0);
        }
      }
      this.btnDeploy.Enabled = true;
      this.lblStatus.Text = "Total: " + (object) this.xapFiles.Count + " file(s)";
    }

    private void ckUninstall_CheckedChanged(object sender, EventArgs e)
    {
      Program.AppSettings.IsForceUninstall = this.ckUninstall.Checked;
    }

    private void ckShutdown_CheckedChanged(object sender, EventArgs e)
    {
      Program.AppSettings.IsShutdown = this.ckShutdown.Checked;
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      Program.AppSettings.Save();
    }

    private void dgvFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0)
        return;
      int num = (int) new ViewDetails((this.xAPInfoBindingSource[e.RowIndex] as XAPInfo).Path).ShowDialog();
    }

    public void ChangeRowStatus(int rowIndex, int status)
    {
      this.dgvFiles[0, rowIndex].Value = (object) this.imageList2.Images[status];
    }

    private void dgvFiles_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex != 0 || !(this.dgvFiles[0, e.RowIndex].ToolTipText != ""))
        return;
      int num = (int) MessageBox.Show((IWin32Window) this, this.dgvFiles[0, e.RowIndex].ToolTipText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }

    private void btnOpenFolder_Click(object sender, EventArgs e)
    {
      if (this.folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;
      foreach (string file in Directory.GetFiles(this.folderBrowserDialog.SelectedPath, "*.xap"))
      {
        this.xAPInfoBindingSource.Add((object) this.GetDetails(file));
        this.ChangeRowStatus(this.xapFiles.Count - 1, 0);
      }
      this.btnDeploy.Enabled = true;
      this.lblStatus.Text = "Total: " + (object) this.xapFiles.Count + " file(s)";
    }

    private void btnUninstall_Click(object sender, EventArgs e)
    {
      this.CreateConnection();
      new Thread((ThreadStart) (() =>
      {
        try
        {
          this.Invoke((Delegate) (() =>
          {
            this.Enabled = false;
            this.lblStatus.Text = "Connecting to " + this.cbTarget.Text + "...";
            this.progressBar.Visible = true;
            this.progressBar.Style = ProgressBarStyle.Marquee;
          }));
          this.Connect();
          Guid appID = new Guid();
          this.Invoke((Delegate) (() =>
          {
            this.lblStatus.Text = "Uninstalling...";
            appID = new Guid(this.txtUninstallAppID.Text);
          }));
          this.WP7Device.GetApplication(appID).Uninstall();
          this.Invoke((Delegate) (() =>
          {
            this.lblStatus.Text = "Completed";
            int num = (int) MessageBox.Show((IWin32Window) this, "Application uninstalled!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          }));
        }
        catch (Exception ex)
        {
          Form1 owner = this;
          this.Invoke((Delegate) (() =>
          {
            owner.lblStatus.Text = "Completed with error";
            int num = (int) MessageBox.Show((IWin32Window) owner, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }));
        }
        finally
        {
          this.Invoke((Delegate) (() =>
          {
            this.Enabled = true;
            this.progressBar.Visible = false;
          }));
        }
      })).Start();
    }

    private void btnGetLink_Click(object sender, EventArgs e)
    {
      this.txtDirectResult.Text = "Getting direct link...";
      this.btnGetLink.Enabled = false;
      WebClient webClient = new WebClient();
      webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(this.wc_DownloadStringCompleted);
      string uriString = string.Format("http://catalog.zune.net/v3.2/en-US/apps/{0}?clientType=WinMobile%207.1", (object) this.txtDirectAppID.Text);
      webClient.DownloadStringAsync(new Uri(uriString));
    }

    private void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
    {
      try
      {
        int startIndex = e.Result.IndexOf("<url>");
        int num = e.Result.IndexOf("</url", startIndex);
        this.txtDirectResult.Text = e.Result.Substring(startIndex + 5, num - startIndex - 5) + "\r\n";
        this.btnGetLink.Enabled = true;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show((IWin32Window) this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      if (Program.defaultFile != "")
      {
        try
        {
          this.xAPInfoBindingSource.Add((object) this.GetDetails(Program.defaultFile));
          this.ChangeRowStatus(0, 0);
          this.lblStatus.Text = "Total: " + (object) this.xapFiles.Count + " file(s)";
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show((IWin32Window) this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      this.btnDeploy.Enabled = this.xapFiles.Count > 0;
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
    }

    private void tabPage1_Click(object sender, EventArgs e)
    {
    }

        /*
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 196);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "btnDeploy";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(273, 196);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "btnAddFile";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(354, 196);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "btnRemove";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(733, 261);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.ResumeLayout(false);

    }
        */
  }
}


// Type: WPV_XAP_Deployer.ViewDetails




using Ionic.Zip;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

#nullable disable
namespace WPV_XAP_Deployer
{
  public partial class ViewDetails : Form
  {
    private string file;
    private string id;
    //private IContainer components;
    //private PictureBox pictureBox1;
    //private TextBox txtDetails;
    //private TextBox txtDesc;
    //private Button button1;
    //private Button btnMarketplace;

    //public ViewDetails() => this.InitializeComponent();

    public ViewDetails(string f)
    {
      this.InitializeComponent();
      this.file = f;
      this.LoadDetails();
    }

    public void LoadDetails()
    {
      ZipFile zipFile = ZipFile.Read(this.file);
      MemoryStream memoryStream1 = new MemoryStream();
      zipFile["WMAppManifest.xml"].Extract((Stream) memoryStream1);
      StreamReader streamReader = new StreamReader((Stream) memoryStream1);
      memoryStream1.Seek(0L, SeekOrigin.Begin);
      XElement xelement = XDocument.Load((TextReader) streamReader).Descendants((XName) "App").First<XElement>();
      this.Text = xelement.Attributes((XName) "Title").First<XAttribute>().Value;
      string fileName = xelement.Descendants((XName) "BackgroundImageURI").First<XElement>().Value;
      MemoryStream memoryStream2 = new MemoryStream();
      zipFile[fileName].Extract((Stream) memoryStream2);
      this.pictureBox1.Image = Image.FromStream((Stream) memoryStream2);
      memoryStream2.Close();
      this.txtDetails.Text = "Title: " + this.Text + "\r\n";
      TextBox txtDetails1 = this.txtDetails;
      txtDetails1.Text = txtDetails1.Text + "Author: " + xelement.Attributes((XName) "Author").First<XAttribute>().Value + "\r\n";
      TextBox txtDetails2 = this.txtDetails;
      txtDetails2.Text = txtDetails2.Text + "Version: " + xelement.Attributes((XName) "Version").First<XAttribute>().Value + "\r\n";
      this.id = xelement.Attributes((XName) "ProductID").First<XAttribute>().Value;
      TextBox txtDetails3 = this.txtDetails;
      txtDetails3.Text = txtDetails3.Text + "Product ID: " + new Guid(this.id).ToString() + "\r\n";
      TextBox txtDetails4 = this.txtDetails;
      txtDetails4.Text = txtDetails4.Text + "Runtime Type: " + xelement.Attributes((XName) "RuntimeType").First<XAttribute>().Value + "\r\n";
      TextBox txtDetails5 = this.txtDetails;
      txtDetails5.Text = txtDetails5.Text + "Publisher: " + xelement.Attributes((XName) "Publisher").First<XAttribute>().Value + "\r\n";
      this.txtDesc.Text = xelement.Attributes((XName) "Description").First<XAttribute>().Value;
      streamReader.Close();
      memoryStream1.Close();
    }

    private void btnMarketplace_Click(object sender, EventArgs e)
    {
      Process.Start("http://www.windowsphone.com/en-US/apps/" + new Guid(this.id).ToString());
    }

        /*
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }
        */

        /*

    private void InitializeComponent()
    {
      this.pictureBox1 = new PictureBox();
      this.txtDetails = new TextBox();
      this.txtDesc = new TextBox();
      this.button1 = new Button();
      this.btnMarketplace = new Button();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.pictureBox1.BackColor = Color.FromArgb(128, 128, (int) byte.MaxValue);
      this.pictureBox1.Location = new Point(12, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(100, 100);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.txtDetails.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.txtDetails.BackColor = SystemColors.Control;
      this.txtDetails.BorderStyle = BorderStyle.None;
      this.txtDetails.Font = new Font("Verdana", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txtDetails.Location = new Point(118, 12);
      this.txtDetails.Multiline = true;
      this.txtDetails.Name = "txtDetails";
      this.txtDetails.ReadOnly = true;
      this.txtDetails.Size = new Size(379, 109);
      this.txtDetails.TabIndex = 2;
      this.txtDesc.BackColor = SystemColors.Control;
      this.txtDesc.BorderStyle = BorderStyle.None;
      this.txtDesc.Location = new Point(12, 128);
      this.txtDesc.Multiline = true;
      this.txtDesc.Name = "txtDesc";
      this.txtDesc.ReadOnly = true;
      this.txtDesc.Size = new Size(485, 54);
      this.txtDesc.TabIndex = 3;
      this.button1.DialogResult = DialogResult.Cancel;
      this.button1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.button1.Location = new Point(391, 190);
      this.button1.Name = "button1";
      this.button1.Size = new Size(106, 30);
      this.button1.TabIndex = 1;
      this.button1.Text = "Close";
      this.button1.UseVisualStyleBackColor = true;
      this.btnMarketplace.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.btnMarketplace.Location = new Point(279, 190);
      this.btnMarketplace.Name = "btnMarketplace";
      this.btnMarketplace.Size = new Size(106, 30);
      this.btnMarketplace.TabIndex = 1;
      this.btnMarketplace.Text = "Store";
      this.btnMarketplace.UseVisualStyleBackColor = true;
      this.btnMarketplace.Click += new EventHandler(this.btnMarketplace_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.button1;
      this.ClientSize = new Size(509, 229);
      this.Controls.Add((Control) this.btnMarketplace);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.txtDesc);
      this.Controls.Add((Control) this.txtDetails);
      this.Controls.Add((Control) this.pictureBox1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ViewDetails);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = nameof (ViewDetails);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
        */
  }
}

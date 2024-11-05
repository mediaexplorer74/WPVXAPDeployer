using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WPV_XAP_Deployer
{
    public partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;



        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            /*
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(56, 196);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "btnDeploy777";
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
            */
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Form1));
            DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
            this.statusStrip1 = new StatusStrip();
            this.progressBar = new ToolStripProgressBar();
            this.lblStatus = new ToolStripStatusLabel();
            this.lblCount = new ToolStripStatusLabel();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.cbTarget = new ToolStripComboBox();
            this.btnDeploy = new Button();
            this.imageList1 = new ImageList(/*this.components*/);
            this.openFileDialog = new OpenFileDialog();
            this.linkLabel1 = new LinkLabel();
            this.dgvFiles = new DataGridView();
            this.Column1 = new DataGridViewImageColumn();
            this.FileSize = new DataGridViewTextBoxColumn();
            this.ckShutdown = new CheckBox();
            this.ckUninstall = new CheckBox();
            this.imageList2 = new ImageList(/*this.components*/);
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.groupBox2 = new GroupBox();
            this.txtDirectAppID = new MaskedTextBox();
            this.txtDirectResult = new TextBox();
            this.btnGetLink = new Button();
            this.label2 = new Label();
            this.groupBox1 = new GroupBox();
            this.txtUninstallAppID = new MaskedTextBox();
            this.btnUninstall = new Button();
            this.label1 = new Label();
            this.folderBrowserDialog = new FolderBrowserDialog();
            this.btnOpenFolder = new Button();
            this.btnAddFile = new Button();
            this.btnRemove = new Button();
            this.titleDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.authorDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.pathDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.xAPInfoBindingSource = new BindingSource(/*this.components*/);
            this.statusStrip1.SuspendLayout();
            ((ISupportInitialize)this.dgvFiles).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((ISupportInitialize)this.xAPInfoBindingSource).BeginInit();
            this.SuspendLayout();
            this.statusStrip1.Items.AddRange(new ToolStripItem[6]
            {
        (ToolStripItem) this.progressBar,
        (ToolStripItem) this.lblStatus,
        (ToolStripItem) this.lblCount,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.toolStripStatusLabel1,
        (ToolStripItem) this.cbTarget
            });
            this.statusStrip1.Location = new Point(0, 413);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new Size(640, 23);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new Size(100, 17);
            this.progressBar.Visible = false;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(39, 18);
            this.lblStatus.Text = "Ready";
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new Size(413, 18);
            this.lblCount.Spring = true;
            this.lblCount.TextAlign = ContentAlignment.MiddleRight;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 23);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new Size(44, 18);
            this.toolStripStatusLabel1.Text = "Target:";
            this.cbTarget.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbTarget.DropDownWidth = 200;
            this.cbTarget.Items.AddRange(new object[2]
            {
        (object) "WP Device",
        (object) "WP Emulator"
            });
            this.cbTarget.Name = "cbTarget";
            this.cbTarget.Size = new Size(121, 23);
            this.btnDeploy.Anchor = AnchorStyles.Bottom;
            this.btnDeploy.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
            this.btnDeploy.ForeColor = Color.Black;
            this.btnDeploy.ImageIndex = 0;
            this.btnDeploy.Location = new Point(241, 331);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new Size(159, 47);
            this.btnDeploy.TabIndex = 6;
            this.btnDeploy.Text = "DEPLOY";
            this.btnDeploy.UseVisualStyleBackColor = true;
            this.btnDeploy.Click += new EventHandler(this.btnDeploy_Click);
            this.imageList1.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            //this.imageList1.Images.SetKeyName(0, "new.png");
            //this.imageList1.Images.SetKeyName(1, "delete.png");
            //this.imageList1.Images.SetKeyName(2, "download.png");
            this.openFileDialog.Filter = "XAP Files|*.xap";
            this.openFileDialog.FilterIndex = 0;
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.Title = "Choose XAP files to deploy";
            this.linkLabel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.linkLabel1.Location = new Point(500, 360);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new Size(97, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "WinphoneViet.com";
            this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            this.dgvFiles.AllowDrop = true;
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.AllowUserToDeleteRows = false;
            this.dgvFiles.AllowUserToResizeRows = false;
            this.dgvFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvFiles.AutoGenerateColumns = false;
            this.dgvFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvFiles.BackgroundColor = Color.White;
            this.dgvFiles.CellBorderStyle = DataGridViewCellBorderStyle.None;
            gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridViewCellStyle1.BackColor = SystemColors.Control;
            gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            gridViewCellStyle1.ForeColor = SystemColors.WindowText;
            gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            this.dgvFiles.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
            this.dgvFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange((DataGridViewColumn)this.Column1, (DataGridViewColumn)this.titleDataGridViewTextBoxColumn, (DataGridViewColumn)this.versionDataGridViewTextBoxColumn, (DataGridViewColumn)this.authorDataGridViewTextBoxColumn, (DataGridViewColumn)this.FileSize, (DataGridViewColumn)this.pathDataGridViewTextBoxColumn);
            this.dgvFiles.DataSource = (object)this.xAPInfoBindingSource;
            this.dgvFiles.Location = new Point(4, 51);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.ReadOnly = true;
            this.dgvFiles.RowHeadersVisible = false;
            this.dgvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvFiles.ShowEditingIcon = false;
            this.dgvFiles.Size = new Size(623, 274);
            this.dgvFiles.TabIndex = 3;
            this.dgvFiles.CellClick += new DataGridViewCellEventHandler(this.dgvFiles_CellClick);
            this.dgvFiles.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvFiles_CellDoubleClick);
            this.dgvFiles.DragDrop += new DragEventHandler(this.dgvFiles_DragDrop);
            this.dgvFiles.DragEnter += new DragEventHandler(this.dgvFiles_DragEnter);
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = DataGridViewTriState.True;
            this.Column1.Width = 5;
            this.FileSize.DataPropertyName = "FileSize";
            gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.FileSize.DefaultCellStyle = gridViewCellStyle2;
            this.FileSize.HeaderText = "Size";
            this.FileSize.Name = "FileSize";
            this.FileSize.ReadOnly = true;
            this.FileSize.Width = 52;
            this.ckShutdown.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.ckShutdown.AutoSize = true;
            this.ckShutdown.Location = new Point(11, 359);
            this.ckShutdown.Name = "ckShutdown";
            this.ckShutdown.Size = new Size(140, 17);
            this.ckShutdown.TabIndex = 5;
            this.ckShutdown.Text = "Shut down after finished";
            this.ckShutdown.UseVisualStyleBackColor = true;
            this.ckShutdown.CheckedChanged += new EventHandler(this.ckShutdown_CheckedChanged);
            this.ckUninstall.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.ckUninstall.AutoSize = true;
            this.ckUninstall.Checked = true;
            this.ckUninstall.CheckState = CheckState.Checked;
            this.ckUninstall.Location = new Point(11, 339);
            this.ckUninstall.Name = "ckUninstall";
            this.ckUninstall.Size = new Size(194, 17);
            this.ckUninstall.TabIndex = 4;
            this.ckUninstall.Text = "Force uninstall installed applications";
            this.ckUninstall.UseVisualStyleBackColor = true;
            this.ckUninstall.CheckedChanged += new EventHandler(this.ckUninstall_CheckedChanged);
            this.imageList2.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageList2.ImageStream");
            this.imageList2.TransparentColor = Color.Transparent;
            //this.imageList2.Images.SetKeyName(0, "feature.alarm.png");
            //this.imageList2.Images.SetKeyName(1, "next.png");
            //this.imageList2.Images.SetKeyName(2, "check.png");
            //this.imageList2.Images.SetKeyName(3, "cancel.png");
            this.tabControl1.Controls.Add((Control)this.tabPage1);
            this.tabControl1.Controls.Add((Control)this.tabPage2);
            this.tabControl1.Cursor = Cursors.Default;
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(640, 413);
            this.tabControl1.TabIndex = 8;
            this.tabPage1.Controls.Add((Control)this.btnOpenFolder);
            this.tabPage1.Controls.Add((Control)this.btnAddFile);
            this.tabPage1.Controls.Add((Control)this.ckUninstall);
            this.tabPage1.Controls.Add((Control)this.btnRemove);
            this.tabPage1.Controls.Add((Control)this.ckShutdown);
            this.tabPage1.Controls.Add((Control)this.dgvFiles);
            this.tabPage1.Controls.Add((Control)this.linkLabel1);
            this.tabPage1.Controls.Add((Control)this.btnDeploy);
            this.tabPage1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tabPage1.Location = new Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(632, 384);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Application Deployment";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new EventHandler(this.tabPage1_Click);
            this.tabPage2.Controls.Add((Control)this.groupBox2);
            this.tabPage2.Controls.Add((Control)this.groupBox1);
            this.tabPage2.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tabPage2.Location = new Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(632, 384);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "XAP Tools";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.groupBox2.Controls.Add((Control)this.txtDirectAppID);
            this.groupBox2.Controls.Add((Control)this.txtDirectResult);
            this.groupBox2.Controls.Add((Control)this.btnGetLink);
            this.groupBox2.Controls.Add((Control)this.label2);
            this.groupBox2.Location = new Point(8, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(604, 179);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Get XAP direct link from Marketplace";
            this.txtDirectAppID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtDirectAppID.Location = new Point(103, 21);
            this.txtDirectAppID.Mask = "&&&&&&&&-&&&&-&&&&-&&&&-&&&&&&&&&&&&";
            this.txtDirectAppID.Name = "txtDirectAppID";
            this.txtDirectAppID.Size = new Size(482, 22);
            this.txtDirectAppID.TabIndex = 3;
            this.txtDirectResult.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtDirectResult.Location = new Point(8, 92);
            this.txtDirectResult.Multiline = true;
            this.txtDirectResult.Name = "txtDirectResult";
            this.txtDirectResult.ReadOnly = true;
            this.txtDirectResult.Size = new Size(589, 72);
            this.txtDirectResult.TabIndex = 3;
            this.btnGetLink.Anchor = AnchorStyles.Top;
            this.btnGetLink.Location = new Point(247, 50);
            this.btnGetLink.Name = "btnGetLink";
            this.btnGetLink.Size = new Size(111, 32);
            this.btnGetLink.TabIndex = 2;
            this.btnGetLink.Text = "Get link";
            this.btnGetLink.UseVisualStyleBackColor = true;
            this.btnGetLink.Click += new EventHandler(this.btnGetLink_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new Size(91, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Application ID";
            this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.groupBox1.Controls.Add((Control)this.txtUninstallAppID);
            this.groupBox1.Controls.Add((Control)this.btnUninstall);
            this.groupBox1.Controls.Add((Control)this.label1);
            this.groupBox1.Location = new Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(604, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Uninstall app";
            this.txtUninstallAppID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtUninstallAppID.Location = new Point(103, 21);
            this.txtUninstallAppID.Mask = "&&&&&&&&-&&&&-&&&&-&&&&-&&&&&&&&&&&&";
            this.txtUninstallAppID.Name = "txtUninstallAppID";
            this.txtUninstallAppID.Size = new Size(482, 22);
            this.txtUninstallAppID.TabIndex = 3;
            this.btnUninstall.Anchor = AnchorStyles.Top;
            this.btnUninstall.Location = new Point(247, 52);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new Size(111, 32);
            this.btnUninstall.TabIndex = 2;
            this.btnUninstall.Text = "Uninstall";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new EventHandler(this.btnUninstall_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(91, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Application ID";
            this.folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog.ShowNewFolderButton = false;
            this.btnOpenFolder.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.btnOpenFolder.ImageIndex = 2;
            this.btnOpenFolder.ImageList = this.imageList1;
            this.btnOpenFolder.Location = new Point(143, 6);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new Size(166, 39);
            this.btnOpenFolder.TabIndex = 0;
            this.btnOpenFolder.Text = "Import from folder";
            this.btnOpenFolder.TextAlign = ContentAlignment.MiddleRight;
            this.btnOpenFolder.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new EventHandler(this.btnOpenFolder_Click);
            this.btnAddFile.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.btnAddFile.ImageIndex = 0;
            this.btnAddFile.ImageList = this.imageList1;
            this.btnAddFile.Location = new Point(4, 6);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new Size(133, 39);
            this.btnAddFile.TabIndex = 0;
            this.btnAddFile.Text = "Add XAP files";
            this.btnAddFile.TextAlign = ContentAlignment.MiddleRight;
            this.btnAddFile.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new EventHandler(this.btnAddFile_Click);
            this.btnRemove.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.btnRemove.ImageIndex = 1;
            this.btnRemove.ImageList = this.imageList1;
            this.btnRemove.Location = new Point(315, 6);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new Size(175, 39);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove selected";
            this.btnRemove.TextAlign = ContentAlignment.MiddleRight;
            this.btnRemove.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.ReadOnly = true;
            this.titleDataGridViewTextBoxColumn.Width = 52;
            this.versionDataGridViewTextBoxColumn.DataPropertyName = "Version";
            gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.versionDataGridViewTextBoxColumn.DefaultCellStyle = gridViewCellStyle3;
            this.versionDataGridViewTextBoxColumn.HeaderText = "Version";
            this.versionDataGridViewTextBoxColumn.Name = "versionDataGridViewTextBoxColumn";
            this.versionDataGridViewTextBoxColumn.ReadOnly = true;
            this.versionDataGridViewTextBoxColumn.Width = 67;
            this.authorDataGridViewTextBoxColumn.DataPropertyName = "Author";
            this.authorDataGridViewTextBoxColumn.HeaderText = "Author";
            this.authorDataGridViewTextBoxColumn.Name = "authorDataGridViewTextBoxColumn";
            this.authorDataGridViewTextBoxColumn.ReadOnly = true;
            this.authorDataGridViewTextBoxColumn.Width = 63;
            this.pathDataGridViewTextBoxColumn.DataPropertyName = "Path";
            this.pathDataGridViewTextBoxColumn.HeaderText = "Path";
            this.pathDataGridViewTextBoxColumn.Name = "pathDataGridViewTextBoxColumn";
            this.pathDataGridViewTextBoxColumn.ReadOnly = true;
            this.pathDataGridViewTextBoxColumn.Width = 54;
            this.xAPInfoBindingSource.DataSource = (object)typeof(XAPInfo);
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(640, 436);
            this.Controls.Add((Control)this.tabControl1);
            this.Controls.Add((Control)this.statusStrip1);
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.MinimumSize = new Size(608, 423);
            this.Name = nameof(Form1);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = nameof(Form1);
            this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((ISupportInitialize)this.dgvFiles).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((ISupportInitialize)this.xAPInfoBindingSource).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();


        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Button btnDeploy;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.CheckBox ckShutdown;
        private System.Windows.Forms.CheckBox ckUninstall;
        private System.Windows.Forms.BindingSource xAPInfoBindingSource;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripStatusLabel lblCount;
        private System.Windows.Forms.ToolStripComboBox cbTarget;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn authorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn pathDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDirectResult;
        private System.Windows.Forms.Button btnGetLink;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtDirectAppID;

        private System.Windows.Forms.MaskedTextBox txtUninstallAppID;

    }
}
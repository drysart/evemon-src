namespace EVEMon
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.tcCharacterTabs = new System.Windows.Forms.TabControl();
            this.niMinimizeIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.niAlertIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tmrAlertRefresh = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrClock = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddChar = new System.Windows.Forms.ToolStripButton();
            this.tsbRemoveChar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsdToolDrop = new System.Windows.Forms.ToolStripDropDownButton();
            this.tmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiMineralSheet = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcCharacterTabs
            // 
            this.tcCharacterTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCharacterTabs.Location = new System.Drawing.Point(0, 25);
            this.tcCharacterTabs.Name = "tcCharacterTabs";
            this.tcCharacterTabs.SelectedIndex = 0;
            this.tcCharacterTabs.Size = new System.Drawing.Size(417, 389);
            this.tcCharacterTabs.TabIndex = 0;
            // 
            // niMinimizeIcon
            // 
            this.niMinimizeIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("niMinimizeIcon.Icon")));
            this.niMinimizeIcon.Click += new System.EventHandler(this.niMinimizeIcon_Click);
            this.niMinimizeIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.niMinimizeIcon_MouseMove);
            // 
            // niAlertIcon
            // 
            this.niAlertIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("niAlertIcon.Icon")));
            this.niAlertIcon.Text = "notifyIcon1";
            this.niAlertIcon.Click += new System.EventHandler(this.niAlertIcon_Click);
            this.niAlertIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.niAlertIcon_MouseClick);
            this.niAlertIcon.BalloonTipClicked += new System.EventHandler(this.niAlertIcon_BalloonTipClicked);
            // 
            // tmrAlertRefresh
            // 
            this.tmrAlertRefresh.Tick += new System.EventHandler(this.tmrAlertRefresh_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 414);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(417, 22);
            this.statusStrip1.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(94, 17);
            this.lblStatus.Text = "Current EVE Time:";
            // 
            // tmrClock
            // 
            this.tmrClock.Enabled = true;
            this.tmrClock.Interval = 1000;
            this.tmrClock.Tick += new System.EventHandler(this.tmrClock_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddChar,
            this.tsbRemoveChar,
            this.toolStripSeparator1,
            this.tsdToolDrop});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(417, 25);
            this.toolStrip1.TabIndex = 2;
            // 
            // tsbAddChar
            // 
            this.tsbAddChar.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddChar.Image")));
            this.tsbAddChar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddChar.Name = "tsbAddChar";
            this.tsbAddChar.Size = new System.Drawing.Size(109, 22);
            this.tsbAddChar.Text = "Add Character...";
            this.tsbAddChar.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbRemoveChar
            // 
            this.tsbRemoveChar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemoveChar.Enabled = false;
            this.tsbRemoveChar.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemoveChar.Image")));
            this.tsbRemoveChar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveChar.Name = "tsbRemoveChar";
            this.tsbRemoveChar.Size = new System.Drawing.Size(23, 22);
            this.tsbRemoveChar.Text = "Remove Character";
            this.tsbRemoveChar.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsdToolDrop
            // 
            this.tsdToolDrop.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiOptions,
            this.scheduleToolStripMenuItem,
            this.tmiMineralSheet,
            this.aboutToolStripMenuItem});
            this.tsdToolDrop.Image = ((System.Drawing.Image)(resources.GetObject("tsdToolDrop.Image")));
            this.tsdToolDrop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsdToolDrop.Name = "tsdToolDrop";
            this.tsdToolDrop.Size = new System.Drawing.Size(61, 22);
            this.tsdToolDrop.Text = "Tools";
            // 
            // tmiOptions
            // 
            this.tmiOptions.Image = ((System.Drawing.Image)(resources.GetObject("tmiOptions.Image")));
            this.tmiOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tmiOptions.Name = "tmiOptions";
            this.tmiOptions.Size = new System.Drawing.Size(174, 22);
            this.tmiOptions.Text = "Options...";
            this.tmiOptions.Click += new System.EventHandler(this.tsbOptions_Click);
            // 
            // tmiMineralSheet
            // 
            this.tmiMineralSheet.Image = ((System.Drawing.Image)(resources.GetObject("tmiMineralSheet.Image")));
            this.tmiMineralSheet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tmiMineralSheet.Name = "tmiMineralSheet";
            this.tmiMineralSheet.Size = new System.Drawing.Size(174, 22);
            this.tmiMineralSheet.Text = "Mineral Worksheet";
            this.tmiMineralSheet.Click += new System.EventHandler(this.tmiMineralSheet_Click);
            // 
            // scheduleToolStripMenuItem
            // 
            this.scheduleToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("scheduleToolStripMenuItem.Image")));
            this.scheduleToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.scheduleToolStripMenuItem.Name = "scheduleToolStripMenuItem";
            this.scheduleToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.scheduleToolStripMenuItem.Text = "Schedule";
            this.scheduleToolStripMenuItem.Click += new System.EventHandler(this.tsbSchedule_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 436);
            this.Controls.Add(this.tcCharacterTabs);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(425, 350);
            this.Name = "MainWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcCharacterTabs;
        private System.Windows.Forms.NotifyIcon niMinimizeIcon;
        private System.Windows.Forms.NotifyIcon niAlertIcon;
        private System.Windows.Forms.Timer tmrAlertRefresh;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Timer tmrClock;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddChar;
        private System.Windows.Forms.ToolStripButton tsbRemoveChar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tsdToolDrop;
        private System.Windows.Forms.ToolStripMenuItem tmiOptions;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tmiMineralSheet;
        private System.Windows.Forms.ToolStripMenuItem scheduleToolStripMenuItem;
    }
}
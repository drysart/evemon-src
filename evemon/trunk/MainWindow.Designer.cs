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
            this.trayIconToolStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.niAlertIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tmrAlertRefresh = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrClock = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddChar = new System.Windows.Forms.ToolStripButton();
            this.tsbRemoveChar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMineralSheet = new System.Windows.Forms.ToolStripButton();
            this.tsbSchedule = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOptions = new System.Windows.Forms.ToolStripButton();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.tmrServerStatus = new System.Windows.Forms.Timer(this.components);
            this.trayIconToolStrip.SuspendLayout();
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
            this.tcCharacterTabs.SelectedIndexChanged += new System.EventHandler(this.tcCharacterTabs_SelectedIndexChanged);
            // 
            // niMinimizeIcon
            // 
            this.niMinimizeIcon.ContextMenuStrip = this.trayIconToolStrip;
            this.niMinimizeIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("niMinimizeIcon.Icon")));
            this.niMinimizeIcon.Click += new System.EventHandler(this.niMinimizeIcon_Click);
            this.niMinimizeIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.niMinimizeIcon_MouseMove);
            // 
            // trayIconToolStrip
            // 
            this.trayIconToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.trayIconToolStrip.Name = "trayIconToolStrip";
            this.trayIconToolStrip.Size = new System.Drawing.Size(153, 70);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
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
            this.tsbMineralSheet,
            this.tsbSchedule,
            this.toolStripSeparator2,
            this.tsbOptions,
            this.tsbAbout});
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
            // tsbMineralSheet
            // 
            this.tsbMineralSheet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMineralSheet.Image = ((System.Drawing.Image)(resources.GetObject("tsbMineralSheet.Image")));
            this.tsbMineralSheet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMineralSheet.Name = "tsbMineralSheet";
            this.tsbMineralSheet.Size = new System.Drawing.Size(23, 22);
            this.tsbMineralSheet.Text = "Mineral Worksheet";
            this.tsbMineralSheet.Click += new System.EventHandler(this.tsbMineralSheet_Click);
            // 
            // tsbSchedule
            // 
            this.tsbSchedule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSchedule.Image = ((System.Drawing.Image)(resources.GetObject("tsbSchedule.Image")));
            this.tsbSchedule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSchedule.Name = "tsbSchedule";
            this.tsbSchedule.Size = new System.Drawing.Size(23, 22);
            this.tsbSchedule.Text = "Schedule";
            this.tsbSchedule.Visible = false;
            this.tsbSchedule.Click += new System.EventHandler(this.tsbSchedule_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbOptions
            // 
            this.tsbOptions.Image = ((System.Drawing.Image)(resources.GetObject("tsbOptions.Image")));
            this.tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOptions.Name = "tsbOptions";
            this.tsbOptions.Size = new System.Drawing.Size(76, 22);
            this.tsbOptions.Text = "Options...";
            this.tsbOptions.Click += new System.EventHandler(this.tsbOptions_Click);
            // 
            // tsbAbout
            // 
            this.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbAbout.Image")));
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(23, 22);
            this.tsbAbout.Text = "About...";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // tmrServerStatus
            // 
            this.tmrServerStatus.Enabled = true;
            this.tmrServerStatus.Interval = 1000;
            this.tmrServerStatus.Tick += new System.EventHandler(this.tmrServerStatus_Tick);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.trayIconToolStrip.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripButton tsbOptions;
        private System.Windows.Forms.ToolStripButton tsbSchedule;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.ToolStripButton tsbMineralSheet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer tmrServerStatus;
        private System.Windows.Forms.ContextMenuStrip trayIconToolStrip;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}

namespace EVEMon.Schedule
{
    partial class ScheduleEditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleEditorWindow));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddEntry = new System.Windows.Forms.ToolStripButton();
            this.tsbDeleteEntry = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblEntryDescription = new System.Windows.Forms.Label();
            this.lbEntries = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudDay = new System.Windows.Forms.NumericUpDown();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.nudYear = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbViewType = new System.Windows.Forms.ComboBox();
            this.ssStatusBar = new System.Windows.Forms.StatusStrip();
            this.tsslStatusBarText = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.calControl = new EVEMon.Schedule.CalendarControl();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).BeginInit();
            this.ssStatusBar.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddEntry,
            this.tsbDeleteEntry});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(712, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAddEntry
            // 
            this.tsbAddEntry.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddEntry.Image")));
            this.tsbAddEntry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddEntry.Name = "tsbAddEntry";
            this.tsbAddEntry.Size = new System.Drawing.Size(133, 22);
            this.tsbAddEntry.Text = "Add Schedule Entry...";
            this.tsbAddEntry.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbDeleteEntry
            // 
            this.tsbDeleteEntry.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeleteEntry.Image")));
            this.tsbDeleteEntry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteEntry.Name = "tsbDeleteEntry";
            this.tsbDeleteEntry.Size = new System.Drawing.Size(87, 22);
            this.tsbDeleteEntry.Text = "Delete Entry";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.calControl);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(712, 441);
            this.splitContainer1.SplitterDistance = 175;
            this.splitContainer1.TabIndex = 2;
            // 
            // lblEntryDescription
            // 
            this.lblEntryDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEntryDescription.Location = new System.Drawing.Point(0, 0);
            this.lblEntryDescription.Name = "lblEntryDescription";
            this.lblEntryDescription.Size = new System.Drawing.Size(171, 136);
            this.lblEntryDescription.TabIndex = 2;
            this.lblEntryDescription.Text = "label3";
            // 
            // lbEntries
            // 
            this.lbEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbEntries.FormattingEnabled = true;
            this.lbEntries.IntegralHeight = false;
            this.lbEntries.Location = new System.Drawing.Point(3, 20);
            this.lbEntries.Name = "lbEntries";
            this.lbEntries.Size = new System.Drawing.Size(168, 274);
            this.lbEntries.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Schedule Entries:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nudDay);
            this.panel1.Controls.Add(this.cbMonth);
            this.panel1.Controls.Add(this.nudYear);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbViewType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 27);
            this.panel1.TabIndex = 1;
            // 
            // nudDay
            // 
            this.nudDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDay.Location = new System.Drawing.Point(338, 3);
            this.nudDay.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nudDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDay.Name = "nudDay";
            this.nudDay.Size = new System.Drawing.Size(38, 21);
            this.nudDay.TabIndex = 4;
            this.nudDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbMonth
            // 
            this.cbMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Location = new System.Drawing.Point(382, 3);
            this.cbMonth.MaxDropDownItems = 12;
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(93, 21);
            this.cbMonth.TabIndex = 3;
            // 
            // nudYear
            // 
            this.nudYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudYear.Location = new System.Drawing.Point(481, 3);
            this.nudYear.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.nudYear.Minimum = new decimal(new int[] {
            2006,
            0,
            0,
            0});
            this.nudYear.Name = "nudYear";
            this.nudYear.Size = new System.Drawing.Size(49, 21);
            this.nudYear.TabIndex = 2;
            this.nudYear.Value = new decimal(new int[] {
            2006,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "View Type:";
            // 
            // cbViewType
            // 
            this.cbViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbViewType.FormattingEnabled = true;
            this.cbViewType.Items.AddRange(new object[] {
            "Month",
            "Week",
            "Day"});
            this.cbViewType.Location = new System.Drawing.Point(69, 3);
            this.cbViewType.Name = "cbViewType";
            this.cbViewType.Size = new System.Drawing.Size(121, 21);
            this.cbViewType.TabIndex = 0;
            // 
            // ssStatusBar
            // 
            this.ssStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatusBarText});
            this.ssStatusBar.Location = new System.Drawing.Point(0, 466);
            this.ssStatusBar.Name = "ssStatusBar";
            this.ssStatusBar.Size = new System.Drawing.Size(712, 22);
            this.ssStatusBar.TabIndex = 3;
            this.ssStatusBar.Text = "statusStrip1";
            // 
            // tsslStatusBarText
            // 
            this.tsslStatusBarText.Name = "tsslStatusBarText";
            this.tsslStatusBarText.Size = new System.Drawing.Size(42, 17);
            this.tsslStatusBarText.Text = "Ready.";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(2, 2);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lbEntries);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1MinSize = 200;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lblEntryDescription);
            this.splitContainer2.Panel2MinSize = 100;
            this.splitContainer2.Size = new System.Drawing.Size(171, 437);
            this.splitContainer2.SplitterDistance = 297;
            this.splitContainer2.TabIndex = 3;
            // 
            // calControl
            // 
            this.calControl.CalendarType = EVEMon.Schedule.CalendarType.Month;
            this.calControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calControl.Location = new System.Drawing.Point(0, 27);
            this.calControl.Month = 4;
            this.calControl.Name = "calControl";
            this.calControl.Size = new System.Drawing.Size(533, 414);
            this.calControl.TabIndex = 0;
            this.calControl.Year = 2006;
            // 
            // ScheduleEditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(712, 488);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ssStatusBar);
            this.Controls.Add(this.toolStrip1);
            this.MinimumSize = new System.Drawing.Size(720, 522);
            this.Name = "ScheduleEditorWindow";
            this.Text = "My Schedule - EVEMon";
            this.Load += new System.EventHandler(this.ScheduleEditorWindow_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).EndInit();
            this.ssStatusBar.ResumeLayout(false);
            this.ssStatusBar.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CalendarControl calControl;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddEntry;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbEntries;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsbDeleteEntry;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbViewType;
        private System.Windows.Forms.NumericUpDown nudDay;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.NumericUpDown nudYear;
        private System.Windows.Forms.Label lblEntryDescription;
        private System.Windows.Forms.StatusStrip ssStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatusBarText;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}

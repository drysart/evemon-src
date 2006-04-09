namespace EveCharacterMonitor.SkillPlanner
{
    partial class PlannerWindow
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
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.tvSkillView = new System.Windows.Forms.TreeView();
            this.cbSkillFilter = new System.Windows.Forms.ComboBox();
            this.pnlSkillDisplay = new System.Windows.Forms.Panel();
            this.lbPlannedList = new System.Windows.Forms.ListBox();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Margin = new System.Windows.Forms.Padding(0);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.lbPlannedList);
            this.scMain.Panel1.Controls.Add(this.tvSkillView);
            this.scMain.Panel1.Controls.Add(this.cbSkillFilter);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.scMain.Panel2.Controls.Add(this.pnlSkillDisplay);
            this.scMain.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.scMain.Size = new System.Drawing.Size(575, 460);
            this.scMain.SplitterDistance = 184;
            this.scMain.TabIndex = 0;
            // 
            // tvSkillView
            // 
            this.tvSkillView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSkillView.Location = new System.Drawing.Point(12, 39);
            this.tvSkillView.Name = "tvSkillView";
            this.tvSkillView.Size = new System.Drawing.Size(169, 409);
            this.tvSkillView.TabIndex = 1;
            this.tvSkillView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSkillView_AfterSelect);
            // 
            // cbSkillFilter
            // 
            this.cbSkillFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSkillFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkillFilter.FormattingEnabled = true;
            this.cbSkillFilter.Items.AddRange(new object[] {
            "Show All Skills",
            "Show Known Skills",
            "Show Planned Skills",
            "Order Planned Skills"});
            this.cbSkillFilter.Location = new System.Drawing.Point(12, 12);
            this.cbSkillFilter.Name = "cbSkillFilter";
            this.cbSkillFilter.Size = new System.Drawing.Size(169, 21);
            this.cbSkillFilter.TabIndex = 0;
            this.cbSkillFilter.SelectedIndexChanged += new System.EventHandler(this.cbSkillFilter_SelectedIndexChanged);
            // 
            // pnlSkillDisplay
            // 
            this.pnlSkillDisplay.AutoScroll = true;
            this.pnlSkillDisplay.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlSkillDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSkillDisplay.Location = new System.Drawing.Point(3, 3);
            this.pnlSkillDisplay.Name = "pnlSkillDisplay";
            this.pnlSkillDisplay.Size = new System.Drawing.Size(381, 454);
            this.pnlSkillDisplay.TabIndex = 0;
            this.pnlSkillDisplay.ClientSizeChanged += new System.EventHandler(this.pnlSkillDisplay_ClientSizeChanged);
            // 
            // lbPlannedList
            // 
            this.lbPlannedList.FormattingEnabled = true;
            this.lbPlannedList.IntegralHeight = false;
            this.lbPlannedList.Location = new System.Drawing.Point(0, 98);
            this.lbPlannedList.Name = "lbPlannedList";
            this.lbPlannedList.Size = new System.Drawing.Size(76, 43);
            this.lbPlannedList.TabIndex = 2;
            this.lbPlannedList.Visible = false;
            // 
            // PlannerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 460);
            this.Controls.Add(this.scMain);
            this.Name = "PlannerWindow";
            this.Text = "EVE Skill Planner";
            this.Load += new System.EventHandler(this.PlannerWindow_Load);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.TreeView tvSkillView;
        private System.Windows.Forms.ComboBox cbSkillFilter;
        private System.Windows.Forms.Panel pnlSkillDisplay;
        private System.Windows.Forms.ListBox lbPlannedList;
    }
}
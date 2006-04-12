namespace EveCharacterMonitor.SkillPlanner
{
    partial class NewPlannerWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewPlannerWindow));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvSkillView = new System.Windows.Forms.TreeView();
            this.cbSkillFilter = new System.Windows.Forms.ComboBox();
            this.cmsSkillContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miPlanTo1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo2 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo3 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo4 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miCancelPlanMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.miCancelAll = new System.Windows.Forms.ToolStripMenuItem();
            this.miCancelThis = new System.Windows.Forms.ToolStripMenuItem();
            this.skillTreeDisplay1 = new EveCharacterMonitor.SkillPlanner.SkillTreeDisplay();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmsSkillContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvSkillView);
            this.splitContainer1.Panel1.Controls.Add(this.cbSkillFilter);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.skillTreeDisplay1);
            this.splitContainer1.Size = new System.Drawing.Size(622, 609);
            this.splitContainer1.SplitterDistance = 207;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvSkillView
            // 
            this.tvSkillView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSkillView.Location = new System.Drawing.Point(12, 39);
            this.tvSkillView.Name = "tvSkillView";
            this.tvSkillView.Size = new System.Drawing.Size(192, 558);
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
            "Show Planned Skills"});
            this.cbSkillFilter.Location = new System.Drawing.Point(12, 12);
            this.cbSkillFilter.Name = "cbSkillFilter";
            this.cbSkillFilter.Size = new System.Drawing.Size(192, 21);
            this.cbSkillFilter.TabIndex = 0;
            this.cbSkillFilter.SelectedIndexChanged += new System.EventHandler(this.cbSkillFilter_SelectedIndexChanged);
            // 
            // cmsSkillContext
            // 
            this.cmsSkillContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPlanTo1,
            this.miPlanTo2,
            this.miPlanTo3,
            this.miPlanTo4,
            this.miPlanTo5,
            this.toolStripMenuItem1,
            this.miCancelPlanMenu});
            this.cmsSkillContext.Name = "cmsSkillContext";
            this.cmsSkillContext.Size = new System.Drawing.Size(181, 164);
            // 
            // miPlanTo1
            // 
            this.miPlanTo1.Name = "miPlanTo1";
            this.miPlanTo1.Size = new System.Drawing.Size(180, 22);
            this.miPlanTo1.Text = "Plan to Level I";
            this.miPlanTo1.Click += new System.EventHandler(this.miPlanTo1_Click);
            // 
            // miPlanTo2
            // 
            this.miPlanTo2.Name = "miPlanTo2";
            this.miPlanTo2.Size = new System.Drawing.Size(180, 22);
            this.miPlanTo2.Text = "Plan to Level II";
            this.miPlanTo2.Click += new System.EventHandler(this.miPlanTo2_Click);
            // 
            // miPlanTo3
            // 
            this.miPlanTo3.Name = "miPlanTo3";
            this.miPlanTo3.Size = new System.Drawing.Size(180, 22);
            this.miPlanTo3.Text = "Plan to Level III";
            this.miPlanTo3.Click += new System.EventHandler(this.miPlanTo3_Click);
            // 
            // miPlanTo4
            // 
            this.miPlanTo4.Name = "miPlanTo4";
            this.miPlanTo4.Size = new System.Drawing.Size(180, 22);
            this.miPlanTo4.Text = "Plan to Level IV";
            this.miPlanTo4.Click += new System.EventHandler(this.miPlanTo4_Click);
            // 
            // miPlanTo5
            // 
            this.miPlanTo5.Name = "miPlanTo5";
            this.miPlanTo5.Size = new System.Drawing.Size(180, 22);
            this.miPlanTo5.Text = "Plan to Level V";
            this.miPlanTo5.Click += new System.EventHandler(this.miPlanTo5_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // miCancelPlanMenu
            // 
            this.miCancelPlanMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCancelAll,
            this.miCancelThis});
            this.miCancelPlanMenu.Name = "miCancelPlanMenu";
            this.miCancelPlanMenu.Size = new System.Drawing.Size(180, 22);
            this.miCancelPlanMenu.Text = "Cancel Current Plan";
            // 
            // miCancelAll
            // 
            this.miCancelAll.Name = "miCancelAll";
            this.miCancelAll.Size = new System.Drawing.Size(226, 22);
            this.miCancelAll.Text = "Cancel Plan and Prerequisites";
            // 
            // miCancelThis
            // 
            this.miCancelThis.Name = "miCancelThis";
            this.miCancelThis.Size = new System.Drawing.Size(226, 22);
            this.miCancelThis.Text = "Cancel Plan for This Skill Only";
            // 
            // skillTreeDisplay1
            // 
            this.skillTreeDisplay1.AutoScroll = true;
            this.skillTreeDisplay1.AutoScrollMinSize = new System.Drawing.Size(1000, 2000);
            this.skillTreeDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillTreeDisplay1.Location = new System.Drawing.Point(0, 0);
            this.skillTreeDisplay1.Name = "skillTreeDisplay1";
            this.skillTreeDisplay1.Plan = null;
            this.skillTreeDisplay1.RootSkill = null;
            this.skillTreeDisplay1.Size = new System.Drawing.Size(411, 609);
            this.skillTreeDisplay1.TabIndex = 0;
            this.skillTreeDisplay1.SkillClicked += new EveCharacterMonitor.SkillPlanner.SkillClickedHandler(this.skillTreeDisplay1_SkillClicked);
            this.skillTreeDisplay1.Load += new System.EventHandler(this.skillTreeDisplay1_Load);
            // 
            // NewPlannerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 609);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewPlannerWindow";
            this.Text = "EVEMon Skill Planner";
            this.Load += new System.EventHandler(this.NewPlannerWindow_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.cmsSkillContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private SkillTreeDisplay skillTreeDisplay1;
        private System.Windows.Forms.ComboBox cbSkillFilter;
        private System.Windows.Forms.TreeView tvSkillView;
        private System.Windows.Forms.ContextMenuStrip cmsSkillContext;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo1;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo2;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo3;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo4;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo5;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miCancelPlanMenu;
        private System.Windows.Forms.ToolStripMenuItem miCancelAll;
        private System.Windows.Forms.ToolStripMenuItem miCancelThis;
    }
}
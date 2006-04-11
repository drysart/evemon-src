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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.skillTreeDisplay1 = new EveCharacterMonitor.SkillPlanner.SkillTreeDisplay();
            this.cbSkillFilter = new System.Windows.Forms.ComboBox();
            this.tvSkillView = new System.Windows.Forms.TreeView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            // skillTreeDisplay1
            // 
            this.skillTreeDisplay1.AutoScroll = true;
            this.skillTreeDisplay1.AutoScrollMinSize = new System.Drawing.Size(1000, 2000);
            this.skillTreeDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillTreeDisplay1.Location = new System.Drawing.Point(0, 0);
            this.skillTreeDisplay1.Name = "skillTreeDisplay1";
            this.skillTreeDisplay1.RootSkill = null;
            this.skillTreeDisplay1.Size = new System.Drawing.Size(411, 609);
            this.skillTreeDisplay1.TabIndex = 0;
            this.skillTreeDisplay1.Load += new System.EventHandler(this.skillTreeDisplay1_Load);
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
            // NewPlannerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 609);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "NewPlannerWindow";
            this.Text = "NewPlannerWindow";
            this.Load += new System.EventHandler(this.NewPlannerWindow_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private SkillTreeDisplay skillTreeDisplay1;
        private System.Windows.Forms.ComboBox cbSkillFilter;
        private System.Windows.Forms.TreeView tvSkillView;
    }
}
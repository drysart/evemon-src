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
            this.skillTreeDisplay1 = new EveCharacterMonitor.SkillPlanner.SkillTreeDisplay();
            this.pnlPlanControl = new System.Windows.Forms.Panel();
            this.lblPlanDescription = new System.Windows.Forms.Label();
            this.btnCancelPlan = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPlanTo1 = new System.Windows.Forms.Button();
            this.btnPlanTo2 = new System.Windows.Forms.Button();
            this.btnPlanTo3 = new System.Windows.Forms.Button();
            this.btnPlanTo4 = new System.Windows.Forms.Button();
            this.btnPlanTo5 = new System.Windows.Forms.Button();
            this.lblLevel5Time = new System.Windows.Forms.Label();
            this.lblLevel4Time = new System.Windows.Forms.Label();
            this.lblLevel3Time = new System.Windows.Forms.Label();
            this.lblLevel2Time = new System.Windows.Forms.Label();
            this.lblLevel1Time = new System.Windows.Forms.Label();
            this.lblSkillName = new System.Windows.Forms.Label();
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
            this.tmrSkillTick = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.slblStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.planEditor = new EveCharacterMonitor.SkillPlanner.PlanOrderEditorControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlPlanControl.SuspendLayout();
            this.cmsSkillContext.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.pnlPlanControl);
            this.splitContainer1.Size = new System.Drawing.Size(751, 587);
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
            this.tvSkillView.Size = new System.Drawing.Size(192, 536);
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
            "Show Available, Untrained Skills",
            "View Plan"});
            this.cbSkillFilter.Location = new System.Drawing.Point(12, 12);
            this.cbSkillFilter.Name = "cbSkillFilter";
            this.cbSkillFilter.Size = new System.Drawing.Size(192, 21);
            this.cbSkillFilter.TabIndex = 0;
            this.cbSkillFilter.SelectedIndexChanged += new System.EventHandler(this.cbSkillFilter_SelectedIndexChanged);
            // 
            // skillTreeDisplay1
            // 
            this.skillTreeDisplay1.AutoScroll = true;
            this.skillTreeDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillTreeDisplay1.Location = new System.Drawing.Point(0, 92);
            this.skillTreeDisplay1.Name = "skillTreeDisplay1";
            this.skillTreeDisplay1.Plan = null;
            this.skillTreeDisplay1.RootSkill = null;
            this.skillTreeDisplay1.Size = new System.Drawing.Size(540, 495);
            this.skillTreeDisplay1.TabIndex = 0;
            this.skillTreeDisplay1.SkillClicked += new EveCharacterMonitor.SkillPlanner.SkillClickedHandler(this.skillTreeDisplay1_SkillClicked);
            this.skillTreeDisplay1.Load += new System.EventHandler(this.skillTreeDisplay1_Load);
            // 
            // pnlPlanControl
            // 
            this.pnlPlanControl.Controls.Add(this.lblPlanDescription);
            this.pnlPlanControl.Controls.Add(this.btnCancelPlan);
            this.pnlPlanControl.Controls.Add(this.label5);
            this.pnlPlanControl.Controls.Add(this.btnPlanTo1);
            this.pnlPlanControl.Controls.Add(this.btnPlanTo2);
            this.pnlPlanControl.Controls.Add(this.btnPlanTo3);
            this.pnlPlanControl.Controls.Add(this.btnPlanTo4);
            this.pnlPlanControl.Controls.Add(this.btnPlanTo5);
            this.pnlPlanControl.Controls.Add(this.lblLevel5Time);
            this.pnlPlanControl.Controls.Add(this.lblLevel4Time);
            this.pnlPlanControl.Controls.Add(this.lblLevel3Time);
            this.pnlPlanControl.Controls.Add(this.lblLevel2Time);
            this.pnlPlanControl.Controls.Add(this.lblLevel1Time);
            this.pnlPlanControl.Controls.Add(this.lblSkillName);
            this.pnlPlanControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPlanControl.Location = new System.Drawing.Point(0, 0);
            this.pnlPlanControl.Name = "pnlPlanControl";
            this.pnlPlanControl.Size = new System.Drawing.Size(540, 92);
            this.pnlPlanControl.TabIndex = 1;
            this.pnlPlanControl.Visible = false;
            // 
            // lblPlanDescription
            // 
            this.lblPlanDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlanDescription.Location = new System.Drawing.Point(372, 12);
            this.lblPlanDescription.Name = "lblPlanDescription";
            this.lblPlanDescription.Size = new System.Drawing.Size(165, 16);
            this.lblPlanDescription.TabIndex = 13;
            this.lblPlanDescription.Text = "Not currently planned.";
            this.lblPlanDescription.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // btnCancelPlan
            // 
            this.btnCancelPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelPlan.Location = new System.Drawing.Point(456, 62);
            this.btnCancelPlan.Name = "btnCancelPlan";
            this.btnCancelPlan.Size = new System.Drawing.Size(75, 23);
            this.btnCancelPlan.TabIndex = 12;
            this.btnCancelPlan.Text = "Cancel Plan";
            this.btnCancelPlan.UseVisualStyleBackColor = true;
            this.btnCancelPlan.Click += new System.EventHandler(this.btnCancelPlan_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(315, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Plan To:";
            // 
            // btnPlanTo1
            // 
            this.btnPlanTo1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlanTo1.Location = new System.Drawing.Point(367, 33);
            this.btnPlanTo1.Name = "btnPlanTo1";
            this.btnPlanTo1.Size = new System.Drawing.Size(28, 23);
            this.btnPlanTo1.TabIndex = 10;
            this.btnPlanTo1.Text = "I";
            this.btnPlanTo1.UseVisualStyleBackColor = true;
            this.btnPlanTo1.Click += new System.EventHandler(this.btnPlanTo1_Click);
            // 
            // btnPlanTo2
            // 
            this.btnPlanTo2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlanTo2.Location = new System.Drawing.Point(401, 33);
            this.btnPlanTo2.Name = "btnPlanTo2";
            this.btnPlanTo2.Size = new System.Drawing.Size(28, 23);
            this.btnPlanTo2.TabIndex = 9;
            this.btnPlanTo2.Text = "II";
            this.btnPlanTo2.UseVisualStyleBackColor = true;
            this.btnPlanTo2.Click += new System.EventHandler(this.btnPlanTo2_Click);
            // 
            // btnPlanTo3
            // 
            this.btnPlanTo3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlanTo3.Location = new System.Drawing.Point(435, 33);
            this.btnPlanTo3.Name = "btnPlanTo3";
            this.btnPlanTo3.Size = new System.Drawing.Size(28, 23);
            this.btnPlanTo3.TabIndex = 8;
            this.btnPlanTo3.Text = "III";
            this.btnPlanTo3.UseVisualStyleBackColor = true;
            this.btnPlanTo3.Click += new System.EventHandler(this.btnPlanTo3_Click);
            // 
            // btnPlanTo4
            // 
            this.btnPlanTo4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlanTo4.Location = new System.Drawing.Point(469, 33);
            this.btnPlanTo4.Name = "btnPlanTo4";
            this.btnPlanTo4.Size = new System.Drawing.Size(28, 23);
            this.btnPlanTo4.TabIndex = 7;
            this.btnPlanTo4.Text = "IV";
            this.btnPlanTo4.UseVisualStyleBackColor = true;
            this.btnPlanTo4.Click += new System.EventHandler(this.btnPlanTo4_Click);
            // 
            // btnPlanTo5
            // 
            this.btnPlanTo5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlanTo5.Location = new System.Drawing.Point(503, 33);
            this.btnPlanTo5.Name = "btnPlanTo5";
            this.btnPlanTo5.Size = new System.Drawing.Size(28, 23);
            this.btnPlanTo5.TabIndex = 6;
            this.btnPlanTo5.Text = "V";
            this.btnPlanTo5.UseVisualStyleBackColor = true;
            this.btnPlanTo5.Click += new System.EventHandler(this.btnPlanTo5_Click);
            // 
            // lblLevel5Time
            // 
            this.lblLevel5Time.AutoSize = true;
            this.lblLevel5Time.Location = new System.Drawing.Point(4, 72);
            this.lblLevel5Time.Name = "lblLevel5Time";
            this.lblLevel5Time.Size = new System.Drawing.Size(113, 13);
            this.lblLevel5Time.TabIndex = 5;
            this.lblLevel5Time.Text = "Level V: ..... (plus ...)";
            // 
            // lblLevel4Time
            // 
            this.lblLevel4Time.AutoSize = true;
            this.lblLevel4Time.Location = new System.Drawing.Point(4, 59);
            this.lblLevel4Time.Name = "lblLevel4Time";
            this.lblLevel4Time.Size = new System.Drawing.Size(117, 13);
            this.lblLevel4Time.TabIndex = 4;
            this.lblLevel4Time.Text = "Level IV: ..... (plus ...)";
            // 
            // lblLevel3Time
            // 
            this.lblLevel3Time.AutoSize = true;
            this.lblLevel3Time.Location = new System.Drawing.Point(4, 46);
            this.lblLevel3Time.Name = "lblLevel3Time";
            this.lblLevel3Time.Size = new System.Drawing.Size(119, 13);
            this.lblLevel3Time.TabIndex = 3;
            this.lblLevel3Time.Text = "Level III: ..... (plus ...)";
            // 
            // lblLevel2Time
            // 
            this.lblLevel2Time.AutoSize = true;
            this.lblLevel2Time.Location = new System.Drawing.Point(4, 33);
            this.lblLevel2Time.Name = "lblLevel2Time";
            this.lblLevel2Time.Size = new System.Drawing.Size(115, 13);
            this.lblLevel2Time.TabIndex = 2;
            this.lblLevel2Time.Text = "Level II: ..... (plus ...)";
            // 
            // lblLevel1Time
            // 
            this.lblLevel1Time.AutoSize = true;
            this.lblLevel1Time.Location = new System.Drawing.Point(4, 20);
            this.lblLevel1Time.Name = "lblLevel1Time";
            this.lblLevel1Time.Size = new System.Drawing.Size(111, 13);
            this.lblLevel1Time.TabIndex = 1;
            this.lblLevel1Time.Text = "Level I: ..... (plus ...)";
            // 
            // lblSkillName
            // 
            this.lblSkillName.AutoSize = true;
            this.lblSkillName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkillName.Location = new System.Drawing.Point(4, 4);
            this.lblSkillName.Name = "lblSkillName";
            this.lblSkillName.Size = new System.Drawing.Size(65, 13);
            this.lblSkillName.TabIndex = 0;
            this.lblSkillName.Text = "Skill Name";
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
            this.cmsSkillContext.Size = new System.Drawing.Size(181, 142);
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
            this.miCancelAll.Click += new System.EventHandler(this.miCancelAll_Click);
            // 
            // miCancelThis
            // 
            this.miCancelThis.Name = "miCancelThis";
            this.miCancelThis.Size = new System.Drawing.Size(226, 22);
            this.miCancelThis.Text = "Cancel Plan for This Skill Only";
            this.miCancelThis.Click += new System.EventHandler(this.miCancelThis_Click);
            // 
            // tmrSkillTick
            // 
            this.tmrSkillTick.Enabled = true;
            this.tmrSkillTick.Interval = 1000;
            this.tmrSkillTick.Tick += new System.EventHandler(this.tmrSkillTick_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slblStatusText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 587);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(751, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // slblStatusText
            // 
            this.slblStatusText.Name = "slblStatusText";
            this.slblStatusText.Size = new System.Drawing.Size(79, 17);
            this.slblStatusText.Text = "0 Skills Planned";
            // 
            // planEditor
            // 
            this.planEditor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.planEditor.Location = new System.Drawing.Point(74, 81);
            this.planEditor.Name = "planEditor";
            this.planEditor.Plan = null;
            this.planEditor.Size = new System.Drawing.Size(176, 150);
            this.planEditor.TabIndex = 2;
            this.planEditor.Visible = false;
            // 
            // NewPlannerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 609);
            this.Controls.Add(this.planEditor);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(759, 350);
            this.Name = "NewPlannerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EVEMon Skill Planner";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewPlannerWindow_FormClosed);
            this.Load += new System.EventHandler(this.NewPlannerWindow_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.pnlPlanControl.ResumeLayout(false);
            this.pnlPlanControl.PerformLayout();
            this.cmsSkillContext.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Panel pnlPlanControl;
        private System.Windows.Forms.Button btnCancelPlan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnPlanTo1;
        private System.Windows.Forms.Button btnPlanTo2;
        private System.Windows.Forms.Button btnPlanTo3;
        private System.Windows.Forms.Button btnPlanTo4;
        private System.Windows.Forms.Button btnPlanTo5;
        private System.Windows.Forms.Label lblLevel5Time;
        private System.Windows.Forms.Label lblLevel4Time;
        private System.Windows.Forms.Label lblLevel3Time;
        private System.Windows.Forms.Label lblLevel2Time;
        private System.Windows.Forms.Label lblLevel1Time;
        private System.Windows.Forms.Label lblSkillName;
        private System.Windows.Forms.Label lblPlanDescription;
        private System.Windows.Forms.Timer tmrSkillTick;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel slblStatusText;
        private PlanOrderEditorControl planEditor;
    }
}
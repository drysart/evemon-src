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
            this.lblNoResults = new System.Windows.Forms.Label();
            this.lbFilteredSkills = new System.Windows.Forms.ListBox();
            this.pbSearchImage = new System.Windows.Forms.PictureBox();
            this.tbSkillFilter = new System.Windows.Forms.TextBox();
            this.tvSkillView = new System.Windows.Forms.TreeView();
            this.cbSkillFilter = new System.Windows.Forms.ComboBox();
            this.pnlPlanControl = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPlanSelect = new System.Windows.Forms.ComboBox();
            this.lblAttributes = new System.Windows.Forms.Label();
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
            this.tslSuggestion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSaveAs = new System.Windows.Forms.ToolStripButton();
            this.tsbCopyForum = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDeletePlan = new System.Windows.Forms.ToolStripButton();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.lblSearchNote = new System.Windows.Forms.Label();
            this.planEditor = new EveCharacterMonitor.SkillPlanner.PlanOrderEditorControl();
            this.skillTreeDisplay1 = new EveCharacterMonitor.SkillPlanner.SkillTreeDisplay();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).BeginInit();
            this.pnlPlanControl.SuspendLayout();
            this.cmsSkillContext.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.lblSearchNote);
            this.splitContainer1.Panel1.Controls.Add(this.lblNoResults);
            this.splitContainer1.Panel1.Controls.Add(this.lbFilteredSkills);
            this.splitContainer1.Panel1.Controls.Add(this.pbSearchImage);
            this.splitContainer1.Panel1.Controls.Add(this.tbSkillFilter);
            this.splitContainer1.Panel1.Controls.Add(this.tvSkillView);
            this.splitContainer1.Panel1.Controls.Add(this.cbSkillFilter);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.skillTreeDisplay1);
            this.splitContainer1.Panel2.Controls.Add(this.pnlPlanControl);
            this.splitContainer1.Size = new System.Drawing.Size(751, 562);
            this.splitContainer1.SplitterDistance = 207;
            this.splitContainer1.TabIndex = 0;
            // 
            // lblNoResults
            // 
            this.lblNoResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNoResults.BackColor = System.Drawing.SystemColors.Window;
            this.lblNoResults.Location = new System.Drawing.Point(22, 72);
            this.lblNoResults.Name = "lblNoResults";
            this.lblNoResults.Size = new System.Drawing.Size(169, 46);
            this.lblNoResults.TabIndex = 20;
            this.lblNoResults.Text = "No skills match your search.";
            this.lblNoResults.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblNoResults.Visible = false;
            // 
            // lbFilteredSkills
            // 
            this.lbFilteredSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFilteredSkills.FormattingEnabled = true;
            this.lbFilteredSkills.IntegralHeight = false;
            this.lbFilteredSkills.Location = new System.Drawing.Point(34, 183);
            this.lbFilteredSkills.Name = "lbFilteredSkills";
            this.lbFilteredSkills.Size = new System.Drawing.Size(120, 96);
            this.lbFilteredSkills.TabIndex = 19;
            this.lbFilteredSkills.SelectedIndexChanged += new System.EventHandler(this.lbFilteredSkills_SelectedIndexChanged);
            // 
            // pbSearchImage
            // 
            this.pbSearchImage.Image = ((System.Drawing.Image)(resources.GetObject("pbSearchImage.Image")));
            this.pbSearchImage.InitialImage = null;
            this.pbSearchImage.Location = new System.Drawing.Point(12, 30);
            this.pbSearchImage.Name = "pbSearchImage";
            this.pbSearchImage.Size = new System.Drawing.Size(16, 21);
            this.pbSearchImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearchImage.TabIndex = 18;
            this.pbSearchImage.TabStop = false;
            // 
            // tbSkillFilter
            // 
            this.tbSkillFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSkillFilter.Location = new System.Drawing.Point(34, 30);
            this.tbSkillFilter.Name = "tbSkillFilter";
            this.tbSkillFilter.Size = new System.Drawing.Size(170, 21);
            this.tbSkillFilter.TabIndex = 2;
            this.tbSkillFilter.Enter += new System.EventHandler(this.tbSkillFilter_Enter);
            this.tbSkillFilter.Leave += new System.EventHandler(this.tbSkillFilter_Leave);
            this.tbSkillFilter.TextChanged += new System.EventHandler(this.tbSkillFilter_TextChanged);
            // 
            // tvSkillView
            // 
            this.tvSkillView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSkillView.Location = new System.Drawing.Point(12, 57);
            this.tvSkillView.Name = "tvSkillView";
            this.tvSkillView.Size = new System.Drawing.Size(192, 493);
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
            this.cbSkillFilter.Location = new System.Drawing.Point(12, 3);
            this.cbSkillFilter.Name = "cbSkillFilter";
            this.cbSkillFilter.Size = new System.Drawing.Size(192, 21);
            this.cbSkillFilter.TabIndex = 0;
            this.cbSkillFilter.SelectedIndexChanged += new System.EventHandler(this.cbSkillFilter_SelectedIndexChanged);
            // 
            // pnlPlanControl
            // 
            this.pnlPlanControl.Controls.Add(this.lblDescription);
            this.pnlPlanControl.Controls.Add(this.label1);
            this.pnlPlanControl.Controls.Add(this.cbPlanSelect);
            this.pnlPlanControl.Controls.Add(this.lblAttributes);
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
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescription.Location = new System.Drawing.Point(305, 19);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(232, 43);
            this.lblDescription.TabIndex = 17;
            this.lblDescription.Text = "label2";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(391, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Plan:";
            // 
            // cbPlanSelect
            // 
            this.cbPlanSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPlanSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlanSelect.FormattingEnabled = true;
            this.cbPlanSelect.Items.AddRange(new object[] {
            "Not Planned",
            "Level I",
            "Level II",
            "Level III",
            "Level IV",
            "Level V"});
            this.cbPlanSelect.Location = new System.Drawing.Point(428, 65);
            this.cbPlanSelect.Name = "cbPlanSelect";
            this.cbPlanSelect.Size = new System.Drawing.Size(100, 21);
            this.cbPlanSelect.TabIndex = 15;
            // 
            // lblAttributes
            // 
            this.lblAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAttributes.Location = new System.Drawing.Point(302, 4);
            this.lblAttributes.Name = "lblAttributes";
            this.lblAttributes.Size = new System.Drawing.Size(235, 13);
            this.lblAttributes.TabIndex = 14;
            this.lblAttributes.Text = "Primary: Intelligence, Secondary: Willpower";
            this.lblAttributes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.slblStatusText,
            this.tslSuggestion});
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
            // tslSuggestion
            // 
            this.tslSuggestion.Image = ((System.Drawing.Image)(resources.GetObject("tslSuggestion.Image")));
            this.tslSuggestion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tslSuggestion.IsLink = true;
            this.tslSuggestion.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.tslSuggestion.Name = "tslSuggestion";
            this.tslSuggestion.Size = new System.Drawing.Size(88, 17);
            this.tslSuggestion.Text = "Suggestion...";
            this.tslSuggestion.Visible = false;
            this.tslSuggestion.Click += new System.EventHandler(this.tslSuggestion_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSaveAs,
            this.tsbCopyForum,
            this.toolStripSeparator1,
            this.tsbDeletePlan});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(751, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSaveAs
            // 
            this.tsbSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveAs.Image")));
            this.tsbSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAs.Name = "tsbSaveAs";
            this.tsbSaveAs.Size = new System.Drawing.Size(95, 22);
            this.tsbSaveAs.Text = "Save to File...";
            this.tsbSaveAs.Click += new System.EventHandler(this.tsbSaveAs_Click);
            // 
            // tsbCopyForum
            // 
            this.tsbCopyForum.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopyForum.Image")));
            this.tsbCopyForum.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopyForum.Name = "tsbCopyForum";
            this.tsbCopyForum.Size = new System.Drawing.Size(102, 22);
            this.tsbCopyForum.Text = "Copy for Forum";
            this.tsbCopyForum.Click += new System.EventHandler(this.tsbCopyForum_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbDeletePlan
            // 
            this.tsbDeletePlan.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeletePlan.Image")));
            this.tsbDeletePlan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeletePlan.Name = "tsbDeletePlan";
            this.tsbDeletePlan.Size = new System.Drawing.Size(81, 22);
            this.tsbDeletePlan.Text = "Delete Plan";
            this.tsbDeletePlan.Click += new System.EventHandler(this.tsbDeletePlan_Click);
            // 
            // ttToolTip
            // 
            this.ttToolTip.AutoPopDelay = 5000000;
            this.ttToolTip.InitialDelay = 500;
            this.ttToolTip.ReshowDelay = 100;
            // 
            // sfdSave
            // 
            this.sfdSave.Filter = "EVEMon Plan Format (*.emp)|*.emp|XML Format (*.xml)|*.xml|Text Format (*.txt)|*.t" +
                "xt";
            this.sfdSave.Title = "Save to File";
            // 
            // lblSearchNote
            // 
            this.lblSearchNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearchNote.BackColor = System.Drawing.SystemColors.Window;
            this.lblSearchNote.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblSearchNote.Location = new System.Drawing.Point(36, 32);
            this.lblSearchNote.Name = "lblSearchNote";
            this.lblSearchNote.Size = new System.Drawing.Size(166, 17);
            this.lblSearchNote.TabIndex = 21;
            this.lblSearchNote.Text = "Search Filter";
            this.lblSearchNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSearchNote.Click += new System.EventHandler(this.lblSearchNote_Click);
            // 
            // planEditor
            // 
            this.planEditor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.planEditor.Location = new System.Drawing.Point(51, 349);
            this.planEditor.Name = "planEditor";
            this.planEditor.Plan = null;
            this.planEditor.Size = new System.Drawing.Size(176, 150);
            this.planEditor.TabIndex = 2;
            this.planEditor.Visible = false;
            // 
            // skillTreeDisplay1
            // 
            this.skillTreeDisplay1.AutoScroll = true;
            this.skillTreeDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillTreeDisplay1.Location = new System.Drawing.Point(0, 92);
            this.skillTreeDisplay1.Name = "skillTreeDisplay1";
            this.skillTreeDisplay1.Plan = null;
            this.skillTreeDisplay1.RootSkill = null;
            this.skillTreeDisplay1.Size = new System.Drawing.Size(540, 470);
            this.skillTreeDisplay1.TabIndex = 0;
            this.skillTreeDisplay1.SkillClicked += new EveCharacterMonitor.SkillPlanner.SkillClickedHandler(this.skillTreeDisplay1_SkillClicked);
            this.skillTreeDisplay1.Load += new System.EventHandler(this.skillTreeDisplay1_Load);
            // 
            // NewPlannerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 609);
            this.Controls.Add(this.planEditor);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(759, 350);
            this.Name = "NewPlannerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EVEMon Skill Planner";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewPlannerWindow_FormClosed);
            this.Shown += new System.EventHandler(this.NewPlannerWindow_Shown);
            this.Load += new System.EventHandler(this.NewPlannerWindow_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).EndInit();
            this.pnlPlanControl.ResumeLayout(false);
            this.pnlPlanControl.PerformLayout();
            this.cmsSkillContext.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private System.Windows.Forms.Label lblLevel5Time;
        private System.Windows.Forms.Label lblLevel4Time;
        private System.Windows.Forms.Label lblLevel3Time;
        private System.Windows.Forms.Label lblLevel2Time;
        private System.Windows.Forms.Label lblLevel1Time;
        private System.Windows.Forms.Label lblSkillName;
        private System.Windows.Forms.Timer tmrSkillTick;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel slblStatusText;
        private PlanOrderEditorControl planEditor;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbDeletePlan;
        private System.Windows.Forms.ToolStripStatusLabel tslSuggestion;
        private System.Windows.Forms.ToolTip ttToolTip;
        private System.Windows.Forms.ToolStripButton tsbSaveAs;
        private System.Windows.Forms.ToolStripButton tsbCopyForum;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label lblAttributes;
        private System.Windows.Forms.SaveFileDialog sfdSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPlanSelect;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.PictureBox pbSearchImage;
        private System.Windows.Forms.TextBox tbSkillFilter;
        private System.Windows.Forms.ListBox lbFilteredSkills;
        private System.Windows.Forms.Label lblNoResults;
        private System.Windows.Forms.Label lblSearchNote;
    }
}
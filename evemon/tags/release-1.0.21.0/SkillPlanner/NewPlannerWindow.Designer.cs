namespace EVEMon.SkillPlanner
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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbImplantCalculator = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDeletePlan = new System.Windows.Forms.ToolStripButton();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSkillBrowser = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.skillSelectControl1 = new EVEMon.SkillPlanner.SkillSelectControl();
            this.skillTreeDisplay1 = new EVEMon.SkillPlanner.SkillTreeDisplay();
            this.tpShipBrowser = new System.Windows.Forms.TabPage();
            this.scShipSelect = new System.Windows.Forms.SplitContainer();
            this.shipSelectControl1 = new EVEMon.SkillPlanner.ShipSelectControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShipSkillsAdd = new System.Windows.Forms.Button();
            this.lblShipTimeRequired = new System.Windows.Forms.Label();
            this.lblShipSkill3 = new System.Windows.Forms.Label();
            this.lblShipSkill2 = new System.Windows.Forms.Label();
            this.lblShipSkill1 = new System.Windows.Forms.Label();
            this.lblShipDescription = new System.Windows.Forms.Label();
            this.lbShipProperties = new System.Windows.Forms.ListBox();
            this.lblShipName = new System.Windows.Forms.Label();
            this.lblShipClass = new System.Windows.Forms.Label();
            this.pbShipImage = new System.Windows.Forms.PictureBox();
            this.tpPlanQueue = new System.Windows.Forms.TabPage();
            this.planEditor = new EVEMon.SkillPlanner.PlanOrderEditorControl();
            this.pnlPlanControl.SuspendLayout();
            this.cmsSkillContext.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpSkillBrowser.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tpShipBrowser.SuspendLayout();
            this.scShipSelect.Panel1.SuspendLayout();
            this.scShipSelect.Panel2.SuspendLayout();
            this.scShipSelect.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbShipImage)).BeginInit();
            this.tpPlanQueue.SuspendLayout();
            this.SuspendLayout();
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
            this.pnlPlanControl.Size = new System.Drawing.Size(565, 92);
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
            this.lblDescription.Size = new System.Drawing.Size(257, 43);
            this.lblDescription.TabIndex = 17;
            this.lblDescription.Text = "label2";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(416, 68);
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
            this.cbPlanSelect.Location = new System.Drawing.Point(453, 65);
            this.cbPlanSelect.Name = "cbPlanSelect";
            this.cbPlanSelect.Size = new System.Drawing.Size(100, 21);
            this.cbPlanSelect.TabIndex = 15;
            // 
            // lblAttributes
            // 
            this.lblAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAttributes.Location = new System.Drawing.Point(327, 4);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 496);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(741, 22);
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
            this.toolStripSeparator2,
            this.tsbImplantCalculator,
            this.toolStripSeparator1,
            this.tsbDeletePlan});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(741, 25);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbImplantCalculator
            // 
            this.tsbImplantCalculator.Image = ((System.Drawing.Image)(resources.GetObject("tsbImplantCalculator.Image")));
            this.tsbImplantCalculator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImplantCalculator.Name = "tsbImplantCalculator";
            this.tsbImplantCalculator.Size = new System.Drawing.Size(114, 22);
            this.tsbImplantCalculator.Text = "Implant Calculator";
            this.tsbImplantCalculator.Click += new System.EventHandler(this.tsbImplantCalculator_Click);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpSkillBrowser);
            this.tabControl1.Controls.Add(this.tpShipBrowser);
            this.tabControl1.Controls.Add(this.tpPlanQueue);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(741, 471);
            this.tabControl1.TabIndex = 4;
            // 
            // tpSkillBrowser
            // 
            this.tpSkillBrowser.Controls.Add(this.splitContainer2);
            this.tpSkillBrowser.Location = new System.Drawing.Point(4, 22);
            this.tpSkillBrowser.Name = "tpSkillBrowser";
            this.tpSkillBrowser.Padding = new System.Windows.Forms.Padding(3);
            this.tpSkillBrowser.Size = new System.Drawing.Size(733, 445);
            this.tpSkillBrowser.TabIndex = 0;
            this.tpSkillBrowser.Text = "Skill Browser";
            this.tpSkillBrowser.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.skillSelectControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.skillTreeDisplay1);
            this.splitContainer2.Panel2.Controls.Add(this.pnlPlanControl);
            this.splitContainer2.Size = new System.Drawing.Size(727, 439);
            this.splitContainer2.SplitterDistance = 158;
            this.splitContainer2.TabIndex = 0;
            // 
            // skillSelectControl1
            // 
            this.skillSelectControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillSelectControl1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.skillSelectControl1.GrandCharacterInfo = null;
            this.skillSelectControl1.Location = new System.Drawing.Point(0, 0);
            this.skillSelectControl1.Name = "skillSelectControl1";
            this.skillSelectControl1.Plan = null;
            this.skillSelectControl1.Size = new System.Drawing.Size(158, 439);
            this.skillSelectControl1.TabIndex = 0;
            this.skillSelectControl1.Load += new System.EventHandler(this.skillSelectControl1_Load);
            this.skillSelectControl1.SelectedSkillChanged += new System.EventHandler<System.EventArgs>(this.skillSelectControl1_SelectedSkillChanged);
            // 
            // skillTreeDisplay1
            // 
            this.skillTreeDisplay1.AutoScroll = true;
            this.skillTreeDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skillTreeDisplay1.Location = new System.Drawing.Point(0, 92);
            this.skillTreeDisplay1.Name = "skillTreeDisplay1";
            this.skillTreeDisplay1.Plan = null;
            this.skillTreeDisplay1.RootSkill = null;
            this.skillTreeDisplay1.Size = new System.Drawing.Size(565, 347);
            this.skillTreeDisplay1.TabIndex = 0;
            this.skillTreeDisplay1.WorksafeMode = false;
            this.skillTreeDisplay1.SkillClicked += new EVEMon.SkillPlanner.SkillClickedHandler(this.skillTreeDisplay1_SkillClicked);
            this.skillTreeDisplay1.Load += new System.EventHandler(this.skillTreeDisplay1_Load);
            // 
            // tpShipBrowser
            // 
            this.tpShipBrowser.Controls.Add(this.scShipSelect);
            this.tpShipBrowser.Location = new System.Drawing.Point(4, 22);
            this.tpShipBrowser.Name = "tpShipBrowser";
            this.tpShipBrowser.Size = new System.Drawing.Size(743, 536);
            this.tpShipBrowser.TabIndex = 2;
            this.tpShipBrowser.Text = "Ship Browser";
            this.tpShipBrowser.UseVisualStyleBackColor = true;
            // 
            // scShipSelect
            // 
            this.scShipSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scShipSelect.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scShipSelect.Location = new System.Drawing.Point(0, 0);
            this.scShipSelect.Name = "scShipSelect";
            // 
            // scShipSelect.Panel1
            // 
            this.scShipSelect.Panel1.Controls.Add(this.shipSelectControl1);
            // 
            // scShipSelect.Panel2
            // 
            this.scShipSelect.Panel2.Controls.Add(this.groupBox1);
            this.scShipSelect.Panel2.Controls.Add(this.lblShipDescription);
            this.scShipSelect.Panel2.Controls.Add(this.lbShipProperties);
            this.scShipSelect.Panel2.Controls.Add(this.lblShipName);
            this.scShipSelect.Panel2.Controls.Add(this.lblShipClass);
            this.scShipSelect.Panel2.Controls.Add(this.pbShipImage);
            this.scShipSelect.Size = new System.Drawing.Size(743, 536);
            this.scShipSelect.SplitterDistance = 193;
            this.scShipSelect.TabIndex = 0;
            // 
            // shipSelectControl1
            // 
            this.shipSelectControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.shipSelectControl1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shipSelectControl1.Location = new System.Drawing.Point(3, 3);
            this.shipSelectControl1.Name = "shipSelectControl1";
            this.shipSelectControl1.Size = new System.Drawing.Size(187, 530);
            this.shipSelectControl1.TabIndex = 0;
            this.shipSelectControl1.Load += new System.EventHandler(this.shipSelectControl1_Load);
            this.shipSelectControl1.SelectedShipChanged += new System.EventHandler<System.EventArgs>(this.shipSelectControl1_SelectedShipChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnShipSkillsAdd);
            this.groupBox1.Controls.Add(this.lblShipTimeRequired);
            this.groupBox1.Controls.Add(this.lblShipSkill3);
            this.groupBox1.Controls.Add(this.lblShipSkill2);
            this.groupBox1.Controls.Add(this.lblShipSkill1);
            this.groupBox1.Location = new System.Drawing.Point(287, 422);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 111);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Required Skills";
            // 
            // btnShipSkillsAdd
            // 
            this.btnShipSkillsAdd.Location = new System.Drawing.Point(121, 82);
            this.btnShipSkillsAdd.Name = "btnShipSkillsAdd";
            this.btnShipSkillsAdd.Size = new System.Drawing.Size(129, 23);
            this.btnShipSkillsAdd.TabIndex = 4;
            this.btnShipSkillsAdd.Text = "Add Skills to Plan";
            this.btnShipSkillsAdd.UseVisualStyleBackColor = true;
            this.btnShipSkillsAdd.Click += new System.EventHandler(this.btnShipSkillsAdd_Click);
            // 
            // lblShipTimeRequired
            // 
            this.lblShipTimeRequired.AutoSize = true;
            this.lblShipTimeRequired.Location = new System.Drawing.Point(6, 65);
            this.lblShipTimeRequired.Name = "lblShipTimeRequired";
            this.lblShipTimeRequired.Size = new System.Drawing.Size(79, 13);
            this.lblShipTimeRequired.TabIndex = 3;
            this.lblShipTimeRequired.Text = "Time Required:";
            // 
            // lblShipSkill3
            // 
            this.lblShipSkill3.AutoSize = true;
            this.lblShipSkill3.Location = new System.Drawing.Point(6, 43);
            this.lblShipSkill3.Name = "lblShipSkill3";
            this.lblShipSkill3.Size = new System.Drawing.Size(35, 13);
            this.lblShipSkill3.TabIndex = 2;
            this.lblShipSkill3.Text = "label4";
            // 
            // lblShipSkill2
            // 
            this.lblShipSkill2.AutoSize = true;
            this.lblShipSkill2.Location = new System.Drawing.Point(6, 30);
            this.lblShipSkill2.Name = "lblShipSkill2";
            this.lblShipSkill2.Size = new System.Drawing.Size(35, 13);
            this.lblShipSkill2.TabIndex = 1;
            this.lblShipSkill2.Text = "label3";
            // 
            // lblShipSkill1
            // 
            this.lblShipSkill1.AutoSize = true;
            this.lblShipSkill1.Location = new System.Drawing.Point(6, 17);
            this.lblShipSkill1.Name = "lblShipSkill1";
            this.lblShipSkill1.Size = new System.Drawing.Size(35, 13);
            this.lblShipSkill1.TabIndex = 0;
            this.lblShipSkill1.Text = "label2";
            // 
            // lblShipDescription
            // 
            this.lblShipDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShipDescription.Location = new System.Drawing.Point(287, 262);
            this.lblShipDescription.Name = "lblShipDescription";
            this.lblShipDescription.Size = new System.Drawing.Size(256, 157);
            this.lblShipDescription.TabIndex = 4;
            this.lblShipDescription.Text = "label2";
            // 
            // lbShipProperties
            // 
            this.lbShipProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbShipProperties.FormattingEnabled = true;
            this.lbShipProperties.IntegralHeight = false;
            this.lbShipProperties.Location = new System.Drawing.Point(3, 37);
            this.lbShipProperties.Name = "lbShipProperties";
            this.lbShipProperties.Size = new System.Drawing.Size(278, 496);
            this.lbShipProperties.TabIndex = 3;
            // 
            // lblShipName
            // 
            this.lblShipName.AutoSize = true;
            this.lblShipName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShipName.Location = new System.Drawing.Point(3, 16);
            this.lblShipName.Name = "lblShipName";
            this.lblShipName.Size = new System.Drawing.Size(55, 18);
            this.lblShipName.TabIndex = 2;
            this.lblShipName.Text = "Raven";
            // 
            // lblShipClass
            // 
            this.lblShipClass.AutoSize = true;
            this.lblShipClass.Location = new System.Drawing.Point(3, 3);
            this.lblShipClass.Name = "lblShipClass";
            this.lblShipClass.Size = new System.Drawing.Size(106, 13);
            this.lblShipClass.TabIndex = 1;
            this.lblShipClass.Text = "Battleships > Caldari";
            // 
            // pbShipImage
            // 
            this.pbShipImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbShipImage.Location = new System.Drawing.Point(287, 3);
            this.pbShipImage.Name = "pbShipImage";
            this.pbShipImage.Size = new System.Drawing.Size(256, 256);
            this.pbShipImage.TabIndex = 0;
            this.pbShipImage.TabStop = false;
            // 
            // tpPlanQueue
            // 
            this.tpPlanQueue.Controls.Add(this.planEditor);
            this.tpPlanQueue.Location = new System.Drawing.Point(4, 22);
            this.tpPlanQueue.Name = "tpPlanQueue";
            this.tpPlanQueue.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlanQueue.Size = new System.Drawing.Size(743, 536);
            this.tpPlanQueue.TabIndex = 1;
            this.tpPlanQueue.Text = "Plan Queue";
            this.tpPlanQueue.UseVisualStyleBackColor = true;
            // 
            // planEditor
            // 
            this.planEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.planEditor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.planEditor.Location = new System.Drawing.Point(3, 3);
            this.planEditor.Name = "planEditor";
            this.planEditor.Plan = null;
            this.planEditor.Size = new System.Drawing.Size(737, 530);
            this.planEditor.TabIndex = 2;
            // 
            // NewPlannerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 518);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(666, 353);
            this.Name = "NewPlannerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "EVEMon Skill Planner";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewPlannerWindow_FormClosed);
            this.Shown += new System.EventHandler(this.NewPlannerWindow_Shown);
            this.Load += new System.EventHandler(this.NewPlannerWindow_Load);
            this.pnlPlanControl.ResumeLayout(false);
            this.pnlPlanControl.PerformLayout();
            this.cmsSkillContext.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpSkillBrowser.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tpShipBrowser.ResumeLayout(false);
            this.scShipSelect.Panel1.ResumeLayout(false);
            this.scShipSelect.Panel2.ResumeLayout(false);
            this.scShipSelect.Panel2.PerformLayout();
            this.scShipSelect.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbShipImage)).EndInit();
            this.tpPlanQueue.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SkillTreeDisplay skillTreeDisplay1;
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpSkillBrowser;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabPage tpPlanQueue;
        private SkillSelectControl skillSelectControl1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbImplantCalculator;
        private System.Windows.Forms.TabPage tpShipBrowser;
        private System.Windows.Forms.SplitContainer scShipSelect;
        private ShipSelectControl shipSelectControl1;
        private System.Windows.Forms.PictureBox pbShipImage;
        private System.Windows.Forms.Label lblShipName;
        private System.Windows.Forms.Label lblShipClass;
        private System.Windows.Forms.Label lblShipDescription;
        private System.Windows.Forms.ListBox lbShipProperties;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnShipSkillsAdd;
        private System.Windows.Forms.Label lblShipTimeRequired;
        private System.Windows.Forms.Label lblShipSkill3;
        private System.Windows.Forms.Label lblShipSkill2;
        private System.Windows.Forms.Label lblShipSkill1;
    }
}
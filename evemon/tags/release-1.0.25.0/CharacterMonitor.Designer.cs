namespace EVEMon
{
    partial class CharacterMonitor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbCharImage = new System.Windows.Forms.PictureBox();
            this.lblCharacterName = new System.Windows.Forms.Label();
            this.lblBioInfo = new System.Windows.Forms.Label();
            this.lblCorpInfo = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblWillpower = new System.Windows.Forms.Label();
            this.lblMemory = new System.Windows.Forms.Label();
            this.lblPerception = new System.Windows.Forms.Label();
            this.lblCharisma = new System.Windows.Forms.Label();
            this.lblIntelligence = new System.Windows.Forms.Label();
            this.pnlTraining = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTrainingSkill = new System.Windows.Forms.Label();
            this.lblTrainingRemain = new System.Windows.Forms.Label();
            this.lblTrainingEst = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.pnlCharData = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.pbThrobber = new System.Windows.Forms.PictureBox();
            this.cmsThrobberMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miHitEveO = new System.Windows.Forms.ToolStripMenuItem();
            this.miChangeInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.lblUpdateTimer = new System.Windows.Forms.Label();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.llToggleAll = new System.Windows.Forms.LinkLabel();
            this.btnMoreOptions = new System.Windows.Forms.Button();
            this.cmsMoreOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miManualImplants = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPlan = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSkillHeader = new System.Windows.Forms.Label();
            this.tmrTick = new System.Windows.Forms.Timer(this.components);
            this.sfdSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tmrThrobber = new System.Windows.Forms.Timer(this.components);
            this.lbSkills = new EVEMon.NoFlickerListBox();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSPPerHour = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbCharImage)).BeginInit();
            this.pnlTraining.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.pnlCharData.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThrobber)).BeginInit();
            this.cmsThrobberMenu.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.cmsMoreOptions.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCharImage
            // 
            this.pbCharImage.Location = new System.Drawing.Point(0, 0);
            this.pbCharImage.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.pbCharImage.MinimumSize = new System.Drawing.Size(128, 128);
            this.pbCharImage.Name = "pbCharImage";
            this.tableLayoutPanel1.SetRowSpan(this.pbCharImage, 3);
            this.pbCharImage.Size = new System.Drawing.Size(128, 128);
            this.pbCharImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCharImage.TabIndex = 0;
            this.pbCharImage.TabStop = false;
            // 
            // lblCharacterName
            // 
            this.lblCharacterName.AutoSize = true;
            this.lblCharacterName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharacterName.Location = new System.Drawing.Point(0, 0);
            this.lblCharacterName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblCharacterName.Name = "lblCharacterName";
            this.lblCharacterName.Size = new System.Drawing.Size(129, 18);
            this.lblCharacterName.TabIndex = 1;
            this.lblCharacterName.Text = "Character Name";
            // 
            // lblBioInfo
            // 
            this.lblBioInfo.AutoSize = true;
            this.lblBioInfo.Location = new System.Drawing.Point(0, 18);
            this.lblBioInfo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblBioInfo.Name = "lblBioInfo";
            this.lblBioInfo.Size = new System.Drawing.Size(44, 13);
            this.lblBioInfo.TabIndex = 3;
            this.lblBioInfo.Text = "Bio Info";
            // 
            // lblCorpInfo
            // 
            this.lblCorpInfo.AutoSize = true;
            this.lblCorpInfo.Location = new System.Drawing.Point(0, 31);
            this.lblCorpInfo.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblCorpInfo.Name = "lblCorpInfo";
            this.lblCorpInfo.Size = new System.Drawing.Size(87, 13);
            this.lblCorpInfo.TabIndex = 4;
            this.lblCorpInfo.Text = "Corporation Info";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(0, 44);
            this.lblBalance.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(92, 13);
            this.lblBalance.TabIndex = 5;
            this.lblBalance.Text = "Balance: 0.00 ISK";
            // 
            // lblWillpower
            // 
            this.lblWillpower.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblWillpower.AutoSize = true;
            this.lblWillpower.Location = new System.Drawing.Point(0, 52);
            this.lblWillpower.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblWillpower.Name = "lblWillpower";
            this.lblWillpower.Size = new System.Drawing.Size(62, 13);
            this.lblWillpower.TabIndex = 7;
            this.lblWillpower.Text = "0 Willpower";
            // 
            // lblMemory
            // 
            this.lblMemory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMemory.AutoSize = true;
            this.lblMemory.Location = new System.Drawing.Point(0, 39);
            this.lblMemory.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblMemory.Name = "lblMemory";
            this.lblMemory.Size = new System.Drawing.Size(54, 13);
            this.lblMemory.TabIndex = 8;
            this.lblMemory.Text = "0 Memory";
            // 
            // lblPerception
            // 
            this.lblPerception.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPerception.AutoSize = true;
            this.lblPerception.Location = new System.Drawing.Point(0, 26);
            this.lblPerception.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblPerception.Name = "lblPerception";
            this.lblPerception.Size = new System.Drawing.Size(67, 13);
            this.lblPerception.TabIndex = 9;
            this.lblPerception.Text = "0 Perception";
            // 
            // lblCharisma
            // 
            this.lblCharisma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCharisma.AutoSize = true;
            this.lblCharisma.Location = new System.Drawing.Point(0, 13);
            this.lblCharisma.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblCharisma.Name = "lblCharisma";
            this.lblCharisma.Size = new System.Drawing.Size(60, 13);
            this.lblCharisma.TabIndex = 10;
            this.lblCharisma.Text = "0 Charisma";
            // 
            // lblIntelligence
            // 
            this.lblIntelligence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblIntelligence.AutoSize = true;
            this.lblIntelligence.Location = new System.Drawing.Point(0, 0);
            this.lblIntelligence.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblIntelligence.Name = "lblIntelligence";
            this.lblIntelligence.Size = new System.Drawing.Size(71, 13);
            this.lblIntelligence.TabIndex = 11;
            this.lblIntelligence.Text = "0 Intelligence";
            // 
            // pnlTraining
            // 
            this.pnlTraining.AutoSize = true;
            this.pnlTraining.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlTraining.Controls.Add(this.tableLayoutPanel2);
            this.pnlTraining.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTraining.Location = new System.Drawing.Point(0, 457);
            this.pnlTraining.Name = "pnlTraining";
            this.pnlTraining.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.pnlTraining.Size = new System.Drawing.Size(392, 42);
            this.pnlTraining.TabIndex = 13;
            this.pnlTraining.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel6, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 3);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(392, 39);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this.lblTrainingSkill);
            this.flowLayoutPanel3.Controls.Add(this.lblTrainingRemain);
            this.flowLayoutPanel3.Controls.Add(this.lblTrainingEst);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(115, 0);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(47, 39);
            this.flowLayoutPanel3.TabIndex = 5;
            // 
            // lblTrainingSkill
            // 
            this.lblTrainingSkill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTrainingSkill.AutoSize = true;
            this.lblTrainingSkill.Location = new System.Drawing.Point(0, 0);
            this.lblTrainingSkill.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblTrainingSkill.Name = "lblTrainingSkill";
            this.lblTrainingSkill.Size = new System.Drawing.Size(44, 13);
            this.lblTrainingSkill.TabIndex = 1;
            this.lblTrainingSkill.Text = "Nothing";
            // 
            // lblTrainingRemain
            // 
            this.lblTrainingRemain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTrainingRemain.AutoSize = true;
            this.lblTrainingRemain.Location = new System.Drawing.Point(0, 13);
            this.lblTrainingRemain.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblTrainingRemain.Name = "lblTrainingRemain";
            this.lblTrainingRemain.Size = new System.Drawing.Size(11, 13);
            this.lblTrainingRemain.TabIndex = 2;
            this.lblTrainingRemain.Text = ".";
            // 
            // lblTrainingEst
            // 
            this.lblTrainingEst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTrainingEst.AutoSize = true;
            this.lblTrainingEst.Location = new System.Drawing.Point(0, 26);
            this.lblTrainingEst.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblTrainingEst.Name = "lblTrainingEst";
            this.lblTrainingEst.Size = new System.Drawing.Size(11, 13);
            this.lblTrainingEst.TabIndex = 3;
            this.lblTrainingEst.Text = ".";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Currently Training:";
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // pnlCharData
            // 
            this.pnlCharData.AutoSize = true;
            this.pnlCharData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlCharData.Controls.Add(this.tableLayoutPanel1);
            this.pnlCharData.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCharData.Location = new System.Drawing.Point(0, 0);
            this.pnlCharData.Name = "pnlCharData";
            this.pnlCharData.Size = new System.Drawing.Size(392, 147);
            this.pnlCharData.TabIndex = 14;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel4, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbCharImage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblSkillHeader, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(392, 147);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this.pbThrobber);
            this.flowLayoutPanel5.Controls.Add(this.lblUpdateTimer);
            this.flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(333, 0);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(59, 24);
            this.flowLayoutPanel5.TabIndex = 15;
            this.flowLayoutPanel5.WrapContents = false;
            this.flowLayoutPanel5.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel5_Paint);
            // 
            // pbThrobber
            // 
            this.pbThrobber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbThrobber.ContextMenuStrip = this.cmsThrobberMenu;
            this.pbThrobber.Location = new System.Drawing.Point(35, 0);
            this.pbThrobber.Margin = new System.Windows.Forms.Padding(0);
            this.pbThrobber.Name = "pbThrobber";
            this.pbThrobber.Size = new System.Drawing.Size(24, 24);
            this.pbThrobber.TabIndex = 16;
            this.pbThrobber.TabStop = false;
            this.ttToolTip.SetToolTip(this.pbThrobber, "Click to update now.");
            this.pbThrobber.Click += new System.EventHandler(this.pbThrobber_Click);
            this.pbThrobber.MouseEnter += new System.EventHandler(this.pbThrobber_MouseEnter);
            // 
            // cmsThrobberMenu
            // 
            this.cmsThrobberMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHitEveO,
            this.miChangeInfo});
            this.cmsThrobberMenu.Name = "cmsThrobberMenu";
            this.cmsThrobberMenu.Size = new System.Drawing.Size(217, 48);
            // 
            // miHitEveO
            // 
            this.miHitEveO.Name = "miHitEveO";
            this.miHitEveO.Size = new System.Drawing.Size(216, 22);
            this.miHitEveO.Text = "Get data from EVE Online";
            this.miHitEveO.Click += new System.EventHandler(this.miHitEveO_Click);
            // 
            // miChangeInfo
            // 
            this.miChangeInfo.Name = "miChangeInfo";
            this.miChangeInfo.Size = new System.Drawing.Size(216, 22);
            this.miChangeInfo.Text = "Change login information...";
            this.miChangeInfo.Click += new System.EventHandler(this.miChangeInfo_Click);
            // 
            // lblUpdateTimer
            // 
            this.lblUpdateTimer.AutoSize = true;
            this.lblUpdateTimer.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblUpdateTimer.Location = new System.Drawing.Point(0, 0);
            this.lblUpdateTimer.Margin = new System.Windows.Forms.Padding(0);
            this.lblUpdateTimer.Name = "lblUpdateTimer";
            this.lblUpdateTimer.Size = new System.Drawing.Size(35, 13);
            this.lblUpdateTimer.TabIndex = 17;
            this.lblUpdateTimer.Text = "label2";
            this.lblUpdateTimer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblUpdateTimer.Visible = false;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel4.Controls.Add(this.lblWillpower);
            this.flowLayoutPanel4.Controls.Add(this.lblMemory);
            this.flowLayoutPanel4.Controls.Add(this.lblPerception);
            this.flowLayoutPanel4.Controls.Add(this.lblCharisma);
            this.flowLayoutPanel4.Controls.Add(this.lblIntelligence);
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(131, 63);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(74, 65);
            this.flowLayoutPanel4.TabIndex = 19;
            this.flowLayoutPanel4.WrapContents = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.llToggleAll);
            this.flowLayoutPanel2.Controls.Add(this.btnMoreOptions);
            this.flowLayoutPanel2.Controls.Add(this.btnSave);
            this.flowLayoutPanel2.Controls.Add(this.btnPlan);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(326, 42);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.tableLayoutPanel1.SetRowSpan(this.flowLayoutPanel2, 3);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(66, 102);
            this.flowLayoutPanel2.TabIndex = 20;
            // 
            // llToggleAll
            // 
            this.llToggleAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.llToggleAll.AutoSize = true;
            this.llToggleAll.Location = new System.Drawing.Point(13, 89);
            this.llToggleAll.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.llToggleAll.Name = "llToggleAll";
            this.llToggleAll.Size = new System.Drawing.Size(53, 13);
            this.llToggleAll.TabIndex = 17;
            this.llToggleAll.TabStop = true;
            this.llToggleAll.Text = "Toggle All";
            this.llToggleAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llToggleAll_LinkClicked);
            // 
            // btnMoreOptions
            // 
            this.btnMoreOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoreOptions.AutoSize = true;
            this.btnMoreOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMoreOptions.ContextMenuStrip = this.cmsMoreOptions;
            this.btnMoreOptions.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnMoreOptions.Location = new System.Drawing.Point(3, 61);
            this.btnMoreOptions.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btnMoreOptions.Name = "btnMoreOptions";
            this.btnMoreOptions.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnMoreOptions.Size = new System.Drawing.Size(63, 22);
            this.btnMoreOptions.TabIndex = 19;
            this.btnMoreOptions.Text = "More...";
            this.btnMoreOptions.UseVisualStyleBackColor = true;
            this.btnMoreOptions.Click += new System.EventHandler(this.btnMoreOptions_Click);
            // 
            // cmsMoreOptions
            // 
            this.cmsMoreOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miManualImplants});
            this.cmsMoreOptions.Name = "cmsMoreOptions";
            this.cmsMoreOptions.Size = new System.Drawing.Size(176, 26);
            // 
            // miManualImplants
            // 
            this.miManualImplants.Name = "miManualImplants";
            this.miManualImplants.Size = new System.Drawing.Size(175, 22);
            this.miManualImplants.Text = "Manual Implants...";
            this.miManualImplants.Click += new System.EventHandler(this.miManualImplants_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AutoSize = true;
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(3, 32);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSave.Size = new System.Drawing.Size(63, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPlan
            // 
            this.btnPlan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlan.AutoSize = true;
            this.btnPlan.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPlan.Enabled = false;
            this.btnPlan.Location = new System.Drawing.Point(3, 3);
            this.btnPlan.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btnPlan.Name = "btnPlan";
            this.btnPlan.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPlan.Size = new System.Drawing.Size(63, 23);
            this.btnPlan.TabIndex = 18;
            this.btnPlan.Text = "Plan...";
            this.btnPlan.UseVisualStyleBackColor = true;
            this.btnPlan.Click += new System.EventHandler(this.btnPlan_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.lblCharacterName);
            this.flowLayoutPanel1.Controls.Add(this.lblBioInfo);
            this.flowLayoutPanel1.Controls.Add(this.lblCorpInfo);
            this.flowLayoutPanel1.Controls.Add(this.lblBalance);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(131, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.tableLayoutPanel1.SetRowSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(132, 60);
            this.flowLayoutPanel1.TabIndex = 18;
            this.flowLayoutPanel1.WrapContents = false;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // lblSkillHeader
            // 
            this.lblSkillHeader.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblSkillHeader, 2);
            this.lblSkillHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkillHeader.Location = new System.Drawing.Point(0, 131);
            this.lblSkillHeader.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.lblSkillHeader.Name = "lblSkillHeader";
            this.lblSkillHeader.Size = new System.Drawing.Size(148, 13);
            this.lblSkillHeader.TabIndex = 14;
            this.lblSkillHeader.Text = "Known Skills (0 Total SP):";
            // 
            // tmrTick
            // 
            this.tmrTick.Interval = 1000;
            this.tmrTick.Tick += new System.EventHandler(this.tmrTick_Tick);
            // 
            // sfdSaveDialog
            // 
            this.sfdSaveDialog.Filter = "Text Format|*.txt|HTML Format|*.html|XML Format|*.xml";
            this.sfdSaveDialog.Title = "Save Character Info";
            // 
            // ttToolTip
            // 
            this.ttToolTip.AutoPopDelay = 5000000;
            this.ttToolTip.InitialDelay = 500;
            this.ttToolTip.ReshowDelay = 100;
            this.ttToolTip.Popup += new System.Windows.Forms.PopupEventHandler(this.ttToolTip_Popup);
            // 
            // tmrThrobber
            // 
            this.tmrThrobber.Tick += new System.EventHandler(this.tmrThrobber_Tick);
            // 
            // lbSkills
            // 
            this.lbSkills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSkills.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbSkills.FormattingEnabled = true;
            this.lbSkills.IntegralHeight = false;
            this.lbSkills.ItemHeight = 15;
            this.lbSkills.Location = new System.Drawing.Point(0, 147);
            this.lbSkills.Name = "lbSkills";
            this.lbSkills.Size = new System.Drawing.Size(392, 310);
            this.lbSkills.TabIndex = 12;
            this.lbSkills.MouseEnter += new System.EventHandler(this.lbSkills_MouseEnter);
            this.lbSkills.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbSkills_MouseClick);
            this.lbSkills.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbSkills_DrawItem);
            this.lbSkills.SelectedIndexChanged += new System.EventHandler(this.lbSkills_SelectedIndexChanged);
            this.lbSkills.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbSkills_MeasureItem);
            this.lbSkills.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbSkills_MouseMove);
            this.lbSkills.MouseLeave += new System.EventHandler(this.lbSkills_MouseLeave);
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel6.AutoSize = true;
            this.flowLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel6.Controls.Add(this.label1);
            this.flowLayoutPanel6.Controls.Add(this.lblSPPerHour);
            this.flowLayoutPanel6.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(115, 39);
            this.flowLayoutPanel6.TabIndex = 15;
            this.flowLayoutPanel6.WrapContents = false;
            // 
            // lblSPPerHour
            // 
            this.lblSPPerHour.AutoSize = true;
            this.lblSPPerHour.Location = new System.Drawing.Point(0, 13);
            this.lblSPPerHour.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblSPPerHour.Name = "lblSPPerHour";
            this.lblSPPerHour.Size = new System.Drawing.Size(35, 13);
            this.lblSPPerHour.TabIndex = 1;
            this.lblSPPerHour.Text = "label2";
            // 
            // CharacterMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lbSkills);
            this.Controls.Add(this.pnlCharData);
            this.Controls.Add(this.pnlTraining);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CharacterMonitor";
            this.Size = new System.Drawing.Size(392, 499);
            this.Load += new System.EventHandler(this.CharacterMonitor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCharImage)).EndInit();
            this.pnlTraining.ResumeLayout(false);
            this.pnlTraining.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.pnlCharData.ResumeLayout(false);
            this.pnlCharData.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbThrobber)).EndInit();
            this.cmsThrobberMenu.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.cmsMoreOptions.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCharImage;
        private System.Windows.Forms.Label lblCharacterName;
        private System.Windows.Forms.Label lblBioInfo;
        private System.Windows.Forms.Label lblCorpInfo;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblWillpower;
        private System.Windows.Forms.Label lblMemory;
        private System.Windows.Forms.Label lblPerception;
        private System.Windows.Forms.Label lblCharisma;
        private System.Windows.Forms.Label lblIntelligence;
        private EVEMon.NoFlickerListBox lbSkills;
        private System.Windows.Forms.Panel pnlTraining;
        private System.Windows.Forms.Label lblTrainingEst;
        private System.Windows.Forms.Label lblTrainingRemain;
        private System.Windows.Forms.Label lblTrainingSkill;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Panel pnlCharData;
        private System.Windows.Forms.Timer tmrTick;
        private System.Windows.Forms.SaveFileDialog sfdSaveDialog;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSkillHeader;
        private System.Windows.Forms.ToolTip ttToolTip;
        private System.Windows.Forms.PictureBox pbThrobber;
        private System.Windows.Forms.Timer tmrThrobber;
        private System.Windows.Forms.LinkLabel llToggleAll;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnPlan;
        private System.Windows.Forms.Button btnMoreOptions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label lblUpdateTimer;
        private System.Windows.Forms.ContextMenuStrip cmsThrobberMenu;
        private System.Windows.Forms.ToolStripMenuItem miHitEveO;
        private System.Windows.Forms.ToolStripMenuItem miChangeInfo;
        private System.Windows.Forms.ContextMenuStrip cmsMoreOptions;
        private System.Windows.Forms.ToolStripMenuItem miManualImplants;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.Label lblSPPerHour;
    }
}

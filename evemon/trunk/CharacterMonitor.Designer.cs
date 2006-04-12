namespace EveCharacterMonitor
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
            this.lbSkills = new System.Windows.Forms.ListBox();
            this.pnlTraining = new System.Windows.Forms.Panel();
            this.lblTrainingEst = new System.Windows.Forms.Label();
            this.lblTrainingRemain = new System.Windows.Forms.Label();
            this.lblTrainingSkill = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.pnlCharData = new System.Windows.Forms.Panel();
            this.btnDebugError = new System.Windows.Forms.Button();
            this.lblSkillHeader = new System.Windows.Forms.Label();
            this.btnPlan = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tmrTick = new System.Windows.Forms.Timer(this.components);
            this.sfdSaveDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbCharImage)).BeginInit();
            this.pnlTraining.SuspendLayout();
            this.pnlCharData.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCharImage
            // 
            this.pbCharImage.Location = new System.Drawing.Point(0, 0);
            this.pbCharImage.Name = "pbCharImage";
            this.pbCharImage.Size = new System.Drawing.Size(128, 128);
            this.pbCharImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCharImage.TabIndex = 0;
            this.pbCharImage.TabStop = false;
            // 
            // lblCharacterName
            // 
            this.lblCharacterName.AutoSize = true;
            this.lblCharacterName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharacterName.Location = new System.Drawing.Point(134, 0);
            this.lblCharacterName.Name = "lblCharacterName";
            this.lblCharacterName.Size = new System.Drawing.Size(129, 18);
            this.lblCharacterName.TabIndex = 1;
            this.lblCharacterName.Text = "Character Name";
            // 
            // lblBioInfo
            // 
            this.lblBioInfo.AutoSize = true;
            this.lblBioInfo.Location = new System.Drawing.Point(134, 18);
            this.lblBioInfo.Name = "lblBioInfo";
            this.lblBioInfo.Size = new System.Drawing.Size(44, 13);
            this.lblBioInfo.TabIndex = 3;
            this.lblBioInfo.Text = "Bio Info";
            // 
            // lblCorpInfo
            // 
            this.lblCorpInfo.AutoSize = true;
            this.lblCorpInfo.Location = new System.Drawing.Point(134, 31);
            this.lblCorpInfo.Name = "lblCorpInfo";
            this.lblCorpInfo.Size = new System.Drawing.Size(87, 13);
            this.lblCorpInfo.TabIndex = 4;
            this.lblCorpInfo.Text = "Corporation Info";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(134, 44);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(92, 13);
            this.lblBalance.TabIndex = 5;
            this.lblBalance.Text = "Balance: 0.00 ISK";
            // 
            // lblWillpower
            // 
            this.lblWillpower.AutoSize = true;
            this.lblWillpower.Location = new System.Drawing.Point(134, 115);
            this.lblWillpower.Name = "lblWillpower";
            this.lblWillpower.Size = new System.Drawing.Size(62, 13);
            this.lblWillpower.TabIndex = 7;
            this.lblWillpower.Text = "0 Willpower";
            // 
            // lblMemory
            // 
            this.lblMemory.AutoSize = true;
            this.lblMemory.Location = new System.Drawing.Point(134, 102);
            this.lblMemory.Name = "lblMemory";
            this.lblMemory.Size = new System.Drawing.Size(54, 13);
            this.lblMemory.TabIndex = 8;
            this.lblMemory.Text = "0 Memory";
            // 
            // lblPerception
            // 
            this.lblPerception.AutoSize = true;
            this.lblPerception.Location = new System.Drawing.Point(134, 89);
            this.lblPerception.Name = "lblPerception";
            this.lblPerception.Size = new System.Drawing.Size(67, 13);
            this.lblPerception.TabIndex = 9;
            this.lblPerception.Text = "0 Perception";
            // 
            // lblCharisma
            // 
            this.lblCharisma.AutoSize = true;
            this.lblCharisma.Location = new System.Drawing.Point(134, 76);
            this.lblCharisma.Name = "lblCharisma";
            this.lblCharisma.Size = new System.Drawing.Size(60, 13);
            this.lblCharisma.TabIndex = 10;
            this.lblCharisma.Text = "0 Charisma";
            // 
            // lblIntelligence
            // 
            this.lblIntelligence.AutoSize = true;
            this.lblIntelligence.Location = new System.Drawing.Point(134, 63);
            this.lblIntelligence.Name = "lblIntelligence";
            this.lblIntelligence.Size = new System.Drawing.Size(71, 13);
            this.lblIntelligence.TabIndex = 11;
            this.lblIntelligence.Text = "0 Intelligence";
            // 
            // lbSkills
            // 
            this.lbSkills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSkills.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lbSkills.FormattingEnabled = true;
            this.lbSkills.IntegralHeight = false;
            this.lbSkills.ItemHeight = 15;
            this.lbSkills.Location = new System.Drawing.Point(0, 148);
            this.lbSkills.Name = "lbSkills";
            this.lbSkills.Size = new System.Drawing.Size(384, 209);
            this.lbSkills.TabIndex = 12;
            this.lbSkills.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbSkills_DrawItem);
            this.lbSkills.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lbSkills_MeasureItem);
            // 
            // pnlTraining
            // 
            this.pnlTraining.Controls.Add(this.lblTrainingEst);
            this.pnlTraining.Controls.Add(this.lblTrainingRemain);
            this.pnlTraining.Controls.Add(this.lblTrainingSkill);
            this.pnlTraining.Controls.Add(this.label1);
            this.pnlTraining.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTraining.Location = new System.Drawing.Point(0, 357);
            this.pnlTraining.Name = "pnlTraining";
            this.pnlTraining.Size = new System.Drawing.Size(384, 45);
            this.pnlTraining.TabIndex = 13;
            this.pnlTraining.Visible = false;
            // 
            // lblTrainingEst
            // 
            this.lblTrainingEst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTrainingEst.AutoSize = true;
            this.lblTrainingEst.Location = new System.Drawing.Point(116, 32);
            this.lblTrainingEst.Name = "lblTrainingEst";
            this.lblTrainingEst.Size = new System.Drawing.Size(11, 13);
            this.lblTrainingEst.TabIndex = 3;
            this.lblTrainingEst.Text = ".";
            // 
            // lblTrainingRemain
            // 
            this.lblTrainingRemain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTrainingRemain.AutoSize = true;
            this.lblTrainingRemain.Location = new System.Drawing.Point(116, 19);
            this.lblTrainingRemain.Name = "lblTrainingRemain";
            this.lblTrainingRemain.Size = new System.Drawing.Size(11, 13);
            this.lblTrainingRemain.TabIndex = 2;
            this.lblTrainingRemain.Text = ".";
            // 
            // lblTrainingSkill
            // 
            this.lblTrainingSkill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTrainingSkill.AutoSize = true;
            this.lblTrainingSkill.Location = new System.Drawing.Point(116, 6);
            this.lblTrainingSkill.Name = "lblTrainingSkill";
            this.lblTrainingSkill.Size = new System.Drawing.Size(44, 13);
            this.lblTrainingSkill.TabIndex = 1;
            this.lblTrainingSkill.Text = "Nothing";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-3, 6);
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
            this.pnlCharData.Controls.Add(this.btnDebugError);
            this.pnlCharData.Controls.Add(this.lblSkillHeader);
            this.pnlCharData.Controls.Add(this.btnPlan);
            this.pnlCharData.Controls.Add(this.btnSave);
            this.pnlCharData.Controls.Add(this.pbCharImage);
            this.pnlCharData.Controls.Add(this.lblCharacterName);
            this.pnlCharData.Controls.Add(this.lblBioInfo);
            this.pnlCharData.Controls.Add(this.lblIntelligence);
            this.pnlCharData.Controls.Add(this.lblCorpInfo);
            this.pnlCharData.Controls.Add(this.lblCharisma);
            this.pnlCharData.Controls.Add(this.lblBalance);
            this.pnlCharData.Controls.Add(this.lblPerception);
            this.pnlCharData.Controls.Add(this.lblWillpower);
            this.pnlCharData.Controls.Add(this.lblMemory);
            this.pnlCharData.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCharData.Location = new System.Drawing.Point(0, 0);
            this.pnlCharData.Name = "pnlCharData";
            this.pnlCharData.Size = new System.Drawing.Size(384, 148);
            this.pnlCharData.TabIndex = 14;
            // 
            // btnDebugError
            // 
            this.btnDebugError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDebugError.Location = new System.Drawing.Point(323, 47);
            this.btnDebugError.Name = "btnDebugError";
            this.btnDebugError.Size = new System.Drawing.Size(58, 23);
            this.btnDebugError.TabIndex = 15;
            this.btnDebugError.Text = "ERROR";
            this.btnDebugError.UseVisualStyleBackColor = true;
            this.btnDebugError.Visible = false;
            this.btnDebugError.Click += new System.EventHandler(this.btnDebugError_Click);
            // 
            // lblSkillHeader
            // 
            this.lblSkillHeader.AutoSize = true;
            this.lblSkillHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkillHeader.Location = new System.Drawing.Point(-3, 132);
            this.lblSkillHeader.Name = "lblSkillHeader";
            this.lblSkillHeader.Size = new System.Drawing.Size(148, 13);
            this.lblSkillHeader.TabIndex = 14;
            this.lblSkillHeader.Text = "Known Skills (0 Total SP):";
            // 
            // btnPlan
            // 
            this.btnPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlan.Enabled = false;
            this.btnPlan.Location = new System.Drawing.Point(323, 76);
            this.btnPlan.Name = "btnPlan";
            this.btnPlan.Size = new System.Drawing.Size(61, 23);
            this.btnPlan.TabIndex = 13;
            this.btnPlan.Text = "Plan...";
            this.btnPlan.UseVisualStyleBackColor = true;
            this.btnPlan.Click += new System.EventHandler(this.btnPlan_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(323, 105);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(61, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button1_Click);
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
            this.Size = new System.Drawing.Size(384, 402);
            ((System.ComponentModel.ISupportInitialize)(this.pbCharImage)).EndInit();
            this.pnlTraining.ResumeLayout(false);
            this.pnlTraining.PerformLayout();
            this.pnlCharData.ResumeLayout(false);
            this.pnlCharData.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ListBox lbSkills;
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
        private System.Windows.Forms.Button btnPlan;
        private System.Windows.Forms.Label lblSkillHeader;
        private System.Windows.Forms.Button btnDebugError;
    }
}

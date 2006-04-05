namespace EveCharacterMonitor
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
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
            this.pnlSkillInTraining = new System.Windows.Forms.Panel();
            this.lblTrainingCompletion = new System.Windows.Forms.Label();
            this.lblTrainingTimeLeft = new System.Windows.Forms.Label();
            this.lblSkillInTraining = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tmrTrainingTimer = new System.Windows.Forms.Timer(this.components);
            this.lnkChange = new System.Windows.Forms.LinkLabel();
            this.niAlert = new System.Windows.Forms.NotifyIcon(this.components);
            this.tmrAlert = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlSkillInTraining.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblCharacterName
            // 
            this.lblCharacterName.AutoSize = true;
            this.lblCharacterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharacterName.Location = new System.Drawing.Point(145, 9);
            this.lblCharacterName.Name = "lblCharacterName";
            this.lblCharacterName.Size = new System.Drawing.Size(14, 20);
            this.lblCharacterName.TabIndex = 1;
            this.lblCharacterName.Text = ".";
            // 
            // lblBioInfo
            // 
            this.lblBioInfo.AutoSize = true;
            this.lblBioInfo.Location = new System.Drawing.Point(146, 29);
            this.lblBioInfo.Name = "lblBioInfo";
            this.lblBioInfo.Size = new System.Drawing.Size(10, 13);
            this.lblBioInfo.TabIndex = 2;
            this.lblBioInfo.Text = ".";
            // 
            // lblCorpInfo
            // 
            this.lblCorpInfo.AutoSize = true;
            this.lblCorpInfo.Location = new System.Drawing.Point(146, 42);
            this.lblCorpInfo.Name = "lblCorpInfo";
            this.lblCorpInfo.Size = new System.Drawing.Size(10, 13);
            this.lblCorpInfo.TabIndex = 3;
            this.lblCorpInfo.Text = ".";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(146, 55);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(10, 13);
            this.lblBalance.TabIndex = 4;
            this.lblBalance.Text = ".";
            // 
            // lblWillpower
            // 
            this.lblWillpower.AutoSize = true;
            this.lblWillpower.Location = new System.Drawing.Point(146, 127);
            this.lblWillpower.Name = "lblWillpower";
            this.lblWillpower.Size = new System.Drawing.Size(10, 13);
            this.lblWillpower.TabIndex = 5;
            this.lblWillpower.Text = ".";
            // 
            // lblMemory
            // 
            this.lblMemory.AutoSize = true;
            this.lblMemory.Location = new System.Drawing.Point(146, 114);
            this.lblMemory.Name = "lblMemory";
            this.lblMemory.Size = new System.Drawing.Size(10, 13);
            this.lblMemory.TabIndex = 6;
            this.lblMemory.Text = ".";
            // 
            // lblPerception
            // 
            this.lblPerception.AutoSize = true;
            this.lblPerception.Location = new System.Drawing.Point(146, 101);
            this.lblPerception.Name = "lblPerception";
            this.lblPerception.Size = new System.Drawing.Size(10, 13);
            this.lblPerception.TabIndex = 7;
            this.lblPerception.Text = ".";
            // 
            // lblCharisma
            // 
            this.lblCharisma.AutoSize = true;
            this.lblCharisma.Location = new System.Drawing.Point(146, 88);
            this.lblCharisma.Name = "lblCharisma";
            this.lblCharisma.Size = new System.Drawing.Size(10, 13);
            this.lblCharisma.TabIndex = 8;
            this.lblCharisma.Text = ".";
            // 
            // lblIntelligence
            // 
            this.lblIntelligence.AutoSize = true;
            this.lblIntelligence.Location = new System.Drawing.Point(146, 75);
            this.lblIntelligence.Name = "lblIntelligence";
            this.lblIntelligence.Size = new System.Drawing.Size(10, 13);
            this.lblIntelligence.TabIndex = 9;
            this.lblIntelligence.Text = ".";
            // 
            // lbSkills
            // 
            this.lbSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSkills.FormattingEnabled = true;
            this.lbSkills.IntegralHeight = false;
            this.lbSkills.Location = new System.Drawing.Point(12, 146);
            this.lbSkills.Name = "lbSkills";
            this.lbSkills.Size = new System.Drawing.Size(402, 234);
            this.lbSkills.TabIndex = 10;
            // 
            // pnlSkillInTraining
            // 
            this.pnlSkillInTraining.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSkillInTraining.Controls.Add(this.lblTrainingCompletion);
            this.pnlSkillInTraining.Controls.Add(this.lblTrainingTimeLeft);
            this.pnlSkillInTraining.Controls.Add(this.lblSkillInTraining);
            this.pnlSkillInTraining.Controls.Add(this.label10);
            this.pnlSkillInTraining.Location = new System.Drawing.Point(12, 386);
            this.pnlSkillInTraining.Name = "pnlSkillInTraining";
            this.pnlSkillInTraining.Size = new System.Drawing.Size(402, 42);
            this.pnlSkillInTraining.TabIndex = 11;
            // 
            // lblTrainingCompletion
            // 
            this.lblTrainingCompletion.AutoSize = true;
            this.lblTrainingCompletion.Location = new System.Drawing.Point(120, 26);
            this.lblTrainingCompletion.Name = "lblTrainingCompletion";
            this.lblTrainingCompletion.Size = new System.Drawing.Size(156, 13);
            this.lblTrainingCompletion.TabIndex = 3;
            this.lblTrainingCompletion.Text = "Completion: Today, 4:14:12 AM";
            // 
            // lblTrainingTimeLeft
            // 
            this.lblTrainingTimeLeft.AutoSize = true;
            this.lblTrainingTimeLeft.Location = new System.Drawing.Point(120, 13);
            this.lblTrainingTimeLeft.Name = "lblTrainingTimeLeft";
            this.lblTrainingTimeLeft.Size = new System.Drawing.Size(166, 13);
            this.lblTrainingTimeLeft.TabIndex = 2;
            this.lblTrainingTimeLeft.Text = "12 hours, 15 minutes, 12 seconds";
            // 
            // lblSkillInTraining
            // 
            this.lblSkillInTraining.AutoSize = true;
            this.lblSkillInTraining.Location = new System.Drawing.Point(120, 0);
            this.lblSkillInTraining.Name = "lblSkillInTraining";
            this.lblSkillInTraining.Size = new System.Drawing.Size(116, 13);
            this.lblSkillInTraining.TabIndex = 1;
            this.lblSkillInTraining.Text = "Scout Drone Operation";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Currently Training:";
            // 
            // tmrTrainingTimer
            // 
            this.tmrTrainingTimer.Enabled = true;
            this.tmrTrainingTimer.Interval = 1000;
            this.tmrTrainingTimer.Tick += new System.EventHandler(this.tmrTrainingTimer_Tick);
            // 
            // lnkChange
            // 
            this.lnkChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkChange.AutoSize = true;
            this.lnkChange.Location = new System.Drawing.Point(312, 127);
            this.lnkChange.Name = "lnkChange";
            this.lnkChange.Size = new System.Drawing.Size(102, 13);
            this.lnkChange.TabIndex = 13;
            this.lnkChange.TabStop = true;
            this.lnkChange.Text = "Change Character...";
            this.lnkChange.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkChange_LinkClicked);
            // 
            // niAlert
            // 
            this.niAlert.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.niAlert.Icon = ((System.Drawing.Icon)(resources.GetObject("niAlert.Icon")));
            this.niAlert.Text = "Skill Training Completed";
            this.niAlert.Click += new System.EventHandler(this.niAlert_Click);
            this.niAlert.MouseClick += new System.Windows.Forms.MouseEventHandler(this.niAlert_MouseClick);
            this.niAlert.BalloonTipClicked += new System.EventHandler(this.niAlert_BalloonTipClicked);
            // 
            // tmrAlert
            // 
            this.tmrAlert.Interval = 60000;
            this.tmrAlert.Tick += new System.EventHandler(this.tmrAlert_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 440);
            this.Controls.Add(this.lnkChange);
            this.Controls.Add(this.pnlSkillInTraining);
            this.Controls.Add(this.lbSkills);
            this.Controls.Add(this.lblIntelligence);
            this.Controls.Add(this.lblCharisma);
            this.Controls.Add(this.lblPerception);
            this.Controls.Add(this.lblMemory);
            this.Controls.Add(this.lblWillpower);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.lblCorpInfo);
            this.Controls.Add(this.lblBioInfo);
            this.Controls.Add(this.lblCharacterName);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(368, 306);
            this.Name = "Form1";
            this.Text = "EVE Character Monitor";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlSkillInTraining.ResumeLayout(false);
            this.pnlSkillInTraining.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
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
        private System.Windows.Forms.Panel pnlSkillInTraining;
        private System.Windows.Forms.Label lblTrainingCompletion;
        private System.Windows.Forms.Label lblTrainingTimeLeft;
        private System.Windows.Forms.Label lblSkillInTraining;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Timer tmrTrainingTimer;
        private System.Windows.Forms.LinkLabel lnkChange;
        private System.Windows.Forms.NotifyIcon niAlert;
        private System.Windows.Forms.Timer tmrAlert;
    }
}


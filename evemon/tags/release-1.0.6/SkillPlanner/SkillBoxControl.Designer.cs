namespace EveCharacterMonitor.SkillPlanner
{
    partial class SkillBoxControl
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSkillName = new System.Windows.Forms.Label();
            this.lblCurrentLevel = new System.Windows.Forms.Label();
            this.lblRequiredLevel = new System.Windows.Forms.Label();
            this.lblTimeRequired = new System.Windows.Forms.Label();
            this.lblTotalTimeRequired = new System.Windows.Forms.Label();
            this.cmsPrimaryMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miPlanTo1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo2 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo3 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo4 = new System.Windows.Forms.ToolStripMenuItem();
            this.miPlanTo5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miCancelPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.ttToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.cmsPrimaryMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.flowLayoutPanel1.Controls.Add(this.lblSkillName);
            this.flowLayoutPanel1.Controls.Add(this.lblCurrentLevel);
            this.flowLayoutPanel1.Controls.Add(this.lblRequiredLevel);
            this.flowLayoutPanel1.Controls.Add(this.lblTimeRequired);
            this.flowLayoutPanel1.Controls.Add(this.lblTotalTimeRequired);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(220, 73);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lblSkillName
            // 
            this.lblSkillName.AutoSize = true;
            this.lblSkillName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkillName.Location = new System.Drawing.Point(5, 2);
            this.lblSkillName.Name = "lblSkillName";
            this.lblSkillName.Size = new System.Drawing.Size(135, 13);
            this.lblSkillName.TabIndex = 0;
            this.lblSkillName.Text = "Scout Drone Operation";
            this.ttToolTip.SetToolTip(this.lblSkillName, "The name of the skill represented by this skillbox.");
            // 
            // lblCurrentLevel
            // 
            this.lblCurrentLevel.AutoSize = true;
            this.lblCurrentLevel.Location = new System.Drawing.Point(5, 15);
            this.lblCurrentLevel.Name = "lblCurrentLevel";
            this.lblCurrentLevel.Size = new System.Drawing.Size(87, 13);
            this.lblCurrentLevel.TabIndex = 2;
            this.lblCurrentLevel.Text = "Current Level: II";
            this.ttToolTip.SetToolTip(this.lblCurrentLevel, "The level of the skill you\'ve already trained.");
            // 
            // lblRequiredLevel
            // 
            this.lblRequiredLevel.AutoSize = true;
            this.lblRequiredLevel.Location = new System.Drawing.Point(5, 28);
            this.lblRequiredLevel.Name = "lblRequiredLevel";
            this.lblRequiredLevel.Size = new System.Drawing.Size(95, 13);
            this.lblRequiredLevel.TabIndex = 1;
            this.lblRequiredLevel.Text = "Required Level: IV";
            this.ttToolTip.SetToolTip(this.lblRequiredLevel, "The level you\'re planning on training this skill to.");
            // 
            // lblTimeRequired
            // 
            this.lblTimeRequired.AutoSize = true;
            this.lblTimeRequired.Location = new System.Drawing.Point(5, 41);
            this.lblTimeRequired.Name = "lblTimeRequired";
            this.lblTimeRequired.Size = new System.Drawing.Size(149, 13);
            this.lblTimeRequired.TabIndex = 3;
            this.lblTimeRequired.Text = "This Time: 3D, 23H, 13M, 14S";
            this.ttToolTip.SetToolTip(this.lblTimeRequired, "How much time it will take to learn this skill, not including any prerequisite sk" +
                    "ill training.");
            // 
            // lblTotalTimeRequired
            // 
            this.lblTotalTimeRequired.AutoSize = true;
            this.lblTotalTimeRequired.Location = new System.Drawing.Point(5, 54);
            this.lblTotalTimeRequired.Name = "lblTotalTimeRequired";
            this.lblTotalTimeRequired.Size = new System.Drawing.Size(154, 13);
            this.lblTotalTimeRequired.TabIndex = 4;
            this.lblTotalTimeRequired.Text = "Total Time: 3D, 23H, 13M, 14S";
            this.ttToolTip.SetToolTip(this.lblTotalTimeRequired, "How much time it will take to learn this skill, including all necessary prerequis" +
                    "ite skill training.");
            // 
            // cmsPrimaryMenu
            // 
            this.cmsPrimaryMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPlanTo1,
            this.miPlanTo2,
            this.miPlanTo3,
            this.miPlanTo4,
            this.miPlanTo5,
            this.toolStripMenuItem1,
            this.miCancelPlan});
            this.cmsPrimaryMenu.Name = "cmsPrimaryMenu";
            this.cmsPrimaryMenu.Size = new System.Drawing.Size(181, 142);
            this.cmsPrimaryMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsPrimaryMenu_Opening);
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
            // miCancelPlan
            // 
            this.miCancelPlan.Name = "miCancelPlan";
            this.miCancelPlan.Size = new System.Drawing.Size(180, 22);
            this.miCancelPlan.Text = "Cancel Current Plan";
            this.miCancelPlan.Click += new System.EventHandler(this.miCancelPlan_Click);
            // 
            // SkillBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SkillBoxControl";
            this.Size = new System.Drawing.Size(220, 73);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.cmsPrimaryMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblSkillName;
        private System.Windows.Forms.Label lblRequiredLevel;
        private System.Windows.Forms.Label lblCurrentLevel;
        private System.Windows.Forms.Label lblTimeRequired;
        private System.Windows.Forms.Label lblTotalTimeRequired;
        private System.Windows.Forms.ContextMenuStrip cmsPrimaryMenu;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo1;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo2;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo3;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo4;
        private System.Windows.Forms.ToolStripMenuItem miPlanTo5;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miCancelPlan;
        private System.Windows.Forms.ToolTip ttToolTip;
    }
}

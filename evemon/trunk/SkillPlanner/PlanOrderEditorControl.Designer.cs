namespace EveCharacterMonitor.SkillPlanner
{
    partial class PlanOrderEditorControl
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lvSkills = new EveCharacterMonitor.SkillPlanner.DraggableListView();
            this.colSkill = new System.Windows.Forms.ColumnHeader();
            this.colTrainingTime = new System.Windows.Forms.ColumnHeader();
            this.colEarliestStart = new System.Windows.Forms.ColumnHeader();
            this.colEarliestFinish = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Move Up";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Move Down";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 106);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Remove Skill";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // lvSkills
            // 
            this.lvSkills.AllowDrop = true;
            this.lvSkills.AllowRowReorder = true;
            this.lvSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSkills.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSkill,
            this.colTrainingTime,
            this.colEarliestStart,
            this.colEarliestFinish});
            this.lvSkills.FullRowSelect = true;
            this.lvSkills.Location = new System.Drawing.Point(0, 0);
            this.lvSkills.Name = "lvSkills";
            this.lvSkills.Size = new System.Drawing.Size(683, 558);
            this.lvSkills.TabIndex = 3;
            this.lvSkills.UseCompatibleStateImageBehavior = false;
            this.lvSkills.View = System.Windows.Forms.View.Details;
            this.lvSkills.ListViewItemsDragged += new System.EventHandler<System.EventArgs>(this.lvSkills_ListViewItemsDragged);
            this.lvSkills.ListViewItemsDragging += new System.EventHandler<EveCharacterMonitor.SkillPlanner.ListViewDragEventArgs>(this.lvSkills_ListViewItemsDragging);
            // 
            // colSkill
            // 
            this.colSkill.Text = "Skill";
            this.colSkill.Width = 216;
            // 
            // colTrainingTime
            // 
            this.colTrainingTime.Text = "Training Time";
            this.colTrainingTime.Width = 117;
            // 
            // colEarliestStart
            // 
            this.colEarliestStart.Text = "Earliest Start";
            this.colEarliestStart.Width = 136;
            // 
            // colEarliestFinish
            // 
            this.colEarliestFinish.Text = "Earliest Finish";
            this.colEarliestFinish.Width = 136;
            // 
            // PlanOrderEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lvSkills);
            this.Name = "PlanOrderEditorControl";
            this.Size = new System.Drawing.Size(683, 558);
            this.ResumeLayout(false);

        }

        #endregion

        private EveCharacterMonitor.SkillPlanner.DraggableListView lvSkills;
        private System.Windows.Forms.ColumnHeader colSkill;
        private System.Windows.Forms.ColumnHeader colTrainingTime;
        private System.Windows.Forms.ColumnHeader colEarliestStart;
        private System.Windows.Forms.ColumnHeader colEarliestFinish;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

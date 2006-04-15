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
            this.components = new System.ComponentModel.Container();
            this.cmsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miRemoveFromPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.tmrTick = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lvSkills = new EveCharacterMonitor.SkillPlanner.DraggableListView();
            this.colSkill = new System.Windows.Forms.ColumnHeader();
            this.colTrainingTime = new System.Windows.Forms.ColumnHeader();
            this.colEarliestStart = new System.Windows.Forms.ColumnHeader();
            this.colEarliestFinish = new System.Windows.Forms.ColumnHeader();
            this.cmsContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsContextMenu
            // 
            this.cmsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRemoveFromPlan});
            this.cmsContextMenu.Name = "cmsContextMenu";
            this.cmsContextMenu.Size = new System.Drawing.Size(185, 26);
            this.cmsContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsContextMenu_Opening);
            // 
            // miRemoveFromPlan
            // 
            this.miRemoveFromPlan.Name = "miRemoveFromPlan";
            this.miRemoveFromPlan.Size = new System.Drawing.Size(184, 22);
            this.miRemoveFromPlan.Text = "Remove from Plan...";
            this.miRemoveFromPlan.Click += new System.EventHandler(this.miRemoveFromPlan_Click);
            // 
            // sfdSave
            // 
            this.sfdSave.Filter = "EVEMon Plan Format (*.emp)|*.emp|XML Format (*.xml)|*.xml|Text Format (*.txt)|*.t" +
                "xt";
            this.sfdSave.Title = "Save Plan As...";
            // 
            // tmrTick
            // 
            this.tmrTick.Interval = 30000;
            this.tmrTick.Tick += new System.EventHandler(this.tmrTick_Tick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 545);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(350, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tip: You can drag and drop skills to change their planned training order.";
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
            this.lvSkills.ContextMenuStrip = this.cmsContextMenu;
            this.lvSkills.FullRowSelect = true;
            this.lvSkills.Location = new System.Drawing.Point(0, 0);
            this.lvSkills.Name = "lvSkills";
            this.lvSkills.Size = new System.Drawing.Size(683, 542);
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
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvSkills);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PlanOrderEditorControl";
            this.Size = new System.Drawing.Size(683, 558);
            this.cmsContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EveCharacterMonitor.SkillPlanner.DraggableListView lvSkills;
        private System.Windows.Forms.ColumnHeader colSkill;
        private System.Windows.Forms.ColumnHeader colTrainingTime;
        private System.Windows.Forms.ColumnHeader colEarliestStart;
        private System.Windows.Forms.ColumnHeader colEarliestFinish;
        private System.Windows.Forms.ContextMenuStrip cmsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem miRemoveFromPlan;
        private System.Windows.Forms.SaveFileDialog sfdSave;
        private System.Windows.Forms.Timer tmrTick;
        private System.Windows.Forms.Label label1;
    }
}

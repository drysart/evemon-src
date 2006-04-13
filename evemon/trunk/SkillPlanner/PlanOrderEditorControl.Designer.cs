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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanOrderEditorControl));
            this.tsToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.cmsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miRemoveFromPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.lvSkills = new EveCharacterMonitor.SkillPlanner.DraggableListView();
            this.colSkill = new System.Windows.Forms.ColumnHeader();
            this.colTrainingTime = new System.Windows.Forms.ColumnHeader();
            this.colEarliestStart = new System.Windows.Forms.ColumnHeader();
            this.colEarliestFinish = new System.Windows.Forms.ColumnHeader();
            this.tsbCopyForum = new System.Windows.Forms.ToolStripButton();
            this.tsToolStrip.SuspendLayout();
            this.cmsContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsToolStrip
            // 
            this.tsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.tsbCopyForum});
            this.tsToolStrip.Location = new System.Drawing.Point(0, 0);
            this.tsToolStrip.Name = "tsToolStrip";
            this.tsToolStrip.Size = new System.Drawing.Size(683, 25);
            this.tsToolStrip.TabIndex = 7;
            this.tsToolStrip.Text = "toolStrip1";
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "Save to File...";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
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
            this.sfdSave.Filter = "XML Format|*.xml|Text Format|*.txt";
            this.sfdSave.Title = "Save Plan As...";
            // 
            // lvSkills
            // 
            this.lvSkills.AllowDrop = true;
            this.lvSkills.AllowRowReorder = true;
            this.lvSkills.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSkill,
            this.colTrainingTime,
            this.colEarliestStart,
            this.colEarliestFinish});
            this.lvSkills.ContextMenuStrip = this.cmsContextMenu;
            this.lvSkills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSkills.FullRowSelect = true;
            this.lvSkills.Location = new System.Drawing.Point(0, 25);
            this.lvSkills.Name = "lvSkills";
            this.lvSkills.Size = new System.Drawing.Size(683, 533);
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
            // tsbCopyForum
            // 
            this.tsbCopyForum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCopyForum.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopyForum.Image")));
            this.tsbCopyForum.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopyForum.Name = "tsbCopyForum";
            this.tsbCopyForum.Size = new System.Drawing.Size(23, 22);
            this.tsbCopyForum.Text = "Copy in Forum Format";
            this.tsbCopyForum.Click += new System.EventHandler(this.tsbCopyForum_Click);
            // 
            // PlanOrderEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvSkills);
            this.Controls.Add(this.tsToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PlanOrderEditorControl";
            this.Size = new System.Drawing.Size(683, 558);
            this.tsToolStrip.ResumeLayout(false);
            this.tsToolStrip.PerformLayout();
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
        private System.Windows.Forms.ToolStrip tsToolStrip;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ContextMenuStrip cmsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem miRemoveFromPlan;
        private System.Windows.Forms.SaveFileDialog sfdSave;
        private System.Windows.Forms.ToolStripButton tsbCopyForum;
    }
}

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
            this.cmsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miRemoveFromPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.tmrTick = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lvSkills = new EveCharacterMonitor.SkillPlanner.DraggableListView();
            this.cmsContextMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(38, 558);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(35, 13);
            this.toolStripLabel1.Text = "Move:";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(35, 20);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(35, 20);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(35, 6);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(35, 13);
            this.toolStripLabel2.Text = "Add:";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(35, 20);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(38, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 21);
            this.panel1.TabIndex = 11;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(551, 5);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(91, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Select Columns...";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lvSkills
            // 
            this.lvSkills.AllowColumnReorder = true;
            this.lvSkills.AllowDrop = true;
            this.lvSkills.AllowRowReorder = true;
            this.lvSkills.ContextMenuStrip = this.cmsContextMenu;
            this.lvSkills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSkills.FullRowSelect = true;
            this.lvSkills.Location = new System.Drawing.Point(38, 21);
            this.lvSkills.Name = "lvSkills";
            this.lvSkills.Size = new System.Drawing.Size(645, 537);
            this.lvSkills.TabIndex = 3;
            this.lvSkills.UseCompatibleStateImageBehavior = false;
            this.lvSkills.View = System.Windows.Forms.View.Details;
            this.lvSkills.ListViewItemsDragged += new System.EventHandler<System.EventArgs>(this.lvSkills_ListViewItemsDragged);
            this.lvSkills.ListViewItemsDragging += new System.EventHandler<EveCharacterMonitor.SkillPlanner.ListViewDragEventArgs>(this.lvSkills_ListViewItemsDragging);
            this.lvSkills.ColumnReordered += new System.Windows.Forms.ColumnReorderedEventHandler(this.lvSkills_ColumnReordered);
            // 
            // PlanOrderEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvSkills);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PlanOrderEditorControl";
            this.Size = new System.Drawing.Size(683, 558);
            this.cmsContextMenu.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EveCharacterMonitor.SkillPlanner.DraggableListView lvSkills;
        private System.Windows.Forms.ContextMenuStrip cmsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem miRemoveFromPlan;
        private System.Windows.Forms.SaveFileDialog sfdSave;
        private System.Windows.Forms.Timer tmrTick;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

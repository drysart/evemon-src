namespace EVEMon
{
    partial class ManualImplantWindow
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
            this.lvImplants = new System.Windows.Forms.ListView();
            this.chAttribute = new System.Windows.Forms.ColumnHeader();
            this.chAdjust = new System.Windows.Forms.ColumnHeader();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.cmsImplantCommands = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miModify = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditSep = new System.Windows.Forms.ToolStripSeparator();
            this.miAddNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmsImplantCommands.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvImplants
            // 
            this.lvImplants.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvImplants.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAttribute,
            this.chAdjust,
            this.chName});
            this.lvImplants.ContextMenuStrip = this.cmsImplantCommands;
            this.lvImplants.FullRowSelect = true;
            this.lvImplants.Location = new System.Drawing.Point(15, 15);
            this.lvImplants.MinimumSize = new System.Drawing.Size(400, 175);
            this.lvImplants.Name = "lvImplants";
            this.lvImplants.Size = new System.Drawing.Size(426, 247);
            this.lvImplants.TabIndex = 0;
            this.lvImplants.UseCompatibleStateImageBehavior = false;
            this.lvImplants.View = System.Windows.Forms.View.Details;
            this.lvImplants.DoubleClick += new System.EventHandler(this.lvImplants_DoubleClick);
            this.lvImplants.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvImplants_KeyDown);
            this.lvImplants.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvImplants_KeyPress);
            // 
            // chAttribute
            // 
            this.chAttribute.Text = "Attribute";
            this.chAttribute.Width = 74;
            // 
            // chAdjust
            // 
            this.chAdjust.Text = "Adjust";
            this.chAdjust.Width = 57;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 255;
            // 
            // cmsImplantCommands
            // 
            this.cmsImplantCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miModify,
            this.miDelete,
            this.msEditSep,
            this.miAddNew});
            this.cmsImplantCommands.Name = "cmsImplantCommands";
            this.cmsImplantCommands.Size = new System.Drawing.Size(180, 76);
            this.cmsImplantCommands.Opening += new System.ComponentModel.CancelEventHandler(this.cmsImplantCommands_Opening);
            // 
            // miModify
            // 
            this.miModify.Name = "miModify";
            this.miModify.Size = new System.Drawing.Size(179, 22);
            this.miModify.Text = "Modify Implant...";
            this.miModify.Click += new System.EventHandler(this.miModify_Click);
            // 
            // miDelete
            // 
            this.miDelete.Name = "miDelete";
            this.miDelete.Size = new System.Drawing.Size(179, 22);
            this.miDelete.Text = "Delete Implant...";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // msEditSep
            // 
            this.msEditSep.Name = "msEditSep";
            this.msEditSep.Size = new System.Drawing.Size(176, 6);
            // 
            // miAddNew
            // 
            this.miAddNew.Name = "miAddNew";
            this.miAddNew.Size = new System.Drawing.Size(179, 22);
            this.miAddNew.Text = "Add New Implant...";
            this.miAddNew.Click += new System.EventHandler(this.miAddNew_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lvImplants, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(12);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(456, 326);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.btnOk);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(282, 285);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(162, 29);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(84, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 265);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tip: Right-click to add, change, or remove implants.";
            // 
            // ManualImplantWindow
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(456, 326);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(464, 360);
            this.Name = "ManualImplantWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manual Implants";
            this.Load += new System.EventHandler(this.ManualImplantWindow_Load);
            this.cmsImplantCommands.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvImplants;
        private System.Windows.Forms.ColumnHeader chAttribute;
        private System.Windows.Forms.ColumnHeader chAdjust;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cmsImplantCommands;
        private System.Windows.Forms.ToolStripMenuItem miModify;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.ToolStripSeparator msEditSep;
        private System.Windows.Forms.ToolStripMenuItem miAddNew;
    }
}

namespace EVEMon.SkillPlanner
{
    partial class SkillSelectControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillSelectControl));
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.pbSearchImage = new System.Windows.Forms.PictureBox();
            this.tvSkillList = new System.Windows.Forms.TreeView();
            this.lbSearchList = new System.Windows.Forms.ListBox();
            this.lblSearchTip = new System.Windows.Forms.Label();
            this.lblNoMatches = new System.Windows.Forms.Label();
            this.cbShowNonPublic = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSorting = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lvSortedSkillList = new System.Windows.Forms.ListView();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chSortKey = new System.Windows.Forms.ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).BeginInit();
            this.SuspendLayout();
            // 
            // cbFilter
            // 
            this.cbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "All Skills",
            "Known Skills",
            "Not Known Skills",
            "Planned Skills",
            "Level I Ready Skills",
            "Trainable Skills"});
            this.cbFilter.Location = new System.Drawing.Point(38, 0);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(125, 21);
            this.cbFilter.TabIndex = 0;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Location = new System.Drawing.Point(22, 54);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(141, 21);
            this.tbSearch.TabIndex = 1;
            this.tbSearch.Enter += new System.EventHandler(this.tbSearch_Enter);
            this.tbSearch.Leave += new System.EventHandler(this.tbSearch_Leave);
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // pbSearchImage
            // 
            this.pbSearchImage.Image = ((System.Drawing.Image)(resources.GetObject("pbSearchImage.Image")));
            this.pbSearchImage.InitialImage = null;
            this.pbSearchImage.Location = new System.Drawing.Point(0, 54);
            this.pbSearchImage.Name = "pbSearchImage";
            this.pbSearchImage.Size = new System.Drawing.Size(16, 21);
            this.pbSearchImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearchImage.TabIndex = 19;
            this.pbSearchImage.TabStop = false;
            // 
            // tvSkillList
            // 
            this.tvSkillList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSkillList.Location = new System.Drawing.Point(0, 81);
            this.tvSkillList.Name = "tvSkillList";
            this.tvSkillList.Size = new System.Drawing.Size(163, 232);
            this.tvSkillList.TabIndex = 20;
            this.tvSkillList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSkillList_AfterSelect);
            // 
            // lbSearchList
            // 
            this.lbSearchList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSearchList.FormattingEnabled = true;
            this.lbSearchList.IntegralHeight = false;
            this.lbSearchList.Location = new System.Drawing.Point(65, 220);
            this.lbSearchList.Name = "lbSearchList";
            this.lbSearchList.Size = new System.Drawing.Size(93, 82);
            this.lbSearchList.TabIndex = 21;
            this.lbSearchList.Visible = false;
            this.lbSearchList.SelectedIndexChanged += new System.EventHandler(this.lbSearchList_SelectedIndexChanged);
            // 
            // lblSearchTip
            // 
            this.lblSearchTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearchTip.BackColor = System.Drawing.SystemColors.Window;
            this.lblSearchTip.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblSearchTip.Location = new System.Drawing.Point(24, 56);
            this.lblSearchTip.Name = "lblSearchTip";
            this.lblSearchTip.Size = new System.Drawing.Size(137, 17);
            this.lblSearchTip.TabIndex = 22;
            this.lblSearchTip.Text = "Search Text";
            this.lblSearchTip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSearchTip.Click += new System.EventHandler(this.lblSearchTip_Click);
            // 
            // lblNoMatches
            // 
            this.lblNoMatches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNoMatches.BackColor = System.Drawing.SystemColors.Window;
            this.lblNoMatches.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblNoMatches.Location = new System.Drawing.Point(5, 111);
            this.lblNoMatches.Name = "lblNoMatches";
            this.lblNoMatches.Padding = new System.Windows.Forms.Padding(5);
            this.lblNoMatches.Size = new System.Drawing.Size(153, 106);
            this.lblNoMatches.TabIndex = 23;
            this.lblNoMatches.Text = "No skills match your search.";
            this.lblNoMatches.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblNoMatches.Visible = false;
            // 
            // cbShowNonPublic
            // 
            this.cbShowNonPublic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbShowNonPublic.AutoSize = true;
            this.cbShowNonPublic.Location = new System.Drawing.Point(0, 317);
            this.cbShowNonPublic.Name = "cbShowNonPublic";
            this.cbShowNonPublic.Size = new System.Drawing.Size(130, 17);
            this.cbShowNonPublic.TabIndex = 24;
            this.cbShowNonPublic.Text = "Show Non-Public Skills";
            this.cbShowNonPublic.UseVisualStyleBackColor = true;
            this.cbShowNonPublic.CheckedChanged += new System.EventHandler(this.cbShowNonPublic_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Filter:";
            // 
            // cbSorting
            // 
            this.cbSorting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSorting.FormattingEnabled = true;
            this.cbSorting.Items.AddRange(new object[] {
            "No Sorting",
            "Time to Next Level",
            "Time to Level V"});
            this.cbSorting.Location = new System.Drawing.Point(38, 27);
            this.cbSorting.Name = "cbSorting";
            this.cbSorting.Size = new System.Drawing.Size(125, 21);
            this.cbSorting.TabIndex = 26;
            this.cbSorting.SelectedIndexChanged += new System.EventHandler(this.cbSorting_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Sort:";
            // 
            // lvSortedSkillList
            // 
            this.lvSortedSkillList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSortedSkillList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chSortKey});
            this.lvSortedSkillList.FullRowSelect = true;
            this.lvSortedSkillList.Location = new System.Drawing.Point(30, 184);
            this.lvSortedSkillList.Name = "lvSortedSkillList";
            this.lvSortedSkillList.Size = new System.Drawing.Size(100, 89);
            this.lvSortedSkillList.TabIndex = 28;
            this.lvSortedSkillList.UseCompatibleStateImageBehavior = false;
            this.lvSortedSkillList.View = System.Windows.Forms.View.Details;
            this.lvSortedSkillList.Visible = false;
            this.lvSortedSkillList.SelectedIndexChanged += new System.EventHandler(this.lvSortedSkillList_SelectedIndexChanged);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            // 
            // chSortKey
            // 
            this.chSortKey.Text = "Sort";
            // 
            // SkillSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNoMatches);
            this.Controls.Add(this.lvSortedSkillList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSorting);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbShowNonPublic);
            this.Controls.Add(this.lblSearchTip);
            this.Controls.Add(this.lbSearchList);
            this.Controls.Add(this.tvSkillList);
            this.Controls.Add(this.pbSearchImage);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.cbFilter);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SkillSelectControl";
            this.Size = new System.Drawing.Size(163, 334);
            this.Load += new System.EventHandler(this.SkillSelectControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.PictureBox pbSearchImage;
        private System.Windows.Forms.TreeView tvSkillList;
        private System.Windows.Forms.ListBox lbSearchList;
        private System.Windows.Forms.Label lblSearchTip;
        private System.Windows.Forms.Label lblNoMatches;
        private System.Windows.Forms.CheckBox cbShowNonPublic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSorting;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvSortedSkillList;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chSortKey;
    }
}

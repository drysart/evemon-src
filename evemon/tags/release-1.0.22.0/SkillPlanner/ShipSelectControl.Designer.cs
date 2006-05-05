namespace EVEMon.SkillPlanner
{
    partial class ShipSelectControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShipSelectControl));
            this.pbSearchImage = new System.Windows.Forms.PictureBox();
            this.tbSearchText = new System.Windows.Forms.TextBox();
            this.tvShips = new System.Windows.Forms.TreeView();
            this.lbSearchTextHint = new System.Windows.Forms.Label();
            this.lbNoMatches = new System.Windows.Forms.Label();
            this.lbShipResults = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSearchImage
            // 
            this.pbSearchImage.Image = ((System.Drawing.Image)(resources.GetObject("pbSearchImage.Image")));
            this.pbSearchImage.InitialImage = null;
            this.pbSearchImage.Location = new System.Drawing.Point(0, 0);
            this.pbSearchImage.Name = "pbSearchImage";
            this.pbSearchImage.Size = new System.Drawing.Size(16, 21);
            this.pbSearchImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSearchImage.TabIndex = 20;
            this.pbSearchImage.TabStop = false;
            // 
            // tbSearchText
            // 
            this.tbSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchText.Location = new System.Drawing.Point(22, 0);
            this.tbSearchText.Name = "tbSearchText";
            this.tbSearchText.Size = new System.Drawing.Size(175, 21);
            this.tbSearchText.TabIndex = 21;
            this.tbSearchText.Enter += new System.EventHandler(this.tbSearchText_Enter);
            this.tbSearchText.Leave += new System.EventHandler(this.tbSearchText_Leave);
            this.tbSearchText.TextChanged += new System.EventHandler(this.tbSearchText_TextChanged);
            // 
            // tvShips
            // 
            this.tvShips.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvShips.Location = new System.Drawing.Point(0, 27);
            this.tvShips.Name = "tvShips";
            this.tvShips.Size = new System.Drawing.Size(197, 471);
            this.tvShips.TabIndex = 22;
            this.tvShips.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvShips_AfterSelect);
            // 
            // lbSearchTextHint
            // 
            this.lbSearchTextHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSearchTextHint.BackColor = System.Drawing.SystemColors.Window;
            this.lbSearchTextHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbSearchTextHint.Location = new System.Drawing.Point(24, 2);
            this.lbSearchTextHint.Name = "lbSearchTextHint";
            this.lbSearchTextHint.Size = new System.Drawing.Size(171, 17);
            this.lbSearchTextHint.TabIndex = 23;
            this.lbSearchTextHint.Text = "Search Text";
            this.lbSearchTextHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbSearchTextHint.Click += new System.EventHandler(this.lbSearchTextHint_Click);
            // 
            // lbNoMatches
            // 
            this.lbNoMatches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNoMatches.BackColor = System.Drawing.SystemColors.Window;
            this.lbNoMatches.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbNoMatches.Location = new System.Drawing.Point(3, 39);
            this.lbNoMatches.Name = "lbNoMatches";
            this.lbNoMatches.Size = new System.Drawing.Size(191, 54);
            this.lbNoMatches.TabIndex = 24;
            this.lbNoMatches.Text = "No ships match your search.";
            this.lbNoMatches.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbNoMatches.Visible = false;
            // 
            // lbShipResults
            // 
            this.lbShipResults.FormattingEnabled = true;
            this.lbShipResults.Location = new System.Drawing.Point(40, 76);
            this.lbShipResults.Name = "lbShipResults";
            this.lbShipResults.Size = new System.Drawing.Size(72, 82);
            this.lbShipResults.TabIndex = 25;
            this.lbShipResults.Visible = false;
            this.lbShipResults.SelectedIndexChanged += new System.EventHandler(this.lbShipResults_SelectedIndexChanged);
            // 
            // ShipSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbNoMatches);
            this.Controls.Add(this.lbShipResults);
            this.Controls.Add(this.lbSearchTextHint);
            this.Controls.Add(this.tvShips);
            this.Controls.Add(this.tbSearchText);
            this.Controls.Add(this.pbSearchImage);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ShipSelectControl";
            this.Size = new System.Drawing.Size(197, 498);
            this.Load += new System.EventHandler(this.ShipSelectControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSearchImage;
        private System.Windows.Forms.TextBox tbSearchText;
        private System.Windows.Forms.TreeView tvShips;
        private System.Windows.Forms.Label lbSearchTextHint;
        private System.Windows.Forms.Label lbNoMatches;
        private System.Windows.Forms.ListBox lbShipResults;
    }
}

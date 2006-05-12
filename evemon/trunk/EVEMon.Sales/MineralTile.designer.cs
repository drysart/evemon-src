namespace EVEMon.Sales
{
    partial class MineralTile
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
            this.lblName = new System.Windows.Forms.Label();
            this.detailPanel = new System.Windows.Forms.Panel();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtLastSell = new System.Windows.Forms.TextBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblLastPrice = new System.Windows.Forms.Label();
            this.icon = new System.Windows.Forms.PictureBox();
            this.detailPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(87)))));
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(79, 0);
            this.lblName.Margin = new System.Windows.Forms.Padding(3);
            this.lblName.Name = "lblName";
            this.lblName.Padding = new System.Windows.Forms.Padding(3);
            this.lblName.Size = new System.Drawing.Size(179, 25);
            this.lblName.TabIndex = 0;
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // detailPanel
            // 
            this.detailPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(56)))));
            this.detailPanel.Controls.Add(this.txtStock);
            this.detailPanel.Controls.Add(this.lblName);
            this.detailPanel.Controls.Add(this.txtLastSell);
            this.detailPanel.Controls.Add(this.lblQuantity);
            this.detailPanel.Controls.Add(this.lblLastPrice);
            this.detailPanel.Controls.Add(this.icon);
            this.detailPanel.Location = new System.Drawing.Point(3, 3);
            this.detailPanel.Name = "detailPanel";
            this.detailPanel.Size = new System.Drawing.Size(258, 76);
            this.detailPanel.TabIndex = 1;
            // 
            // txtStock
            // 
            this.txtStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStock.Location = new System.Drawing.Point(165, 50);
            this.txtStock.Margin = new System.Windows.Forms.Padding(0);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(90, 20);
            this.txtStock.TabIndex = 3;
            this.txtStock.Text = "0";
            this.txtStock.TextChanged += new System.EventHandler(this.txtStock_TextChanged);
            // 
            // txtLastSell
            // 
            this.txtLastSell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastSell.Location = new System.Drawing.Point(165, 29);
            this.txtLastSell.Margin = new System.Windows.Forms.Padding(0);
            this.txtLastSell.Name = "txtLastSell";
            this.txtLastSell.Size = new System.Drawing.Size(90, 20);
            this.txtLastSell.TabIndex = 2;
            this.txtLastSell.Text = "0";
            this.txtLastSell.TextChanged += new System.EventHandler(this.txtLastSell_TextChanged);
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.ForeColor = System.Drawing.Color.White;
            this.lblQuantity.Location = new System.Drawing.Point(80, 50);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(68, 20);
            this.lblQuantity.TabIndex = 2;
            this.lblQuantity.Text = "Quantity";
            // 
            // lblLastPrice
            // 
            this.lblLastPrice.AutoSize = true;
            this.lblLastPrice.ForeColor = System.Drawing.Color.White;
            this.lblLastPrice.Location = new System.Drawing.Point(80, 29);
            this.lblLastPrice.Name = "lblLastPrice";
            this.lblLastPrice.Size = new System.Drawing.Size(80, 20);
            this.lblLastPrice.TabIndex = 2;
            this.lblLastPrice.Text = "Sell Value";
            // 
            // icon
            // 
            this.icon.InitialImage = null;
            this.icon.Location = new System.Drawing.Point(6, 6);
            this.icon.Margin = new System.Windows.Forms.Padding(6);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(64, 64);
            this.icon.TabIndex = 2;
            this.icon.TabStop = false;
            // 
            // MineralTile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.Controls.Add(this.detailPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MineralTile";
            this.Size = new System.Drawing.Size(264, 83);
            this.detailPanel.ResumeLayout(false);
            this.detailPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel detailPanel;
        private System.Windows.Forms.PictureBox icon;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblLastPrice;
        private System.Windows.Forms.TextBox txtLastSell;
        private System.Windows.Forms.TextBox txtStock;
    }
}

namespace EVEMon.Sales
{
    partial class MineralWorksheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MineralWorksheet));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnLockPrices = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddFetch = new System.Windows.Forms.ToolStripDropDownButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mt_tritanium = new EVEMon.Sales.MineralTile();
            this.mt_nocxium = new EVEMon.Sales.MineralTile();
            this.mt_morphite = new EVEMon.Sales.MineralTile();
            this.mt_zydrine = new EVEMon.Sales.MineralTile();
            this.mt_isogen = new EVEMon.Sales.MineralTile();
            this.mt_megacyte = new EVEMon.Sales.MineralTile();
            this.mt_pyerite = new EVEMon.Sales.MineralTile();
            this.mt_mexallon = new EVEMon.Sales.MineralTile();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.mineralTile1 = new EVEMon.Sales.MineralTile();
            this.mineralTile2 = new EVEMon.Sales.MineralTile();
            this.mineralTile3 = new EVEMon.Sales.MineralTile();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLockPrices,
            this.toolStripSeparator1,
            this.tsddFetch});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(654, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnLockPrices
            // 
            this.btnLockPrices.Image = ((System.Drawing.Image)(resources.GetObject("btnLockPrices.Image")));
            this.btnLockPrices.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLockPrices.Name = "btnLockPrices";
            this.btnLockPrices.Size = new System.Drawing.Size(79, 22);
            this.btnLockPrices.Text = "Lock Prices";
            this.btnLockPrices.Click += new System.EventHandler(this.btnLockPrices_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsddFetch
            // 
            this.tsddFetch.Image = ((System.Drawing.Image)(resources.GetObject("tsddFetch.Image")));
            this.tsddFetch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddFetch.Name = "tsddFetch";
            this.tsddFetch.Size = new System.Drawing.Size(127, 22);
            this.tsddFetch.Text = "Fetch Online Prices";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.mt_tritanium, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mt_nocxium, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.mt_morphite, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.mt_zydrine, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.mt_isogen, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.mt_megacyte, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.mt_pyerite, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.mt_mexallon, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(654, 635);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // mt_tritanium
            // 
            this.mt_tritanium.AutoSize = true;
            this.mt_tritanium.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mt_tritanium.Location = new System.Drawing.Point(4, 4);
            this.mt_tritanium.Margin = new System.Windows.Forms.Padding(4);
            this.mt_tritanium.MineralName = "Tritanium";
            this.mt_tritanium.Name = "mt_tritanium";
            this.mt_tritanium.PriceLocked = false;
            this.mt_tritanium.PricePerUnit = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.mt_tritanium.Quantity = 0;
            this.mt_tritanium.Size = new System.Drawing.Size(261, 109);
            this.mt_tritanium.TabIndex = 1;
            // 
            // mt_nocxium
            // 
            this.mt_nocxium.AutoSize = true;
            this.mt_nocxium.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mt_nocxium.Location = new System.Drawing.Point(273, 4);
            this.mt_nocxium.Margin = new System.Windows.Forms.Padding(4);
            this.mt_nocxium.MineralName = "Nocxium";
            this.mt_nocxium.Name = "mt_nocxium";
            this.mt_nocxium.PriceLocked = false;
            this.mt_nocxium.PricePerUnit = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.mt_nocxium.Quantity = 0;
            this.mt_nocxium.Size = new System.Drawing.Size(261, 109);
            this.mt_nocxium.TabIndex = 5;
            // 
            // mt_morphite
            // 
            this.mt_morphite.AutoSize = true;
            this.mt_morphite.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mt_morphite.Location = new System.Drawing.Point(273, 355);
            this.mt_morphite.Margin = new System.Windows.Forms.Padding(4);
            this.mt_morphite.MineralName = "Morphite";
            this.mt_morphite.Name = "mt_morphite";
            this.mt_morphite.PriceLocked = false;
            this.mt_morphite.PricePerUnit = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.mt_morphite.Quantity = 0;
            this.mt_morphite.Size = new System.Drawing.Size(261, 109);
            this.mt_morphite.TabIndex = 8;
            // 
            // mt_zydrine
            // 
            this.mt_zydrine.AutoSize = true;
            this.mt_zydrine.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mt_zydrine.Location = new System.Drawing.Point(273, 121);
            this.mt_zydrine.Margin = new System.Windows.Forms.Padding(4);
            this.mt_zydrine.MineralName = "Zydrine";
            this.mt_zydrine.Name = "mt_zydrine";
            this.mt_zydrine.PriceLocked = false;
            this.mt_zydrine.PricePerUnit = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.mt_zydrine.Quantity = 0;
            this.mt_zydrine.Size = new System.Drawing.Size(261, 109);
            this.mt_zydrine.TabIndex = 6;
            // 
            // mt_isogen
            // 
            this.mt_isogen.AutoSize = true;
            this.mt_isogen.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mt_isogen.Location = new System.Drawing.Point(4, 355);
            this.mt_isogen.Margin = new System.Windows.Forms.Padding(4);
            this.mt_isogen.MineralName = "Isogen";
            this.mt_isogen.Name = "mt_isogen";
            this.mt_isogen.PriceLocked = false;
            this.mt_isogen.PricePerUnit = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.mt_isogen.Quantity = 0;
            this.mt_isogen.Size = new System.Drawing.Size(261, 109);
            this.mt_isogen.TabIndex = 4;
            // 
            // mt_megacyte
            // 
            this.mt_megacyte.AutoSize = true;
            this.mt_megacyte.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mt_megacyte.Location = new System.Drawing.Point(273, 238);
            this.mt_megacyte.Margin = new System.Windows.Forms.Padding(4);
            this.mt_megacyte.MineralName = "Megacyte";
            this.mt_megacyte.Name = "mt_megacyte";
            this.mt_megacyte.PriceLocked = false;
            this.mt_megacyte.PricePerUnit = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.mt_megacyte.Quantity = 0;
            this.mt_megacyte.Size = new System.Drawing.Size(261, 109);
            this.mt_megacyte.TabIndex = 7;
            // 
            // mt_pyerite
            // 
            this.mt_pyerite.AutoSize = true;
            this.mt_pyerite.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mt_pyerite.Location = new System.Drawing.Point(4, 121);
            this.mt_pyerite.Margin = new System.Windows.Forms.Padding(4);
            this.mt_pyerite.MineralName = "Pyerite";
            this.mt_pyerite.Name = "mt_pyerite";
            this.mt_pyerite.PriceLocked = false;
            this.mt_pyerite.PricePerUnit = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.mt_pyerite.Quantity = 0;
            this.mt_pyerite.Size = new System.Drawing.Size(261, 109);
            this.mt_pyerite.TabIndex = 2;
            this.mt_pyerite.Load += new System.EventHandler(this.mt_pyerite_Load);
            // 
            // mt_mexallon
            // 
            this.mt_mexallon.AutoSize = true;
            this.mt_mexallon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mt_mexallon.Location = new System.Drawing.Point(4, 238);
            this.mt_mexallon.Margin = new System.Windows.Forms.Padding(4);
            this.mt_mexallon.MineralName = "Mexallon";
            this.mt_mexallon.Name = "mt_mexallon";
            this.mt_mexallon.PriceLocked = false;
            this.mt_mexallon.PricePerUnit = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.mt_mexallon.Quantity = 0;
            this.mt_mexallon.Size = new System.Drawing.Size(261, 109);
            this.mt_mexallon.TabIndex = 3;
            this.mt_mexallon.Load += new System.EventHandler(this.mt_mexallon_Load);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tslTotal});
            this.statusStrip1.Location = new System.Drawing.Point(0, 660);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(654, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(64, 17);
            this.toolStripStatusLabel1.Text = "Total Value:";
            // 
            // tslTotal
            // 
            this.tslTotal.Image = ((System.Drawing.Image)(resources.GetObject("tslTotal.Image")));
            this.tslTotal.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
            this.tslTotal.Name = "tslTotal";
            this.tslTotal.Size = new System.Drawing.Size(64, 17);
            this.tslTotal.Text = "0.00 ISK";
            // 
            // mineralTile1
            // 
            this.mineralTile1.AutoSize = true;
            this.mineralTile1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mineralTile1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mineralTile1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mineralTile1.Location = new System.Drawing.Point(13, 29);
            this.mineralTile1.Margin = new System.Windows.Forms.Padding(4);
            this.mineralTile1.MineralName = "";
            this.mineralTile1.Name = "mineralTile1";
            this.mineralTile1.PriceLocked = false;
            this.mineralTile1.PricePerUnit = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.mineralTile1.Quantity = 0;
            this.mineralTile1.Size = new System.Drawing.Size(264, 83);
            this.mineralTile1.TabIndex = 0;
            // 
            // mineralTile2
            // 
            this.mineralTile2.AutoSize = true;
            this.mineralTile2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mineralTile2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mineralTile2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mineralTile2.Location = new System.Drawing.Point(13, 120);
            this.mineralTile2.Margin = new System.Windows.Forms.Padding(4);
            this.mineralTile2.MineralName = "";
            this.mineralTile2.Name = "mineralTile2";
            this.mineralTile2.PriceLocked = false;
            this.mineralTile2.PricePerUnit = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.mineralTile2.Quantity = 0;
            this.mineralTile2.Size = new System.Drawing.Size(264, 83);
            this.mineralTile2.TabIndex = 1;
            // 
            // mineralTile3
            // 
            this.mineralTile3.AutoSize = true;
            this.mineralTile3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mineralTile3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mineralTile3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mineralTile3.Location = new System.Drawing.Point(13, 211);
            this.mineralTile3.Margin = new System.Windows.Forms.Padding(4);
            this.mineralTile3.MineralName = "";
            this.mineralTile3.Name = "mineralTile3";
            this.mineralTile3.PriceLocked = false;
            this.mineralTile3.PricePerUnit = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.mineralTile3.Quantity = 0;
            this.mineralTile3.Size = new System.Drawing.Size(264, 83);
            this.mineralTile3.TabIndex = 2;
            // 
            // MineralWorksheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(654, 682);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MineralWorksheet";
            this.Text = "Mineral Worksheet";
            this.Load += new System.EventHandler(this.MineralWorksheet_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MineralTile mt_pyerite;
        private MineralTile mt_mexallon;
        private MineralTile mt_isogen;
        private MineralTile mt_nocxium;
        private MineralTile mt_megacyte;
        private MineralTile mt_morphite;
        private MineralTile mt_zydrine;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private MineralTile mineralTile1;
        private MineralTile mineralTile2;
        private MineralTile mineralTile3;
        private MineralTile mt_tritanium;
        private System.Windows.Forms.ToolStripButton btnLockPrices;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tsddFetch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tslTotal;

    }
}


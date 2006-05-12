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
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.phoenixIndustriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tslTotal = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSubtotals = new System.Windows.Forms.ToolStripButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.mt_tritanium = new EVEMon.Sales.MineralTile();
            this.mt_zydrine = new EVEMon.Sales.MineralTile();
            this.mt_morphite = new EVEMon.Sales.MineralTile();
            this.mt_megacyte = new EVEMon.Sales.MineralTile();
            this.mt_nocxium = new EVEMon.Sales.MineralTile();
            this.mt_isogen = new EVEMon.Sales.MineralTile();
            this.mt_mexallon = new EVEMon.Sales.MineralTile();
            this.mt_pyerite = new EVEMon.Sales.MineralTile();
            this.mineralTile1 = new EVEMon.Sales.MineralTile();
            this.mineralTile2 = new EVEMon.Sales.MineralTile();
            this.mineralTile3 = new EVEMon.Sales.MineralTile();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLockPrices,
            this.toolStripSeparator1,
            this.toolStripDropDownButton1,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.tslTotal,
            this.toolStripSeparator3,
            this.tsbSubtotals});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(562, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnLockPrices
            // 
            this.btnLockPrices.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnLockPrices.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLockPrices.Name = "btnLockPrices";
            this.btnLockPrices.Size = new System.Drawing.Size(63, 22);
            this.btnLockPrices.Text = "Lock Prices";
            this.btnLockPrices.Click += new System.EventHandler(this.btnLockPrices_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.phoenixIndustriesToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(78, 22);
            this.toolStripDropDownButton1.Text = "Fetch Prices";
            // 
            // phoenixIndustriesToolStripMenuItem
            // 
            this.phoenixIndustriesToolStripMenuItem.Name = "phoenixIndustriesToolStripMenuItem";
            this.phoenixIndustriesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.phoenixIndustriesToolStripMenuItem.Text = "Phoenix Industries";
            this.phoenixIndustriesToolStripMenuItem.Click += new System.EventHandler(this.phoenixIndustriesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel1.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(64, 22);
            this.toolStripLabel1.Text = "Total Value:";
            // 
            // tslTotal
            // 
            this.tslTotal.Image = ((System.Drawing.Image)(resources.GetObject("tslTotal.Image")));
            this.tslTotal.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tslTotal.Name = "tslTotal";
            this.tslTotal.Size = new System.Drawing.Size(29, 22);
            this.tslTotal.Text = "0";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSubtotals
            // 
            this.tsbSubtotals.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSubtotals.Image = ((System.Drawing.Image)(resources.GetObject("tsbSubtotals.Image")));
            this.tsbSubtotals.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSubtotals.Name = "tsbSubtotals";
            this.tsbSubtotals.Size = new System.Drawing.Size(85, 22);
            this.tsbSubtotals.Text = "Show Subtotals";
            this.tsbSubtotals.Click += new System.EventHandler(this.tsbSubtotals_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // mt_tritanium
            // 
            this.mt_tritanium.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mt_tritanium.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mt_tritanium.Location = new System.Drawing.Point(13, 29);
            this.mt_tritanium.Margin = new System.Windows.Forms.Padding(4);
            this.mt_tritanium.MineralName = "Tritanium";
            this.mt_tritanium.Name = "mt_tritanium";
            this.mt_tritanium.PriceLocked = false;
            this.mt_tritanium.Quantity = 0;
            this.mt_tritanium.SellPricePer = 0F;
            this.mt_tritanium.ShowSubtotals = false;
            this.mt_tritanium.Size = new System.Drawing.Size(264, 83);
            this.mt_tritanium.TabIndex = 1;
            // 
            // mt_zydrine
            // 
            this.mt_zydrine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mt_zydrine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mt_zydrine.Location = new System.Drawing.Point(285, 120);
            this.mt_zydrine.Margin = new System.Windows.Forms.Padding(4);
            this.mt_zydrine.MineralName = "Zydrine";
            this.mt_zydrine.Name = "mt_zydrine";
            this.mt_zydrine.PriceLocked = false;
            this.mt_zydrine.Quantity = 0;
            this.mt_zydrine.SellPricePer = 0F;
            this.mt_zydrine.ShowSubtotals = false;
            this.mt_zydrine.Size = new System.Drawing.Size(264, 83);
            this.mt_zydrine.TabIndex = 6;
            // 
            // mt_morphite
            // 
            this.mt_morphite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mt_morphite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mt_morphite.Location = new System.Drawing.Point(285, 302);
            this.mt_morphite.Margin = new System.Windows.Forms.Padding(4);
            this.mt_morphite.MineralName = "Morphite";
            this.mt_morphite.Name = "mt_morphite";
            this.mt_morphite.PriceLocked = false;
            this.mt_morphite.Quantity = 0;
            this.mt_morphite.SellPricePer = 0F;
            this.mt_morphite.ShowSubtotals = false;
            this.mt_morphite.Size = new System.Drawing.Size(264, 83);
            this.mt_morphite.TabIndex = 8;
            // 
            // mt_megacyte
            // 
            this.mt_megacyte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mt_megacyte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mt_megacyte.Location = new System.Drawing.Point(285, 211);
            this.mt_megacyte.Margin = new System.Windows.Forms.Padding(4);
            this.mt_megacyte.MineralName = "Megacyte";
            this.mt_megacyte.Name = "mt_megacyte";
            this.mt_megacyte.PriceLocked = false;
            this.mt_megacyte.Quantity = 0;
            this.mt_megacyte.SellPricePer = 0F;
            this.mt_megacyte.ShowSubtotals = false;
            this.mt_megacyte.Size = new System.Drawing.Size(264, 83);
            this.mt_megacyte.TabIndex = 7;
            // 
            // mt_nocxium
            // 
            this.mt_nocxium.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mt_nocxium.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mt_nocxium.Location = new System.Drawing.Point(285, 29);
            this.mt_nocxium.Margin = new System.Windows.Forms.Padding(4);
            this.mt_nocxium.MineralName = "Nocxium";
            this.mt_nocxium.Name = "mt_nocxium";
            this.mt_nocxium.PriceLocked = false;
            this.mt_nocxium.Quantity = 0;
            this.mt_nocxium.SellPricePer = 0F;
            this.mt_nocxium.ShowSubtotals = false;
            this.mt_nocxium.Size = new System.Drawing.Size(264, 83);
            this.mt_nocxium.TabIndex = 5;
            // 
            // mt_isogen
            // 
            this.mt_isogen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mt_isogen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mt_isogen.Location = new System.Drawing.Point(13, 302);
            this.mt_isogen.Margin = new System.Windows.Forms.Padding(4);
            this.mt_isogen.MineralName = "Isogen";
            this.mt_isogen.Name = "mt_isogen";
            this.mt_isogen.PriceLocked = false;
            this.mt_isogen.Quantity = 0;
            this.mt_isogen.SellPricePer = 0F;
            this.mt_isogen.ShowSubtotals = false;
            this.mt_isogen.Size = new System.Drawing.Size(264, 83);
            this.mt_isogen.TabIndex = 4;
            // 
            // mt_mexallon
            // 
            this.mt_mexallon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mt_mexallon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mt_mexallon.Location = new System.Drawing.Point(13, 211);
            this.mt_mexallon.Margin = new System.Windows.Forms.Padding(4);
            this.mt_mexallon.MineralName = "Mexallon";
            this.mt_mexallon.Name = "mt_mexallon";
            this.mt_mexallon.PriceLocked = false;
            this.mt_mexallon.Quantity = 0;
            this.mt_mexallon.SellPricePer = 0F;
            this.mt_mexallon.ShowSubtotals = false;
            this.mt_mexallon.Size = new System.Drawing.Size(264, 83);
            this.mt_mexallon.TabIndex = 3;
            // 
            // mt_pyerite
            // 
            this.mt_pyerite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mt_pyerite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mt_pyerite.Location = new System.Drawing.Point(13, 120);
            this.mt_pyerite.Margin = new System.Windows.Forms.Padding(4);
            this.mt_pyerite.MineralName = "Pyerite";
            this.mt_pyerite.Name = "mt_pyerite";
            this.mt_pyerite.PriceLocked = false;
            this.mt_pyerite.Quantity = 0;
            this.mt_pyerite.SellPricePer = 0F;
            this.mt_pyerite.ShowSubtotals = false;
            this.mt_pyerite.Size = new System.Drawing.Size(264, 83);
            this.mt_pyerite.TabIndex = 2;
            // 
            // mineralTile1
            // 
            this.mineralTile1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mineralTile1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mineralTile1.Location = new System.Drawing.Point(13, 29);
            this.mineralTile1.Margin = new System.Windows.Forms.Padding(4);
            this.mineralTile1.MineralName = "";
            this.mineralTile1.Name = "mineralTile1";
            this.mineralTile1.PriceLocked = false;
            this.mineralTile1.Quantity = 0;
            this.mineralTile1.SellPricePer = 0F;
            this.mineralTile1.ShowSubtotals = false;
            this.mineralTile1.Size = new System.Drawing.Size(264, 83);
            this.mineralTile1.TabIndex = 0;
            // 
            // mineralTile2
            // 
            this.mineralTile2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mineralTile2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mineralTile2.Location = new System.Drawing.Point(13, 120);
            this.mineralTile2.Margin = new System.Windows.Forms.Padding(4);
            this.mineralTile2.MineralName = "";
            this.mineralTile2.Name = "mineralTile2";
            this.mineralTile2.PriceLocked = false;
            this.mineralTile2.Quantity = 0;
            this.mineralTile2.SellPricePer = 0F;
            this.mineralTile2.ShowSubtotals = false;
            this.mineralTile2.Size = new System.Drawing.Size(264, 83);
            this.mineralTile2.TabIndex = 1;
            // 
            // mineralTile3
            // 
            this.mineralTile3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.mineralTile3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mineralTile3.Location = new System.Drawing.Point(13, 211);
            this.mineralTile3.Margin = new System.Windows.Forms.Padding(4);
            this.mineralTile3.MineralName = "";
            this.mineralTile3.Name = "mineralTile3";
            this.mineralTile3.PriceLocked = false;
            this.mineralTile3.Quantity = 0;
            this.mineralTile3.SellPricePer = 0F;
            this.mineralTile3.ShowSubtotals = false;
            this.mineralTile3.Size = new System.Drawing.Size(264, 83);
            this.mineralTile3.TabIndex = 2;
            // 
            // MineralWorksheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(562, 394);
            this.Controls.Add(this.mt_tritanium);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mt_zydrine);
            this.Controls.Add(this.mt_morphite);
            this.Controls.Add(this.mt_megacyte);
            this.Controls.Add(this.mt_nocxium);
            this.Controls.Add(this.mt_isogen);
            this.Controls.Add(this.mt_mexallon);
            this.Controls.Add(this.mt_pyerite);
            this.Name = "MineralWorksheet";
            this.Text = "Mineral Worksheet";
            this.Load += new System.EventHandler(this.MineralWorksheet_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem phoenixIndustriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel tslTotal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbSubtotals;

    }
}


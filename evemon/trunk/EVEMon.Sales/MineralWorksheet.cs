using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.Sales
{
    delegate void TileUpdate(MineralTile mt, Single s);
    public partial class MineralWorksheet : EVEMonForm
    {
        private MineralTile[] _tiles;
        private SubtotalChangedDelegate tileChangeHandler;
        private Settings m_settings;
        public MineralWorksheet()
        {
            InitializeComponent();
            
            _tiles = new MineralTile[]{
        this.mt_isogen, this.mt_megacyte, this.mt_mexallon,
        this.mt_morphite, this.mt_nocxium, this.mt_pyerite,
        this.mt_tritanium, this.mt_zydrine};

            tileChangeHandler = new SubtotalChangedDelegate(TileSubtotalChanged);

            foreach (MineralTile mt in _tiles)
            {
                mt.SubtotalChanged += tileChangeHandler;
            }            
        }

        private void TileSubtotalChanged(MineralTile tile,  float newValue)
        {
            float total = 0;
            foreach (MineralTile mt in _tiles)
            {
                total += mt.Subtotal;
            }
            tslTotal.Text = String.Format("{0:n2}", total);
        }

        public MineralWorksheet(Settings s)
            : this()
        {
            m_settings = s;
        }

        private void MineralWorksheet_Load(object sender, EventArgs e)
        {

        }

        private bool m_pricesLocked = false;

        private bool PricesLocked
        {
            get{
                return m_pricesLocked;
            }
            set
            {
                m_pricesLocked = value;
                mt_isogen.PriceLocked = value;
                mt_megacyte.PriceLocked = value;
                mt_mexallon.PriceLocked = value;
                mt_morphite.PriceLocked = value;
                mt_nocxium.PriceLocked = value;
                mt_pyerite.PriceLocked = value;
                mt_tritanium.PriceLocked = value;
                mt_zydrine.PriceLocked = value;
            }
        }

        private void btnLockPrices_Click(object sender, EventArgs e)
        {
            if (m_pricesLocked) {
                PricesLocked = false;
                btnLockPrices.Text = "Lock Prices";
            }
            else {
                PricesLocked = true;
                btnLockPrices.Text = "Unlock Prices";
            }
        }
        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            IMineralParser p = (IMineralParser)e.Argument;
            
            TileUpdate price = new TileUpdate(SetPrice);

            foreach (MineralTile mt in _tiles)
            {
                this.Invoke(price, new object[]{mt, p[mt.MineralName]});
            }
        }

        private void SetPrice(MineralTile tile, Single price)
        {
                tile.SellPricePer = price;
        }

        

        private bool m_subtotals = false;

        public bool Subtotals
        {
            get { return m_subtotals; }
            set { m_subtotals = value;
                foreach(MineralTile mt in _tiles)
                {
                    mt.ShowSubtotals = m_subtotals;
                }
                 }
        }
        private void tsbSubtotals_Click(object sender, EventArgs e)
        {
            if (m_subtotals)
            {
                Subtotals = false;
                tsbSubtotals.Text = "Show Subtotals";
            }
            else
            {
                Subtotals = true;
                tsbSubtotals.Text = "Hide Subtotals";
            }
        }

        private void tsbHelp_Click(object sender, EventArgs e)
        {
            Instructions inst = new Instructions();
            inst.Show();
        }

        private void matariMineralIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync(new MatariParser("http://www.evegeek.com/mineralindex.php"));
        }

        private void phoenixIndustriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync(new PhoenixParser("http://www.phoenix-industries.org"));
        }
    }
}
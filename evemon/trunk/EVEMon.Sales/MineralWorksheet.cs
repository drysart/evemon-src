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
        private EventHandler<EventArgs> tileChangeHandler;
        private Settings m_settings;

        public MineralWorksheet()
        {
            InitializeComponent();      
        }

        public MineralWorksheet(Settings s)
            : this()
        {
            m_settings = s;
        }

        private IEnumerable<MineralTile> Tiles
        {
            get
            {
                foreach (Control c in this.tableLayoutPanel1.Controls)
                {
                    if (c is MineralTile)
                    {
                        yield return c as MineralTile;
                    }
                }
            }
        }

        private void TileSubtotalChanged(object sender, EventArgs e)
        {
            Decimal total = 0;
            foreach (MineralTile mt in Tiles)
            {
                total += mt.Subtotal;
            }
            tslTotal.Text = total.ToString("N") + " ISK";
        }

        private void MineralWorksheet_Load(object sender, EventArgs e)
        {
            tileChangeHandler = new EventHandler<EventArgs>(TileSubtotalChanged);
            foreach (MineralTile mt in Tiles)
            {
                mt.SubtotalChanged += tileChangeHandler;
            }

            SortedList<string, Pair<string, string>> parsersSorted = new SortedList<string, Pair<string, string>>();

            foreach (Pair<string, IMineralParser> p in MineralDataRequest.Parsers)
            {
                parsersSorted.Add(p.B.Title, new Pair<string, string>(p.A, p.B.Title));
            }

            foreach (Pair<string, string> p in parsersSorted.Values)
            {
                ToolStripMenuItem mi = new ToolStripMenuItem();
                mi.Text = p.B;
                mi.Tag = p.A;
                mi.Click += new EventHandler(mi_Click);
                tsddFetch.DropDownItems.Add(mi);
            }
        }

        private void mi_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            string s = (string)mi.Tag;

            Dictionary<string, Decimal> prices = new Dictionary<string, decimal>();
            try
            {
                foreach (Pair<string, Decimal> p in MineralDataRequest.GetPrices(s))
                {
                    prices[p.A] = p.B;
                }
            }
            catch (MineralParserException mpe)
            {
                MessageBox.Show("Failed to retrieve mineral pricing data:\n" + mpe.Message,
                    "Failed to Retrieve Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (MineralTile mt in Tiles)
            {
                if (prices.ContainsKey(mt.MineralName))
                    mt.PricePerUnit = prices[mt.MineralName];
            }
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
                foreach (MineralTile mt in Tiles)
                {
                    mt.PriceLocked = value;
                }
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

        private void SetPrice(MineralTile tile, Single price)
        {
            //TODO
                //tile.PricePerUnit = price;
        }

        private void mt_pyerite_Load(object sender, EventArgs e)
        {

        }

        private void mt_mexallon_Load(object sender, EventArgs e)
        {

        }
    }
}
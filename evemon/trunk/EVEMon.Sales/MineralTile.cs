using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace EVEMon.Sales
{
    public delegate void SubtotalChangedDelegate(MineralTile sender, float newTotal);

    [Serializable]
    public partial class MineralTile : UserControl
    {
        public event SubtotalChangedDelegate SubtotalChanged;

        public MineralTile()
        {
            InitializeComponent();
        }

        private string m_mineralName;
        private bool m_showSubtotals = false;

        public bool ShowSubtotals
        {
            get { return m_showSubtotals; }
            set { 
                
                m_showSubtotals = value;
                if (m_showSubtotals)
                {
                    lblName.Text = Subtotal.ToString();
                }
                else
                {
                    lblName.Text = m_mineralName;
                }
            
            }
        }

        public Single Subtotal
        {
            get
            {
                try
                {
                    return Int32.Parse(txtStock.Text, System.Globalization.NumberStyles.AllowThousands) * Single.Parse(txtLastSell.Text, System.Globalization.NumberStyles.AllowDecimalPoint |
                        System.Globalization.NumberStyles.AllowThousands);
                }
                catch (FormatException)
                {
                    return 0;
                }
            }
        }
        

        public String MineralName
        {
            get
            {
                return m_mineralName;
            }
            set
            {
                m_mineralName = value;
                //try to do icon things
                this.icon.Image = (Image)Minerals.ResourceManager.GetObject(value);
                if (!m_showSubtotals)
                {
                    lblName.Text = m_mineralName;
                }
            }
        }

        public int Quantity
        {
            get
            {
                return Int32.Parse(txtStock.Text, System.Globalization.NumberStyles.AllowThousands);
            }
            set
            {
                txtStock.Text = String.Format("{0}", value);
                if(SubtotalChanged != null)
                    FireSubtotalChanged();
            }
        }

        public float SellPricePer
        {
            get
            {
                return Single.Parse(txtLastSell.Text, System.Globalization.NumberStyles.AllowDecimalPoint |
                    System.Globalization.NumberStyles.AllowThousands);
            }
            set
            {
                txtLastSell.Text = String.Format("{0}", value);
                if (SubtotalChanged != null)
                    FireSubtotalChanged();
            }
        }

        public bool PriceLocked
        {
            get
            {
                return txtLastSell.ReadOnly;
            }
            set
            {
                txtLastSell.TabStop = !value;
                txtStock.TabStop = value;
                txtLastSell.ReadOnly = value;
            }
        }

        private void FireSubtotalChanged()
        {
            if (m_showSubtotals)
            {
                lblName.Text = Subtotal.ToString();
            }
            SubtotalChanged(this, Subtotal);
        }

        private void txtLastSell_TextChanged(object sender, EventArgs e)
        {
            FireSubtotalChanged();
        }


    }
}

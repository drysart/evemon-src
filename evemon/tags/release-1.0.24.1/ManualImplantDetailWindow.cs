using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon
{
    public partial class ManualImplantDetailWindow : EVEMon.Common.EVEMonForm
    {
        public ManualImplantDetailWindow()
        {
            InitializeComponent();
        }

        public ManualImplantDetailWindow(GrandEveAttributeBonus b)
            : this()
        {
            m_inBonus = b;
        }

        private GrandEveAttributeBonus m_inBonus = null;

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ManualImplantDetailWindow_Load(object sender, EventArgs e)
        {
            cbAttribute.BeginUpdate();
            try
            {
                cbAttribute.Items.Clear();
                int aCount = Enum.GetValues(typeof(EveAttribute)).Length;
                for (int i=0; i<aCount; i++)
                {
                    EveAttribute a = (EveAttribute)i;
                    if (a != EveAttribute.None)
                    {
                        cbAttribute.Items.Add(a.ToString());
                    }
                }
                if (m_inBonus==null)
                    cbAttribute.SelectedIndex = 0;
            }
            finally
            {
                cbAttribute.EndUpdate();
            }

            if (m_inBonus != null)
            {
                cbAttribute.SelectedIndex = (int)m_inBonus.EveAttribute;
                nudAmount.Value = m_inBonus.Amount;
                tbName.Text = m_inBonus.Name;
            }

            CheckDisables();
        }

        private void cbAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckDisables();
        }

        private void nudAmount_ValueChanged(object sender, EventArgs e)
        {
            CheckDisables();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            CheckDisables();
        }

        private void CheckDisables()
        {
            btnOk.Enabled =
                cbAttribute.SelectedIndex >= 0 &&
                Convert.ToInt32(nudAmount.Value) > 0 &&
                !String.IsNullOrEmpty(tbName.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public GrandEveAttributeBonus ResultBonus
        {
            get { return new GrandEveAttributeBonus(m_name, m_attribute, m_amount, true); }
        }

        private EveAttribute m_attribute;
        private int m_amount;
        private string m_name;

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_attribute = (EveAttribute)cbAttribute.SelectedIndex;
            m_amount = Convert.ToInt32(nudAmount.Value);
            m_name = tbName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}


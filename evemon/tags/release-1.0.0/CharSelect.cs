using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EveCharacterMonitor
{
    public partial class CharSelect : Form
    {
        public CharSelect()
        {
            InitializeComponent();
        }

        public CharSelect(IEnumerable<string> charEnum)
            : this()
        {
            int c = 0;
            lbChars.Items.Clear();
            foreach (string s in charEnum)
            {
                c++;
                lbChars.Items.Add(s);
            }
            if (c == 1)
                m_result = lbChars.Items[0] as string;
        }

        private void lbChars_DoubleClick(object sender, EventArgs e)
        {
            HandleSelect();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            HandleSelect();
        }

        private string m_result;

        public string Result
        {
            get { return m_result; }
        }

        private void HandleSelect()
        {
            if (lbChars.SelectedItem != null)
            {
                this.DialogResult = DialogResult.OK;
                m_result = lbChars.SelectedItem as String;
                this.Close();
            }
        }

        private void lbChars_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelect.Enabled = (lbChars.SelectedItem != null);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EveCharacterMonitor.SkillPlanner
{
    public partial class NewPlanWindow : EVEMonForm
    {
        public NewPlanWindow()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private string m_result;

        public string Result
        {
            get { return m_result; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_result = textBox1.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = (!String.IsNullOrEmpty(textBox1.Text));
        }
    }
}
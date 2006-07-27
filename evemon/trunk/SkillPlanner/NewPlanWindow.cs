using System;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
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

        private string m_result = String.Empty;

        public string Result
        {
            get { return m_result; }
            set { m_result = value; }
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

        private void NewPlanWindow_Load(object sender, EventArgs e)
        {

        }

        private void NewPlanWindow_Shown(object sender, EventArgs e)
        {
            textBox1.Text = m_result;
            textBox1.SelectAll();
            m_result = String.Empty;
        }
    }
}

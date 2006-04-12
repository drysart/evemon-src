using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EveCharacterMonitor.SkillPlanner
{
    public partial class AddPlanConfirmWindow : Form
    {
        public AddPlanConfirmWindow()
        {
            InitializeComponent();
        }

        public AddPlanConfirmWindow(IEnumerable<PlanEntry> entries)
            : this()
        {
            m_entries = entries;
        }

        private IEnumerable<PlanEntry> m_entries;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddPlanConfirmWindow_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (PlanEntry pe in m_entries)
            {
                listBox1.Items.Add(pe.SkillName + " " + GrandSkill.GetRomanSkillNumber(pe.Level));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
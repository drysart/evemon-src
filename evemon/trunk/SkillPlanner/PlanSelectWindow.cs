using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EveCharacterMonitor.SkillPlanner
{
    public partial class PlanSelectWindow : Form
    {
        public PlanSelectWindow()
        {
            InitializeComponent();
        }

        public PlanSelectWindow(Settings s, GrandCharacterInfo gci)
            : this()
        {
            m_settings = s;
            m_grandCharacterInfo = gci;
        }

        private Settings m_settings;
        private GrandCharacterInfo m_grandCharacterInfo;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PlanSelectWindow_Load(object sender, EventArgs e)
        {
            lbPlanList.Items.Clear();
            lbPlanList.Items.Add("<New Plan>");

            foreach (string planName in m_settings.GetPlansForCharacter(m_grandCharacterInfo.Name))
            {
                lbPlanList.Items.Add(planName);
            }
        }

        private void lbPlanList_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOpen.Enabled = (lbPlanList.SelectedItem != null);
        }

        private Plan m_result;

        public Plan ResultPlan
        {
            get { return m_result; }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (lbPlanList.SelectedIndex == 0)
                m_result = null;
            else
            {
                string s = (string)lbPlanList.SelectedItem;
                m_result = m_settings.GetPlanByName(m_grandCharacterInfo.Name, s);
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lbPlanList_DoubleClick(object sender, EventArgs e)
        {
            if (lbPlanList.SelectedItems.Count > 0)
                btnOpen_Click(this, new EventArgs());
        }
    }
}
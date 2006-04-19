using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class PlanOrderEditorColumnSelectWindow : EVEMonForm
    {
        public PlanOrderEditorColumnSelectWindow()
        {
            InitializeComponent();
        }

        public PlanOrderEditorColumnSelectWindow(ColumnPreference pref)
            :this()
        {
            m_preference = pref;
        }

        private ColumnPreference m_preference;
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int ccount = ColumnPreference.ColumnCount;
            for (int i = 0; i < ccount; i++)
            {
                m_preference[i] = false;
            }
            for (int i = 0; i < clbColumns.CheckedIndices.Count; i++)
            {
                m_preference[clbColumns.CheckedIndices[i]] = true;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PlanOrderEditorColumnSelectWindow_Load(object sender, EventArgs e)
        {
            clbColumns.Items.Clear();

            int ccount = ColumnPreference.ColumnCount;
            for (int i = 0; i < ccount; i++)
            {
                clbColumns.Items.Add(ColumnPreference.GetDescription(i),
                    m_preference[i]);
            }
        }
    }
}
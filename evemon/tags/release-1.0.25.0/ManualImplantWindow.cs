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
    public partial class ManualImplantWindow : EVEMon.Common.EVEMonForm
    {
        public ManualImplantWindow()
        {
            InitializeComponent();
        }

        public ManualImplantWindow(IEnumerable<GrandEveAttributeBonus> bonuses)
            : this()
        {
            foreach (GrandEveAttributeBonus b in bonuses)
            {
                if (b.Manual)
                    workingList.Add(b);
            }
        }

        private List<GrandEveAttributeBonus> workingList = new List<GrandEveAttributeBonus>();

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_resultList = new List<GrandEveAttributeBonus>();
            foreach (ListViewItem lvi in lvImplants.Items)
            {
                m_resultList.Add(lvi.Tag as GrandEveAttributeBonus);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private List<GrandEveAttributeBonus> m_resultList = null;

        public IEnumerable<GrandEveAttributeBonus> ResultBonuses
        {
            get { return m_resultList; }
        }

        private void cmsImplantCommands_Opening(object sender, CancelEventArgs e)
        {
            miModify.Enabled = (lvImplants.SelectedItems.Count == 1);
            miModify.Visible = (lvImplants.SelectedItems.Count > 0);
            miDelete.Enabled = (lvImplants.SelectedItems.Count > 0);
            miDelete.Visible = (lvImplants.SelectedItems.Count > 0);
            msEditSep.Visible = (lvImplants.SelectedItems.Count > 0);
        }

        private void miAddNew_Click(object sender, EventArgs e)
        {
            using (ManualImplantDetailWindow f = new ManualImplantDetailWindow())
            {
                DialogResult dr = f.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    lvImplants.Items.Add(CreateItemForBonus(f.ResultBonus));
                }
            }
        }

        private ListViewItem CreateItemForBonus(GrandEveAttributeBonus b)
        {
            ListViewItem lvi = new ListViewItem(b.EveAttribute.ToString());
            lvi.Tag = b;
            lvi.SubItems.Add(b.Amount.ToString());
            lvi.SubItems.Add(b.Name);
            return lvi;
        }

        private void ManualImplantWindow_Load(object sender, EventArgs e)
        {
            lvImplants.BeginUpdate();
            try
            {
                foreach (GrandEveAttributeBonus b in workingList)
                {
                    ListViewItem lvi = CreateItemForBonus(b);
                    lvImplants.Items.Add(lvi);
                }
                chName.Width = -2;
                workingList = null;
            }
            finally
            {
                lvImplants.EndUpdate();
            }
        }

        private void miModify_Click(object sender, EventArgs e)
        {
            if (lvImplants.SelectedItems.Count != 1)
                return;

            ListViewItem lvi = lvImplants.SelectedItems[0];
            using (ManualImplantDetailWindow f = new ManualImplantDetailWindow(lvi.Tag as GrandEveAttributeBonus))
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    ListViewItem rlvi = CreateItemForBonus(f.ResultBonus);
                    lvImplants.Items[lvImplants.Items.IndexOf(lvi)] = rlvi;
                }
            }
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            if (lvImplants.SelectedItems.Count == 0)
                return;

            List<ListViewItem> removeItems = new List<ListViewItem>();
            DialogResult dr = DialogResult.No;
            if (lvImplants.SelectedItems.Count == 1)
            {
                ListViewItem lvi = lvImplants.SelectedItems[0];
                GrandEveAttributeBonus b = lvi.Tag as GrandEveAttributeBonus;
                dr = MessageBox.Show("Are you sure you want to delete \"" + b.Name + "\"?",
                    "Delete Implant?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                removeItems.Add(lvi);
            }
            else
            {
                dr = MessageBox.Show("Are you sure you want to delete these implants?",
                    "Delete Implants?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                foreach (ListViewItem lvi in lvImplants.SelectedItems)
                {
                    removeItems.Add(lvi);
                }
            }

            lvImplants.BeginUpdate();
            try
            {
                if (dr == DialogResult.Yes)
                {
                    foreach (ListViewItem lvi in removeItems)
                    {
                        lvImplants.Items.Remove(lvi);
                    }
                }
            }
            finally
            {
                lvImplants.EndUpdate();
            }
        }

        private void lvImplants_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void lvImplants_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                miDelete_Click(this, new EventArgs());
            }
        }

        private void lvImplants_DoubleClick(object sender, EventArgs e)
        {
            if (lvImplants.SelectedItems.Count > 0)
            {
                miModify_Click(this, new EventArgs());
            }
            else
            {
                miAddNew_Click(this, new EventArgs());
            }
        }
    }
}


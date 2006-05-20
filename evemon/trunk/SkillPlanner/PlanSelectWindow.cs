using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class PlanSelectWindow : EVEMonForm
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
            m_charKey = m_grandCharacterInfo.Name;
        }

        private Settings m_settings;
        private GrandCharacterInfo m_grandCharacterInfo;
        private string m_charKey;

        public string CharKey
        {
            get { return m_charKey; }
            set { m_charKey = value; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PlanSelectWindow_Load(object sender, EventArgs e)
        {
            PopulatePlanList();
        }

        private void PopulatePlanList()
        {
            lbPlanList.BeginUpdate();
            try
            {
                lbPlanList.Items.Clear();
                lbPlanList.Items.Add("<New Plan>");

                foreach (string planName in m_settings.GetPlansForCharacter(m_charKey))
                {
                    lbPlanList.Items.Add(planName);
                }
            }
            finally
            {
                lbPlanList.EndUpdate();
            }
        }

        private void lbPlanList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbPlanList.SelectionMode == SelectionMode.MultiExtended)
            {
                if (lbPlanList.SelectedIndices.Contains(0))
                {
                    lbPlanList.SelectionMode = SelectionMode.One;
                    lbPlanList.SelectedIndex = 0;
                }
            }
            else
            {
                if (lbPlanList.SelectedIndex != 0)
                {
                    lbPlanList.SelectionMode = SelectionMode.MultiExtended;
                }
            }

            btnOpen.Enabled = (lbPlanList.SelectedItem != null);
            tsbRenamePlan.Enabled = (lbPlanList.SelectedItem != null && lbPlanList.SelectedIndex > 0 && lbPlanList.SelectedItems.Count == 1);
            tsbDeletePlan.Enabled = (lbPlanList.SelectedItem != null && lbPlanList.SelectedIndex > 0 && lbPlanList.SelectedItems.Count == 1);
            btnOpen.Text = (lbPlanList.SelectedItems.Count > 1 ? "Merge" : "Open");

            if (lbPlanList.SelectedItem == null || lbPlanList.SelectedItems.Count > 1)
            {
                tsbMoveUp.Enabled = false;
                tsbMoveDown.Enabled = false;
            }
            else
            {
                int idx = lbPlanList.SelectedIndex;
                tsbMoveUp.Enabled = (idx > 1);
                tsbMoveDown.Enabled = (idx < lbPlanList.Items.Count - 1 && idx > 0);
            }
        }

        private Plan m_result;

        public Plan ResultPlan
        {
            get { return m_result; }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (lbPlanList.SelectedItems.Count > 1)
            {
                m_result = new Plan();

                foreach (object plan in lbPlanList.SelectedItems)
                {
                    string s = (string)plan;
                    Plan p = m_settings.GetPlanByName(m_charKey, s);
                    foreach (PlanEntry entry in p.Entries)
                    {
                        if (m_result.GetEntry(entry.SkillName, entry.Level) == null)
                        {
                            m_result.Entries.Add(entry);
                        }
                    }
                }
            }
            else if (lbPlanList.SelectedIndex == 0)
            {
                m_result = null;
            }
            else
            {
                string s = (string)lbPlanList.SelectedItem;
                m_result = m_settings.GetPlanByName(m_charKey, s);
            }
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lbPlanList_DoubleClick(object sender, EventArgs e)
        {
            if (lbPlanList.SelectedItems.Count > 0)
                btnOpen_Click(this, new EventArgs());
        }

        private void LoadPlan()
        {
            DialogResult dr = ofdOpenDialog.ShowDialog();
            if (dr == DialogResult.Cancel)
                return;

            try
            {
                Plan loadedPlan = null;
                using (Stream s = new FileStream(ofdOpenDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Plan));
                    try
                    {
                        loadedPlan = (Plan)xs.Deserialize(s);
                    }
                    catch
                    {
                        s.Seek(0, SeekOrigin.Begin);
                        using (System.IO.Compression.GZipStream gzs = new System.IO.Compression.GZipStream(s, System.IO.Compression.CompressionMode.Decompress))
                        {
                            try
                            {
                                loadedPlan = (Plan)xs.Deserialize(gzs);
                            }
                            catch
                            {
                                throw new ApplicationException("Could not determine input file format.");
                            }
                        }
                    }
                }

                using (NewPlanWindow npw = new NewPlanWindow())
                {
                    npw.Text = "Load Plan";
                    npw.Result = Path.GetFileNameWithoutExtension(ofdOpenDialog.FileName);
                    DialogResult xdr = npw.ShowDialog();
                    if (xdr == DialogResult.Cancel)
                        return;
                    string planName = npw.Result;
                    loadedPlan.GrandCharacterInfo = m_grandCharacterInfo;

                    m_settings.AddPlanFor(m_charKey, loadedPlan, planName);
                }
                
            }
            catch (Exception err)
            {
                MessageBox.Show("There was an error loading the saved plan:\n" + err.Message,
                    "Could Not Load Plan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PopulatePlanList();
        }

        private void tsbLoadPlan_Click(object sender, EventArgs e)
        {
            LoadPlan();
        }

        private void tsbRenamePlan_Click(object sender, EventArgs e)
        {
            using (NewPlanWindow f = new NewPlanWindow())
            {
                f.Text = "Rename Plan";
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;
                string oldName = (string)lbPlanList.SelectedItem;
                string newName = f.Result;
                if (oldName == newName)
                    return;
                if (m_settings.GetPlanByName(m_charKey, newName) != null)
                {
                    MessageBox.Show("A plan with that name already exists.",
                        "Duplicate Plan Name", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                m_settings.RenamePlanFor(m_charKey,
                    oldName, f.Result);

                PopulatePlanList();
            }
        }

        private void tsbMoveUp_Click(object sender, EventArgs e)
        {
            int idx = lbPlanList.SelectedIndex;
            List<string> newOrder = new List<string>();
            for (int i = 1; i < lbPlanList.Items.Count; i++)
            {
                if (i == idx - 1)
                    newOrder.Add((string)lbPlanList.SelectedItem);
                if (i != idx)
                    newOrder.Add((string)lbPlanList.Items[i]);
            }
            FinalizePlanReorder(idx - 1, newOrder);
        }

        private void FinalizePlanReorder(int idx, List<string> newOrder)
        {
            m_settings.RearrangePlansFor(m_charKey, newOrder);

            lbPlanList.BeginUpdate();
            try
            {
                PopulatePlanList();
                lbPlanList.SelectedIndex = idx;
            }
            finally
            {
                lbPlanList.EndUpdate();
            }
        }

        private void tsbMoveDown_Click(object sender, EventArgs e)
        {
            int idx = lbPlanList.SelectedIndex;
            List<string> newOrder = new List<string>();
            for (int i = 1; i < lbPlanList.Items.Count; i++)
            {
                if (i != idx)
                    newOrder.Add((string)lbPlanList.Items[i]);
                if (i == idx + 1)
                    newOrder.Add((string)lbPlanList.SelectedItem);
            }
            FinalizePlanReorder(idx + 1, newOrder);
        }

        private void tsbDeletePlan_Click(object sender, EventArgs e)
        {
            string planName = (string)lbPlanList.SelectedItem;

            DialogResult dr = MessageBox.Show("Are you sure you want to delete \"" + planName +
                "\"?", "Delete Plan", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr != DialogResult.Yes)
                return;

            m_settings.RemovePlanFor(m_charKey, planName);
            PopulatePlanList();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

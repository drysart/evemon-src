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

namespace EveCharacterMonitor.SkillPlanner
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
            PopulatePlanList();
        }

        private void PopulatePlanList()
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

        private void btnLoadFromFile_Click(object sender, EventArgs e)
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
                    DialogResult xdr = npw.ShowDialog();
                    if (xdr == DialogResult.Cancel)
                        return;
                    string planName = npw.Result;
                    loadedPlan.GrandCharacterInfo = m_grandCharacterInfo;
                    
                    m_settings.AddPlanFor(m_grandCharacterInfo.Name, loadedPlan, planName);
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
    }
}
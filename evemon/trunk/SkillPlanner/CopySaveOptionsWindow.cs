using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class CopySaveOptionsWindow : EVEMonForm
    {
        public CopySaveOptionsWindow()
        {
            InitializeComponent();
        }

        public CopySaveOptionsWindow(PlanTextOptions pto, Plan p, bool isForCopy)
            : this()
        {
            m_planTextOptions = pto;
            m_plan = p;
            m_isForCopy = isForCopy;
        }

        private PlanTextOptions m_planTextOptions;
        private Plan m_plan;
        private bool m_isForCopy = false;
        private bool m_setAsDefault = false;

        private void CopySaveOptionsWindow_Load(object sender, EventArgs e)
        {
            if (m_isForCopy)
                this.Text = "Copy Options";
            else
                this.Text = "Save Options";

            cbIncludeHeader.Checked = m_planTextOptions.IncludeHeader;
            cbEntryNumber.Checked = m_planTextOptions.EntryNumber;
            cbEntryTrainingTimes.Checked = m_planTextOptions.EntryTrainingTimes;
            cbEntryStartDate.Checked = m_planTextOptions.EntryStartDate;
            cbEntryFinishDate.Checked = m_planTextOptions.EntryFinishDate;
            cbFooterCount.Checked = m_planTextOptions.FooterCount;
            cbFooterTotalTime.Checked = m_planTextOptions.FooterTotalTime;
            cbFooterDate.Checked = m_planTextOptions.FooterDate;

            RecurseUnder(this);
            OptionChange();
        }

        private void RecurseUnder(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is CheckBox && c != cbRememberOptions)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.CheckedChanged += new EventHandler(cb_CheckedChanged);
                }
                RecurseUnder(c);
            }
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
            OptionChange();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void UpdateOptions()
        {
            m_planTextOptions.IncludeHeader = cbIncludeHeader.Checked;
            m_planTextOptions.EntryNumber = cbEntryNumber.Checked;
            m_planTextOptions.EntryTrainingTimes = cbEntryTrainingTimes.Checked;
            m_planTextOptions.EntryStartDate = cbEntryStartDate.Checked;
            m_planTextOptions.EntryFinishDate = cbEntryFinishDate.Checked;
            m_planTextOptions.FooterCount = cbFooterCount.Checked;
            m_planTextOptions.FooterTotalTime = cbFooterTotalTime.Checked;
            m_planTextOptions.FooterDate = cbFooterDate.Checked;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            UpdateOptions();

            m_setAsDefault = cbRememberOptions.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OptionChange()
        {
            UpdateOptions();
            using (MemoryStream ms = new MemoryStream())
            using (StreamWriter sw = new StreamWriter(ms))
            {
                m_plan.SaveAsText(sw, m_planTextOptions, m_isForCopy);
                sw.Flush();
                string s = Encoding.Default.GetString(ms.ToArray());
                tbPreview.Text = s;
            }
        }

        public bool SetAsDefault
        {
            get { return m_setAsDefault; }
        }
    }
}


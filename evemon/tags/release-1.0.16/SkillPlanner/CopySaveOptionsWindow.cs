using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace EveCharacterMonitor.SkillPlanner
{
    public partial class CopySaveOptionsWindow : EveCharacterMonitor.EVEMonForm
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
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(ms))
            {
                m_plan.SaveAsText(sw, m_planTextOptions, m_isForCopy);
                sw.Flush();
                string s = System.Text.Encoding.Default.GetString(ms.ToArray());
                tbPreview.Text = s;
            }
        }

        public bool SetAsDefault
        {
            get { return m_setAsDefault; }
        }
    }

    [XmlRoot]
    public class PlanTextOptions: ICloneable
    {
        private bool m_includeHeader = true;

        [XmlAttribute]
        public bool IncludeHeader
        {
            get { return m_includeHeader; }
            set { m_includeHeader = value; }
        }

        private bool m_entryNumber = true;

        [XmlAttribute]
        public bool EntryNumber
        {
            get { return m_entryNumber; }
            set { m_entryNumber = value; }
        }

        private bool m_entryTrainingTimes = true;

        [XmlAttribute]
        public bool EntryTrainingTimes
        {
            get { return m_entryTrainingTimes; }
            set { m_entryTrainingTimes = value; }
        }

        private bool m_entryStartDate = false;

        [XmlAttribute]
        public bool EntryStartDate
        {
            get { return m_entryStartDate; }
            set { m_entryStartDate = value; }
        }

        private bool m_entryFinishDate = false;

        [XmlAttribute]
        public bool EntryFinishDate
        {
            get { return m_entryFinishDate; }
            set { m_entryFinishDate = value; }
        }

        private bool m_footerCount = false;

        [XmlAttribute]
        public bool FooterCount
        {
            get { return m_footerCount; }
            set { m_footerCount = value; }
        }

        private bool m_footerTotalTime = false;

        [XmlAttribute]
        public bool FooterTotalTime
        {
            get { return m_footerTotalTime; }
            set { m_footerTotalTime = value; }
        }

        private bool m_footerDate = false;

        [XmlAttribute]
        public bool FooterDate
        {
            get { return m_footerDate; }
            set { m_footerDate = value; }
        }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace EVEMon.Common
{
    [XmlRoot]
    public class PlanTextOptions : ICloneable
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

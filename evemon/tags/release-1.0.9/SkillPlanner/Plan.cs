using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Serialization;
using EveCharacterMonitor;

namespace EveCharacterMonitor.SkillPlanner
{
    public class Plan
    {
        public Plan()
        {
            m_entries = new MonitoredList<PlanEntry>();
            m_entries.Changed += new EventHandler<ChangedEventArgs<PlanEntry>>(m_entries_Changed);
        }

        void m_entries_Changed(object sender, ChangedEventArgs<PlanEntry> e)
        {
            switch (e.ChangeType)
            {
                case ChangeType.Added:
                    e.Item.Plan = this;
                    break;
            }
            OnChange();
        }

        public event EventHandler<EventArgs> Changed;

        private void OnChange()
        {
            if (Changed != null)
                Changed(this, new EventArgs());
        }

        private MonitoredList<PlanEntry> m_entries;

        public IList<PlanEntry> Entries
        {
            get { return m_entries; }
        }

        private GrandCharacterInfo m_grandCharacterInfo = null;

        [XmlIgnore]
        public GrandCharacterInfo GrandCharacterInfo
        {
            get { return m_grandCharacterInfo; }
            set { m_grandCharacterInfo = value; }
        }
    }

    public enum PlanEntryType
    {
        Planned,
        Prerequisite
    }

    public class PlanEntry
    {
        private Plan m_owner;
        private string m_skillName;
        private int m_level;
        private PlanEntryType m_entryType;

        [XmlIgnore]
        public Plan Plan
        {
            get { return m_owner; }
            set { m_owner = value; }
        }

        public string SkillName
        {
            get { return m_skillName; }
            set { m_skillName = value; }
        }

        public int Level
        {
            get { return m_level; }
            set { m_level = value; }
        }

        public PlanEntryType EntryType
        {
            get { return m_entryType; }
            set { m_entryType = value; }
        }

        [XmlIgnore]
        public GrandSkill Skill
        {
            get
            {
                if (m_owner == null || m_owner.GrandCharacterInfo == null)
                    return null;
                return m_owner.GrandCharacterInfo.GetSkill(m_skillName);
            }
        }
    }
}

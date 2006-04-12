using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Serialization;
using EveCharacterMonitor;

namespace EveCharacterMonitor.SkillPlanner
{
    [XmlRoot("plan")]
    public class Plan
    {
        public Plan()
        {
            //m_entries = new MonitoredList<PlanEntry>();
            m_entries.Changed += new EventHandler<ChangedEventArgs<PlanEntry>>(m_entries_Changed);
        }

        void m_entries_Changed(object sender, ChangedEventArgs<PlanEntry> e)
        {
            m_uniqueSkillCount = -1;
            switch (e.ChangeType)
            {
                case ChangeType.Added:
                    e.Item.Plan = this;
                    break;
            }
            OnChange();
        }

        public event EventHandler<EventArgs> Changed;

        private delegate void FireEventInvoker();

        private object m_eventLock = new object();
        private int m_suppression = 0;
        private Queue<FireEventInvoker> m_firedEvents = new Queue<FireEventInvoker>();
        private Dictionary<string, bool> m_eventsInQueue = new Dictionary<string, bool>();

        public void SuppressEvents()
        {
            lock (m_eventLock)
            {
                m_suppression++;
            }
        }

        public void ResumeEvents()
        {
            lock (m_eventLock)
            {
                m_suppression--;
                if (m_suppression <= 0)
                {
                    m_suppression = 0;
                    while (m_firedEvents.Count > 0)
                    {
                        FireEventInvoker fei = m_firedEvents.Dequeue();
                        fei();
                    }
                    m_eventsInQueue.Clear();
                }
            }
        }

        private void FireEvent(FireEventInvoker fei, string key)
        {
            lock (m_eventLock)
            {
                if (m_suppression > 0)
                {
                    if (String.IsNullOrEmpty(key) || !m_eventsInQueue.ContainsKey(key))
                    {
                        m_firedEvents.Enqueue(fei);
                        if (!String.IsNullOrEmpty(key))
                            m_eventsInQueue.Add(key, true);
                    }
                }
                else
                    fei();
            }
        }

        private void OnChange()
        {
            FireEvent(delegate
            {
                if (Changed != null)
                    Changed(this, new EventArgs());
            }, "change");
        }

        private MonitoredList<PlanEntry> m_entries = new MonitoredList<PlanEntry>();

        [XmlArrayItem("entry")]
        public MonitoredList<PlanEntry> Entries
        {
            get { return m_entries; }
        }

        private int m_uniqueSkillCount = -1;

        [XmlIgnore]
        public int UniqueSkillCount
        {
            get
            {
                if (m_uniqueSkillCount == -1)
                    CountUniqueSkills();
                return m_uniqueSkillCount;
            }
        }

        private void CountUniqueSkills()
        {
            Dictionary<string, bool> counted = new Dictionary<string, bool>();
            m_uniqueSkillCount = 0;
            foreach (PlanEntry pe in m_entries)
            {
                if (!counted.ContainsKey(pe.SkillName))
                {
                    m_uniqueSkillCount++;
                    counted[pe.SkillName] = true;
                }
            }
        }

        private GrandCharacterInfo m_grandCharacterInfo = null;

        [XmlIgnore]
        public GrandCharacterInfo GrandCharacterInfo
        {
            get { return m_grandCharacterInfo; }
            set {
                if (m_grandCharacterInfo!=null)
                    m_grandCharacterInfo.SkillChanged -= new SkillChangedHandler(m_grandCharacterInfo_SkillChanged);
                m_grandCharacterInfo = value;
                if (m_grandCharacterInfo != null)
                    m_grandCharacterInfo.SkillChanged += new SkillChangedHandler(m_grandCharacterInfo_SkillChanged);
                CheckForCompletedSkills();
            }
        }

        private void CheckForCompletedSkills()
        {
            this.SuppressEvents();
            try
            {
                for (int i = 0; i < m_entries.Count; i++)
                {
                    PlanEntry pe = m_entries[i];
                    GrandSkill gs = m_grandCharacterInfo.GetSkill(pe.SkillName);
                    if (gs.Level >= pe.Level)
                    {
                        m_entries.RemoveAt(i);
                        i--;
                    }
                }
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        private void m_grandCharacterInfo_SkillChanged(object sender, SkillChangedEventArgs e)
        {
            CheckForCompletedSkills();
        }

        public bool IsPlanned(GrandSkill gs)
        {
            foreach (PlanEntry pe in m_entries)
            {
                if (pe.Skill == gs)
                    return true;
            }
            return false;
        }

        public bool IsPlanned(GrandSkill gs, int level)
        {
            foreach (PlanEntry pe in m_entries)
            {
                if (pe.SkillName == gs.Name && pe.Level == level)
                    return true;
            }
            return false;
        }

        public void AddList(List<PlanEntry> planEntries)
        {
            this.SuppressEvents();
            try
            {
                foreach (PlanEntry pe in planEntries)
                {
                    m_entries.Add(pe);
                }
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        public bool RemoveEntry(GrandSkill gs, bool removePrerequisites, bool nonPlannedOnly)
        {
            this.SuppressEvents();
            try
            {
                int minNeeded = 0;
                // Verify nothing else in the plan requires this...
                foreach (PlanEntry pe in m_entries)
                {
                    GrandSkill tSkill = pe.Skill;
                    int thisMinNeeded;
                    if (tSkill.HasAsPrerequisite(gs, out thisMinNeeded))
                    {
                        if (thisMinNeeded == 5)  // All are needed, fail now
                            return false;
                        if (thisMinNeeded > minNeeded)
                            minNeeded = thisMinNeeded;
                    }
                }
                // Remove this skill...
                bool anyRemoved = false;
                for (int i = 0; i < m_entries.Count; i++)
                {
                    PlanEntry tpe = m_entries[i];
                    bool canRemove = (nonPlannedOnly && tpe.EntryType == PlanEntryType.Prerequisite) || !nonPlannedOnly;
                    if (tpe.Skill == gs && tpe.Level > minNeeded && canRemove)
                    {
                        anyRemoved = true;
                        m_entries.RemoveAt(i);
                        i--;
                    }
                }
                if (!anyRemoved)
                    return false;
                if (!removePrerequisites)
                    return true;

                // Remove prerequisites
                foreach (GrandSkill.Prereq pp in gs.Prereqs)
                {
                    RemoveEntry(pp.Skill, true, true);
                }
                return true;
            }
            finally
            {
                this.ResumeEvents();
            }
        }

        public PlanEntry GetEntryWithRoman(string name)
        {
            int level = 0;
            for (int i = 1; i <= 5; i++)
            {
                string tRomanSuffix = " " + GrandSkill.GetRomanSkillNumber(i);
                if (name.EndsWith(tRomanSuffix))
                {
                    level = i;
                    name = name.Substring(0, name.Length - tRomanSuffix.Length);
                    break;
                }
            }
            foreach (PlanEntry pe in m_entries)
            {
                if (pe.SkillName == name && pe.Level == level)
                    return pe;
            }
            return null;
        }
    }

    public enum PlanEntryType
    {
        Planned,
        Prerequisite
    }

    [XmlRoot("planentry")]
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

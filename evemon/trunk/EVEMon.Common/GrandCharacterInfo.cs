using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace EVEMon.Common
{
    public class GrandCharacterInfo
    {
        private string m_name;
        private int m_characterId;
        private string m_race;
        private string m_bloodLine;
        private string m_gender;
        private string m_corporationName;
        private Decimal m_balance;
        private GrandEveAttributes m_attributes = new GrandEveAttributes();
        private MonitoredList<GrandEveAttributeBonus> m_attributeBonuses = new MonitoredList<GrandEveAttributeBonus>();
        private Dictionary<string, GrandSkillGroup> m_skillGroups = new Dictionary<string, GrandSkillGroup>();

        public GrandCharacterInfo(int characterId, string name)
        {
            m_characterId = characterId;
            m_name = name;

            BuildSkillTree();
            m_attributeBonuses.Changed += new EventHandler<ChangedEventArgs<GrandEveAttributeBonus>>(m_attributeBonuses_Changed);
        }

        private void BuildSkillTree()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            using (Stream s = asm.GetManifestResourceStream("EVEMon.Common.eve-skills2.xml.gz"))
            using (GZipStream zs = new GZipStream(s, CompressionMode.Decompress))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(zs);

                Dictionary<string, GrandSkill> allSkills = new Dictionary<string, GrandSkill>();
                foreach (XmlElement sgel in xdoc.SelectNodes("/skills/c"))
                {
                    List<GrandSkill> skills = new List<GrandSkill>();
                    foreach (XmlElement sel in sgel.SelectNodes("s"))
                    {
                        List<GrandSkill.Prereq> prereqs = new List<GrandSkill.Prereq>();
                        foreach (XmlElement pel in sel.SelectNodes("p"))
                        {
                            GrandSkill.Prereq p = new GrandSkill.Prereq(
                                pel.GetAttribute("n"),
                                Convert.ToInt32(pel.GetAttribute("l")));
                            prereqs.Add(p);
                        }

                        bool _pub = (sel.GetAttribute("p") != "false");
                        string _name = sel.GetAttribute("n");
                        int _id = Convert.ToInt32(sel.GetAttribute("i"));
                        string _desc = sel.GetAttribute("d");
                        EveAttribute _primAttr = (EveAttribute)Enum.Parse(typeof(EveAttribute), sel.GetAttribute("a1"), true);
                        EveAttribute _secAttr = (EveAttribute)Enum.Parse(typeof(EveAttribute), sel.GetAttribute("a2"), true);
                        int _rank = Convert.ToInt32(sel.GetAttribute("r"));

                        GrandSkill gs = new GrandSkill(
                            this, _pub, _name, _id, _desc, _primAttr, _secAttr, _rank,
                            prereqs, allSkills);
                        gs.Changed += new EventHandler(gs_Changed);

                        skills.Add(gs);
                        allSkills[gs.Name] = gs;
                    }

                    GrandSkillGroup gsg = new GrandSkillGroup(sgel.GetAttribute("n"),
                        skills);
                    this.m_skillGroups[gsg.Name] = gsg;
                }
            }
        }

        private int m_suppressed = 0;
        private Queue<InternalEvent> m_events = new Queue<InternalEvent>();
        private Dictionary<string, bool> m_coalescedEventTable = new Dictionary<string, bool>();

        private delegate void InternalEvent();


        private void FireEvent(InternalEvent evt, string coalesceKey)
        {
            lock (m_events)
            {
                if (m_suppressed==0)
                    evt();
                else
                {
                    if (String.IsNullOrEmpty(coalesceKey) || !m_coalescedEventTable.ContainsKey(coalesceKey))
                    {
                        m_events.Enqueue(evt);
                        if (!String.IsNullOrEmpty(coalesceKey))
                            m_coalescedEventTable[coalesceKey] = true;
                    }
                }
            }
        }

        public void SuppressEvents()
        {
            lock (m_events)
            {
                m_suppressed++;
            }
        }

        public void ResumeEvents()
        {
            lock (m_events)
            {
                m_suppressed--;
                if (m_suppressed <= 0)
                {
                    m_suppressed = 0;
                    while (m_events.Count > 0)
                        m_events.Dequeue()();
                    m_coalescedEventTable.Clear();
                }
            }
        }

        private void gs_Changed(object sender, EventArgs e)
        {
            GrandSkill gs = (GrandSkill)sender;
            if (gs == m_currentlyTrainingSkill && gs.InTraining == false)
                m_currentlyTrainingSkill = null;
            else if (gs.InTraining)
                m_currentlyTrainingSkill = gs;

            OnSkillChanged(gs);
        }

        public string Name
        {
            get { return m_name; }
            set { if (m_name != value) { m_name = value; OnBioInfoChanged(); } }
        }

        public int CharacterId
        {
            get { return m_characterId; }
            set { if (m_characterId != value) { m_characterId = value; OnBioInfoChanged(); } }
        }

        public string Race
        {
            get { return m_race; }
            set { if (m_race != value) { m_race = value; OnBioInfoChanged(); } }
        }

        public string Bloodline
        {
            get { return m_bloodLine; }
            set { if (m_bloodLine != value) { m_bloodLine = value; OnBioInfoChanged(); } }
        }

        public string Gender
        {
            get { return m_gender; }
            set { if (m_gender != value) { m_gender = value; OnBioInfoChanged(); } }
        }

        public string CorporationName
        {
            get { return m_corporationName; }
            set { if (m_corporationName != value) { m_corporationName = value; OnBioInfoChanged(); } }
        }

        private void OnBioInfoChanged()
        {
            FireEvent(delegate
            {
                if (BioInfoChanged != null)
                    BioInfoChanged(this, new EventArgs());
            }, "bioinfo");
        }

        public event EventHandler BioInfoChanged;

        public Decimal Balance
        {
            get { return m_balance; }
            set { m_balance = value; OnBalanceChanged(); }
        }

        private void OnBalanceChanged()
        {
            FireEvent(delegate
            {
                if (BalanceChanged != null)
                    BalanceChanged(this, new EventArgs());
            }, "balance");
        }

        public event EventHandler BalanceChanged;

        public int GetBaseAttribute(EveAttribute attribute)
        {
            return m_attributes[attribute];
        }

        public void SetBaseAttribute(EveAttribute attribute, int value)
        {
            if (m_attributes[attribute] != value)
            {
                m_attributes[attribute] = value;
                OnAttributeChanged();
            }
        }

        private void OnAttributeChanged()
        {
            FireEvent(delegate
            {
                if (AttributeChanged != null)
                    AttributeChanged(this, new EventArgs());
            }, "attribute");
        }

        public event EventHandler AttributeChanged;

        public int BaseIntelligence
        {
            get { return GetBaseAttribute(EveAttribute.Intelligence); }
            set { SetBaseAttribute(EveAttribute.Intelligence, value); }
        }

        public int BaseCharisma
        {
            get { return GetBaseAttribute(EveAttribute.Charisma); }
            set { SetBaseAttribute(EveAttribute.Charisma, value); }
        }

        public int BasePerception
        {
            get { return GetBaseAttribute(EveAttribute.Perception); }
            set { SetBaseAttribute(EveAttribute.Perception, value); }
        }

        public int BaseMemory
        {
            get { return GetBaseAttribute(EveAttribute.Memory); }
            set { SetBaseAttribute(EveAttribute.Memory, value); }
        }

        public int BaseWillpower
        {
            get { return GetBaseAttribute(EveAttribute.Willpower); }
            set { SetBaseAttribute(EveAttribute.Willpower, value); }
        }

        public double GetEffectiveAttribute(EveAttribute attribute)
        {
            return GetEffectiveAttribute(attribute, null);
        }

        public double GetEffectiveAttribute(EveAttribute attribute, EveAttributeScratchpad scratchpad)
        {
            return GetEffectiveAttribute(attribute, scratchpad, true, true);
        }

        public double GetEffectiveAttribute(EveAttribute attribute, EveAttributeScratchpad scratchpad, bool includeLearning, bool includeImplants)
        {
            double result = Convert.ToDouble(m_attributes[attribute]);
            double learningBonus = 1.0F;

            if (includeImplants)
            {
                foreach (GrandEveAttributeBonus geab in m_attributeBonuses)
                {
                    if (geab.EveAttribute == attribute)
                        result += geab.Amount;
                }
            }

            // XXX: include implants on scratchpad?
            GrandSkillGroup learningSg = m_skillGroups["Learning"];
            switch (attribute)
            {
                case EveAttribute.Intelligence:
                    result += learningSg["Analytical Mind"].Level;
                    result += learningSg["Logic"].Level;
                    break;
                case EveAttribute.Charisma:
                    result += learningSg["Empathy"].Level;
                    result += learningSg["Presence"].Level;
                    break;
                case EveAttribute.Memory:
                    result += learningSg["Instant Recall"].Level;
                    result += learningSg["Eidetic Memory"].Level;
                    break;
                case EveAttribute.Willpower:
                    result += learningSg["Iron Will"].Level;
                    result += learningSg["Focus"].Level;
                    break;
                case EveAttribute.Perception:
                    result += learningSg["Spatial Awareness"].Level;
                    result += learningSg["Clarity"].Level;
                    break;
            }
            if (scratchpad != null)
                result += scratchpad.GetAttributeBonus(attribute);

            if (includeLearning)
            {
                int learningLevel = learningSg["Learning"].Level;
                if (scratchpad != null)
                    learningLevel += scratchpad.LearningLevelBonus;

                learningBonus = 1.0 + (0.02 * learningLevel);

                return (result * learningBonus);
            }
            else
            {
                return result;
            }
        }

        public double EffectiveIntelligence
        {
            get { return GetEffectiveAttribute(EveAttribute.Intelligence); }
        }

        public double EffectiveCharisma
        {
            get { return GetEffectiveAttribute(EveAttribute.Charisma); }
        }

        public double EffectivePerception
        {
            get { return GetEffectiveAttribute(EveAttribute.Perception); }
        }

        public double EffectiveMemory
        {
            get { return GetEffectiveAttribute(EveAttribute.Memory); }
        }

        public double EffectiveWillpower
        {
            get { return GetEffectiveAttribute(EveAttribute.Willpower); }
        }

        public double GetAttributeBonusFromImplants(EveAttribute eveAttribute)
        {
            double result = 0.0F;
            double learningBonus = 1.0F;
            foreach (GrandEveAttributeBonus geab in m_attributeBonuses)
            {
                if (geab.EveAttribute == eveAttribute)
                    result += geab.Amount;
            }
            int learningLevel = m_skillGroups["Learning"]["Learning"].Level;
            learningBonus = 1.0 + (0.02 * learningLevel);
            return result * learningBonus;
        }

        public IList<GrandEveAttributeBonus> AttributeBonuses
        {
            get { return  m_attributeBonuses; }
        }

        void m_attributeBonuses_Changed(object sender, EventArgs e)
        {
            OnAttributeBonusChanged();
        }

        private void OnAttributeBonusChanged()
        {
            OnAttributeChanged();
            FireEvent(delegate
            {
                if (AttributeBonusChanged != null)
                    AttributeBonusChanged(this, new EventArgs());
            }, "attributebonus");
        }

        public event EventHandler AttributeBonusChanged;

        public Dictionary<string, GrandSkillGroup> SkillGroups
        {
            get { return m_skillGroups; }
        }

        private int m_cachedSkillPointTotal = -1;
        private int m_cachedKnownSkillCount = -1;

        private List<GrandSkill> m_skillsChanged = new List<GrandSkill>();

        private void OnSkillChanged(GrandSkill gs)
        {
            m_cachedSkillPointTotal = -1;
            m_cachedKnownSkillCount = -1;
            lock (m_skillsChanged)
            {
                m_skillsChanged.Add(gs);
            }

            FireEvent(delegate
            {
                GrandSkill[] mySkillsChanged;
                lock (m_skillsChanged)
                {
                    mySkillsChanged = new GrandSkill[m_skillsChanged.Count];
                    m_skillsChanged.CopyTo(mySkillsChanged);
                    m_skillsChanged.Clear();
                }
                if (SkillChanged != null)
                    SkillChanged(this, new SkillChangedEventArgs(mySkillsChanged));
            }, "skill");
            if (m_skillGroups["Learning"].Contains(gs.Name))
                OnAttributeChanged();
        }

        public event SkillChangedHandler SkillChanged;

        public GrandSkill GetSkill(string skillName)
        {
            foreach (GrandSkillGroup gsg in m_skillGroups.Values)
            {
                GrandSkill gs = gsg[skillName];
                if (gs != null)
                    return gs;
            }
            return null;
        }

        public int SkillPointTotal
        {
            get
            {
                if (m_cachedSkillPointTotal == -1)
                {
                    m_cachedSkillPointTotal = 0;
                    foreach (GrandSkillGroup gsg in m_skillGroups.Values)
                    {
                        foreach (GrandSkill gs in gsg)
                        {
                            if (!gs.InTraining)
                                m_cachedSkillPointTotal += gs.CurrentSkillPoints;
                        }
                    }
                }

                GrandSkill cts = this.CurrentlyTrainingSkill;
                if (cts == null)
                    return m_cachedSkillPointTotal;
                else
                    return m_cachedSkillPointTotal + cts.CurrentSkillPoints;
            }
        }

        public int KnownSkillCount
        {
            get
            {
                if (m_cachedKnownSkillCount == -1)
                {
                    m_cachedKnownSkillCount = 0;
                    foreach (GrandSkillGroup gsg in m_skillGroups.Values)
                    {
                        m_cachedKnownSkillCount += gsg.KnownCount;
                    }
                }
                return m_cachedKnownSkillCount;
            }
        }

        private GrandSkill m_currentlyTrainingSkill;

        public GrandSkill CurrentlyTrainingSkill
        {
            get { return m_currentlyTrainingSkill; }
        }

        public void CancelCurrentSkillTraining()
        {
            if (m_currentlyTrainingSkill != null)
            {
                m_currentlyTrainingSkill.StopTraining();
            }
        }

        private bool m_isCached = false;

        public bool IsCached
        {
            get { return m_isCached; }
            set { if (m_isCached != value) { m_isCached = value; OnBioInfoChanged(); } }
        }

        public void AssignFromSerializableCharacterInfo(SerializableCharacterInfo ci)
        {
            this.SuppressEvents();
            this.Name = ci.Name;
            this.CharacterId = ci.CharacterId;
            this.IsCached = ci.IsCached;
            this.Gender = ci.Gender;
            this.Race = ci.Race;
            this.Bloodline = ci.BloodLine;
            this.CorporationName = ci.CorpName;
            this.Balance = ci.Balance;

            this.BaseIntelligence = ci.Attributes.BaseIntelligence;
            this.BaseCharisma = ci.Attributes.BaseCharisma;
            this.BasePerception = ci.Attributes.BasePerception;
            this.BaseMemory = ci.Attributes.BaseMemory;
            this.BaseWillpower = ci.Attributes.BaseWillpower;

            List<GrandEveAttributeBonus> manualBonuses = new List<GrandEveAttributeBonus>();
            foreach (GrandEveAttributeBonus tb in this.AttributeBonuses)
            {
                if (tb.Manual)
                    manualBonuses.Add(tb);
            }

            this.AttributeBonuses.Clear();
            foreach (SerializableEveAttributeBonus bonus in ci.AttributeBonuses.Bonuses)
            {
                GrandEveAttributeBonus geab = new GrandEveAttributeBonus(bonus.Name, bonus.EveAttribute, bonus.Amount, bonus.Manual);
                this.AttributeBonuses.Add(geab);
            }
            foreach (GrandEveAttributeBonus tb in manualBonuses)
            {
                this.AttributeBonuses.Add(tb);
            }

            foreach (SerializableSkillGroup sg in ci.SkillGroups)
            {
                GrandSkillGroup gsg = m_skillGroups[sg.Name];
                foreach (SerializableSkill s in sg.Skills)
                {
                    GrandSkill gs = gsg[s.Name];
                    if (gs == null)
                    {
                        gs = new GrandSkill(this, false,
                            s.Name, s.Id, "Unknown Skill", EveAttribute.Intelligence, EveAttribute.Willpower,
                            s.Rank, new List<GrandSkill.Prereq>(), new Dictionary<string, GrandSkill>());
                        gs.Changed += new EventHandler(gs_Changed);

                        gsg.InsertSkill(gs);
                    }
                    gs.CurrentSkillPoints = s.SkillPoints;
                    gs.Known = true;
                }
            }
            this.CancelCurrentSkillTraining();
            if (ci.SkillInTraining != null)
            {
                GrandSkill newTrainingSkill = this.GetSkill(ci.SkillInTraining.SkillName);
                if (newTrainingSkill != null)
                {
                    newTrainingSkill.SetTrainingInfo(ci.SkillInTraining.TrainingToLevel, ci.SkillInTraining.EstimatedCompletion);
                }
            }
            this.ResumeEvents();
        }

        public SerializableCharacterInfo ExportSerializableCharacterInfo()
        {
            SerializableCharacterInfo ci = new SerializableCharacterInfo();
            ci.Name = this.Name;
            ci.CharacterId = this.CharacterId;
            ci.Gender = this.Gender;
            ci.Race = this.Race;
            ci.BloodLine = this.Bloodline;
            ci.CorpName = this.CorporationName;
            ci.Balance = this.Balance;

            ci.Attributes.BaseIntelligence = this.BaseIntelligence;
            ci.Attributes.BaseCharisma = this.BaseCharisma;
            ci.Attributes.BasePerception = this.BasePerception;
            ci.Attributes.BaseMemory = this.BaseMemory;
            ci.Attributes.BaseWillpower = this.BaseWillpower;

            foreach (GrandEveAttributeBonus geab in this.AttributeBonuses)
            {
                SerializableEveAttributeBonus eab = null;
                switch (geab.EveAttribute)
                {
                    case EveAttribute.Intelligence:
                        eab = new SerializableIntelligenceBonus();
                        break;
                    case EveAttribute.Charisma:
                        eab = new SerializableCharismaBonus();
                        break;
                    case EveAttribute.Perception:
                        eab = new SerializablePerceptionBonus();
                        break;
                    case EveAttribute.Memory:
                        eab = new SerializableMemoryBonus();
                        break;
                    case EveAttribute.Willpower:
                        eab = new SerializableWillpowerBonus();
                        break;
                }
                if (eab != null)
                {
                    eab.Name = geab.Name;
                    eab.Amount = geab.Amount;
                    eab.Manual = geab.Manual;
                    ci.AttributeBonuses.Bonuses.Add(eab);
                }
            }

            foreach (GrandSkillGroup gsg in this.SkillGroups.Values)
            {
                SerializableSkillGroup sg = new SerializableSkillGroup();
                bool added = false;
                foreach (GrandSkill gs in gsg)
                {
                    if (gs.CurrentSkillPoints > 0)
                    {
                        SerializableSkill s = new SerializableSkill();
                        s.Name = gs.Name;
                        s.Id = gs.Id;
                        s.Level = gs.Level;
                        s.Rank = gs.Rank;
                        s.SkillPoints = gs.CurrentSkillPoints;
                        sg.Skills.Add(s);
                        added = true;
                    }
                }
                if (added)
                {
                    sg.Name = gsg.Name;
                    ci.SkillGroups.Add(sg);
                }
            }

            GrandSkill gsit = this.CurrentlyTrainingSkill;
            if (gsit != null)
            {
                SerializableSkillInTraining sit = new SerializableSkillInTraining();
                sit.SkillName = gsit.Name;
                sit.TrainingToLevel = gsit.TrainingToLevel;
                sit.CurrentPoints = gsit.CurrentSkillPoints;
                sit.EstimatedCompletion = gsit.EstimatedCompletion;
                sit.NeededPoints = gsit.GetPointsRequiredForLevel(gsit.TrainingToLevel);
                ci.SkillInTraining = sit;
            }

            return ci;
        }

        public TimeSpan GetTrainingTimeToMultipleSkills(IEnumerable<Pair<GrandSkill, int>> skills)
        {
            Dictionary<GrandSkill, int> trainedAlready = new Dictionary<GrandSkill,int>();
            TimeSpan result = TimeSpan.Zero;

            foreach (Pair<GrandSkill, int> ts in skills)
            {
                result += ts.A.GetPrerequisiteTime(trainedAlready);
                for (int i = 1; i <= ts.B; i++)
                {
                    int pointsReq = ts.A.GetPointsRequiredForLevel(i);
                    bool needTrain = true;
                    if ((trainedAlready.ContainsKey(ts.A) &&
                        trainedAlready[ts.A] >= pointsReq) ||
                        ts.A.Level >= ts.B)
                    {
                        needTrain = false;
                    }
                    if (needTrain)
                    {
                        result += ts.A.GetTrainingTimeOfLevelOnly(i, true);
                        trainedAlready[ts.A] = ts.A.GetPointsRequiredForLevel(i);
                    }
                }
            }

            return result;
        }
    }

    public delegate void SkillChangedHandler(object sender, SkillChangedEventArgs e);

    public class SkillChangedEventArgs : EventArgs
    {
        private GrandSkill[] m_skillList;

        public ICollection<GrandSkill> SkillList
        {
            get { return m_skillList; }
        }

        public SkillChangedEventArgs(GrandSkill gs)
        {
            m_skillList = new GrandSkill[1];
            m_skillList[0] = gs;
        }

        public SkillChangedEventArgs(GrandSkill[] skillList)
        {
            m_skillList = new GrandSkill[skillList.Length];
            skillList.CopyTo(m_skillList, 0);
        }
    }

    public enum ChangeType
    {
        Added,
        Removed,
        Cleared
    }

    public class ChangedEventArgs<T> : EventArgs
    {
        private T m_item;
        private ChangeType m_changeType;

        public T Item
        {
            get { return m_item; }
        }

        public ChangeType ChangeType
        {
            get { return m_changeType; }
        }

        public ChangedEventArgs(T item, ChangeType changeType)
        {
            m_item = item;
            m_changeType = changeType;
        }
    }

    public class ClearedEventArgs<T> : EventArgs
    {
        private IEnumerable<T> m_items;

        public IEnumerable<T> Items
        {
            get { return m_items; }
        }

        public ClearedEventArgs(IEnumerable<T> items)
        {
            m_items = items;
        }
    }

    [XmlRoot("monitoredList")]
    public class MonitoredList<T> : IList<T>
        where T : class
    {
        private List<T> m_inner = new List<T>();

        public event EventHandler<ChangedEventArgs<T>> Changed;
        public event EventHandler<ClearedEventArgs<T>> Cleared;

        private void OnChanged(T item, ChangeType changeType)
        {
            if (Changed != null)
                Changed(this, new ChangedEventArgs<T>(item, changeType));
        }

        private void OnCleared(IEnumerable<T> items)
        {
            if (Cleared != null)
                Cleared(this, new ClearedEventArgs<T>(items));
            OnChanged(null, ChangeType.Cleared);
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return m_inner.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            m_inner.Insert(index, item);
            OnChanged(item, ChangeType.Added);
        }

        public void RemoveAt(int index)
        {
            T rItem = m_inner[index];
            m_inner.RemoveAt(index);
            OnChanged(rItem, ChangeType.Removed);
        }

        public T this[int index]
        {
            get
            {
                return m_inner[index];
            }
            set
            {
                bool change = false;
                if (!m_inner[index].Equals(value))
                {
                    OnChanged(m_inner[index], ChangeType.Removed);
                    change = true;
                }
                m_inner[index] = value;
                if (change)
                {
                    OnChanged(value, ChangeType.Added);
                }
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            m_inner.Add(item);
            OnChanged(item, ChangeType.Added);
        }

        public void Clear()
        {
            List<T> removed = new List<T>();
            foreach (T item in m_inner)
                removed.Add(item);
            m_inner.Clear();
            OnCleared(removed);
        }

        public bool Contains(T item)
        {
            return m_inner.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_inner.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return m_inner.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            bool result = m_inner.Remove(item);
            if (result)
                OnChanged(item, ChangeType.Removed);
            return result;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return m_inner.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return m_inner.GetEnumerator();
        }

        #endregion
    }

    public class GrandEveAttributes
    {
        private int[] m_values = new int[5] { 0, 0, 0, 0, 0 };

        public int this[EveAttribute attribute]
        {
            get { return m_values[(int)attribute]; }
            set { m_values[(int)attribute] = value; }
        }
    }

    public class GrandEveAttributeBonus
    {
        private string m_name;
        private EveAttribute m_attribute;
        private int m_amount;
        private bool m_manual = false;

        public string Name
        {
            get { return m_name; }
        }

        public EveAttribute EveAttribute
        {
            get { return m_attribute; }
        }

        public int Amount
        {
            get { return m_amount; }
        }

        public bool Manual
        {
            get { return m_manual; }
        }

        public GrandEveAttributeBonus(string name, EveAttribute attr, int amount)
            : this(name, attr, amount, false)
        {
        }

        public GrandEveAttributeBonus(string name, EveAttribute attr, int amount, bool manual)
        {
            m_name = name;
            m_attribute = attr;
            m_amount = amount;
            m_manual = manual;
        }
    }

    public class GrandSkillGroup: IEnumerable<GrandSkill>
    {
        private string m_name;
        private Dictionary<string, GrandSkill> m_skills = new Dictionary<string, GrandSkill>();

        public GrandSkillGroup(string name, IEnumerable<GrandSkill> skills)
        {
            m_name = name;
            foreach (GrandSkill gs in skills)
            {
                m_skills[gs.Name] = gs;
                gs.Changed += new EventHandler(gs_Changed);
                gs.SetSkillGroup(this);
            }
        }

        private int m_cachedKnownCount = -1;

        private void gs_Changed(object sender, EventArgs e)
        {
            m_cachedKnownCount = -1;
        }

        public string Name
        {
            get { return m_name; }
        }

        public GrandSkill this[string name]
        {
            get
            {
                GrandSkill result;
                m_skills.TryGetValue(name, out result);
                return result;
            }
        }

        public int Count
        {
            get { return m_skills.Count; }
        }

        public int KnownCount
        {
            get
            {
                if (m_cachedKnownCount == -1)
                {
                    m_cachedKnownCount = 0;
                    foreach (GrandSkill gs in m_skills.Values)
                    {
                        if (gs.Known)
                            m_cachedKnownCount++;
                    }
                }
                return m_cachedKnownCount;
            }
        }

        public bool Contains(string skillName)
        {
            return m_skills.ContainsKey(skillName);
        }

        public bool Contains(GrandSkill gs)
        {
            return m_skills.ContainsValue(gs);
        }

        public int GetTotalPoints()
        {
            int result = 0;
            foreach (GrandSkill gs in m_skills.Values)
            {
                result += gs.CurrentSkillPoints;
            }
            return result;
        }

        #region IEnumerable<GrandSkill> Members

        public IEnumerator<GrandSkill> GetEnumerator()
        {
            foreach (GrandSkill gs in m_skills.Values)
            {
                yield return gs;
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        internal void InsertSkill(GrandSkill gs)
        {
            m_skills[gs.Name] = gs;
            gs.Changed += new EventHandler(gs_Changed);
            gs.SetSkillGroup(this);

            gs_Changed(this, new EventArgs());
        }

        #region Appearance in List box

        private static Image m_collapseImage = null;
        private static Image m_expandImage = null;
        private bool m_collapsed = false;

        public bool isCollapsed
        {
            get { return m_collapsed; }
            set 
            { 
                if (m_collapsed != value)
                {
                    m_collapsed = value; 
                } 
            }
        }

        public static Image CollapseImage
        {
            get
            {
                if (m_collapseImage == null)
                {
                    // Do not use a "using" block because Image.FromStream requires that
                    // the stream be left open.
                    Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                        "EVEMon.Common.Collapse_large.png");
                    m_collapseImage = Image.FromStream(s, true, true);
                }
                return m_collapseImage;
            }
        }

        public static Image ExpandImage
        {
            get
            {
                if (m_expandImage == null)
                {
                    // Do not use a "using" block because Image.FromStream requires that
                    // the stream be left open.
                    Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                        "EVEMon.Common.Expand_large.png");
                    m_expandImage = Image.FromStream(s, true, true);
                }
                return m_expandImage;
            }
        }

        private const int SKILL_HEADER_HEIGHT = 21;

        private const int SG_COLLAPSER_PAD_RIGHT = 6;

        public static int Height
        {
            get { return SKILL_HEADER_HEIGHT; }
        }

        public void Draw(DrawItemEventArgs e)
        {
            Font fontr = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((0)));
            Graphics g = e.Graphics;
            bool hastrainingskill = false;
            foreach (GrandSkill gs in m_skills.Values)
            {
                if (gs.Known)
                    hastrainingskill = hastrainingskill || gs.InTraining;
            }

            using (Brush b = new SolidBrush(Color.FromArgb(75, 75, 75)))
            {
                g.FillRectangle(b, e.Bounds);
            }
            using (Pen p = new Pen(Color.FromArgb(100, 100, 100)))
            {
                g.DrawLine(p, e.Bounds.Left, e.Bounds.Top, e.Bounds.Right + 1, e.Bounds.Top);
            }
            using (Font boldf = new Font(fontr, FontStyle.Bold))
            {
                Size titleSizeInt = TextRenderer.MeasureText(g, this.Name, boldf, new Size(0, 0), TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                Point titleTopLeftInt = new Point(e.Bounds.Left + 3,
                    e.Bounds.Top + ((e.Bounds.Height / 2) - (titleSizeInt.Height / 2)));
                Point detailTopLeftInt = new Point(titleTopLeftInt.X + titleSizeInt.Width, titleTopLeftInt.Y);

                string trainingStr = String.Empty;
                if (hastrainingskill)
                {
                    trainingStr = ", 1 training";
                }
                string detailText = String.Format(", {0} Skill{1}, {2} Points{3}",
                    this.KnownCount,
                    this.KnownCount > 1 ? "s" : "",
                    this.GetTotalPoints().ToString("#,##0"),
                    trainingStr);
                TextRenderer.DrawText(g, this.Name, boldf, titleTopLeftInt, Color.White);
                TextRenderer.DrawText(g, detailText, fontr, detailTopLeftInt, Color.White);
            }
            Image i;
            if (isCollapsed)
                i = GrandSkillGroup.ExpandImage;
            else
                i = GrandSkillGroup.CollapseImage;
            g.DrawImageUnscaled(i, new Point(e.Bounds.Right - i.Width - SG_COLLAPSER_PAD_RIGHT,
                (SKILL_HEADER_HEIGHT / 2) - (i.Height / 2) + e.Bounds.Top));
        }

        public Rectangle GetButtonRectangle (Rectangle itemRect)
        {
            Image btnImage;
            if (isCollapsed)
                btnImage = GrandSkillGroup.ExpandImage;
            else
                btnImage = GrandSkillGroup.CollapseImage;
            Size btnSize = btnImage.Size;
            Point btnPoint = new Point(itemRect.Right - btnImage.Width - SG_COLLAPSER_PAD_RIGHT,
                (SKILL_HEADER_HEIGHT / 2) - (btnImage.Height / 2) + itemRect.Top);
            return new Rectangle(btnPoint, btnSize);
        }

        #endregion
    }

    public class GrandSkill
    {
        private GrandCharacterInfo m_owner;
        private bool m_public;
        private string m_name;
        private int m_id;
        private string m_description;
        private string m_descriptionNl;
        private EveAttribute m_primaryAttribute;
        private EveAttribute m_secondaryAttribute;
        private int m_rank;

        private IEnumerable<Prereq> m_prereqs;
        private IDictionary<string, GrandSkill> m_otherSkills;
        private bool m_prereqCooked = false;

        private int m_currentSkillPoints;

        public GrandSkill(GrandCharacterInfo gci, bool pub, string name, int id, string description, 
            EveAttribute a1, EveAttribute a2, int rank, IEnumerable<Prereq> prereqs, IDictionary<string, GrandSkill> otherSkills)
        {
            m_owner = gci;
            m_public = pub;
            m_name = name;
            m_id = id;
            m_description = description;
            m_descriptionNl = description;
            m_primaryAttribute = a1;
            m_secondaryAttribute = a2;
            m_rank = rank;
            m_prereqs = prereqs;
            m_otherSkills = otherSkills;
        }

        private GrandSkillGroup m_skillGroup;

        public GrandSkillGroup SkillGroup
        {
            get { return m_skillGroup; }
        }

        internal void SetSkillGroup(GrandSkillGroup gsg)
        {
            if (m_skillGroup != null)
                throw new InvalidOperationException("can only set skillgroup once");

            m_skillGroup = gsg;
        }

        public int CurrentSkillPoints
        {
            get
            {
                if (m_inTraining)
                {
                    TimeSpan timeRemainSpan = m_estimatedCompletion - DateTime.Now;
                    if (timeRemainSpan <= TimeSpan.Zero)
                        return GetPointsRequiredForLevel(m_trainingToLevel);

                    return GetPointsRequiredForLevel(m_trainingToLevel) - GetPointsForTimeSpan(timeRemainSpan);
                }
                else
                {
                    return m_currentSkillPoints;
                }
            }
            set { m_currentSkillPoints = value; OnChanged(); }
        }

        public bool IsLearningSkill
        {
            get
            {
                return (this.AttributeModified != EveAttribute.None);
            }
        }

        public EveAttribute AttributeModified
        {
            get
            {
                if (m_name == "Analytical Mind" || m_name == "Logic")
                    return EveAttribute.Intelligence;
                else if (m_name == "Empathy" || m_name == "Presence")
                    return EveAttribute.Charisma;
                else if (m_name == "Instant Recall" || m_name == "Eidetic Memory")
                    return EveAttribute.Memory;
                else if (m_name == "Iron Will" || m_name == "Focus")
                    return EveAttribute.Willpower;
                else if (m_name == "Spatial Awareness" || m_name == "Clarity")
                    return EveAttribute.Perception;
                return EveAttribute.None;
            }
        }

        private bool m_known = false;

        public bool Known
        {
            get { return m_known; }
            set { m_known = value; OnChanged();  }
        }

        private int GetPointsForTimeSpan(TimeSpan span)
        {
            // m = points/(primAttr+(secondaryAttr/2))
            // ... so ...
            // m * (primAttr+(secondaryAttr/2)) = points

            double primAttr = m_owner.GetEffectiveAttribute(m_primaryAttribute);
            double secondaryAttr = m_owner.GetEffectiveAttribute(m_secondaryAttribute);
            double points = span.TotalMinutes * (primAttr + (secondaryAttr / 2));
            return Convert.ToInt32(Math.Ceiling(points));
        }

        private TimeSpan GetTimeSpanForPoints(int points)
        {
            return GetTimeSpanForPoints(points, null);
        }

        private TimeSpan GetTimeSpanForPoints(int points, EveAttributeScratchpad scratchpad)
        {
            double primAttr = m_owner.GetEffectiveAttribute(m_primaryAttribute, scratchpad);
            double secondaryAttr = m_owner.GetEffectiveAttribute(m_secondaryAttribute, scratchpad);
            double minutes = Convert.ToDouble(points) / (primAttr + (secondaryAttr / 2));
            return TimeSpan.FromMinutes(minutes);
        }

        public int GetPointsRequiredForLevel(int level)
        {
            if (level == 0)
                return 0;
            int pointsForLevel = Convert.ToInt32(250 * m_rank * Math.Pow(32, Convert.ToDouble(level - 1) / 2));
            // There's some sort of weird rounding error
            // these values need to be corrected by one.
            if (pointsForLevel == 1414)
                pointsForLevel = 1415;
            else if (pointsForLevel == 2828)
                pointsForLevel = 2829;
            else if (pointsForLevel == 7071)
                pointsForLevel = 7072;
            else if (pointsForLevel == 181019)
                pointsForLevel = 181020;
            else if (pointsForLevel == 226274)
                pointsForLevel = 226275;
            return pointsForLevel;
        }

        public bool Public
        {
            get { return m_public; }
        }

        public string Name
        {
            get { return m_name; }
        }

        public int Id
        {
            get { return m_id; }
        }

        public string Description
        {
            get { return m_description; }
        }
       
        public string DescriptionNl
        {
            get { return m_descriptionNl.Replace(@".", ".\n"); }
        }

        public EveAttribute PrimaryAttribute
        {
            get { return m_primaryAttribute; }
        }

        public EveAttribute SecondaryAttribute
        {
            get { return m_secondaryAttribute; }
        }

        public int Rank
        {
            get { return m_rank; }
        }

        public int Level
        {
            get
            {
                return CalculateLevel();
            }
        }

        private int CalculateLevel()
        {
            if (m_inTraining)
            {
                return this.TrainingToLevel - 1;
            }
            int csp = this.CurrentSkillPoints;
            int result = 0;
            for (int i = 1; i <= 5; i++)
            {
                if (GetPointsRequiredForLevel(i) <= csp)
                    result = i;
            }
            return result;
        }

        public bool IsPartiallyTrained()
        {
            int csp = this.CurrentSkillPoints;
            int lvl = CalculateLevel();
            return (csp > GetPointsRequiredForLevel(lvl));
        }

        public IEnumerable<Prereq> Prereqs
        {
            get
            {
                if (!m_prereqCooked)
                    PreparePrereqList();
                return m_prereqs;
            }
        }

        private void PreparePrereqList()
        {
            foreach (Prereq pr in m_prereqs)
            {
                GrandSkill gs = m_otherSkills[pr.Name];
                pr.SetSkill(gs);
            }
            m_prereqCooked = true;
        }

        private void OnChanged()
        {
            if (Changed != null)
                Changed(this, new EventArgs());
        }

        public event EventHandler Changed;

        public class Prereq
        {
            private string m_name;
            private GrandSkill m_pointedSkill;
            private int m_requiredLevel;

            public string Name
            {
                get { return m_name; }
            }

            public GrandSkill Skill
            {
                get { return m_pointedSkill; }
            }

            internal void SetSkill(GrandSkill gs)
            {
                if (m_pointedSkill != null)
                    throw new InvalidOperationException("pointed skill can only be set once");
                m_pointedSkill = gs;
            }

            public int RequiredLevel
            {
                get { return m_requiredLevel; }
            }

            internal Prereq(string name, int requiredLevel)
            {
                m_name = name;
                m_pointedSkill = null;
                m_requiredLevel = requiredLevel;
            }
        }

        private bool m_inTraining = false;
        private int m_trainingToLevel = 0;
        private DateTime m_estimatedCompletion = DateTime.MaxValue;

        public bool InTraining
        {
            get { return m_inTraining; }
        }

        public int TrainingToLevel
        {
            get { return m_trainingToLevel; }
        }

        public DateTime EstimatedCompletion
        {
            get { return m_estimatedCompletion; }
        }

        internal void SetTrainingInfo(int trainingToLevel, DateTime estimatedCompletion)
        {
            m_inTraining = true;
            m_trainingToLevel = trainingToLevel;
            m_estimatedCompletion = estimatedCompletion;
            OnChanged();
        }

        internal void StopTraining()
        {
            m_inTraining = false;
            m_trainingToLevel = 0;
            m_estimatedCompletion = DateTime.MaxValue;
            OnChanged();
        }

        public TimeSpan GetTrainingTimeToLevel(int level)
        {
            int currentSp = this.CurrentSkillPoints;
            int desiredSp = this.GetPointsRequiredForLevel(level);
            if (desiredSp <= currentSp)
                return TimeSpan.Zero;
            return this.GetTimeSpanForPoints(desiredSp - currentSp);
        }

        public TimeSpan GetTrainingTimeOfLevelOnly(int level)
        {
            return GetTrainingTimeOfLevelOnly(level, false);
        }

        public TimeSpan GetTrainingTimeOfLevelOnly(int level, bool includeCurrentSP)
        {
            return GetTrainingTimeOfLevelOnly(level, includeCurrentSP, null);
        }

        public TimeSpan GetTrainingTimeOfLevelOnly(int level, bool includeCurrentSP, EveAttributeScratchpad scratchpad)
        {
            int startSp = GetPointsRequiredForLevel(level - 1);
            int endSp = GetPointsRequiredForLevel(level);
            if (includeCurrentSP)
                startSp = Math.Max(startSp, this.CurrentSkillPoints);
            if (endSp <= startSp)
                return TimeSpan.Zero;
            return this.GetTimeSpanForPoints(endSp - startSp, scratchpad);
        }

        #region GetPrerequisiteTime overloads

        public TimeSpan GetPrerequisiteTime()
        {
            bool junk = false;
            return GetPrerequisiteTime(new Dictionary<GrandSkill, int>(), ref junk);
        }

        public TimeSpan GetPrerequisiteTime(Dictionary<GrandSkill, int> alreadyCountedList)
        {
            bool junk = false;
            return GetPrerequisiteTime(alreadyCountedList, ref junk);
        }

        public TimeSpan GetPrerequisiteTime(ref bool timeIsCurrentlyTraining)
        {
            return GetPrerequisiteTime(new Dictionary<GrandSkill, int>(), ref timeIsCurrentlyTraining);
        }

        public TimeSpan GetPrerequisiteTime(Dictionary<GrandSkill, int> alreadyCountedList, ref bool timeIsCurrentlyTraining)
        {
            TimeSpan result = TimeSpan.Zero;
            foreach (Prereq pp in this.Prereqs)
            {
                GrandSkill gs = pp.Skill;
                if (gs.InTraining)
                    timeIsCurrentlyTraining = true;

                int fromPoints = gs.CurrentSkillPoints;
                int toPoints = gs.GetPointsRequiredForLevel(pp.RequiredLevel);
                if (alreadyCountedList.ContainsKey(gs))
                {
                    fromPoints = alreadyCountedList[gs];
                }
                if (fromPoints < toPoints)
                {
                    result += gs.GetTimeSpanForPoints(toPoints - fromPoints);
                }
                alreadyCountedList[gs] = Math.Max(fromPoints, toPoints);

                result += gs.GetPrerequisiteTime(alreadyCountedList, ref timeIsCurrentlyTraining);
            }
            return result;
        }

        #endregion

        public bool PrerequisitesMet
        {
            get
            {
                foreach (Prereq pp in this.Prereqs)
                {
                    if (pp.Skill.Level < pp.RequiredLevel)
                        return false;
                }
                return true;
            }
        }

        public bool HasPrerequisite(int myLevel, string skillName, int level)
        {
            if (skillName == m_name && level < myLevel)
                return true;
            foreach (Prereq pp in this.Prereqs)
            {
                if (skillName == pp.Skill.Name && level <= pp.RequiredLevel)
                    return true;
                if (pp.Skill.HasPrerequisite(pp.RequiredLevel, skillName, level))
                    return true;
            }
            return false;
        }

        public bool IsPrerequisiteFor(int myLevel, string skillName, int level)
        {
            if (skillName == m_name && level > myLevel)
                return true;
            GrandSkill gs = this.m_owner.GetSkill(skillName);
            return gs.HasPrerequisite(myLevel, m_name, 1);
        }

        public static string TimeSpanToDescriptiveText(TimeSpan ts, DescriptiveTextOptions dto)
        {
            StringBuilder sb = new StringBuilder();
            BuildDescriptiveFragment(sb, ts.Days, dto, "days");
            BuildDescriptiveFragment(sb, ts.Hours, dto, "hours");
            BuildDescriptiveFragment(sb, ts.Minutes, dto, "minutes");
            BuildDescriptiveFragment(sb, ts.Seconds, dto, "seconds");
            if (sb.Length == 0)
                sb.Append("(none)");
            return sb.ToString();
        }

        private static void BuildDescriptiveFragment(StringBuilder sb, int p, DescriptiveTextOptions dto, string dstr)
        {
            if (((dto & DescriptiveTextOptions.IncludeZeroes) == 0) && p == 0)
                return;
            if ((dto & DescriptiveTextOptions.IncludeCommas) != 0)
            {
                if (sb.Length > 0)
                    sb.Append(", ");
            }
            sb.Append(p.ToString());
            if ((dto & DescriptiveTextOptions.SpaceText) != 0)
                sb.Append(' ');
            if ((dto & DescriptiveTextOptions.UppercaseText) != 0)
                dstr = dstr.ToUpper();
            if ((dto & DescriptiveTextOptions.FullText) != 0)
            {
                if (p == 1)
                    dstr = dstr.Substring(0, dstr.Length - 1);
                sb.Append(dstr);
            }
            else
            {
                sb.Append(dstr[0]);
            }
        }

        public static string GetRomanSkillNumber(int number)
        {
            switch (number)
            {
                case 1:
                    return "I";
                case 2:
                    return "II";
                case 3:
                    return "III";
                case 4:
                    return "IV";
                case 5:
                    return "V";
                default:
                    return "(none)";
            }
        }

        public bool HasAsPrerequisite(GrandSkill gs, out int neededLevel)
        {
            foreach (Prereq pp in this.Prereqs)
            {
                if (pp.Skill == gs)
                {
                    neededLevel = pp.RequiredLevel;
                    return true;
                }
                if (pp.Skill.HasAsPrerequisite(gs, out neededLevel))
                    return true;
            }
            neededLevel = 0;
            return false;
        }

        public static int GetIntForRoman(string r)
        {
            if (r == "I")
                return 1;
            else if (r == "II")
                return 2;
            else if (r == "III")
                return 3;
            else if (r == "IV")
                return 4;
            else if (r == "V")
                return 5;
            return 0;
        }

        public override string ToString()
        {
            return this.Name;
        }

        #region Appearance in List Box

        private const int SKILL_DETAIL_HEIGHT = 31;

        public static int Height
        {
            get { return SKILL_DETAIL_HEIGHT; }
        }

        public void Draw(DrawItemEventArgs e)
        {
            Font fontr = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((0)));
            Graphics g = e.Graphics;

            if (m_inTraining)
                g.FillRectangle(Brushes.LightSteelBlue, e.Bounds);
            else if ((e.Index % 2) == 0)
                g.FillRectangle(Brushes.White, e.Bounds);
            else
                g.FillRectangle(Brushes.LightGray, e.Bounds);

            using (Font boldf = new Font(fontr, FontStyle.Bold)) 
            {
                double percentComplete = 1.0f;
                if (this.Level == 0)
                {
                    int NextLevel = this.Level + 1;
                    percentComplete = Convert.ToDouble(m_currentSkillPoints) / Convert.ToDouble(GetPointsRequiredForLevel(NextLevel));
                }
                else if (this.Level < 5)
                {
                    int pointsToNextLevel = this.GetPointsRequiredForLevel(Math.Min(this.Level + 1, 5));
                    int pointsToThisLevel = this.GetPointsRequiredForLevel(this.Level);
                    int pointsDelta = pointsToNextLevel - pointsToThisLevel;
                    percentComplete = Convert.ToDouble(this.CurrentSkillPoints - pointsToThisLevel) / Convert.ToDouble(pointsDelta);
                }

                string skillName = this.Name + " " + GrandSkill.GetRomanSkillNumber(this.Level);
                string rankText = " (Rank " + this.Rank.ToString() + ")";
                string spText = "SP: " + this.CurrentSkillPoints.ToString("#,##0") + "/" +
                    this.GetPointsRequiredForLevel(Math.Min(this.Level + 1, 5)).ToString("#,##0");
                string levelText = "Level " + this.Level.ToString();
                string pctText = percentComplete.ToString("0%") + " Done";

                int PAD_TOP = 2;
                int PAD_LEFT = 6;
                int PAD_RIGHT = 7;
                int LINE_VPAD = 0;

                Size skillNameSize = TextRenderer.MeasureText(g, skillName, boldf, new Size(0, 0), TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                Size levelTextSize = TextRenderer.MeasureText(g, levelText, fontr, new Size(0, 0), TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                Size pctTextSize = TextRenderer.MeasureText(g, pctText, fontr, new Size(0, 0), TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);

                TextRenderer.DrawText(g, skillName, boldf, new Point(e.Bounds.Left + PAD_LEFT, e.Bounds.Top + PAD_TOP), Color.Black);
                TextRenderer.DrawText(g, rankText, fontr,
                    new Point(e.Bounds.Left + PAD_LEFT + skillNameSize.Width, e.Bounds.Top + PAD_TOP), Color.Black);
                TextRenderer.DrawText(g, spText, fontr,
                    new Point(e.Bounds.Left + PAD_LEFT, e.Bounds.Top + PAD_TOP + skillNameSize.Height + LINE_VPAD), Color.Black);

                // Boxes
                int BOX_WIDTH = 57;
                int BOX_HEIGHT = 14;
                int SUBBOX_HEIGHT = 8;
                int BOX_HPAD = 6;
                int BOX_VPAD = 2;
                g.DrawRectangle(Pens.Black,
                    new Rectangle(e.Bounds.Right - BOX_WIDTH - PAD_RIGHT, e.Bounds.Top + PAD_TOP, BOX_WIDTH, BOX_HEIGHT));
                int bWidth = (BOX_WIDTH - 4 - 3) / 5;
                for (int bn = 1; bn <= 5; bn++)
                {
                    Rectangle brect = new Rectangle(e.Bounds.Right - BOX_WIDTH - PAD_RIGHT + 2 + (bWidth * (bn - 1)) + (bn - 1),
                        e.Bounds.Top + PAD_TOP + 2, bWidth, BOX_HEIGHT - 3);
                    if (bn <= this.Level)
                        g.FillRectangle(Brushes.Black, brect);
                    else
                        g.FillRectangle(Brushes.DarkGray, brect);
                }

                // Percent Bar
                g.DrawRectangle(Pens.Black,
                    new Rectangle(e.Bounds.Right - BOX_WIDTH - PAD_RIGHT, e.Bounds.Top + PAD_TOP + BOX_HEIGHT + BOX_VPAD, BOX_WIDTH, SUBBOX_HEIGHT));
                Rectangle pctBarRect = new Rectangle(e.Bounds.Right - BOX_WIDTH - PAD_RIGHT + 2,
                    e.Bounds.Top + PAD_TOP + BOX_HEIGHT + BOX_VPAD + 2,
                    BOX_WIDTH - 3, SUBBOX_HEIGHT - 3);
                g.FillRectangle(Brushes.DarkGray, pctBarRect);
                int fillWidth = Convert.ToInt32(
                    Math.Round(Convert.ToDouble(pctBarRect.Width) * percentComplete));
                if (fillWidth > 0)
                {
                    Rectangle fillRect = new Rectangle(pctBarRect.X, pctBarRect.Y,
                        fillWidth, pctBarRect.Height);
                    g.FillRectangle(Brushes.Black, fillRect);
                }
                TextRenderer.DrawText(g, levelText, fontr,
                    new Point(e.Bounds.Right - BOX_WIDTH - PAD_RIGHT - BOX_HPAD - levelTextSize.Width, e.Bounds.Top + PAD_TOP), Color.Black);
                TextRenderer.DrawText(g, pctText, fontr,
                    new Point(e.Bounds.Right - BOX_WIDTH - PAD_RIGHT - BOX_HPAD - pctTextSize.Width, e.Bounds.Top + PAD_TOP + levelTextSize.Height + LINE_VPAD), Color.Black);
            }
        }

        #endregion
    }

    [Flags]
    public enum DescriptiveTextOptions
    {
        Default = 0,
        FullText = 1,
        UppercaseText = 2,
        SpaceText = 4,
        IncludeCommas = 8,
        IncludeZeroes = 16
    }

    public class EveAttributeScratchpad
    {
        private int m_learningLevelBonus = 0;
        private int[] m_attributeBonuses = new int[5] { 0, 0, 0, 0, 0 };

        public int LearningLevelBonus
        {
            get { return m_learningLevelBonus; }
            set { m_learningLevelBonus = value; }
        }

        public void AdjustLearningLevelBonus(int adjustmentAmount)
        {
            m_learningLevelBonus += adjustmentAmount;
        }

        public int GetAttributeBonus(EveAttribute attribute)
        {
            return m_attributeBonuses[(int)attribute];
        }

        public void AdjustAttributeBonus(EveAttribute attribute, int adjustmentAmount)
        {
            m_attributeBonuses[(int)attribute] += adjustmentAmount;
        }

        public void ApplyALevelOf(GrandSkill gs)
        {
            if (gs.Name == "Learning")
                this.AdjustLearningLevelBonus(1);
            else if (gs.IsLearningSkill)
                this.AdjustAttributeBonus(gs.AttributeModified, 1);
        }
    }
}

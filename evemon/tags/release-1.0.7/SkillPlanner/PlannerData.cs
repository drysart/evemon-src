using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;

using EveCharacterMonitor;

namespace EveCharacterMonitor.SkillPlanner
{
    [XmlRoot("skills")]
    public class PlannerData
    {
        public PlannerData()
        {
        }

        private static WeakReference m_plannerDataDocument;

        public static PlannerData GetPlannerData()
        {
            if (m_plannerDataDocument != null)
            {
                object o = m_plannerDataDocument.Target;
                if (o != null && o is PlannerData)
                    return (PlannerData)o;
                m_plannerDataDocument = null;
            }

            Assembly curAsm = Assembly.GetExecutingAssembly();
            using (Stream s = curAsm.GetManifestResourceStream("EveCharacterMonitor.SkillPlanner.eve-skills2.xml.gz"))
            using (GZipStream zs = new GZipStream(s, CompressionMode.Decompress))
            using (XmlTextReader xtr = new XmlTextReader(zs))
            {
                XmlSerializer xs = new XmlSerializer(typeof(PlannerData));
                PlannerData res = (PlannerData)xs.Deserialize(xtr);
                res.Cook();

                m_plannerDataDocument = new WeakReference(res);
                return res;
            }
        }

        private List<PlannerSkillGroup> m_skillGroups = new List<PlannerSkillGroup>();

        [XmlElement("c")]
        public List<PlannerSkillGroup> SkillGroups
        {
            get { return m_skillGroups; }
            set { m_skillGroups = value; }
        }

        private void Cook()
        {
            foreach (PlannerSkillGroup psg in m_skillGroups)
            {
                psg.Cook(this);
            }
        }

        internal PlannerSkill GetSkill(string skillName)
        {
            foreach (PlannerSkillGroup psg in m_skillGroups)
            {
                PlannerSkill ps = psg.GetSkill(skillName);
                if (ps != null)
                    return ps;
            }
            return null;
        }
    }

    [XmlRoot("c")]
    public class PlannerSkillGroup
    {
        private string m_name;
        private List<PlannerSkill> m_skills = new List<PlannerSkill>();

        [XmlAttribute("n")]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        [XmlElement("s")]
        public List<PlannerSkill> Skills
        {
            get { return m_skills; }
            set { m_skills = value; }
        }

        private PlannerData m_owner;

        [XmlIgnore]
        public PlannerData Owner
        {
            get { return m_owner; }
        }

        internal void Cook(PlannerData owner)
        {
            m_owner = owner;
            foreach (PlannerSkill ps in m_skills)
            {
                ps.Cook(owner, this);
            }
        }

        internal PlannerSkill GetSkill(string skillName)
        {
            foreach (PlannerSkill ps in m_skills)
            {
                if (ps.Name == skillName)
                    return ps;
            }
            return null;
        }
    }

    [XmlRoot("s")]
    public class PlannerSkill
    {
        private string m_name;

        [XmlAttribute("n")]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private int m_id;

        [XmlAttribute("i")]
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private string m_description;

        [XmlAttribute("d")]
        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        private EveAttribute m_primaryAttribute;

        [XmlAttribute("a1")]
        public EveAttribute PrimaryAttribute
        {
            get { return m_primaryAttribute; }
            set { m_primaryAttribute = value; }
        }

        private EveAttribute m_secondaryAttribute;

        [XmlAttribute("a2")]
        public EveAttribute SecondaryAttribute
        {
            get { return m_secondaryAttribute; }
            set { m_secondaryAttribute = value; }
        }

        private int m_rank;

        [XmlAttribute("r")]
        public int Rank
        {
            get { return m_rank; }
            set { m_rank = value; }
        }

        private List<PlannerPrereq> m_prereqs = new List<PlannerPrereq>();

        [XmlElement("p")]
        public List<PlannerPrereq> Prereqs
        {
            get { return m_prereqs; }
            set { m_prereqs = value; }
        }

        private PlannerData m_owner;
        private PlannerSkillGroup m_parent;

        [XmlIgnore]
        public PlannerData Owner
        {
            get { return m_owner; }
        }

        [XmlIgnore]
        public PlannerSkillGroup Parent
        {
            get { return m_parent; }
        }

        internal void Cook(PlannerData owner, PlannerSkillGroup plannerSkillGroup)
        {
            m_owner = owner;
            m_parent = plannerSkillGroup;
            foreach (PlannerPrereq pp in m_prereqs)
            {
                pp.Cook(owner, this);
            }
        }

        public TimeSpan CalculateTimeToSkill(int fromLevel, int toLevel, EveAttributes attributes)
        {
            Double fromPoints = Convert.ToDouble(Skill.GetSkillPointsForLevel(m_rank, fromLevel));
            Double toPoints = Convert.ToDouble(Skill.GetSkillPointsForLevel(m_rank, toLevel));

            Double min = (toPoints - fromPoints) /
                (attributes.GetAttributeAdjustment(m_primaryAttribute, EveAttributeAdjustment.AllWithLearning) +
                    (attributes.GetAttributeAdjustment(m_secondaryAttribute, EveAttributeAdjustment.AllWithLearning) / 2));
            return TimeSpan.FromMinutes(min);
        }
    }

    [XmlRoot("p")]
    public class PlannerPrereq
    {
        private string m_name;
        private int m_level;

        [XmlAttribute("n")]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        [XmlAttribute("l")]
        public int Level
        {
            get { return m_level; }
            set { m_level = value; }
        }

        private PlannerData m_owner;
        private PlannerSkill m_parent;

        [XmlIgnore]
        public PlannerData Owner
        {
            get { return m_owner; }
        }

        [XmlIgnore]
        public PlannerSkill Parent
        {
            get { return m_parent; }
        }

        internal void Cook(PlannerData owner, PlannerSkill plannerSkill)
        {
            m_owner = owner;
            m_parent = plannerSkill;
        }
    }
}

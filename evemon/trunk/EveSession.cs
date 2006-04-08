using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Threading;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace EveCharacterMonitor
{
    public class EveSession
    {
        private List<Pair<string, int>> m_storedCharacterList = null;

        public List<Pair<string, int>> GetCharacterList()
        {
            if (m_storedCharacterList != null)
                return m_storedCharacterList;

            List<Pair<string, int>> result = new List<Pair<string, int>>();
            bool firstAttempt = true;
        AGAIN:
            string s = GetUrl("http://myeve.eve-online.com/character/skilltree.asp", null);

            Regex re = new Regex(@"<a href=""/character/skilltree.asp\?characterID=(\d+)"".*?<br>([^<>]+?)<\/td>", RegexOptions.IgnoreCase);
            MatchCollection mcol = re.Matches(s);
            if (mcol.Count == 0)
            {
                if (!WebLogin())
                    return null;
                if (!firstAttempt)
                    return null;
                firstAttempt = false;
                goto AGAIN;
            }
            foreach (Match m in mcol)
            {
                Pair<string, int> p = new Pair<string, int>();
                p.A = m.Groups[2].Value;
                p.B = Convert.ToInt32(m.Groups[1].Value);
                result.Add(p);
            }
            m_storedCharacterList = result;
            return result;
        }

        public int GetCharacterId(string charName)
        {
            if (m_storedCharacterList == null)
                GetCharacterList();
            if (m_storedCharacterList == null)
                return -2;

            foreach (Pair<string, int> p in m_storedCharacterList)
            {
                if (p.A == charName)
                    return p.B;
            }

            return -1;
        }

        public void GetCharacterInfoAsync(int charId, GetCharacterInfoCallback callback)
        {
            Pair<int, GetCharacterInfoCallback> p = new Pair<int, GetCharacterInfoCallback>();
            p.A = charId;
            p.B = callback;
            ThreadPool.QueueUserWorkItem(new WaitCallback(GetCharacterInfoInternal), p);
        }

        private void GetCharacterInfoInternal(object state)
        {
            Pair<int, GetCharacterInfoCallback> p = state as Pair<int, GetCharacterInfoCallback>;
            int charId = p.A;
            GetCharacterInfoCallback callback = p.B;

            bool firstAttempt = true;
            AGAIN:
            string htmld = GetUrl("http://myeve.eve-online.com/character/skilltree.asp?characterID=" +
                                    charId.ToString(), null);
            if (!htmld.Contains("skills trained, for a total of"))
            {
                if (!WebLogin() || !firstAttempt)
                {
                    callback(this, null);
                    return;
                }
                firstAttempt = false;
                goto AGAIN;
            }

            SkillInTraining sit = null;
            int cti = htmld.IndexOf("Currently training to: ");
            if (cti != -1)
            {
                sit = new SkillInTraining();
                string bsubstr = ReverseString(htmld.Substring(cti - 400, 400));
                string s1 = Regex.Match(bsubstr, @"knaR>i< / (.+?)>""xp11:ezis-tnof").Groups[1].Value;
                sit.SkillName = ReverseString(s1);
                string fsubstr = htmld.Substring(cti, 800);
                sit.TrainingToLevel = Convert.ToInt32(Regex.Match(fsubstr, @"Currently training to: <\/font><strong>level (\d) </st").Groups[1].Value);
                string timeLeft = Regex.Match(fsubstr, @"Time left: <\/font><strong>(.+?)<\/strong>").Groups[1].Value;
                sit.EstimatedCompletion = DateTime.Now + ConvertTimeStringToTimeSpan(timeLeft);
                sit.CurrentPoints = Convert.ToInt32(Regex.Match(fsubstr, @"SP done: </font><strong>(\d+) of \d+</strong>").Groups[1].Value);
                sit.NeededPoints = Convert.ToInt32(Regex.Match(fsubstr, @"SP done: </font><strong>\d+ of (\d+)</strong>").Groups[1].Value);
            }
            else
            {
                sit = null;
            }

            firstAttempt = true;
            BAGAIN:
            string data = GetUrl("http://myeve.eve-online.com/character/xml.asp?characterID=" +
                                    charId.ToString(), null);
            if (!data.Contains("<charactersheet>"))
            {
                if (!WebLogin() || !firstAttempt)
                {
                    callback(this, null);
                    return;
                }
                firstAttempt = false;
                goto BAGAIN;
            }
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(data);

            CharacterInfo result = ProcessCharacterXml(xdoc, charId);
            result.SkillInTraining = sit;
#if DEBUG
            IntelligenceBonus ib = new IntelligenceBonus();
            ib.Name = "woof woof";
            ib.Amount = 3;
            result.AttributeBonuses.Bonuses.Add(ib);
#endif
            callback(this, result);
        }

        private CharacterInfo ProcessCharacterXml(XmlDocument xdoc, int characterId)
        {
            XmlSerializer xs = new XmlSerializer(typeof(CharacterInfo));
            XmlElement charRoot = xdoc.DocumentElement.SelectSingleNode("/charactersheet/characters/character[@characterID='" + characterId.ToString() + "']") as XmlElement;

            using (XmlNodeReader xnr = new XmlNodeReader(charRoot))
            {
                return (CharacterInfo)xs.Deserialize(xnr);
            }
        }

        private TimeSpan ConvertTimeStringToTimeSpan(string timeLeft)
        {
            TimeSpan result = new TimeSpan();
            if (timeLeft.Contains("second"))
                result += TimeSpan.FromSeconds(
                    Convert.ToInt32(Regex.Match(timeLeft, @"(\d+) seconds?").Groups[1].Value));
            if (timeLeft.Contains("minute"))
                result += TimeSpan.FromMinutes(
                    Convert.ToInt32(Regex.Match(timeLeft, @"(\d+) minutes?").Groups[1].Value));
            if (timeLeft.Contains("hour"))
                result += TimeSpan.FromHours(
                    Convert.ToInt32(Regex.Match(timeLeft, @"(\d+) hours?").Groups[1].Value));
            if (timeLeft.Contains("day"))
                result += TimeSpan.FromDays(
                    Convert.ToInt32(Regex.Match(timeLeft, @"(\d+) days?").Groups[1].Value));
            return result;
        }

        private string ReverseString(string p)
        {
            char[] ca = new char[p.Length];
            for (int i = 0; i < p.Length; i++)
                ca[p.Length - i - 1] = p[i];
            return new String(ca);
        }

        private string m_username;
        private string m_password;

        private EveSession(string username, string password)
        {
            m_username = username;
            m_password = password;

            if (!WebLogin())
                throw new ApplicationException("Unable to log in");
        }

        private static Thread m_mainThread;

        public static Thread MainThread
        {
            get { return m_mainThread; }
            set { m_mainThread = value; }
        }

        private void MainThreadInvoke(MethodInvoker mi)
        {
            if (Thread.CurrentThread != m_mainThread)
                mi.Invoke();
            else
            {
                using (BusyDialog f = new BusyDialog())
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object o)
                    {
                        mi.Invoke();
                        f.Complete();
                    }));
                    f.ShowDialog();
                }
            }

        }

        private CookieContainer m_cookies;

        private string GetUrl(string url, string refer)
        {
            string result = null;
            MainThreadInvoke(new MethodInvoker(delegate
            {
                result = InternalGetUrl(url, refer);
            }));
            return result;
        }

        private string InternalGetUrl(string url, string refer)
        {
            if (String.IsNullOrEmpty(refer))
                refer = "http://myeve.eve-online.com/news.asp";

            if (m_cookies == null)
                m_cookies = new CookieContainer();

            int maxRedirects = 6;
        AGAIN:
            if (maxRedirects-- <= 0)
                return String.Empty;
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.1) Gecko/20060111 Firefox/1.5.0.1";
            wr.Referer = refer;
            wr.CookieContainer = m_cookies;
            wr.AllowAutoRedirect = false;
            HttpWebResponse resp = null;
            try
            {
                resp = (HttpWebResponse)wr.GetResponse();
            }
            catch (WebException)
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
                goto AGAIN;
            }
            if (resp.StatusCode == HttpStatusCode.Redirect)
            {
                string loc = resp.GetResponseHeader("Location");
                Uri x = new Uri(url);
                Uri newUri = new Uri(x, loc);
                refer = url;
                url = newUri.ToString();
                resp.Close();
                goto AGAIN;
            }

            string res = String.Empty;
            using (Stream rs = resp.GetResponseStream())
            using (StreamReader sreader = new StreamReader(rs, Encoding.GetEncoding("iso-8859-1")))
            {
                res = sreader.ReadToEnd();
                sreader.Close();
                rs.Close();
                resp.Close();
            }

            if (res.Contains("document.onload=window.location.href='"))
            {
                Match m = Regex.Match(res, @"document\.onload=window\.location\.href='(.*?)';");
                refer = url;
                url = m.Groups[1].Value;
                goto AGAIN;
            }

            return res;
        }

        public void ReLogin()
        {
            WebLogin();
        }

        private bool WebLogin()
        {
            GetUrl("https://myeve.eve-online.com/login.asp?username=" + m_username + "&password=" + m_password +
                "&login=Login&Check=OK&r=&t=", null);
            string s = GetUrl("http://myeve.eve-online.com/character/skilltree.asp", null);
            Regex re = new Regex(@"<a href=""/character/skilltree.asp\?characterID=(\d+)"".*?<br>([^<>]+?)<\/td>", RegexOptions.IgnoreCase);
            if (re.IsMatch(s))
                return true;
            return false;
        }

        private static Dictionary<string, WeakReference> m_sessions = new Dictionary<string, WeakReference>();

        public static EveSession GetSession(string username, string password)
        {
            lock (m_sessions)
            {
                string hkey = username + ":" + password;
                EveSession result = null;
                if (m_sessions.ContainsKey(hkey))
                {
                    result = m_sessions[hkey].Target as EveSession;
                }
                if (result == null)
                {
                    try
                    {
                        EveSession s = new EveSession(username, password);
                        m_sessions[hkey] = new WeakReference(s);
                        result = s;
                    }
                    catch (ApplicationException) { }
                }
                return result;
            }
        }

        public void GetCharaterImageAsync(int charId, GetCharacterImageCallback callback)
        {
            Pair<HttpWebRequest, GetCharacterImageCallback> p = new Pair<HttpWebRequest, GetCharacterImageCallback>();
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create("http://img.eve.is/serv.asp?s=512&c=" + charId.ToString());
            p.A = wr;
            p.B = callback;
            wr.BeginGetResponse(new AsyncCallback(GotCharacterImage), p);
        }

        private void GotCharacterImage(IAsyncResult ar)
        {
            Pair<HttpWebRequest, GetCharacterImageCallback> p = ar.AsyncState as Pair<HttpWebRequest, GetCharacterImageCallback>;
            HttpWebRequest wr = p.A;
            GetCharacterImageCallback callback = p.B;
            try
            {
                HttpWebResponse resp = (HttpWebResponse)wr.EndGetResponse(ar);
                int contentLength = Convert.ToInt32(resp.ContentLength);
                int bytesToGo = contentLength;
                byte[] data = new byte[bytesToGo];
                using (Stream s = resp.GetResponseStream())
                {
                    while (bytesToGo > 0)
                    {
                        int bytesRead = s.Read(data, contentLength - bytesToGo, bytesToGo);
                        bytesToGo -= bytesRead;
                    }
                }
                MemoryStream ms = new MemoryStream(data);
                Image i = Image.FromStream(ms, true, true);
                callback(this, i);
            }
            catch
            {
                callback(this, null);
            }
        }
    }

    public class Pair<TypeA, TypeB>
    {
        private TypeA m_a;
        private TypeB m_b;

        public TypeA A
        {
            get { return m_a; }
            set { m_a = value; }
        }

        public TypeB B
        {
            get { return m_b; }
            set { m_b = value; }
        }
    }

    public delegate void GetCharacterImageCallback(EveSession sender, Image i);

    public delegate void GetCharacterInfoCallback(EveSession sender, CharacterInfo ci);

    [XmlRoot("attributes")]
    public class EveAttributes
    {
        private CharacterInfo m_owner;

        internal void SetOwner(CharacterInfo ci)
        {
            m_owner = ci;
        }

        private int[] m_values = new int[5] { 0, 0, 0, 0, 0 };

        [XmlElement("intelligence")]
        public int BaseIntelligence
        {
            get { return m_values[(int)EveAttribute.Intelligence]; }
            set { m_values[(int)EveAttribute.Intelligence] = value; }
        }

        [XmlElement("charisma")]
        public int BaseCharisma
        {
            get { return m_values[(int)EveAttribute.Charisma]; }
            set { m_values[(int)EveAttribute.Charisma] = value; }
        }

        [XmlElement("perception")]
        public int BasePerception
        {
            get { return m_values[(int)EveAttribute.Perception]; }
            set { m_values[(int)EveAttribute.Perception] = value; }
        }

        [XmlElement("memory")]
        public int BaseMemory
        {
            get { return m_values[(int)EveAttribute.Memory]; }
            set { m_values[(int)EveAttribute.Memory] = value; }
        }

        [XmlElement("willpower")]
        public int BaseWillpower
        {
            get { return m_values[(int)EveAttribute.Willpower]; }
            set { m_values[(int)EveAttribute.Willpower] = value; }
        }

        [XmlIgnore]
        public double AdjustedIntelligence
        {
            get { return GetAdjustedAttribute(EveAttribute.Intelligence); }
        }

        [XmlIgnore]
        public double AdjustedCharisma
        {
            get { return GetAdjustedAttribute(EveAttribute.Charisma); }
        }

        [XmlIgnore]
        public double AdjustedPerception
        {
            get { return GetAdjustedAttribute(EveAttribute.Perception); }
        }

        [XmlIgnore]
        public double AdjustedMemory
        {
            get { return GetAdjustedAttribute(EveAttribute.Memory); }
        }

        [XmlIgnore]
        public double AdjustedWillpower
        {
            get { return GetAdjustedAttribute(EveAttribute.Willpower); }
        }

        [XmlElement("adjustedIntelligence")]
        public string _adjustedIntelligence
        {
            get { return this.AdjustedIntelligence.ToString("#.00"); }
            set { /* ignored */ }
        }

        [XmlElement("adjustedCharisma")]
        public string _adjustedCharisma
        {
            get { return this.AdjustedCharisma.ToString("#.00"); }
            set { /* ignored */ }
        }

        [XmlElement("adjustedPerception")]
        public string _adjustedPerception
        {
            get { return this.AdjustedPerception.ToString("#.00"); }
            set { /* ignored */ }
        }
        
        [XmlElement("adjustedMemory")]
        public string _adjustedMemory
        {
            get { return this.AdjustedMemory.ToString("#.00"); }
            set { /* ignored */ }
        }
        
        [XmlElement("adjustedWillpower")]
        public string _adjustedWillpower
        {
            get { return this.AdjustedWillpower.ToString("#.00"); }
            set { /* ignored */ }
        }

        public double GetAttributeAdjustment(EveAttribute eveAttribute, EveAttributeAdjustment adjustment)
        {
            double result = 0.0;
            double learningBonus = 1.0;
            if ((adjustment & EveAttributeAdjustment.Base) != 0)
                result += m_values[(int)eveAttribute];
            if ((adjustment & EveAttributeAdjustment.Implants) != 0)
            {
                foreach (EveAttributeBonus eab in m_owner.AttributeBonuses.Bonuses)
                {
                    if (eab.EveAttribute == eveAttribute)
                        result += eab.Amount;
                }
            }
            if (((adjustment & EveAttributeAdjustment.Skills) != 0) ||
                ((adjustment & EveAttributeAdjustment.Learning) != 0))
            {
                foreach (SkillGroup sg in m_owner.SkillGroups)
                {
                    if (sg.Name == "Learning")
                    {
                        foreach (Skill s in sg.Skills)
                        {
                            if ((adjustment & EveAttributeAdjustment.Skills) != 0)
                            {
                                switch (eveAttribute)
                                {
                                    case EveAttribute.Intelligence:
                                        if (s.Name == "Analytical Mind" || s.Name == "Logic")
                                            result += s.Level;
                                        break;
                                    case EveAttribute.Charisma:
                                        if (s.Name == "Empathy" || s.Name == "Presence")
                                            result += s.Level;
                                        break;
                                    case EveAttribute.Memory:
                                        if (s.Name == "Instant Recall" || s.Name == "Eidetic Memory")
                                            result += s.Level;
                                        break;
                                    case EveAttribute.Willpower:
                                        if (s.Name == "Iron Will" || s.Name == "Focus")
                                            result += s.Level;
                                        break;
                                    case EveAttribute.Perception:
                                        if (s.Name == "Spatial Awareness" || s.Name == "Clarity")
                                            result += s.Level;
                                        break;
                                }
                            }
                            if (s.Name == "Learning")
                                learningBonus = 1.0 + (0.02 * s.Level);
                        }
                    }
                }
            }
            if ((adjustment & EveAttributeAdjustment.Learning) != 0)
            {
                result = result * learningBonus;
            }
            return result;
        }

        private double GetAdjustedAttribute(EveAttribute eveAttribute)
        {
            return GetAttributeAdjustment(eveAttribute, EveAttributeAdjustment.AllWithLearning);
        }
    }

    [Flags]
    public enum EveAttributeAdjustment
    {
        Base = 1,
        Skills = 2,
        Implants = 4,
        AllWithoutLearning = 7,
        Learning = 8,
        AllWithLearning = 15
    }

    public enum EveAttribute
    {
        [XmlEnum("intelligence")]
        Intelligence,
        [XmlEnum("charisma")]
        Charisma,
        [XmlEnum("perception")]
        Perception,
        [XmlEnum("memory")]
        Memory,
        [XmlEnum("willpower")]
        Willpower
    }

    public abstract class EveAttributeBonus
    {
        private string m_name;
        private int m_amount;

        [XmlIgnore]
        public abstract EveAttribute EveAttribute { get; }

        [XmlElement("augmentatorName")]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        [XmlElement("augmentatorValue")]
        public int Amount
        {
            get { return m_amount; }
            set { m_amount = value; }
        }
    }

    [XmlRoot("intelligenceBonus")]
    public class IntelligenceBonus: EveAttributeBonus
    {
        public override EveAttribute EveAttribute
        {
        	get { return EveAttribute.Intelligence; }
        }
    }

    [XmlRoot("charismaBonus")]
    public class CharismaBonus: EveAttributeBonus
    {
        public override EveAttribute EveAttribute
        {
        	get { return EveAttribute.Charisma; }
        }
    }
    
    [XmlRoot("perceptionBonus")]
    public class PerceptionBonus: EveAttributeBonus
    {
        public override EveAttribute EveAttribute
        {
        	get { return EveAttribute.Perception; }
        }
    }

    [XmlRoot("memoryBonus")]
    public class MemoryBonus: EveAttributeBonus
    {
        public override EveAttribute EveAttribute
        {
        	get { return EveAttribute.Memory; }
        }
    }

    [XmlRoot("willpowerBonus")]
    public class WillpowerBonus: EveAttributeBonus
    {
        public override EveAttribute EveAttribute
        {
        	get { return EveAttribute.Willpower; }
        }
    }

    [XmlRoot("attributeEnhancers")]
    public class AttributeBonusCollection
    {
        private List<EveAttributeBonus> m_attributeBonuses = new List<EveAttributeBonus>();

        [XmlElement("intelligenceBonus", typeof(IntelligenceBonus))]
        [XmlElement("charismaBonus", typeof(CharismaBonus))]
        [XmlElement("perceptionBonus", typeof(PerceptionBonus))]
        [XmlElement("memoryBonus", typeof(MemoryBonus))]
        [XmlElement("willpowerBonus", typeof(WillpowerBonus))]
        public List<EveAttributeBonus> Bonuses
        {
            get { return m_attributeBonuses; }
            set { m_attributeBonuses = value; }
        }
    }

    [XmlRoot("character")]
    public class CharacterInfo
    {
        private string m_name;

        [XmlAttribute("name")]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private int m_characterId;

        [XmlAttribute("characterID")]
        public int CharacterId
        {
            get { return m_characterId; }
            set { m_characterId = value; }
        }

        private SkillInTraining m_skillInTraining;

        [XmlElement("skillInTraining")]
        public SkillInTraining SkillInTraining
        {
            get { return m_skillInTraining; }
            set { m_skillInTraining = value; }
        }

        private string m_race;

        [XmlElement("race")]
        public string Race
        {
            get { return m_race; }
            set { m_race = value; }
        }

        private string m_bloodLine;

        [XmlElement("bloodLine")]
        public string BloodLine
        {
            get { return m_bloodLine; }
            set { m_bloodLine = value; }
        }

        private string m_gender;

        [XmlElement("gender")]
        public string Gender
        {
            get { return m_gender; }
            set { m_gender = value; }
        }

        private string m_corpName;

        [XmlElement("corporationName")]
        public string CorpName
        {
            get { return m_corpName; }
            set { m_corpName = value; }
        }

        private Decimal m_balance;

        [XmlElement("balance")]
        public Decimal Balance
        {
            get { return m_balance; }
            set { m_balance = value; }
        }

        private EveAttributes m_attributes = new EveAttributes();

        [XmlElement("attributes")]
        public EveAttributes Attributes
        {
            get { return m_attributes; }
            set {
                m_attributes = value;
                if (value != null)
                    value.SetOwner(this);
            }
        }

        private AttributeBonusCollection m_attributeBonuses = new AttributeBonusCollection();

        [XmlElement("attributeEnhancers")]
        public AttributeBonusCollection AttributeBonuses
        {
            get { return m_attributeBonuses; }
            set { m_attributeBonuses = value; }
        }

        [XmlIgnore]
        [Obsolete]
        public int Intelligence
        {
            get { return m_attributes.BaseIntelligence; }
            set { m_attributes.BaseIntelligence = value; }
        }

        [XmlIgnore]
        [Obsolete]
        public int Charisma
        {
            get { return m_attributes.BaseCharisma; }
            set { m_attributes.BaseCharisma = value; }
        }

        [XmlIgnore]
        [Obsolete]
        public int Perception
        {
            get { return m_attributes.BasePerception; }
            set { m_attributes.BasePerception = value; }
        }

        [XmlIgnore]
        [Obsolete]
        public int Memory
        {
            get { return m_attributes.BaseMemory; }
            set { m_attributes.BaseMemory = value; }
        }

        [XmlIgnore]
        [Obsolete]
        public int Willpower
        {
            get { return m_attributes.BaseWillpower; }
            set { m_attributes.BaseWillpower = value; }
        }

        private List<SkillGroup> m_skillGroups = new List<SkillGroup>();

        [XmlArray("skills")]
        [XmlArrayItem("skillGroup")]
        public List<SkillGroup> SkillGroups
        {
            get { return m_skillGroups; }
            set { m_skillGroups = value; }
        }

        public Skill GetSkill(string skillName)
        {
            foreach (SkillGroup sg in m_skillGroups)
            {
                foreach (Skill s in sg.Skills)
                {
                    if (s.Name == skillName)
                        return s;
                }
            }
            return null;
        }
    }

    [XmlRoot("skillGroup")]
    public class SkillGroup
    {
        private string m_name;
        private int m_id;
        private List<Skill> m_skills = new List<Skill>();

        [XmlAttribute("groupName")]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        [XmlAttribute("groupID")]
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        [XmlElement("skill")]
        public List<Skill> Skills
        {
            get { return m_skills; }
            set { m_skills = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(m_name);
            sb.Append(" - ");
            sb.Append(m_skills.Count);
            sb.Append(" Skill");
            if (m_skills.Count > 1)
                sb.Append("s");
            sb.Append(", ");
            int points = 0;
            foreach (Skill s in m_skills)
            {
                points += s.SkillPoints;
            }
            sb.Append(points.ToString("#,##0"));
            sb.Append(" points");
            return sb.ToString();
        }

        public int GetTotalPoints()
        {
            int result = 0;
            foreach (Skill s in m_skills)
            {
                result += s.SkillPoints;
            }
            return result;
        }
    }

    [XmlRoot("skill")]
    public class Skill
    {
        private string m_name;
        private int m_id;
        private int m_groupId;
        private int m_flag;
        private int m_rank;
        private int m_skillPoints;
        private int m_level;

        [XmlAttribute("typeName")]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        [XmlAttribute("typeID")]
        public int Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        [XmlElement("groupID")]
        public int GroupId
        {
            get { return m_groupId; }
            set { m_groupId = value; }
        }

        [XmlElement("flag")]
        public int Flag
        {
            get { return m_flag; }
            set { m_flag = value; }
        }

        [XmlElement("rank")]
        public int Rank
        {
            get { return m_rank; }
            set {
                m_rank = value;
                for (int i = 0; i < 5; i++)
                {
                    if (m_skillLevel[i] == 0)
                    {
                        m_skillLevel[i] = GetSkillPointsForLevel(m_rank, i + 1);
                    }
                }
            }
        }

        public static int GetSkillPointsForLevel(int rank, int level)
        {
            int pointsForLevel = Convert.ToInt32(250 * rank * Math.Pow(32, Convert.ToDouble(level-1) / 2));
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

        [XmlElement("skillpoints")]
        public int SkillPoints
        {
            get { return m_skillPoints; }
            set { m_skillPoints = value; }
        }

        [XmlElement("level")]
        public int Level
        {
            get { return m_level; }
            set { m_level = value; }
        }

        private int[] m_skillLevel = new int[5] { 0, 0, 0, 0, 0 };

        [XmlElement("skilllevel1")]
        public int SkillLevel1
        {
            get { return m_skillLevel[0]; }
            set { m_skillLevel[0] = value; }
        }

        [XmlElement("skilllevel2")]
        public int SkillLevel2
        {
            get { return m_skillLevel[1]; }
            set { m_skillLevel[1] = value; }
        }

        [XmlElement("skilllevel3")]
        public int SkillLevel3
        {
            get { return m_skillLevel[2]; }
            set { m_skillLevel[2] = value; }
        }

        [XmlElement("skilllevel4")]
        public int SkillLevel4
        {
            get { return m_skillLevel[3]; }
            set { m_skillLevel[3] = value; }
        }

        [XmlElement("skilllevel5")]
        public int SkillLevel5
        {
            get { return m_skillLevel[4]; }
            set { m_skillLevel[4] = value; }
        }

        private static string[] s_levels = new string[6] { "(none)", "I", "II", "III", "IV", "V" };
   
        public static string[] RomanSkillLevel
        {
            get { return s_levels; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\t");
            sb.Append(m_name);
            sb.Append(" ");
            sb.Append(s_levels[m_level]);
            sb.Append(" (Rank ");
            sb.Append(m_rank.ToString());
            sb.Append(") ");
            sb.Append(m_skillPoints.ToString("#,##0"));
            sb.Append(" points");
            return sb.ToString();
        }
    }

    [XmlRoot("skillInTraining")]
    public class SkillInTraining
    {
        private string m_skillName;

        public string SkillName
        {
            get { return m_skillName; }
            set { m_skillName = value; }
        }

        private int m_trainingToLevel;

        public int TrainingToLevel
        {
            get { return m_trainingToLevel; }
            set { m_trainingToLevel = value; }
        }

        private DateTime m_estimatedCompletion;

        public DateTime EstimatedCompletion
        {
            get { return m_estimatedCompletion; }
            set { m_estimatedCompletion = value; }
        }

        private int m_currentPoints;

        public int CurrentPoints
        {
            get { return m_currentPoints; }
            set { m_currentPoints = value; }
        }

        private int m_neededPoints;

        public int NeededPoints
        {
            get { return m_neededPoints; }
            set { m_neededPoints = value; }
        }
    }
}

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

            CharacterInfo result = new CharacterInfo();
            result.CharacterId = charId;

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
            int cti = htmld.IndexOf("Currently training to: ");
            if (cti != -1)
            {
                SkillInTraining sit = new SkillInTraining();
                string bsubstr = ReverseString(htmld.Substring(cti - 400, 400));
                string s1 = Regex.Match(bsubstr, @"knaR>i< / (.+?)>""xp11:ezis-tnof").Groups[1].Value;
                sit.SkillName = ReverseString(s1);
                string fsubstr = htmld.Substring(cti, 800);
                sit.TrainingToLevel = Convert.ToInt32(Regex.Match(fsubstr, @"Currently training to: <\/font><strong>level (\d) </st").Groups[1].Value);
                string timeLeft = Regex.Match(fsubstr, @"Time left: <\/font><strong>(.+?)<\/strong>").Groups[1].Value;
                sit.EstimatedCompletion = DateTime.Now + ConvertTimeStringToTimeSpan(timeLeft);

                //DEBUG
                //sit.EstimatedCompletion = DateTime.Now + TimeSpan.FromSeconds(10);

                sit.CurrentPoints = Convert.ToInt32(Regex.Match(fsubstr, @"SP done: </font><strong>(\d+) of \d+</strong>").Groups[1].Value);
                sit.NeededPoints = Convert.ToInt32(Regex.Match(fsubstr, @"SP done: </font><strong>\d+ of (\d+)</strong>").Groups[1].Value);
                result.SkillInTraining = sit;
            }
            else
            {
                result.SkillInTraining = null;

                //DEBUG
                //SkillInTraining sit = new SkillInTraining();
                //sit.SkillName = "Learning";
                //sit.CurrentPoints = 1;
                //sit.EstimatedCompletion = DateTime.Now + TimeSpan.FromSeconds(20);
                //sit.TrainingToLevel = 3;
                //result.SkillInTraining = sit;
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
            ProcessCharacterXml(xdoc, result);

            callback(this, result);
        }

        private void ProcessCharacterXml(XmlDocument xdoc, CharacterInfo ci)
        {
            XmlElement charRoot = xdoc.DocumentElement.SelectSingleNode("/charactersheet/characters/character[@characterID='" + ci.CharacterId.ToString() + "']") as XmlElement;
            ci.Race = charRoot.SelectSingleNode("race").InnerText; 
            ci.BloodLine = charRoot.SelectSingleNode("bloodLine").InnerText;
            ci.Gender = charRoot.SelectSingleNode("gender").InnerText;
            ci.CorpName = charRoot.SelectSingleNode("corporationName").InnerText;
            ci.Balance = Convert.ToDecimal(charRoot.SelectSingleNode("balance").InnerText);
            ci.Intelligence = Convert.ToInt32(charRoot.SelectSingleNode("attributes/intelligence").InnerText);
            ci.Charisma = Convert.ToInt32(charRoot.SelectSingleNode("attributes/charisma").InnerText);
            ci.Perception = Convert.ToInt32(charRoot.SelectSingleNode("attributes/perception").InnerText);
            ci.Memory = Convert.ToInt32(charRoot.SelectSingleNode("attributes/memory").InnerText);
            ci.Willpower = Convert.ToInt32(charRoot.SelectSingleNode("attributes/willpower").InnerText);

            ci.SkillGroups.Clear();
            double learningBonus = 1.0;
            foreach (XmlElement sgel in charRoot.SelectNodes("skills/skillGroup"))
            {
                SkillGroup sg = new SkillGroup();
                sg.Name = sgel.GetAttribute("groupName");
                sg.Id = Convert.ToInt32(sgel.GetAttribute("groupID"));

                foreach (XmlElement skel in sgel.SelectNodes("skill"))
                {
                    Skill s = new Skill();
                    s.Name = skel.GetAttribute("typeName");
                    s.Id = Convert.ToInt32(skel.GetAttribute("typeID"));
                    s.Rank = Convert.ToInt32(skel.SelectSingleNode("rank").InnerText);
                    s.SkillPoints = Convert.ToInt32(skel.SelectSingleNode("skillpoints").InnerText);
                    s.Level = Convert.ToInt32(skel.SelectSingleNode("level").InnerText);
                    sg.Skills.Add(s);


                    if (s.Name == "Analytical Mind" || s.Name == "Logic")
                        ci.Intelligence += s.Level;
                    if (s.Name == "Empathy" || s.Name == "Presence")
                        ci.Charisma += s.Level;
                    if (s.Name == "Instant Recall" || s.Name == "Eidetic Memory")
                        ci.Memory += s.Level;
                    if (s.Name == "Iron Will" || s.Name == "Focus")
                        ci.Willpower += s.Level;
                    if (s.Name == "Spatial Awareness" || s.Name == "Clarity")
                        ci.Perception += s.Level;
                    if (s.Name == "Learning")
                        learningBonus = 1.0 + (0.02 * s.Level);
                }

                ci.SkillGroups.Add(sg);
            }

            ci.Intelligence = Convert.ToInt32(Math.Round(ci.Intelligence * learningBonus));
            ci.Charisma = Convert.ToInt32(Math.Round(ci.Charisma * learningBonus));
            ci.Memory = Convert.ToInt32(Math.Round(ci.Memory * learningBonus));
            ci.Willpower = Convert.ToInt32(Math.Round(ci.Willpower * learningBonus));
            ci.Perception = Convert.ToInt32(Math.Round(ci.Perception * learningBonus));
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

        private CookieContainer m_cookies;

        private string GetUrl(string url, string refer)
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
        private int m_intelligence;

        [XmlElement("intelligence")]
        public int Intelligence
        {
            get { return m_intelligence; }
            set { m_intelligence = value; }
        }

        private int m_charisma;

        [XmlElement("charisma")]
        public int Charisma
        {
            get { return m_charisma; }
            set { m_charisma = value; }
        }

        private int m_perception;

        [XmlElement("perception")]
        public int Perception
        {
            get { return m_perception; }
            set { m_perception = value; }
        }

        private int m_memory;

        [XmlElement("memory")]
        public int Memory
        {
            get { return m_memory; }
            set { m_memory = value; }
        }

        private int m_willpower;

        [XmlElement("willpower")]
        public int Willpower
        {
            get { return m_willpower; }
            set { m_willpower = value; }
        }
    }

    [XmlRoot("character")]
    public class CharacterInfo
    {
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
            set { m_attributes = value; }
        }

        [XmlIgnore]
        public int Intelligence
        {
            get { return m_attributes.Intelligence; }
            set { m_attributes.Intelligence = value; }
        }

        [XmlIgnore]
        public int Charisma
        {
            get { return m_attributes.Charisma; }
            set { m_attributes.Charisma = value; }
        }

        [XmlIgnore]
        public int Perception
        {
            get { return m_attributes.Perception; }
            set { m_attributes.Perception = value; }
        }

        [XmlIgnore]
        public int Memory
        {
            get { return m_attributes.Memory; }
            set { m_attributes.Memory = value; }
        }

        [XmlIgnore]
        public int Willpower
        {
            get { return m_attributes.Willpower; }
            set { m_attributes.Willpower = value; }
        }

        private List<SkillGroup> m_skillGroups = new List<SkillGroup>();

        [XmlArray("skills")]
        [XmlArrayItem("skillGroup")]
        public List<SkillGroup> SkillGroups
        {
            get { return m_skillGroups; }
            set { m_skillGroups = value; }
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
    }

    [XmlRoot("skill")]
    public class Skill
    {
        private string m_name;
        private int m_id;
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

        [XmlElement("rank")]
        public int Rank
        {
            get { return m_rank; }
            set { m_rank = value; }
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

        private static string[] s_levels = new string[6] { "(none)", "I", "II", "III", "IV", "V" };

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

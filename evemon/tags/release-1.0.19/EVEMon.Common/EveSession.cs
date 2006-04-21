using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace EVEMon.Common
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

            SerializableSkillInTraining sit = null;
            int cti = htmld.IndexOf("Currently training to: ");
            if (cti != -1)
            {
                sit = new SerializableSkillInTraining();
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

            SerializableCharacterInfo result = ProcessCharacterXml(xdoc, charId);
            result.SkillInTraining = sit;
#if DEBUG
            SerializableIntelligenceBonus ib = new SerializableIntelligenceBonus();
            ib.Name = "woof woof";
            ib.Amount = 3;
            result.AttributeBonuses.Bonuses.Add(ib);
#endif
            callback(this, result);
        }

        private SerializableCharacterInfo ProcessCharacterXml(XmlDocument xdoc, int characterId)
        {
            int junk;
            return ProcessCharacterXml(xdoc, characterId, out junk);
        }

        private SerializableCharacterInfo ProcessCharacterXml(XmlDocument xdoc, int characterId, out int cacheExpires)
        {
            XmlSerializer xs = new XmlSerializer(typeof(SerializableCharacterInfo));
            XmlElement charRoot = xdoc.DocumentElement.SelectSingleNode("/charactersheet/characters/character[@characterID='" + characterId.ToString() + "']") as XmlElement;

            cacheExpires = Convert.ToInt32(charRoot.GetAttribute("timeLeftInCache"));

            using (XmlNodeReader xnr = new XmlNodeReader(charRoot))
            {
                return (SerializableCharacterInfo)xs.Deserialize(xnr);
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
                //using (BusyDialog f = new BusyDialog())
                //{
                //    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object o)
                //    {
                //        mi.Invoke();
                //        f.Complete();
                //    }));
                //    f.ShowDialog();
                //}

                using (IDisposable d = BusyDialog.GetScope())
                {
                    mi.Invoke();
                }
            }

        }

        public static event EventHandler<NetworkLogEventArgs> NetworkLogEvent;

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

            if (NetworkLogEvent != null)
            {
                NetworkLogEventArgs args = new NetworkLogEventArgs();
                args.NetworkLogEventType = NetworkLogEventType.BeginGetUrl;
                args.Url = url;
                args.Referer = refer;
                //args.Cookies = m_cookies;
                NetworkLogEvent(this, args);
            }

            HttpWebResponse resp = null;
            try
            {
                resp = (HttpWebResponse)wr.GetResponse();
            }
            catch (WebException err)
            {
                if (NetworkLogEvent != null)
                {
                    NetworkLogEventArgs args = new NetworkLogEventArgs();
                    args.NetworkLogEventType = NetworkLogEventType.GotUrlFailure;
                    args.Url = url;
                    args.Referer = refer;
                    args.Cookies = resp.Cookies;
                    args.Exception = err;
                    NetworkLogEvent(this, args);
                }
                Thread.Sleep(TimeSpan.FromSeconds(3));
                goto AGAIN;
            }
            catch (Exception err)
            {
                if (NetworkLogEvent != null)
                {
                    NetworkLogEventArgs args = new NetworkLogEventArgs();
                    args.NetworkLogEventType = NetworkLogEventType.GotUrlFailure;
                    args.Url = url;
                    args.Referer = refer;
                    args.Cookies = resp.Cookies;
                    args.Exception = err;
                    NetworkLogEvent(this, args);
                }
                throw;
            }

            if (resp.StatusCode == HttpStatusCode.Redirect)
            {
                string loc = resp.GetResponseHeader("Location");
                Uri x = new Uri(url);
                Uri newUri = new Uri(x, loc);

                if (NetworkLogEvent != null)
                {
                    NetworkLogEventArgs args = new NetworkLogEventArgs();
                    args.NetworkLogEventType = NetworkLogEventType.Redirected;
                    args.Url = url;
                    args.Referer = refer;
                    args.Cookies = resp.Cookies;
                    args.RedirectTo = newUri.ToString();
                    NetworkLogEvent(this, args);
                }

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

                if (NetworkLogEvent != null)
                {
                    NetworkLogEventArgs args = new NetworkLogEventArgs();
                    args.NetworkLogEventType = NetworkLogEventType.GotUrlSuccess;
                    args.Url = url;
                    args.Referer = refer;
                    args.Cookies = resp.Cookies;
                    args.Data = res;
                    args.StatusCode = resp.StatusCode;
                    NetworkLogEvent(this, args);
                }
            }

            if (res.Contains("document.onload=window.location.href='"))
            {
                Match m = Regex.Match(res, @"document\.onload=window\.location\.href='(.*?)';");
                string newUrl = m.Groups[1].Value;

                if (NetworkLogEvent != null)
                {
                    NetworkLogEventArgs args = new NetworkLogEventArgs();
                    args.NetworkLogEventType = NetworkLogEventType.ParsedRedirect;
                    args.Url = url;
                    args.Referer = refer;
                    args.Cookies = resp.Cookies;
                    args.RedirectTo = newUrl;
                    NetworkLogEvent(this, args);
                }

                refer = url;
                url = newUrl;
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
            GetUrl("https://myeve.eve-online.com/login.asp?username=" +
                System.Web.HttpUtility.UrlEncode(m_username) +
                "&password=" +
                System.Web.HttpUtility.UrlEncode(m_password) +
                "&login=Login&Check=OK&r=&t=", null);
            string s = GetUrl("http://myeve.eve-online.com/character/skilltree.asp", null);
            Regex re = new Regex(@"<a href=""/character/skilltree.asp\?characterID=(\d+)"".*?<br>([^<>]+?)<\/td>", RegexOptions.IgnoreCase);
            if (re.IsMatch(s))
                return true;
            return false;
        }

        private static Dictionary<string, WeakReference<EveSession>> m_sessions = new Dictionary<string, WeakReference<EveSession>>();

        public static EveSession GetSession(string username, string password)
        {
            lock (m_sessions)
            {
                string hkey = username + ":" + password;
                EveSession result = null;
                if (m_sessions.ContainsKey(hkey))
                {
                    result = m_sessions[hkey].Target;
                }
                if (result == null)
                {
                    try
                    {
                        EveSession s = new EveSession(username, password);
                        m_sessions[hkey] = new WeakReference<EveSession>(s);
                        result = s;
                    }
                    catch (ApplicationException) { }
                }
                return result;
            }
        }

        public static void GetCharaterImageAsync(int charId, GetCharacterImageCallback callback)
        {
            Pair<HttpWebRequest, GetCharacterImageCallback> p = new Pair<HttpWebRequest, GetCharacterImageCallback>();
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create("http://img.eve.is/serv.asp?s=512&c=" + charId.ToString());
            p.A = wr;
            p.B = callback;
            wr.BeginGetResponse(new AsyncCallback(GotCharacterImage), p);
        }

        private static void GotCharacterImage(IAsyncResult ar)
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
                callback(null, i);
            }
            catch
            {
                callback(null, null);
            }
        }

        public void UpdateGrandCharacterInfoAsync(GrandCharacterInfo grandCharacterInfo, UpdateGrandCharacterInfoCallback callback)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                int timeLeftInCache = this.UpdateGrandCharacterInfo(grandCharacterInfo);
                callback(this, timeLeftInCache);
            }));
        }

        private const int DEFAULT_RETRY_INTERVAL = 60 * 5;

        public int UpdateGrandCharacterInfo(GrandCharacterInfo grandCharacterInfo)
        {
            GrandSkill newTrainingSkill = null;
            int trainingToLevel = -1;
            DateTime estimatedCompletion = DateTime.MaxValue;

            bool firstAttempt = true;
        AGAIN:
            string htmld = GetUrl("http://myeve.eve-online.com/character/skilltree.asp?characterID=" +
                                    grandCharacterInfo.CharacterId.ToString(), null);
            if (!htmld.Contains("skills trained, for a total of"))
            {
                if (!WebLogin() || !firstAttempt)
                {
//                    callback(this, null);
                    return DEFAULT_RETRY_INTERVAL;
                }
                firstAttempt = false;
                goto AGAIN;
            }

            int cti = htmld.IndexOf("Currently training to: ");
            if (cti != -1)
            {
                string bsubstr = ReverseString(htmld.Substring(cti - 400, 400));
                string s1 = Regex.Match(bsubstr, @"knaR>i< / (.+?)>""xp11:ezis-tnof").Groups[1].Value;
                string skillName = ReverseString(s1);
                string fsubstr = htmld.Substring(cti, 800);
                trainingToLevel = Convert.ToInt32(Regex.Match(fsubstr, @"Currently training to: <\/font><strong>level (\d) </st").Groups[1].Value);
                string timeLeft = Regex.Match(fsubstr, @"Time left: <\/font><strong>(.+?)<\/strong>").Groups[1].Value;
                estimatedCompletion = DateTime.Now + ConvertTimeStringToTimeSpan(timeLeft);

                newTrainingSkill = grandCharacterInfo.GetSkill(skillName);
            }

            firstAttempt = true;
        BAGAIN:
            string data = GetUrl("http://myeve.eve-online.com/character/xml.asp?characterID=" +
                                    grandCharacterInfo.CharacterId.ToString(), null);
            if (!data.Contains("<charactersheet>"))
            {
                if (!WebLogin() || !firstAttempt)
                {
//                    callback(this, null);
                    return DEFAULT_RETRY_INTERVAL;
                }
                firstAttempt = false;
                goto BAGAIN;
            }
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(data);

            int timeLeftInCache;
            SerializableCharacterInfo result = ProcessCharacterXml(xdoc, grandCharacterInfo.CharacterId, out timeLeftInCache);
            grandCharacterInfo.AssignFromSerializableCharacterInfo(result);

            grandCharacterInfo.CancelCurrentSkillTraining();
            if (newTrainingSkill != null)
            {
                newTrainingSkill.SetTrainingInfo(trainingToLevel, estimatedCompletion);
            }

            return timeLeftInCache;
        }
    }

    [XmlRoot("pair")]
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

    public delegate void UpdateGrandCharacterInfoCallback(EveSession sender, int timeLeftInCache);

    public delegate void GetCharacterImageCallback(EveSession sender, Image i);

    public delegate void GetCharacterInfoCallback(EveSession sender, SerializableCharacterInfo ci);

    [XmlRoot("attributes")]
    public class EveAttributes
    {
        private SerializableCharacterInfo m_owner;

        internal void SetOwner(SerializableCharacterInfo ci)
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

        public double GetAttributeAdjustment(EveAttribute eveAttribute, SerializableEveAttributeAdjustment adjustment)
        {
            double result = 0.0;
            double learningBonus = 1.0;
            if ((adjustment & SerializableEveAttributeAdjustment.Base) != 0)
                result += m_values[(int)eveAttribute];
            if ((adjustment & SerializableEveAttributeAdjustment.Implants) != 0)
            {
                foreach (SerializableEveAttributeBonus eab in m_owner.AttributeBonuses.Bonuses)
                {
                    if (eab.EveAttribute == eveAttribute)
                        result += eab.Amount;
                }
            }
            if (((adjustment & SerializableEveAttributeAdjustment.Skills) != 0) ||
                ((adjustment & SerializableEveAttributeAdjustment.Learning) != 0))
            {
                foreach (SerializableSkillGroup sg in m_owner.SkillGroups)
                {
                    if (sg.Name == "Learning")
                    {
                        foreach (SerializableSkill s in sg.Skills)
                        {
                            if ((adjustment & SerializableEveAttributeAdjustment.Skills) != 0)
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
            if ((adjustment & SerializableEveAttributeAdjustment.Learning) != 0)
            {
                result = result * learningBonus;
            }
            return result;
        }

        private double GetAdjustedAttribute(EveAttribute eveAttribute)
        {
            return GetAttributeAdjustment(eveAttribute, SerializableEveAttributeAdjustment.AllWithLearning);
        }
    }

    public enum NetworkLogEventType
    {
        BeginGetUrl,
        Redirected,
        ParsedRedirect,
        GotUrlSuccess,
        GotUrlFailure
    }

    public class NetworkLogEventArgs : EventArgs
    {
        internal NetworkLogEventArgs()
        {
        }

        private NetworkLogEventType m_type;

        public NetworkLogEventType NetworkLogEventType
        {
            get { return m_type; }
            set { m_type = value; }
        }

        private string m_url;

        public string Url
        {
            get { return m_url; }
            set { m_url = value; }
        }

        private string m_referer;

        public string Referer
        {
            get { return m_referer; }
            set { m_referer = value; }
        }

        private CookieCollection m_cookies;

        [XmlIgnore]
        public CookieCollection Cookies
        {
            get { return m_cookies; }
            set { m_cookies = value; }
        }

        public List<string> CookieList
        {
            get
            {
                if (m_cookies == null)
                    return null;
                List<string> cook = new List<string>();
                foreach (Cookie c in m_cookies)
                {
                    cook.Add(c.ToString());
                }
                return cook;
            }
            set { }
        }

        private Exception m_exception;

        [XmlIgnore]
        public Exception Exception
        {
            get { return m_exception; }
            set { m_exception = value; }
        }

        public string ExceptionText
        {
            get {
                if (m_exception != null)
                    return m_exception.ToString();
                else
                    return null;
            }
            set { }
        }

        private string m_redirectTo;

        public string RedirectTo
        {
            get { return m_redirectTo; }
            set { m_redirectTo = value; }
        }

        private HttpStatusCode m_statusCode = HttpStatusCode.OK;

        public HttpStatusCode StatusCode
        {
            get { return m_statusCode; }
            set { m_statusCode = value; }
        }

        private string m_data;

        public string Data
        {
            get { return m_data; }
            set { m_data = value; }
        }
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
        Willpower,
        [XmlEnum("none")]
        None
    }
}

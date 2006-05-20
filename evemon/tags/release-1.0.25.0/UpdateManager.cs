using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Threading;
using System.Reflection;

using EVEMon.Common;

namespace EVEMon
{
    public class UpdateManager
    {
        private UpdateManager()
        {
        }

        private static UpdateManager m_updateManager = null;

        public static UpdateManager GetInstance()
        {
            if (m_updateManager == null)
                m_updateManager = new UpdateManager();
            return m_updateManager;
        }

        private Timer m_timer = null;
        private object m_lockObject = new object();
        private bool m_running = false;

        public void Start()
        {
            lock (m_lockObject)
            {
                m_timer = new Timer(new TimerCallback(TimerTrigger));
                m_timer.Change(10, -1);
                m_running = true;
            }
        }

        public void Stop()
        {
            lock (m_lockObject)
            {
                m_timer.Change(-1, -1);
                m_timer.Dispose();
                m_timer = null;
                m_running = false;
            }
        }

        //private const string UPDATE_URL = "http://static.evercrest.com/www/images2/ext/sa/evemon-data.xml";
        private const string UPDATE_URL = "http://evemon.evercrest.com/patch.xml";

        private void TimerTrigger(object state)
        {
            lock (m_lockObject)
            {
                if (!m_running)
                    return;

                try
                {
                    Version currentVersion = new Version("0.0.0.0");
                    foreach (Attribute a in Assembly.GetExecutingAssembly().GetCustomAttributes(false))
                    {
                        if (a is AssemblyFileVersionAttribute)
                        {
                            AssemblyFileVersionAttribute ava = a as AssemblyFileVersionAttribute;
                            currentVersion = new Version(ava.Version);
                        }
                    }

                    XmlDocument xdoc;
                    try
                    {
                        xdoc = EVEMonWebRequest.LoadXml(UPDATE_URL + "?ver=" + currentVersion.ToString());
                    }
                    catch
                    {
                        return;
                    }

                    if (xdoc.DocumentElement.Name != "evemon")
                        return;

                    XmlElement newestEl = xdoc.DocumentElement.SelectSingleNode("newest") as XmlElement;
                    if (newestEl != null)
                    {
                        Version newestVersion = new Version(newestEl.SelectSingleNode("version").InnerText);
                        string updateUrl = newestEl.SelectSingleNode("url").InnerText;
                        string updateMessage = newestEl.SelectSingleNode("message").InnerText;

                        bool canAutoInstall = false;
                        string installArgs = String.Empty;
                        string installUrl = String.Empty;
                        XmlElement argEl = newestEl.SelectSingleNode("autopatchargs") as XmlElement;
                        XmlElement iUrlEl = newestEl.SelectSingleNode("autopatchurl") as XmlElement;
                        if (iUrlEl != null && argEl != null)
                        {
                            canAutoInstall = true;
                            installUrl = iUrlEl.InnerText;
                            installArgs = argEl.InnerText;
                        }

                        if (newestVersion > currentVersion)
                        {
                            // Use ThreadPool to avoid deadlock if the callback tries to
                            // call Stop() on the UpdateManager.
                            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object o)
                            {
                                OnUpdateAvailable(updateUrl, updateMessage, newestVersion,
                                    currentVersion, canAutoInstall, installArgs, installUrl);
                            }));
                        }
                    }
                }
                finally
                {
                    m_timer.Change(Convert.ToInt32(TimeSpan.FromHours(2).TotalMilliseconds), -1);
                }
            }
        }

        public event UpdateAvailableHandler UpdateAvailable;

        private void OnUpdateAvailable(string updateUrl, string updateMessage,
            Version newestVersion, Version currentVersion, bool canAutoInstall,
            string installArgs, string installUrl)
        {
            if (UpdateAvailable != null)
            {
                UpdateAvailableEventArgs e = new UpdateAvailableEventArgs();
                e.CurrentVersion = currentVersion;
                e.NewestVersion = newestVersion;
                e.UpdateMessage = updateMessage;
                e.UpdateUrl = updateUrl;
                e.CanAutoInstall = canAutoInstall;
                e.AutoInstallUrl = installUrl;
                e.AutoInstallArguments = installArgs;
                UpdateAvailable(this, e);
            }
        }
    }

    public delegate void UpdateAvailableHandler(object sender, UpdateAvailableEventArgs e);

    public class UpdateAvailableEventArgs
    {
        private string m_updateUrl;

        public string UpdateUrl
        {
            get { return m_updateUrl; }
            set { m_updateUrl = value; }
        }

        private string m_updateMessage;

        public string UpdateMessage
        {
            get { return m_updateMessage; }
            set { m_updateMessage = value; }
        }

        private Version m_currentVersion;

        public Version CurrentVersion
        {
            get { return m_currentVersion; }
            set { m_currentVersion = value; }
        }

        private Version m_newestVersion;

        public Version NewestVersion
        {
            get { return m_newestVersion; }
            set { m_newestVersion = value; }
        }

        private bool m_canAutoInstall = false;

        public bool CanAutoInstall
        {
            get { return m_canAutoInstall; }
            set { m_canAutoInstall = value; }
        }

        private string m_autoInstallUrl = String.Empty;

        public string AutoInstallUrl
        {
            get { return m_autoInstallUrl; }
            set { m_autoInstallUrl = value; }
        }

        private string m_autoInstallArguments = String.Empty;

        public string AutoInstallArguments
        {
            get { return m_autoInstallArguments; }
            set { m_autoInstallArguments = value; }
        }
    }
}

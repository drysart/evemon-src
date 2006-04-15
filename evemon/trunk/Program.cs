using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace EveCharacterMonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if !DEBUG
            InstanceManager im = InstanceManager.GetInstance();
            if (!im.CreatedNew)
            {
                im.Signal();
                return;
            }
#endif

            m_settingKey = String.Empty;

            EveSession.MainThread = System.Threading.Thread.CurrentThread;

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1(ca));
            s_settings = Settings.LoadFromKey(m_settingKey);
            Application.Run(new MainWindow(s_settings));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                ShowError((Exception)e.ExceptionObject);
                Application.Exit();
            }
            catch { }
        }

        public static string SettingKey
        {
            get { return m_settingKey; }
        }

        public static Settings Settings
        {
            get { return s_settings; }
        }

        private static string m_settingKey;
        private static Settings s_settings;
        private static bool m_showWindowOnError = true;

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ShowError(e.Exception);
            try
            {
                Application.Exit();
            }
            catch { }
        }

        private static void ShowError(Exception e)
        {
            if (m_showWindowOnError)
            {
                m_showWindowOnError = false;
                using (UnhandledExceptionWindow f = new UnhandledExceptionWindow(e))
                {
                    f.ShowDialog();
                }
            }
        }
    }
}
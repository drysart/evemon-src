using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            string ca = String.Empty;
            if (Environment.GetCommandLineArgs().Length > 1)
                ca = Environment.GetCommandLineArgs()[1];
            m_settingKey = ca;

            EveSession.MainThread = System.Threading.Thread.CurrentThread;

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1(ca));
            s_settings = Settings.LoadFromKey(ca);
            Application.Run(new MainWindow(s_settings));
        }

        public static string SettingKey
        {
            get { return m_settingKey; }
        }

        private static string m_settingKey;
        private static Settings s_settings;
        private static bool m_showWindowOnError = true;

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (m_showWindowOnError)
            {
                m_showWindowOnError = false;
                using (UnhandledExceptionWindow f = new UnhandledExceptionWindow(e.Exception))
                {
                    f.ShowDialog();
                }
                Application.Exit();
            }
        }
    }
}
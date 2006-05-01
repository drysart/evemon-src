using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using EVEMon.Common;

namespace EVEMon
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            foreach (string targ in Environment.GetCommandLineArgs())
            {
                if (targ == "-netlog")
                {
                    StartNetlog();
                    if (m_logger == null)
                        return;
                }
            }

            m_settingKey = String.Empty;

            Plan.PlannerWindowFactory = new SkillPlanner.PlannerWindowFactory();
            EveSession.MainThread = System.Threading.Thread.CurrentThread;

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //Application.Run(new Form1(ca));
            s_settings = Settings.LoadFromKey(m_settingKey);
            Application.Run(new MainWindow(s_settings));
            s_settings.Save();

            SetRelocatorState(false);
            if (m_logger != null)
                m_logger.Dispose();
        }

        private static MainWindow m_mainWindow;

        public static MainWindow MainWindow
        {
            get { return m_mainWindow; }
            set { m_mainWindow = value; }
        }

        private static IDisposable m_logger;

        private static void StartNetlog()
        {
            m_logger = EVEMon.NetworkLogger.Logger.StartLogging();
        }

        private static bool m_relocatorRunning = false;

        public static void SetRelocatorState(bool state)
        {
            if (!state && !m_relocatorRunning)
                return;
            InternalSetRelocatorState(state);
        }

        private static void InternalSetRelocatorState(bool state)
        {
            if (state)
            {
                m_relocatorRunning = true;
                EVEMon.WindowRelocator.Relocator.Start(Program.Settings.RelocateTargetScreen);
            }
            else
            {
                m_relocatorRunning = false;
                EVEMon.WindowRelocator.Relocator.Stop();
            }
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
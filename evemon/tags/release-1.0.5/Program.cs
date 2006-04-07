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

            EveSession.MainThread = System.Threading.Thread.CurrentThread;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1(ca));
            Application.Run(new MainWindow(Settings.LoadFromKey(ca)));
        }
    }
}
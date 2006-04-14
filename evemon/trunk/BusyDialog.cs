using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EveCharacterMonitor
{
    public partial class BusyDialog : Form
    {
        public BusyDialog()
        {
            InitializeComponent();
        }

        internal void Complete()
        {
            this.Invoke(new MethodInvoker(delegate
            {
                this.Close();
            }));
        }

        private static int m_displayCounter = 0;
        private static Thread m_runThread;
        private static BusyDialog m_instance;

        public static void IncrementDisplay()
        {
            if (m_displayCounter == 0)
            {
                AutoResetEvent startEvent = new AutoResetEvent(false);
                m_runThread = new Thread(new ThreadStart(delegate
                {
                    using (BusyDialog d = new BusyDialog())
                    {
                        m_instance = d;
                        d.Shown += new EventHandler(delegate { startEvent.Set(); });
                        d.ShowDialog();
                        m_instance = null;
                    }
                }));
                m_runThread.Start();
                startEvent.WaitOne();
            }
            m_displayCounter++;
        }

        public static void DecrementDisplay()
        {
            m_displayCounter--;
            if (m_displayCounter <= 0)
            {
                m_displayCounter = 0;
                m_instance.Invoke(new MethodInvoker(delegate
                {
                    m_instance.Close();
                }));
                m_runThread.Join();
                m_runThread = null;
            }
        }

        public static IDisposable GetScope()
        {
            return new ScopeController();
        }

        public class ScopeController : IDisposable
        {
            public ScopeController()
            {
                BusyDialog.IncrementDisplay();
            }

            #region IDisposable Members

            public void Dispose()
            {
                BusyDialog.DecrementDisplay();
            }

            #endregion
        }
    }
}
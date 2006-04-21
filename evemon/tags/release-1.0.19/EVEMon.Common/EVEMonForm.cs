using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace EVEMon.Common
{
    public partial class EVEMonForm : Form
    {
        public EVEMonForm()
        {
            InitializeComponent();
        }

        private string m_rememberPositionKey = null;

        public string RememberPositionKey
        {
            get { return m_rememberPositionKey; }
            set { m_rememberPositionKey = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!String.IsNullOrEmpty(m_rememberPositionKey))
            {
                Settings s = Settings.GetInstance();
                if (s.SavedWindowLocations.ContainsKey(m_rememberPositionKey))
                {
                    Rectangle r = s.SavedWindowLocations[m_rememberPositionKey];
                    this.SetBounds(r.Left, r.Top, r.Width, r.Height);
                }
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (!String.IsNullOrEmpty(m_rememberPositionKey))
            {
                Settings s = Settings.GetInstance();
                s.SavedWindowLocations[m_rememberPositionKey] =
                    new Rectangle(this.Location, this.Size);
            }

            base.OnFormClosed(e);
        }
    }
}
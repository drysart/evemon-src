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
                    r = VerifyValidWindowLocation(r);
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

        private Rectangle VerifyValidWindowLocation(Rectangle inRect)
        {
            Point p = inRect.Location;
            Size s = inRect.Size;
            s.Width = Math.Max(s.Width, this.MinimumSize.Width);
            s.Height = Math.Max(s.Height, this.MinimumSize.Height);

            foreach (Screen ts in Screen.AllScreens)
            {
                if (ts.WorkingArea.Contains(inRect))
                {
                    return inRect;
                }
                if (ts.WorkingArea.Contains(p))
                {
                    p.X = Math.Min(p.X, ts.WorkingArea.Right - s.Width);
                    p.Y = Math.Min(p.Y, ts.WorkingArea.Bottom - s.Height);
                    return new Rectangle(p, s);
                }
            }

            p.X = Screen.PrimaryScreen.WorkingArea.X + 5;
            p.Y = Screen.PrimaryScreen.WorkingArea.Y + 5;
            return new Rectangle(p, s);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EVEMon.Common;

namespace EVEMon.Schedule
{
    public partial class ScheduleEditorWindow : EVEMon.Common.EVEMonForm
    {
        public ScheduleEditorWindow()
        {
            InitializeComponent();
        }

        public ScheduleEditorWindow(Settings s)
            : this()
        {
            m_settings = s;
        }

        private Settings m_settings;

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (EditScheduleEntryWindow f = new EditScheduleEntryWindow())
            {
                DialogResult dr = f.ShowDialog();
                if (dr == DialogResult.Cancel)
                    return;
            }
        }

        private void ScheduleEditorWindow_Load(object sender, EventArgs e)
        {
            cbMonth.Items.Clear();
            string[] monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            for (int i = 0; i < 12; i++)
            {
                cbMonth.Items.Add(monthNames[i]);
            }
        }
    }
}


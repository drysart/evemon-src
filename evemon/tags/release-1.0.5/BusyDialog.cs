using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
    }
}
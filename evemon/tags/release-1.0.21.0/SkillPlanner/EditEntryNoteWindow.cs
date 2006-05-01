using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EVEMon.SkillPlanner
{
    public partial class EditEntryNoteWindow : EVEMon.Common.EVEMonForm
    {
        public EditEntryNoteWindow()
        {
            InitializeComponent();
        }

        public EditEntryNoteWindow(string skillName)
            : this()
        {
            this.Text = "Notes for " + skillName;
        }

        public string NoteText
        {
            get { return textBox1.Text; }
            set {
                if (String.IsNullOrEmpty(value))
                    value = String.Empty;
                textBox1.Lines = value.Split(new string[4] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            }
        }

        private void EditEntryNoteWindow_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}


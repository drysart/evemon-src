using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace EveCharacterMonitor
{
    public partial class SkillCompleteDialog : Form
    {
        public SkillCompleteDialog()
        {
            InitializeComponent();

            if (Application.RenderWithVisualStyles)
                m_renderer = new VisualStyleRenderer(VisualStyleElement.Window.Dialog.Normal);
        }

        public SkillCompleteDialog(List<string> skills)
            : this()
        {
            listBox1.Items.Clear();
            foreach (string s in skills)
            {
                listBox1.Items.Add(s);
            }
        }

        private VisualStyleRenderer m_renderer;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!Application.RenderWithVisualStyles)
                return;

            m_renderer.DrawBackground(e.Graphics, this.ClientRectangle);
        }

        private void SkillCompleteDialog_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
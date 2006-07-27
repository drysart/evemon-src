using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon
{
    public partial class SkillCompleteDialog : EVEMonForm
    {
        public SkillCompleteDialog()
        {
            InitializeComponent();

            //if (Application.RenderWithVisualStyles)
            //    m_renderer = new VisualStyleRenderer(VisualStyleElement.Window.Dialog.Normal);
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

        //private VisualStyleRenderer m_renderer;
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    if (!Application.RenderWithVisualStyles)
        //        return;

        //    m_renderer.DrawBackground(e.Graphics, this.ClientRectangle);
        //}

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

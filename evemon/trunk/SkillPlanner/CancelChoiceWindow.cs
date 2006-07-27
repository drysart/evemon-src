using System;
using System.Drawing;
using System.Windows.Forms;
using EVEMon.Common;

namespace EVEMon.SkillPlanner
{
    public partial class CancelChoiceWindow : EVEMonForm
    {
        public CancelChoiceWindow()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnThisOnly_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void CancelChoiceWindow_Load(object sender, EventArgs e)
        {
            int width = SystemIcons.Question.Width;
            int height = SystemIcons.Question.Height;

            Bitmap b = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawIcon(SystemIcons.Question, 0, 0);
            }

            pictureBox1.Image = b;
        }
    }
}

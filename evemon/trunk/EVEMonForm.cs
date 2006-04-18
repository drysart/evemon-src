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
    public partial class EVEMonForm : Form
    {
        public EVEMonForm()
        {
            InitializeComponent();
        }

        //private VisualStyleRenderer m_renderer;

        //protected override void OnLoad(EventArgs e)
        //{
        //    if (Application.RenderWithVisualStyles)
        //    {
        //        m_renderer = new VisualStyleRenderer(VisualStyleElement.Window.Dialog.Normal);
        //        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        //        BackColor = Color.Transparent;
        //    }

        //    base.OnLoad(e);
        //}

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    if (!Application.RenderWithVisualStyles)
        //        return;

        //    m_renderer.DrawBackground(e.Graphics, this.ClientRectangle);
        //}
    }
}
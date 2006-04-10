using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EveCharacterMonitor;

namespace EveCharacterMonitor.SkillPlanner
{
    public partial class NewPlannerWindow : Form
    {
        public NewPlannerWindow()
        {
            InitializeComponent();
        }

        private GrandCharacterInfo m_grandCharacterInfo;

        public NewPlannerWindow(GrandCharacterInfo gci)
            : this()
        {
            m_grandCharacterInfo = gci;
        }

        private void NewPlannerWindow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            skillTreeDisplay1.RootSkill = m_grandCharacterInfo.GetSkill("Caldari Dreadnought");
        }
    }
}
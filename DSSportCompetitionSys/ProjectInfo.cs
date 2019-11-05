using DSSportCompetitionSys.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSSportCompetitionSys
{
    public partial class ProjectInfo : Form
    {
        //private static ProjectInfo ProjectInfoInstance;
        private ProjectInfoEntity  thisProjectInfo = new ProjectInfoEntity();

        public ProjectInfo()
        {
            InitializeComponent();
        }

        public ProjectInfo(ProjectInfoEntity projectInfo)
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(projectInfo.Name))
            {
                tbName.Enabled = false;
            }

            thisProjectInfo = projectInfo;
            tbName.Text = projectInfo.Name;
            tbOrganizer.Text = projectInfo.Organizer;
            tbDate.Text = projectInfo.Date;
            tbPlace.Text = projectInfo.Place;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            thisProjectInfo.Name = tbName.Text.ToString();
            thisProjectInfo.Organizer = tbOrganizer.Text.ToString();
            thisProjectInfo.Place = tbPlace.Text.ToString();
            thisProjectInfo.Date = tbDate.Text.ToString();

            this.Close();
        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

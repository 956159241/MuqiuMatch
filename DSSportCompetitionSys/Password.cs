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
    public partial class Password : Form
    {
        public string InputPassword = string.Empty;
        public Password()
        {
            InitializeComponent();
        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            InputPassword = textBoxX1.Text;
            // 当输入错误密码时，提示密码错误
            if (!(textBoxX1.Text.Trim() == "123456"))
            {
                MessageBox.Show("请输入正确的密码！", "提示信息");
                return;
            }
            this.Close();
        }
    }
}

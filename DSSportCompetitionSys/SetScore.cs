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
    public partial class SetScore : Form
    {
        private DataGridViewRow OriginalRow = new DataGridViewRow();

        private bool CompareIsZero = false;
        public SetScore()
        {
            InitializeComponent();
        }

        public SetScore(DataGridViewRow originalRow, bool compareIsZero, int nComputeLunNo)
        {
            InitializeComponent();

            if (originalRow.Cells[0].Value == null || string.IsNullOrEmpty(originalRow.Cells[0].Value.ToString()))
            {
                return;
            }
            OriginalRow = originalRow;
            CompareIsZero = compareIsZero;

            // 设置标题
            Text = $"设置分数 - {originalRow.Cells[1].Value.ToString()}";
            // 三轮Row的长度为8， 4-9， 5-10， 6-11
            comboBoxEx1.DataSource = new List<string> { "第一轮", "第二轮", "第三轮", "第四轮", "第五轮", "第六轮" };
            if (nComputeLunNo > 0 && nComputeLunNo <= 6)
                comboBoxEx1.SelectedIndex = nComputeLunNo - 1;

            if (originalRow.Cells.Count == 10)
            {
                comboBoxEx1.DataSource = new List<string> { "第一轮", "第二轮", "第三轮", "第四轮", "第五轮" };
                comboBoxEx1.SelectedIndex = nComputeLunNo - 1;
            }
            if (originalRow.Cells.Count == 9)
            {
                comboBoxEx1.DataSource = new List<string> { "第一轮", "第二轮", "第三轮", "第四轮" };
                comboBoxEx1.SelectedIndex = nComputeLunNo - 1;
            }
            if (originalRow.Cells.Count == 8)
            {
                comboBoxEx1.DataSource = new List<string> { "第一轮", "第二轮", "第三轮" };
                comboBoxEx1.SelectedIndex = nComputeLunNo - 1;
            }

            if (compareIsZero && comboBoxEx1.SelectedItem.ToString().Trim() == "第一轮")
            {
                doubleInput1.Enabled = false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRecored = comboBoxEx1.SelectedItem.ToString();
                var value = doubleInput1.Value;
                if (value < 0)
                {
                    OriginalRow.Cells[selectedRecored].Value = "";
                }
                else
                {
                    OriginalRow.Cells[selectedRecored].Value = value.ToString();
                }
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"程序异常：{ex}", "提示信息");
            }

        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CompareIsZero && comboBoxEx1.SelectedItem.ToString().Trim() == "第一轮")
            {
                doubleInput1.Enabled = false;
            }
            else
            {
                doubleInput1.Enabled = true;
                if (!string.IsNullOrWhiteSpace(OriginalRow.Cells[comboBoxEx1.SelectedItem.ToString().Trim()].Value.ToString()))
                {
                    doubleInput1.Text = OriginalRow.Cells[comboBoxEx1.SelectedItem.ToString().Trim()].Value.ToString();
                }
                else
                {
                    doubleInput1.Text = "";
                }
            }

        }
    }
}

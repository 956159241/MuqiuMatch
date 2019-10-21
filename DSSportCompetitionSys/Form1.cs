using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSSportCompetitionSys
{
    public partial class Form1 : Form
    {
        private static string ProjectManageInfoPath = Path.Combine(Directory.GetCurrentDirectory(), "ProjectManageInfo.txt");
        public Form1()
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            dt.Columns.Add("名称", typeof(string));
            dt.Columns.Add("承办", typeof(string));
            dt.Columns.Add("时间", typeof(string));
            dt.Columns.Add("地点", typeof(string));

            GetProjectManageInfo(dt);


            dataGridViewX1.DataSource = dt;
        }

        private void GetProjectManageInfo(DataTable dt)
        {
            try
            {
                using (StreamReader stream = new StreamReader(ProjectManageInfoPath))
                {
                    var line = stream.ReadLine();
                    while (line != null)
                    {
                        var fields = line.Split(';');
                        if (fields.Length > 4 &&
                            (!(string.IsNullOrWhiteSpace(fields[0]) &&
                            string.IsNullOrWhiteSpace(fields[1]) &&
                            string.IsNullOrWhiteSpace(fields[2]) &&
                            string.IsNullOrWhiteSpace(fields[3]))))
                        {
                            var row = dt.NewRow();
                            row["名称"] = fields[0];
                            row["承办"] = fields[1];
                            row["时间"] = fields[2];
                            row["地点"] = fields[3];

                            dt.Rows.Add(row);
                        }
                        line = stream.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                // do nothing
            
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteStream(ProjectManageInfoPath);
        }

        private void WriteStream(string path)
        {
            StringBuilder info = new StringBuilder();

            for (int i = 0; i < dataGridViewX1.RowCount; i++)
            {
                for (int j = 0; j < dataGridViewX1.ColumnCount; j++)
                {
                    if (dataGridViewX1.Rows[i].Cells[j].Value == null)
                        continue;
                    info.Append(dataGridViewX1.Rows[i].Cells[j].Value.ToString() + ";");                
                }

                info.AppendLine();            
            }

            using (StreamWriter stream = new StreamWriter(path: path, append: false))
            {
                stream.Write(info.ToString());           
            }
            
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            var data = new DataGridViewRow();
            try
            {
                data = this.dataGridViewX1.SelectedRows[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show("请点击左侧空白单元格，选中整行后重新设置。", "友情提示");
                return;
            }

            ImportPersonInfoForm importForm = ImportPersonInfoForm.Instance(data);
            importForm.ShowDialog();
        }
    }
}

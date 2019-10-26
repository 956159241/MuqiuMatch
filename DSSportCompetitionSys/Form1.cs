using DSSportCompetitionSys.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DSSportCompetitionSys
{
    public partial class Form1 : Form
    {
        private static string ProjectManageInfoPath = Path.Combine(Directory.GetCurrentDirectory(), @"Temp\ProjectManageInfo.txt");

        public Form1()
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            dt.Columns.Add("名称(Name)", typeof(string));
            dt.Columns.Add("承办(Organizer)", typeof(string));
            dt.Columns.Add("时间(Date)", typeof(string));
            dt.Columns.Add("地点(Place)", typeof(string));

            GetProjectManageInfo(dt);

            dataGridViewX1.DataSource = dt;

            //设置宽度
            dataGridViewX1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                            row["名称(Name)"] = fields[0];
                            row["承办(Organizer)"] = fields[1];
                            row["时间(Date)"] = fields[2];
                            row["地点(Place)"] = fields[3];

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

            // 如果目录不存在，创建目录
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
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
                if (data.Cells[0].Value == null || string.IsNullOrWhiteSpace(data.Cells[0].Value.ToString()))
                {
                    MessageBox.Show("请设置项目名称后重新操作", "友情提示");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("请点击左侧空白单元格，选中整行后重新设置。", "友情提示");
                return;
            }

            ImportPersonInfoForm importForm = ImportPersonInfoForm.Instance(data);
            importForm.ShowDialog();
        }

        private void buttonBallot_Click(object sender, EventArgs e)
        {
            var data = new DataGridViewRow();
            try
            {
                data = this.dataGridViewX1.SelectedRows[0];
                if (data.Cells[0].Value == null || string.IsNullOrWhiteSpace(data.Cells[0].Value.ToString()))
                {
                    MessageBox.Show("请设置项目名称后重新操作", "友情提示");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("请点击左侧空白单元格，选中整行后重新设置。", "友情提示");
                return;
            }

            MatchPersonInfoForm importForm = MatchPersonInfoForm.Instance(data);
            importForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 设置默认选中第一行
            dataGridViewX1.Rows[0].Selected = true;
        }

        private void dataGridViewX1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // 获取当前行
                var selectRows = dataGridViewX1.SelectedRows;
                if (MessageBox.Show("是否删除选中行？", "友情提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (selectRows.Count > 0)
                    {
                        for (int i = 0; i < selectRows.Count; i++)
                        {
                            DataRowView drv = dataGridViewX1.SelectedRows[i].DataBoundItem as DataRowView;
                            if (drv == null)
                            {
                                continue;
                            }
                            drv.Delete();
                        }
                    }
                }
                // 删除行后，数据刷新
                dataGridViewX1.Refresh();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try

            {
                var data = new DataGridViewRow();
                try
                {
                    data = this.dataGridViewX1.SelectedRows[0];
                    if (data.Cells[0].Value == null || string.IsNullOrWhiteSpace(data.Cells[0].Value.ToString()))
                    {
                        MessageBox.Show("请设置项目名称后重新操作", "友情提示");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请点击左侧空白单元格，选中整行后重新设置。", "友情提示");
                    return;
                }
                string key = $"{data.Cells[0].Value.ToString()}_{data.Cells[1].Value.ToString()}_{data.Cells[2].Value.ToString()}_{data.Cells[3].Value.ToString()}";
                var PersonMatchInfoPath = Path.Combine(Directory.GetCurrentDirectory(), $@"Temp\{key.Replace(" ", "").Replace(":", "").Replace(";", "").Replace("*", "")}_PersonMatchInfoPath.txt");

                var infos = GetCachedPersonMatchInfo(PersonMatchInfoPath);
                if (infos.Count == 8)
                {
                    NPOIHelper.Export8PersonMatchInfo(infos, key.Replace(" ", "").Replace(":", "").Replace(";", "").Replace("*", ""));
                }
                else if (infos.Count == 32)
                {
                    NPOIHelper.Export32PersonMatchInfo(infos, key.Replace(" ", "").Replace(":", "").Replace(";", "").Replace("*", ""));
                }
                else if (infos.Count == 64)
                {
                    NPOIHelper.Export64PersonMatchInfo(infos, key.Replace(" ", "").Replace(":", "").Replace(";", "").Replace("*", ""));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序出现异常：" + ex, "提示信息");            
            }

        }

        private List<PersonInfo> GetCachedPersonMatchInfo(string path)
        {
            List<PersonInfo> infos = new List<PersonInfo>();
            try
            {                
                using (StreamReader stream = new StreamReader(path))
                {
                    var line = stream.ReadLine();
                    while (line != null)
                    {
                        PersonInfo info = new PersonInfo();
                        var fields = line.Split(';');
                        info.MatchDisplayNum = Convert.ToInt32(fields[0]);
                        info.Name = fields[1];
                        info.Organization = fields[2];
                        info.Num = Convert.ToInt32(fields[3]);
                        info.SeedNum = Convert.ToInt32(fields[4]);
                        info.FirstRoundscore = string.IsNullOrWhiteSpace(fields[5]) ? 0 : Convert.ToDouble(fields[5]);
                        info.SecondRoundscore = string.IsNullOrWhiteSpace(fields[6]) ? 0 : Convert.ToDouble(fields[6]);
                        info.ThirdRoundscore = string.IsNullOrWhiteSpace(fields[7]) ? 0 : Convert.ToDouble(fields[7]);
                        if (fields.Length >= 9)
                        {
                            info.FourthRoundscore = string.IsNullOrWhiteSpace(fields[8]) ? 0 : Convert.ToDouble(fields[8]);
                        }
                        if (fields.Length >= 10)
                        {
                            info.FifthRoundscore = string.IsNullOrWhiteSpace(fields[9]) ? 0 : Convert.ToDouble(fields[9]);
                        }
                        if (fields.Length == 11)
                        {
                            info.SixRoundscore = string.IsNullOrWhiteSpace(fields[10]) ? 0 : Convert.ToDouble(fields[10]);
                        }

                        line = stream.ReadLine();
                        infos.Add(info);
                    }
                }
            }
            catch (Exception ex)
            {
                // do nothing
            }
            return infos;
        }
    }
}
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
        private AutoSizeFormClass asc = new AutoSizeFormClass();

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

        //private void buttonBallot_Click(object sender, EventArgs e)
        //{
        //    var data = new DataGridViewRow();
        //    try
        //    {
        //        data = this.dataGridViewX1.SelectedRows[0];
        //        if (data.Cells[0].Value == null || string.IsNullOrWhiteSpace(data.Cells[0].Value.ToString()))
        //        {
        //            MessageBox.Show("请设置项目名称后重新操作", "友情提示");
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("请点击左侧空白单元格，选中整行后重新设置。", "友情提示");
        //        return;
        //    }


        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            // 设置默认选中第一行
            if (dataGridViewX1.Rows.Count > 0)
            {
                dataGridViewX1.Rows[0].Selected = true;
            }
        }

        private void dataGridViewX1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // 获取当前行
                var selectRows = dataGridViewX1.SelectedRows;
                if (MessageBox.Show("是否删除选中行？", "友情提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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

       
        //private void Form1_MinimumSizeChanged(object sender, EventArgs e)
        //{
        //}

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void labelX1_MouseHover(object sender, EventArgs e)
        {
            this.labelX1.ForeColor = System.Drawing.Color.Red;
        }

        private void labelX1_MouseLeave(object sender, EventArgs e)
        {
            this.labelX1.ForeColor = System.Drawing.Color.Black;
        }

        private void labelX5_MouseHover(object sender, EventArgs e)
        {
            this.labelX5.ForeColor = System.Drawing.Color.Red;
        }

        private void labelX5_MouseLeave(object sender, EventArgs e)
        {
            this.labelX5.ForeColor = System.Drawing.Color.Black;
        }

        private void labelX1_Click(object sender, EventArgs e)
        {
            ProjectInfoEntity info = new ProjectInfoEntity();

            ProjectInfo importForm = new ProjectInfo(info);
            var result = importForm.ShowDialog();

            if (string.IsNullOrEmpty(info.Name))
            {
                return; 
            }
            var dt = dataGridViewX1.DataSource as DataTable;
            var row = dt.NewRow();
            row["名称(Name)"] = info.Name;
            row["承办(Organizer)"] = info.Organizer;
            row["时间(Date)"] = info.Date;
            row["地点(Place)"] = info.Place;
            dt.Rows.Add(row);
        }

        private void labelX5_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count == 0)
            {
                return;
            }
            var data = this.dataGridViewX1.SelectedRows[0];
            var projectInfo = new ProjectInfoEntity();
            projectInfo.Name = data.Cells["名称(Name)"].Value.ToString();
            projectInfo.Organizer = data.Cells["承办(Organizer)"].Value.ToString();
            projectInfo.Date = data.Cells["时间(Date)"].Value.ToString();
            projectInfo.Place = data.Cells["地点(Place)"].Value.ToString();

            ProjectInfo importForm = new ProjectInfo(projectInfo);
            var result = importForm.ShowDialog();

            if (string.IsNullOrEmpty(projectInfo.Name))
            {
                return;
            }

            data.Cells["名称(Name)"].Value = projectInfo.Name;
            data.Cells["承办(Organizer)"].Value = projectInfo.Organizer;
            data.Cells["时间(Date)"].Value = projectInfo.Date;
            data.Cells["地点(Place)"].Value = projectInfo.Place;
        }

        private void btnManageGroup_Click(object sender, EventArgs e)
        {
            GroupsManage manageForm = new GroupsManage();
            manageForm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            WriteStream(ProjectManageInfoPath);
            this.Close();
        }
    }
}
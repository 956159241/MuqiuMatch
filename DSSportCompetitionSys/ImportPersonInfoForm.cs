using DSSportCompetitionSys.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSSportCompetitionSys
{
    public enum Sex { 男, 女 }

    public partial class ImportPersonInfoForm : Form
    {
        AutoSizeFormClass asc = new AutoSizeFormClass();

        private static DataGridViewRow ParentProjectInfo;

        private static ImportPersonInfoForm ImportPersonInfoFormInstance;

        private static string MatchPersonPath = string.Empty;

        public static ImportPersonInfoForm Instance(DataGridViewRow parentProjectInfo)
        {
            if (ImportPersonInfoFormInstance == null || ImportPersonInfoFormInstance.IsDisposed)
            {
                ParentProjectInfo = parentProjectInfo;

                string key = $"{ParentProjectInfo.Cells[0].Value.ToString()}_{ParentProjectInfo.Cells[1].Value.ToString()}_{ParentProjectInfo.Cells[2].Value.ToString()}_{ParentProjectInfo.Cells[3].Value.ToString()}";
                MatchPersonPath = Path.Combine(Directory.GetCurrentDirectory(), $@"Temp\{key.Replace(" ", "").Replace(":", "").Replace(";", "").Replace("*", "")}_AgainstInformation.txt");

                ImportPersonInfoFormInstance = new ImportPersonInfoForm();

                SetFormTitle(parentProjectInfo);
            }
            return ImportPersonInfoFormInstance;
        }

        private ImportPersonInfoForm()
        {
            InitializeComponent();

            // 设置人的信息列表
            InitialPersonInfoDataGridView();

            // 设置性别选择
            SetComboSex();

            // 设置组别
            SetComboGrop();

            // 获取已缓存的数据，再次打开不用重新导入设置
            GetProjectManageInfo();
        }

        private void InitialPersonInfoDataGridView()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(string));
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("性别", typeof(string));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("组别", typeof(string));
            dt.Columns.Add("种子", typeof(string));

            dataGridViewX1.DataSource = dt;
        }

        private void SetComboSex()
        {
            this.comboSex.Items.Add("男");
            this.comboSex.Items.Add("女");

            this.comboSex.SelectedIndex = 0;
        }

        private void SetComboGrop()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $@"Temp\groups.txt");
            Dictionary<string, string> groups = new Dictionary<string, string>();
            using (StreamReader stream = new StreamReader(path))
            {
                var line = stream.ReadLine();
                while (line != null)
                {
                    var fields = line.Split(';');
                    if (!groups.ContainsKey(fields[0]) && fields.Length > 2)
                    {
                        groups.Add(fields[0], fields[1]);
                    }
                    line = stream.ReadLine();
                }
            }

            foreach (var group in groups.Values)
            {
                this.comboGroup.Items.Add(group);
            }

            this.comboGroup.SelectedIndex = 0;
        }

        private static void SetFormTitle(DataGridViewRow parentProjectInfo)
        {
            if (ParentProjectInfo != null && ParentProjectInfo.Cells[0].Value != null && !string.IsNullOrWhiteSpace(ParentProjectInfo.Cells[0].Value.ToString()))
                ImportPersonInfoForm.Instance(parentProjectInfo).Text = $"设置参赛名单 - {ParentProjectInfo.Cells[0].Value.ToString()}";
        }

        private void GetProjectManageInfo()
        {
            InitialPersonInfoDataGridView();
            var dt = dataGridViewX1.DataSource as DataTable;
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), $@"Temp\groups.txt");
                Dictionary<string, string> groups = new Dictionary<string, string>();
                using (StreamReader stream = new StreamReader(path))
                {
                    var line = stream.ReadLine();
                    while (line != null)
                    {
                        var fields = line.Split(';');
                        if (!groups.ContainsKey(fields[0]) && fields.Length > 2)
                        {
                            groups.Add(fields[0], fields[1]);
                        }
                        line = stream.ReadLine();
                    }
                }

                using (StreamReader stream = new StreamReader(MatchPersonPath))
                {
                    var lineFirst = stream.ReadLine();
                    this.comboSex.SelectedIndex = (int)Enum.Parse(typeof(Sex), lineFirst.Split(';')[0]);
                    var key = groups.Where(x => x.Value.Trim() == lineFirst.Split(';')[1].Trim()).FirstOrDefault().Key;
                    this.comboGroup.SelectedIndex = Convert.ToInt32(key) - 1;
                    //this.comboGroup.SelectedIndex = 0;

                    var line = stream.ReadLine();
                    while (line != null)
                    {
                        var fields = line.Split(';');
                        var row = dt.NewRow();
                        row["序号"] = fields[0];
                        row["姓名"] = fields[1];
                        row["性别"] = fields[2];
                        row["单位"] = fields[3];
                        row["组别"] = fields[4];
                        row["种子"] = fields[5];

                        dt.Rows.Add(row);
                        line = stream.ReadLine();
                    }
                }
                dataGridViewX1.Refresh();
            }
            catch (Exception ex)
            {
                // do nothing
            }
        }

        private void buttonImportPersonInfo_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            var path = file.FileName;
            if (string.IsNullOrWhiteSpace(path))
                return;
            List<PersonInfoEntity> personInfo = new List<PersonInfoEntity>();
            try
            {
                personInfo = NPOIHelper.GetPersonInfo(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件无法打开，请检查文件是否已被打开或被破坏。");
                return;
            }            
            var data = personInfo.Where(x => x.Sex == this.comboSex.SelectedItem.ToString() && x.Group == this.comboGroup.SelectedItem.ToString()).ToList();

            InitialPersonInfoDataGridView();
            var dt = dataGridViewX1.DataSource as DataTable;
            for (int i = 0; i < data.Count; i++)
            {
                var row = dt.NewRow();
                row["序号"] = i + 1;
                row["姓名"] = data[i].Name;
                row["性别"] = data[i].Sex;
                row["单位"] = data[i].Organization;
                row["组别"] = data[i].Group;
                dt.Rows.Add(row);
            }
            dataGridViewX1.Refresh();
        }

        private void buttonResetPersonInfo_Click(object sender, EventArgs e)
        {
            InitialPersonInfoDataGridView();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteStream();
            this.Dispose();
        }

        private void WriteStream()
        {
            // 如果目录不存在，创建目录
            if (!Directory.Exists(Path.GetDirectoryName(MatchPersonPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(MatchPersonPath));
            }
            using (StreamWriter stream = new StreamWriter(path: MatchPersonPath, append: false))
            {
                stream.Write($"{this.comboSex.SelectedItem.ToString()};{this.comboGroup.SelectedItem.ToString()} \n");
            }

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
            if (!Directory.Exists(Path.GetDirectoryName(MatchPersonPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(MatchPersonPath));
            }
            using (StreamWriter stream = new StreamWriter(path: MatchPersonPath, append: true))
            {
                stream.Write(info.ToString());
            }
        }

        private void ImportPersonInfoForm_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
        }

        private void ImportPersonInfoForm_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count == 0)
            {
                return;            
            }
            var selectedRow = dataGridViewX1.SelectedRows[0];
            // 获取最大种子号
            dataGridViewX1.Refresh();
            int maxSeedNum = 0;
            for (int i = 0; i < dataGridViewX1.Rows.Count; i++)
            {
                if (dataGridViewX1.Rows[i] == selectedRow)
                {
                    continue;                
                }
                if (dataGridViewX1.Rows[i].Cells["种子"].Value != null && !string.IsNullOrWhiteSpace(dataGridViewX1.Rows[i].Cells["种子"].Value.ToString()))
                {
                    var seedNum = Convert.ToInt32(dataGridViewX1.Rows[i].Cells["种子"].Value.ToString());
                    if (seedNum > maxSeedNum)
                    {
                        maxSeedNum = seedNum;  
                    } 
                }
            }
            
            // 之所以没有直接按序输入，是以防输入中途推出程序，当再次输入时，应该从当前最大的种子数开始
            selectedRow.Cells["种子"].Value = (maxSeedNum + 1).ToString();
        }

        private void dataGridViewX1_DataSourceChanged(object sender, EventArgs e)
        {
        }

        private void dataGridViewX1_Sorted(object sender, EventArgs e)
        {
        }

        private void buttonBallot_Click(object sender, EventArgs e)
        {
            WriteStream();
            MatchPersonInfoForm importForm = MatchPersonInfoForm.Instance(ParentProjectInfo);
            importForm.ShowDialog();
        }
    }
}
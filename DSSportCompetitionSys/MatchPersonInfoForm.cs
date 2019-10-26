using DevComponents.DotNetBar;
using DSSportCompetitionSys.Entity;
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
    public partial class MatchPersonInfoForm : Form
    {
        private static DataGridViewRow ParentProjectInfo;

        private static MatchPersonInfoForm ImportPersonInfoFormInstance;

        private static string AgainstInformationPath = string.Empty;
        private static string PersonMatchInfoPath = string.Empty;

        public static MatchPersonInfoForm Instance()
        {
            return ImportPersonInfoFormInstance;
        }

        public static MatchPersonInfoForm Instance(DataGridViewRow parentProjectInfo)
        {
            if (ImportPersonInfoFormInstance == null || ImportPersonInfoFormInstance.IsDisposed)
            {
                ParentProjectInfo = parentProjectInfo;

                string key = $"{ParentProjectInfo.Cells[0].Value.ToString()}_{ParentProjectInfo.Cells[1].Value.ToString()}_{ParentProjectInfo.Cells[2].Value.ToString()}_{ParentProjectInfo.Cells[3].Value.ToString()}";
                AgainstInformationPath = Path.Combine(Directory.GetCurrentDirectory(), $@"Temp\{key.Replace(" ", "").Replace(":", "").Replace(";", "").Replace("*", "")}_AgainstInformation.txt");
                PersonMatchInfoPath = Path.Combine(Directory.GetCurrentDirectory(), $@"Temp\{key.Replace(" ", "").Replace(":", "").Replace(";", "").Replace("*", "")}_PersonMatchInfoPath.txt");

                ImportPersonInfoFormInstance = new MatchPersonInfoForm();

                SetFormTitle(parentProjectInfo);
            }
            return ImportPersonInfoFormInstance;
        }

        private MatchPersonInfoForm()
        {
            InitializeComponent();
            Initial8PersonInfoDataGridView();

            GetCachedPersonMatchInfo();
        }

        private void GenerateMatchInfo()
        {
            var per = GetCachPersonInfo();
            var matchInfos = Sort(per);
            if (matchInfos.Count == 8)
            {
                Initial8PersonInfoDataGridView();
            }
            else if (matchInfos.Count == 32)
            {
                Initial32PersonInfoDataGridView();
            }
            else if (matchInfos.Count == 64)
            {
                Initial64PersonInfoDataGridView();
            }

            var dt = dataGridViewX1.DataSource as DataTable;

            foreach (var info in matchInfos)
            {
                var row = dt.NewRow();
                row["原始序号"] = info.Num;
                row["种子"] = info.SeedNum;
                row["序号"] = info.MatchDisplayNum + 1;
                if (!string.IsNullOrEmpty(info.Name))
                {
                    row["姓名"] = info.Name;
                    row["单位"] = info.Organization;
                }
                else
                {
                    row["姓名"] = "轮空";
                    row["单位"] = "";
                }

                dt.Rows.Add(row);
            }

            SetSeedStyle();
        }

        private void Initial8PersonInfoDataGridView()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("原始序号", typeof(int));
            dt.Columns.Add("种子", typeof(int));

            dt.Columns.Add("第一轮", typeof(string));
            dt.Columns.Add("第二轮", typeof(string));
            dt.Columns.Add("第三轮", typeof(string));

            dataGridViewX1.DataSource = dt;
            //设置宽度
            dataGridViewX1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 设置显示样式
            dataGridViewX1.AllowUserToAddRows = false;
            dataGridViewX1.Columns[3].Visible = false;
            dataGridViewX1.Columns[4].Visible = false;
        }

        private void Initial32PersonInfoDataGridView()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("原始序号", typeof(int));
            dt.Columns.Add("种子", typeof(int));

            dt.Columns.Add("第一轮", typeof(string));
            dt.Columns.Add("第二轮", typeof(string));
            dt.Columns.Add("第三轮", typeof(string));

            dt.Columns.Add("第四轮", typeof(string));
            dt.Columns.Add("第五轮", typeof(string));
            //dt.Columns.Add("第六轮", typeof(double));

            dataGridViewX1.DataSource = dt;
            //设置宽度
            dataGridViewX1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 设置显示样式
            dataGridViewX1.AllowUserToAddRows = false;
            dataGridViewX1.Columns[3].Visible = false;
            dataGridViewX1.Columns[4].Visible = false;
        }

        private void Initial64PersonInfoDataGridView()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("原始序号", typeof(int));
            dt.Columns.Add("种子", typeof(int));

            dt.Columns.Add("第一轮", typeof(string));
            dt.Columns.Add("第二轮", typeof(string));
            dt.Columns.Add("第三轮", typeof(string));

            dt.Columns.Add("第四轮", typeof(string));
            dt.Columns.Add("第五轮", typeof(string));
            dt.Columns.Add("第六轮", typeof(string));

            dataGridViewX1.DataSource = dt;
            //设置宽度
            //dataGridViewX1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewX1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewX1.Font = new Font("宋体", 8); // 6轮

            // 设置显示样式
            dataGridViewX1.AllowUserToAddRows = false;
            dataGridViewX1.Columns[3].Visible = false;
            dataGridViewX1.Columns[4].Visible = false;
        }

        private void SetSeedStyle()
        {
            for (int i = 0; i < dataGridViewX1.RowCount; i++)
            {
                if (dataGridViewX1.Rows[i] == null || dataGridViewX1.Rows[i].Cells[4] == null || dataGridViewX1.Rows[i].Cells[4].Value == null)
                    continue;
                if (dataGridViewX1.Rows[i].Cells[4].Value.ToString() != "0")
                {
                    dataGridViewX1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private static void SetFormTitle(DataGridViewRow parentProjectInfo)
        {
            if (ParentProjectInfo != null && ParentProjectInfo.Cells[0].Value != null && !string.IsNullOrWhiteSpace(ParentProjectInfo.Cells[0].Value.ToString()))
                MatchPersonInfoForm.Instance(parentProjectInfo).Text = $"生成对阵表 - {ParentProjectInfo.Cells[0].Value.ToString()}";
        }

        private List<PersonInfo> GetCachPersonInfo()
        {
            List<PersonInfo> result = new List<PersonInfo>();
            try
            {
                using (StreamReader stream = new StreamReader(AgainstInformationPath))
                {
                    var lineFirst = stream.ReadLine();
                    var line = stream.ReadLine();
                    while (line != null)
                    {
                        var fields = line.Split(';');
                        PersonInfo info = new PersonInfo();
                        info.Num = Convert.ToInt32(fields[0]);
                        info.Name = fields[1];
                        info.Organization = fields[3];
                        if (string.IsNullOrWhiteSpace(fields[5]))
                            info.SeedNum = 0;
                        else
                            info.SeedNum = Convert.ToInt32(fields[5]);

                        result.Add(info);
                        line = stream.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                // do nothing
            }
            return result;
        }

        private List<PersonInfo> Sort(List<PersonInfo> persons)
        {
            var totalSeedNum = persons.Where(x => x.SeedNum > 0).Count();

            List<PersonInfo> listPerson = new List<PersonInfo>();
            if (persons.Count <= 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    listPerson.Add(new PersonInfo());
                }
            }
            else if (persons.Count > 8 && persons.Count <= 32)
            {
                for (int i = 0; i < 32; i++)
                {
                    listPerson.Add(new PersonInfo());
                }
            }
            else if (persons.Count > 32 && persons.Count <= 64)
            {
                for (int i = 0; i < 64; i++)
                {
                    listPerson.Add(new PersonInfo());
                }
            }

            // 给定列表一个显示序号
            if (persons.Count <= 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    listPerson[i].MatchDisplayNum = i;
                }
            }
            else if (persons.Count > 8 && persons.Count <= 32)
            {
                for (int i = 0; i < 32; i++)
                {
                    listPerson[i].MatchDisplayNum = i;
                }
            }
            else if (persons.Count > 32 && persons.Count <= 64)
            {
                for (int i = 0; i < 64; i++)
                {
                    listPerson[i].MatchDisplayNum = i;
                }
            }

            // 设置种子选手
            foreach (var seed in persons.Where(x => x.SeedNum > 0).OrderBy(x => x.SeedNum))
            {
                if (seed.SeedNum == 1)
                    ConvertFromNext(listPerson[0], seed);
                if (seed.SeedNum == 2)
                    ConvertFromNext(listPerson[listPerson.Count - 1], seed);
                if (seed.SeedNum == 3)
                    ConvertFromNext(listPerson[listPerson.Count / 4], seed);
                if(seed.SeedNum == 4)
                    ConvertFromNext(listPerson[(listPerson.Count / 4) * 3 - 1], seed);
                if (seed.SeedNum == 5)
                    ConvertFromNext(listPerson[listPerson.Count / 8], seed);
                if (seed.SeedNum == 6)
                    ConvertFromNext(listPerson[(listPerson.Count / 8) * 7 - 1], seed);
                if (seed.SeedNum == 7)
                    ConvertFromNext(listPerson[(listPerson.Count / 8) * 3], seed);
                if (seed.SeedNum == 8)
                    ConvertFromNext(listPerson[(listPerson.Count / 8) * 5 - 1], seed);
            }

            // 设置轮空
            var blankNum = listPerson.Count - persons.Count();
            if (blankNum > 0)
            {
                foreach (var seed in listPerson.Where(x => x.SeedNum > 0).OrderBy(x => x.SeedNum))
                {
                    if (seed.MatchDisplayNum % 2 == 0)
                    {
                        ConvertFromNext(listPerson[seed.MatchDisplayNum + 1], new PersonInfo() { Num = -1 });
                    }
                    else
                    {
                        ConvertFromNext(listPerson[seed.MatchDisplayNum - 1], new PersonInfo() { Num = -1 });
                    }
                    blankNum--;

                    if (blankNum == 0)
                        break;
                }
            }

            if (blankNum > 0)
            {
                foreach (var seed in listPerson.Where(x => x.SeedNum > 0).OrderBy(x => x.SeedNum))
                {
                    if (seed.Num % 2 == 0)
                    {
                        ConvertFromNext(listPerson[seed.MatchDisplayNum + 3], new PersonInfo() { Num = -1 });
                    }
                    else
                    {
                        ConvertFromNext(listPerson[seed.MatchDisplayNum - 3], new PersonInfo() { Num = -1 });
                    }
                    blankNum--;

                    if (blankNum == 0)
                        break;
                }
            }

            // 设置其他位置, 这个时候剩余的位置和剩余的人数量相同
            Random random = new Random();
            List<PersonInfo> normalPersons = new List<PersonInfo>();
            foreach (var p in persons.Where(x => x.SeedNum == 0))
            {
                normalPersons.Add(p);
            }

            // 如果没有种子选手，设置轮空位置
            for (int i = 0; i < blankNum; i++)
            {
                if (i == 0)
                    ConvertFromNext(listPerson[0], new PersonInfo() { Num = -1});
                if (i == 1)
                    ConvertFromNext(listPerson[listPerson.Count - 1], new PersonInfo() { Num = -1 });
                if (i == 2)
                    ConvertFromNext(listPerson[listPerson.Count / 4], new PersonInfo() { Num = -1 });
                if (i == 3)
                    ConvertFromNext(listPerson[(listPerson.Count / 4) * 3 - 1], new PersonInfo() { Num = -1 });
                if (i == 4)
                    ConvertFromNext(listPerson[listPerson.Count / 8], new PersonInfo() { Num = -1 });
                if (i == 5)
                    ConvertFromNext(listPerson[(listPerson.Count / 8) * 7 - 1], new PersonInfo() { Num = -1 });
                if (i == 6)
                    ConvertFromNext(listPerson[(listPerson.Count / 8) * 3], new PersonInfo() { Num = -1 });
                if (i == 7)
                    ConvertFromNext(listPerson[(listPerson.Count / 8) * 5 - 1], new PersonInfo() { Num = -1 });
            }

            // 把四个区分组设置好
            var partA = listPerson.Skip(0).Take(listPerson.Count() / 4).ToList();
            var partB = listPerson.Skip(listPerson.Count() / 4).Take(listPerson.Count() / 4).ToList();
            var partC = listPerson.Skip(listPerson.Count() / 2).Take(listPerson.Count() / 4).ToList();
            var partD = listPerson.Skip((listPerson.Count() / 4) * 3).Take(listPerson.Count() / 4).ToList();

            foreach (var position in listPerson.Where(x => x.Num == 0).ToArray())
            {
                if (normalPersons.Count == 0)
                    break;
                var next = normalPersons[random.Next(normalPersons.Count() - 1)];

                if (partA.Where(x => x.Num == 0).Count() > 0 && (!partA.Select(x => x.Organization).Contains(next.Organization) && !partB.Select(x => x.Organization).Contains(next.Organization)))
                {
                    ConvertFromNext(listPerson[partA.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                }
                else if (partB.Where(x => x.Num == 0).Count() > 0 && (!partA.Select(x => x.Organization).Contains(next.Organization) && !partB.Select(x => x.Organization).Contains(next.Organization)))
                {
                    ConvertFromNext(listPerson[partB.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                }
                else if (partC.Where(x => x.Num == 0).Count() > 0 && (!partC.Select(x => x.Organization).Contains(next.Organization) && !partD.Select(x => x.Organization).Contains(next.Organization)))
                {
                    ConvertFromNext(listPerson[partC.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                }
                else if (partD.Where(x => x.Num == 0).Count() > 0 && (!partC.Select(x => x.Organization).Contains(next.Organization) && !partD.Select(x => x.Organization).Contains(next.Organization)))
                {
                    ConvertFromNext(listPerson[partD.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                }
                else if (partA.Where(x => x.Num == 0).Count() > 0 && (!partA.Select(x => x.Organization).Contains(next.Organization)))
                {
                    ConvertFromNext(listPerson[partA.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                }
                else if (partB.Where(x => x.Num == 0).Count() > 0 && (!partB.Select(x => x.Organization).Contains(next.Organization)))
                {
                    ConvertFromNext(listPerson[partB.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                }
                else if (partC.Where(x => x.Num == 0).Count() > 0 && (!partC.Select(x => x.Organization).Contains(next.Organization)))
                {
                    ConvertFromNext(listPerson[partC.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                }
                else if (partD.Where(x => x.Num == 0).Count() > 0 && (!partD.Select(x => x.Organization).Contains(next.Organization)))
                {
                    ConvertFromNext(listPerson[partD.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                }
                else if(partA.Where(x => x.Num == 0).Count() > 0)
                {
                    ConvertFromNext(listPerson[partA.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                }
                else if (partB.Where(x => x.Num == 0).Count() > 0)
                {
                    ConvertFromNext(listPerson[partB.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                }
                else if (partC.Where(x => x.Num == 0).Count() > 0)
                {
                    ConvertFromNext(listPerson[partC.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                }
                else if (partD.Where(x => x.Num == 0).Count() > 0)
                {
                    ConvertFromNext(listPerson[partD.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                }

                normalPersons.Remove(next);
            }

            return listPerson;
        }

        private void ConvertFromNext(PersonInfo A, PersonInfo B)
        {
            A.Name = B.Name;
            A.Num = B.Num;
            A.SeedNum = B.SeedNum;
            A.Organization = B.Organization;
        }

        private void MatchPersonInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteStream();
            this.Dispose();
        }

        private void WriteStream()
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
                info.Remove(info.Length - 1, 1);

                info.AppendLine();
            }
            // 如果目录不存在，创建目录
            if (!Directory.Exists(Path.GetDirectoryName(PersonMatchInfoPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(PersonMatchInfoPath));
            }
            using (StreamWriter stream = new StreamWriter(path: PersonMatchInfoPath, append: false))
            {
                stream.Write(info.ToString());
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.Rows.Count == 0)
            {
                GenerateMatchInfo();
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Initial8PersonInfoDataGridView();
        }

        private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridViewX1_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {
        }

        private void dataGridViewX1_ContextMenuStripChanged(object sender, EventArgs e)
        {
        }

        private void dataGridViewX1_DataSourceChanged(object sender, EventArgs e)
        {
            SetSeedStyle();
        }

        private void dataGridViewX1_DefaultCellStyleChanged(object sender, EventArgs e)
        {
        }

        private void dataGridViewX1_MouseHover(object sender, EventArgs e)
        {
        }

        private void dataGridViewX1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

        private void dataGridViewX1_Sorted(object sender, EventArgs e)
        {
            SetSeedStyle();
        }

        private void MatchPersonInfoForm_Load(object sender, EventArgs e)
        {
            SetSeedStyle();
        }

        private void GetCachedPersonMatchInfo()
        {
            try
            {
                List<PersonInfo> infos = new List<PersonInfo>();
                using (StreamReader stream = new StreamReader(PersonMatchInfoPath))
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

                if (infos.Count == 8)
                {
                    Initial8PersonInfoDataGridView();
                }
                else if (infos.Count == 32)
                {
                    Initial32PersonInfoDataGridView();
                }
                else if (infos.Count == 64)
                {
                    Initial64PersonInfoDataGridView();
                }


                var dt = dataGridViewX1.DataSource as DataTable;

                foreach (var info in infos)
                {
                    var row = dt.NewRow();
                    row["序号"] = Convert.ToInt32(info.MatchDisplayNum);
                    row["原始序号"] = info.Num.ToString();
                    row["种子"] = info.SeedNum.ToString();
                    row["姓名"] = info.Name;
                    row["单位"] = info.Organization;
                    row["第一轮"] = info.FirstRoundscore == 0 ? "" : info.FirstRoundscore.ToString();
                    row["第二轮"] = info.SecondRoundscore == 0 ? "" : info.SecondRoundscore.ToString();
                    row["第三轮"] = info.ThirdRoundscore == 0 ? "" : info.ThirdRoundscore.ToString();
                    if (dt.Columns["第四轮"] != null)
                    {
                        row["第四轮"] = info.FourthRoundscore == 0 ? "" : info.FourthRoundscore.ToString();
                    }
                    if (dt.Columns["第五轮"] != null)
                    {
                        row["第五轮"] = info.FifthRoundscore == 0 ? "" : info.FifthRoundscore.ToString();
                    }
                    if (dt.Columns["第六轮"] != null)
                    {
                        row["第六轮"] = info.SixRoundscore == 0 ? "" : info.SixRoundscore.ToString();
                    }

                    dt.Rows.Add(row);
                }

                SetSeedStyle();

                dataGridViewX1.Refresh();
            }
            catch (Exception ex)
            {
                // do nothing
            }
        }
    }
}
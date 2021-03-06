﻿using DevComponents.DotNetBar;
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

        AutoSizeFormClass asc = new AutoSizeFormClass();

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

            if (dataGridViewX1.Rows.Count == 0)
            {
                GenerateMatchInfo();
            }
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
            dataGridViewX1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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

            dataGridViewX1.Columns[0].ReadOnly = true;
            dataGridViewX1.Columns[3].ReadOnly = true;
            dataGridViewX1.Columns[4].ReadOnly = true;
        }

        private static void SetFormTitle(DataGridViewRow parentProjectInfo)
        {
            if (ParentProjectInfo != null && ParentProjectInfo.Cells[0].Value != null && !string.IsNullOrWhiteSpace(ParentProjectInfo.Cells[0].Value.ToString()))
                MatchPersonInfoForm.Instance(parentProjectInfo).Text = $"生成对阵表 - {ParentProjectInfo.Cells[0].Value.ToString()}";
        }

        private List<PersonInfoEntity> GetCachPersonInfo()
        {
            List<PersonInfoEntity> result = new List<PersonInfoEntity>();
            try
            {
                using (StreamReader stream = new StreamReader(AgainstInformationPath))
                {
                    var lineFirst = stream.ReadLine();
                    var line = stream.ReadLine();
                    while (line != null)
                    {
                        var fields = line.Split(';');
                        PersonInfoEntity info = new PersonInfoEntity();
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


        private List<PersonInfoEntity> Sort(List<PersonInfoEntity> persons)
        {
            var totalSeedNum = persons.Where(x => x.SeedNum > 0).Count();
            // 根据数据条数，显示比赛几轮
            List<PersonInfoEntity> listPerson = new List<PersonInfoEntity>();
            if (persons.Count <= 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    listPerson.Add(new PersonInfoEntity());
                }
            }
            else if (persons.Count > 8 && persons.Count <= 32)
            {
                for (int i = 0; i < 32; i++)
                {
                    listPerson.Add(new PersonInfoEntity());
                }
            }
            else if (persons.Count > 32 && persons.Count <= 64)
            {
                for (int i = 0; i < 64; i++)
                {
                    listPerson.Add(new PersonInfoEntity());
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
                if (seed.SeedNum == 4)
                    ConvertFromNext(listPerson[(listPerson.Count / 4) * 3 - 1], seed);
                if (seed.SeedNum == 5)
                    ConvertFromNext(listPerson[listPerson.Count / 8], seed);
                if (seed.SeedNum == 6)
                    ConvertFromNext(listPerson[(listPerson.Count / 8) * 7 - 1], seed);
                if (seed.SeedNum == 7)
                    ConvertFromNext(listPerson[(listPerson.Count / 8) * 3], seed);
                if (seed.SeedNum == 8)
                    ConvertFromNext(listPerson[(listPerson.Count / 8) * 5 - 1], seed);
                if (seed.SeedNum == 9)
                    ConvertFromNext(listPerson[listPerson.Count / 16], seed);
                if (seed.SeedNum == 10)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 15 - 1], seed);
                if (seed.SeedNum == 11)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 3], seed);
                if (seed.SeedNum == 12)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 13 - 1], seed);
                if (seed.SeedNum == 13)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 5], seed);
                if (seed.SeedNum == 14)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 11 - 1], seed);
                if (seed.SeedNum == 15)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 7], seed);
                if (seed.SeedNum == 16)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 9 - 1], seed);
                if (seed.SeedNum == 17)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 31 - 1], seed);
                if (seed.SeedNum == 18)
                    ConvertFromNext(listPerson[(listPerson.Count / 32)], seed);
                if (seed.SeedNum == 19)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 29 - 1], seed);
                if (seed.SeedNum == 20)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 3], seed);
                if (seed.SeedNum == 21)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 27 - 1], seed);
                if (seed.SeedNum == 22)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 5], seed);
                if (seed.SeedNum == 23)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 25 - 1], seed);
                if (seed.SeedNum == 24)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 7], seed);
                if (seed.SeedNum == 25)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 23 - 1], seed);
                if (seed.SeedNum == 26)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 9], seed);
                if (seed.SeedNum == 27)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 21 - 1], seed);
                if (seed.SeedNum == 28)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 11], seed);
                if (seed.SeedNum == 29)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 19 - 1], seed);
                if (seed.SeedNum == 30)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 13], seed);
                if (seed.SeedNum == 31)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 17 - 1], seed);
                if (seed.SeedNum == 32)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 15], seed);
            }

            // 设置轮空
            var blankNum = listPerson.Count - persons.Count();
            if (blankNum > 0)
            {
                foreach (var seed in listPerson.Where(x => x.SeedNum > 0).OrderBy(x => x.SeedNum))
                {
                    if (seed.MatchDisplayNum % 2 == 0)
                    {
                        ConvertFromNext(listPerson[seed.MatchDisplayNum + 1], new PersonInfoEntity() { Num = -1 });
                    }
                    else
                    {
                        ConvertFromNext(listPerson[seed.MatchDisplayNum - 1], new PersonInfoEntity() { Num = -1 });
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
                        ConvertFromNext(listPerson[seed.MatchDisplayNum + 3], new PersonInfoEntity() { Num = -1 });
                    }
                    else
                    {
                        ConvertFromNext(listPerson[seed.MatchDisplayNum - 3], new PersonInfoEntity() { Num = -1 });
                    }
                    blankNum--;

                    if (blankNum == 0)
                        break;
                }
            }

            // 设置其他位置, 这个时候剩余的位置和剩余的人数量相同
            Random random = new Random();
            List<PersonInfoEntity> normalPersons = new List<PersonInfoEntity>();
            foreach (var p in persons.Where(x => x.SeedNum == 0))
            {
                normalPersons.Add(p);
            }

            // 根据每队人数，从大到小进行排序
            //var allOrganizations = normalPersons.Select(x => x.Organization).Distinct().ToList();
            //var allOrganizationsWithCount = new Dictionary<string, int>();
            //foreach (var or in allOrganizations)
            //{
            //    var orName = or;
            //    var count = normalPersons.Where(x => x.Organization == orName).Count();
            //    allOrganizationsWithCount.Add(orName, count);
            //}

            // var sortedOrganiztions = allOrganizationsWithCount.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();


            // 如果没有种子选手，设置轮空位置
            for (int i = 0; i < blankNum; i++)
            {
                if (i == 0)
                    ConvertFromNext(listPerson[0], new PersonInfoEntity() { Num = -1 });
                if (i == 1)
                    ConvertFromNext(listPerson[listPerson.Count - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 2)
                    ConvertFromNext(listPerson[listPerson.Count / 4], new PersonInfoEntity() { Num = -1 });
                if (i == 3)
                    ConvertFromNext(listPerson[(listPerson.Count / 4) * 3 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 4)
                    ConvertFromNext(listPerson[listPerson.Count / 8], new PersonInfoEntity() { Num = -1 });
                if (i == 5)
                    ConvertFromNext(listPerson[(listPerson.Count / 8) * 7 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 6)
                    ConvertFromNext(listPerson[(listPerson.Count / 8) * 3], new PersonInfoEntity() { Num = -1 });
                if (i == 7)
                    ConvertFromNext(listPerson[(listPerson.Count / 8) * 5 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 8)
                    ConvertFromNext(listPerson[listPerson.Count / 16], new PersonInfoEntity() { Num = -1 });
                if (i == 9)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 15 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 10)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 3], new PersonInfoEntity() { Num = -1 });
                if (i == 11)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 13 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 12)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 5], new PersonInfoEntity() { Num = -1 });
                if (i == 13)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 11 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 14)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 7], new PersonInfoEntity() { Num = -1 });
                if (i == 15)
                    ConvertFromNext(listPerson[(listPerson.Count / 16) * 9 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 16)
                    ConvertFromNext(listPerson[(listPerson.Count / 32)], new PersonInfoEntity() { Num = -1 });
                if (i == 17)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 31 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 18)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 3], new PersonInfoEntity() { Num = -1 });
                if (i == 19)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 29 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 20)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 5], new PersonInfoEntity() { Num = -1 });
                if (i == 21)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 27 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 22)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 7], new PersonInfoEntity() { Num = -1 });
                if (i == 23)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 25 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 24)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 9], new PersonInfoEntity() { Num = -1 });
                if (i == 25)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 23 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 26)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 11], new PersonInfoEntity() { Num = -1 });
                if (i == 27)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 21 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 28)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 13], new PersonInfoEntity() { Num = -1 });
                if (i == 29)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 19 - 1], new PersonInfoEntity() { Num = -1 });
                if (i == 30)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 15], new PersonInfoEntity() { Num = -1 });
                if (i == 31)
                    ConvertFromNext(listPerson[(listPerson.Count / 32) * 17 - 1], new PersonInfoEntity() { Num = -1 });
            }


            if (listPerson.Count > 32)
            {
                // 把8个区分组设置好,64分8区
                var partA = listPerson.Skip(0).Take(listPerson.Count() / 8).ToList();
                var partB = listPerson.Skip(listPerson.Count() / 8).Take(listPerson.Count() / 8).ToList();
                var partC = listPerson.Skip((listPerson.Count() / 8) * 2).Take(listPerson.Count() / 8).ToList();
                var partD = listPerson.Skip((listPerson.Count() / 8) * 3).Take(listPerson.Count() / 8).ToList();
                var partE = listPerson.Skip((listPerson.Count() / 8) * 4).Take(listPerson.Count() / 8).ToList();
                var partF = listPerson.Skip((listPerson.Count() / 8) * 5).Take(listPerson.Count() / 8).ToList();
                var partG = listPerson.Skip((listPerson.Count() / 8) * 6).Take(listPerson.Count() / 8).ToList();
                var partH = listPerson.Skip((listPerson.Count() / 8) * 7).Take(listPerson.Count() / 8).ToList();

                var orNum = 0;
                foreach (var position in listPerson.Where(x => x.Num == 0).ToArray())
                {
                    if (normalPersons.Count == 0)
                        break;

                    var next = normalPersons[random.Next(normalPersons.Count())];
                    //var next = new PersonInfoEntity();
                    //var orName = sortedOrganiztions[orNum];
                    //if (normalPersons.Where(x => x.Organization == orName).Count() > 0)
                    //{
                    //    var list = normalPersons.Where(x => x.Organization == orName).ToList();
                    //    next = list[random.Next(list.Count())];
                    //}
                    //else
                    //{
                    //    orNum++;
                    //    orName = sortedOrganiztions[orNum];
                    //    var list = normalPersons.Where(x => x.Organization == orName).ToList();
                    //    next = list[random.Next(list.Count())];
                    //}

                    // 两区
                    if (partA.Where(x => x.Num == 0).Count() > 0 &&
                        (!partA.Select(x => x.Organization).Contains(next.Organization) && !partB.Select(x => x.Organization).Contains(next.Organization)) &&
                        (!partC.Select(x => x.Organization).Contains(next.Organization) && !partD.Select(x => x.Organization).Contains(next.Organization))
                        )
                    {
                        ConvertFromNext(listPerson[partA.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partH.Where(x => x.Num == 0).Count() > 0 &&
    (!partE.Select(x => x.Organization).Contains(next.Organization) && !partF.Select(x => x.Organization).Contains(next.Organization)) &&
    (!partG.Select(x => x.Organization).Contains(next.Organization) && !partH.Select(x => x.Organization).Contains(next.Organization))
    )
                    {
                        ConvertFromNext(listPerson[partH.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partB.Where(x => x.Num == 0).Count() > 0 &&
                        (!partA.Select(x => x.Organization).Contains(next.Organization) && !partB.Select(x => x.Organization).Contains(next.Organization)) &&
                        (!partC.Select(x => x.Organization).Contains(next.Organization) && !partD.Select(x => x.Organization).Contains(next.Organization))
                        )
                    {
                        ConvertFromNext(listPerson[partB.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partG.Where(x => x.Num == 0).Count() > 0 &&
    (!partE.Select(x => x.Organization).Contains(next.Organization) && !partF.Select(x => x.Organization).Contains(next.Organization)) &&
    (!partG.Select(x => x.Organization).Contains(next.Organization) && !partH.Select(x => x.Organization).Contains(next.Organization))
    )
                    {
                        ConvertFromNext(listPerson[partG.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partC.Where(x => x.Num == 0).Count() > 0 &&
                        (!partA.Select(x => x.Organization).Contains(next.Organization) && !partB.Select(x => x.Organization).Contains(next.Organization)) &&
                        (!partC.Select(x => x.Organization).Contains(next.Organization) && !partD.Select(x => x.Organization).Contains(next.Organization))
                        )
                    {
                        ConvertFromNext(listPerson[partC.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partF.Where(x => x.Num == 0).Count() > 0 &&
     (!partE.Select(x => x.Organization).Contains(next.Organization) && !partF.Select(x => x.Organization).Contains(next.Organization)) &&
     (!partG.Select(x => x.Organization).Contains(next.Organization) && !partH.Select(x => x.Organization).Contains(next.Organization))
     )
                    {
                        ConvertFromNext(listPerson[partF.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partD.Where(x => x.Num == 0).Count() > 0 &&
                        (!partA.Select(x => x.Organization).Contains(next.Organization) && !partB.Select(x => x.Organization).Contains(next.Organization)) &&
                        (!partC.Select(x => x.Organization).Contains(next.Organization) && !partD.Select(x => x.Organization).Contains(next.Organization))
                        )
                    {
                        ConvertFromNext(listPerson[partD.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partE.Where(x => x.Num == 0).Count() > 0 &&
                        (!partE.Select(x => x.Organization).Contains(next.Organization) && !partF.Select(x => x.Organization).Contains(next.Organization)) &&
                        (!partG.Select(x => x.Organization).Contains(next.Organization) && !partH.Select(x => x.Organization).Contains(next.Organization))
                        )
                    {
                        ConvertFromNext(listPerson[partE.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }



                    // 四区
                    else if (partA.Where(x => x.Num == 0).Count() > 0 &&
                        (!partA.Select(x => x.Organization).Contains(next.Organization) && !partB.Select(x => x.Organization).Contains(next.Organization)))
                    {
                        ConvertFromNext(listPerson[partA.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partH.Where(x => x.Num == 0).Count() > 0 &&
    (!partG.Select(x => x.Organization).Contains(next.Organization) && !partH.Select(x => x.Organization).Contains(next.Organization))
    )
                    {
                        ConvertFromNext(listPerson[partH.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partB.Where(x => x.Num == 0).Count() > 0 &&
                        (!partA.Select(x => x.Organization).Contains(next.Organization) && !partB.Select(x => x.Organization).Contains(next.Organization))
                        )
                    {
                        ConvertFromNext(listPerson[partB.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }

                    else if (partG.Where(x => x.Num == 0).Count() > 0 &&
    (!partG.Select(x => x.Organization).Contains(next.Organization) && !partH.Select(x => x.Organization).Contains(next.Organization))
    )
                    {
                        ConvertFromNext(listPerson[partG.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }

                    else if (partC.Where(x => x.Num == 0).Count() > 0 &&
                        (!partC.Select(x => x.Organization).Contains(next.Organization) && !partD.Select(x => x.Organization).Contains(next.Organization))
                        )
                    {
                        ConvertFromNext(listPerson[partC.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partF.Where(x => x.Num == 0).Count() > 0 &&
(!partE.Select(x => x.Organization).Contains(next.Organization) && !partF.Select(x => x.Organization).Contains(next.Organization))
)
                    {
                        ConvertFromNext(listPerson[partF.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partD.Where(x => x.Num == 0).Count() > 0 &&
                        (!partC.Select(x => x.Organization).Contains(next.Organization) && !partD.Select(x => x.Organization).Contains(next.Organization))
                        )
                    {
                        ConvertFromNext(listPerson[partD.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partE.Where(x => x.Num == 0).Count() > 0 &&
                        (!partE.Select(x => x.Organization).Contains(next.Organization) && !partF.Select(x => x.Organization).Contains(next.Organization))
                        )
                    {
                        ConvertFromNext(listPerson[partE.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }



                    // 八区
                    else if (partA.Where(x => x.Num == 0).Count() > 0 &&
                        (!partA.Select(x => x.Organization).Contains(next.Organization)))
                    {
                        ConvertFromNext(listPerson[partA.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partH.Where(x => x.Num == 0).Count() > 0 &&
                           (!partH.Select(x => x.Organization).Contains(next.Organization))
                          )
                    {
                        ConvertFromNext(listPerson[partH.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partB.Where(x => x.Num == 0).Count() > 0 &&
                        (!partB.Select(x => x.Organization).Contains(next.Organization))
                        )
                    {
                        ConvertFromNext(listPerson[partB.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partG.Where(x => x.Num == 0).Count() > 0 &&
    (!partG.Select(x => x.Organization).Contains(next.Organization))
    )
                    {
                        ConvertFromNext(listPerson[partG.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partC.Where(x => x.Num == 0).Count() > 0 &&
                        (!partC.Select(x => x.Organization).Contains(next.Organization))
                        )
                    {
                        ConvertFromNext(listPerson[partC.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partF.Where(x => x.Num == 0).Count() > 0 &&
    (!partF.Select(x => x.Organization).Contains(next.Organization))
    )
                    {
                        ConvertFromNext(listPerson[partF.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partD.Where(x => x.Num == 0).Count() > 0 &&
                        (!partD.Select(x => x.Organization).Contains(next.Organization))
                        )
                    {
                        ConvertFromNext(listPerson[partD.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partE.Where(x => x.Num == 0).Count() > 0 &&
                        (!partE.Select(x => x.Organization).Contains(next.Organization))
                        )
                    {
                        ConvertFromNext(listPerson[partE.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }



                    // 八区最后填充
                    else if (partF.Where(x => x.Num == 0).Count() > 0)
                    {
                        ConvertFromNext(listPerson[partF.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partB.Where(x => x.Num == 0).Count() > 0)
                    {
                        ConvertFromNext(listPerson[partB.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partD.Where(x => x.Num == 0).Count() > 0)
                    {
                        ConvertFromNext(listPerson[partD.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partE.Where(x => x.Num == 0).Count() > 0)
                    {
                        ConvertFromNext(listPerson[partE.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partC.Where(x => x.Num == 0).Count() > 0)
                    {
                        ConvertFromNext(listPerson[partC.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }

                    else if (partG.Where(x => x.Num == 0).Count() > 0)
                    {
                        ConvertFromNext(listPerson[partG.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    else if (partA.Where(x => x.Num == 0).Count() > 0)
                    {
                        ConvertFromNext(listPerson[partA.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                   
                    else if (partH.Where(x => x.Num == 0).Count() > 0)
                    {
                        ConvertFromNext(listPerson[partH.Where(x => x.Num == 0).First().MatchDisplayNum], next);
                    }
                    normalPersons.Remove(next);
                }
            }
            else
            {

                // 把四个区分组设置好
                var partA = listPerson.Skip(0).Take(listPerson.Count() / 4).ToList();
                var partB = listPerson.Skip(listPerson.Count() / 4).Take(listPerson.Count() / 4).ToList();
                var partC = listPerson.Skip(listPerson.Count() / 2).Take(listPerson.Count() / 4).ToList();
                var partD = listPerson.Skip((listPerson.Count() / 4) * 3).Take(listPerson.Count() / 4).ToList();

                foreach (var position in listPerson.Where(x => x.Num == 0).ToArray())
                {
                    if (normalPersons.Count == 0)
                        break;
                    var next = normalPersons[random.Next(normalPersons.Count())];

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

                    // 避免一个组织超过两人的情况
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
                    else if (partA.Where(x => x.Num == 0).Count() > 0)
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
            }
            JustPersonPosition(listPerson);
            return listPerson;
        }


        // 由于是根据位置挑选人，最后几个人没有选择，极有可能在该区已经有了同组人员，所以这个时候做一个调整。这种数据只有可能在最后一个区出现
        // 所以只需要调整最后一个区即可
        public void JustPersonPosition(List<PersonInfoEntity> listPerson)
        {
            // 此处总共分为两个区，上半区和下半区
            var partA = listPerson.Skip(0).Take(listPerson.Count() / 2).ToList();
            var partB = listPerson.Skip((listPerson.Count() / 2)).Take(listPerson.Count() / 2).ToList();

            // 检查上半区和下半区是否有相同组织的

            foreach (var bperson in partB)
            {
                // 如果有一个组织超过两人，则找出上半区不在下半区的人替换位置
                if (bperson.SeedNum == 0 && bperson.Organization != null && partB.Where(x => x.Organization == bperson.Organization).Count() > 1)
                {
                    isSwitch(partB, partA, bperson);
                }
            }

            foreach (var aperson in partA)
            {
                // 如果有一个组织超过两人，则找出上半区不在下半区的人替换位置
                if (aperson.SeedNum == 0 && aperson.Organization != null && partA.Where(x => x.Organization == aperson.Organization).Count() > 1)
                {
                    isSwitch(partA, partB, aperson);
                }
            }


            Switch4(listPerson);
            Switch8(listPerson);

        }

        private bool isSwitch(List<PersonInfoEntity> originList, List<PersonInfoEntity> willSwitchList, PersonInfoEntity switchEntity) 
        {
            var result = false;

            foreach (var person in willSwitchList.Where(x => x.SeedNum == 0))
            {
                if (!originList.Select(x => x.Organization).ToList().Contains(person.Organization))
                {
                    var tempPerson = new PersonInfoEntity();
                    ConvertFromNext(tempPerson, switchEntity);
                    ConvertFromNext(switchEntity, person);
                    ConvertFromNext(person, tempPerson);
                    result = true;
                    break;
                }
            }

            return result;
        }


        private void Switch4(List<PersonInfoEntity> listPerson)
        {
            // 如果在每1/4区还有相同组织，继续调整
            var part4A = listPerson.Skip(0).Take(listPerson.Count() / 4).ToList();
            var part4B = listPerson.Skip((listPerson.Count() / 4)).Take(listPerson.Count() / 4).ToList();
            var part4C = listPerson.Skip((listPerson.Count() / 4) * 2).Take(listPerson.Count() / 4).ToList();
            var part4D = listPerson.Skip((listPerson.Count() / 4) * 3).Take(listPerson.Count() / 4).ToList();
            foreach (var dperson in part4D)
            {
                if (dperson.SeedNum == 0 && dperson.Organization != null && part4D.Where(x => x.Organization == dperson.Organization).Count() > 1)
                {
                    var isBreak = false;
                    isBreak = isSwitch(part4D, part4A, dperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4D, part4B, dperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4D, part4D, dperson);
                }


            }

            foreach (var cperson in part4C)
            {
                if (cperson.SeedNum == 0 && cperson.Organization != null && part4C.Where(x => x.Organization == cperson.Organization).Count() > 1)
                {
                    var isBreak = false;
                    isBreak = isSwitch(part4C, part4A, cperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4C, part4B, cperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4C, part4D, cperson);
                }
            }

            foreach (var bperson in part4B)
            {
                if (bperson.SeedNum == 0 && bperson.Organization != null && part4B.Where(x => x.Organization == bperson.Organization).Count() > 1)
                {
                    var isBreak = false;
                    isBreak = isSwitch(part4B, part4A, bperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4B, part4C, bperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4B, part4D, bperson);

                }
            }

            foreach (var aperson in part4A)
            {
                var count = part4A.Where(x => x.Organization == aperson.Organization).Count();
                if (aperson.SeedNum == 0 && aperson.Organization != null && count > 1)
                {
                    var isBreak = false;
                    isBreak = isSwitch(part4A, part4B, aperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4A, part4C, aperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4A, part4D, aperson);

                }
            }

        }

        private void Switch8(List<PersonInfoEntity> listPerson)
        {
            // 如果在每1/8区还有相同组织，继续调整
            var part4A = listPerson.Skip(0).Take(listPerson.Count() / 8).ToList();
            var part4B = listPerson.Skip((listPerson.Count() / 8)).Take(listPerson.Count() / 4).ToList();
            var part4C = listPerson.Skip((listPerson.Count() / 8) * 2).Take(listPerson.Count() / 4).ToList();
            var part4D = listPerson.Skip((listPerson.Count() / 8) * 3).Take(listPerson.Count() / 4).ToList();
            var part4E = listPerson.Skip((listPerson.Count() / 8) * 3).Take(listPerson.Count() / 4).ToList();
            var part4F = listPerson.Skip((listPerson.Count() / 8) * 3).Take(listPerson.Count() / 4).ToList();
            var part4G = listPerson.Skip((listPerson.Count() / 8) * 3).Take(listPerson.Count() / 4).ToList();
            var part4H = listPerson.Skip((listPerson.Count() / 8) * 3).Take(listPerson.Count() / 4).ToList();

            foreach (var person in part4D)
            {
                if (person.SeedNum == 0 && person.Organization != null && part4D.Where(x => x.Organization == person.Organization).Count() > 1)
                {
                    var isBreak = false;
                    isBreak = isSwitch(part4D, part4A, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4D, part4B, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4D, part4C, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4D, part4E, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4D, part4F, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4D, part4G, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4D, part4H, person);
                }
            }

            foreach (var person in part4E)
            {
                if (person.SeedNum == 0 && person.Organization != null && part4E.Where(x => x.Organization == person.Organization).Count() > 1)
                {
                    var isBreak = false;
                    isBreak = isSwitch(part4E, part4A, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4E, part4B, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4E, part4C, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4E, part4D, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4E, part4F, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4E, part4G, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4E, part4H, person);
                }
            }

            foreach (var person in part4C)
            {
                if (person.SeedNum == 0 && person.Organization != null && part4C.Where(x => x.Organization == person.Organization).Count() > 1)
                {
                    var isBreak = false;
                    isBreak = isSwitch(part4C, part4A, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4C, part4B, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4C, part4D, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4C, part4E, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4C, part4F, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4C, part4G, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4C, part4H, person);
                }
            }


            foreach (var person in part4F)
            {
                if (person.SeedNum == 0 && person.Organization != null && part4F.Where(x => x.Organization == person.Organization).Count() > 1)
                {
                    var isBreak = false;
                    isBreak = isSwitch(part4F, part4A, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4F, part4B, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4F, part4C, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4F, part4D, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4F, part4E, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4F, part4G, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4F, part4H, person);
                }
            }

            foreach (var person in part4B)
            {
                if (person.SeedNum == 0 && person.Organization != null && part4B.Where(x => x.Organization == person.Organization).Count() > 1)
                {
                    var isBreak = false;
                    isBreak = isSwitch(part4B, part4A, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4B, part4C, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4B, part4D, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4B, part4E, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4B, part4F, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4B, part4G, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4B, part4H, person);
                }
            }


            foreach (var person in part4G)
            {
                if (person.SeedNum == 0 && person.Organization != null && part4G.Where(x => x.Organization == person.Organization).Count() > 1)
                {
                    var isBreak = false;
                    isBreak = isSwitch(part4G, part4A, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4G, part4B, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4G, part4C, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4G, part4D, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4G, part4E, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4G, part4F, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4G, part4H, person);
                }
            }

            foreach (var aperson in part4A)
            {
                if (aperson.SeedNum == 0 && aperson.Organization != null && part4A.Where(x => x.Organization == aperson.Organization).Count() > 1)
                {
                    var isBreak = false;
                    isBreak = isSwitch(part4A, part4B, aperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4A, part4C, aperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4A, part4D, aperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4A, part4E, aperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4A, part4F, aperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4A, part4G, aperson);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4A, part4H, aperson);
                }
            }


            foreach (var person in part4H)
            {
                if (person.SeedNum == 0 && person.Organization != null && part4H.Where(x => x.Organization == person.Organization).Count() > 1)
                {
                    var isBreak = false;
                    isBreak = isSwitch(part4H, part4A, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4H, part4B, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4H, part4C, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4H, part4D, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4H, part4E, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4H, part4F, person);
                    if (isBreak) continue;
                    isBreak = isSwitch(part4H, part4G, person);
                }
            }

        }

        private void ConvertFromNext(PersonInfoEntity A, PersonInfoEntity B)
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
            if (MessageBox.Show("重置对阵表后信息将会全部清除，确定重置？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Initial8PersonInfoDataGridView();
            }
        }

        private void dataGridViewX1_DataSourceChanged(object sender, EventArgs e)
        {
            SetSeedStyle();
        }

        private void dataGridViewX1_Sorted(object sender, EventArgs e)
        {
            SetSeedStyle();
        }

        private void MatchPersonInfoForm_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            SetSeedStyle();
        }

        private void GetCachedPersonMatchInfo()
        {
            try
            {
                List<PersonInfoEntity> infos = new List<PersonInfoEntity>();
                using (StreamReader stream = new StreamReader(PersonMatchInfoPath))
                {
                    var line = stream.ReadLine();
                    while (line != null)
                    {
                        PersonInfoEntity info = new PersonInfoEntity();
                        var fields = line.Split(';');
                        info.MatchDisplayNum = Convert.ToInt32(fields[0]);
                        info.Name = fields[1];
                        info.Organization = fields[2];
                        info.Num = Convert.ToInt32(fields[3]);
                        info.SeedNum = Convert.ToInt32(fields[4]);
                        info.FirstRoundscore = string.IsNullOrWhiteSpace(fields[5]) ? -1 : Convert.ToDouble(fields[5]);
                        info.SecondRoundscore = string.IsNullOrWhiteSpace(fields[6]) ? -1 : Convert.ToDouble(fields[6]);
                        info.ThirdRoundscore = string.IsNullOrWhiteSpace(fields[7]) ? -1 : Convert.ToDouble(fields[7]);
                        if (fields.Length >= 9)
                        {
                            info.FourthRoundscore = string.IsNullOrWhiteSpace(fields[8]) ? -1 : Convert.ToDouble(fields[8]);
                        }
                        if (fields.Length >= 10)
                        {
                            info.FifthRoundscore = string.IsNullOrWhiteSpace(fields[9]) ? -1 : Convert.ToDouble(fields[9]);
                        }
                        if (fields.Length == 11)
                        {
                            info.SixRoundscore = string.IsNullOrWhiteSpace(fields[10]) ? -1 : Convert.ToDouble(fields[10]);
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
                    row["第一轮"] = info.FirstRoundscore == -1 ? "" : info.FirstRoundscore.ToString();
                    row["第二轮"] = info.SecondRoundscore == -1 ? "" : info.SecondRoundscore.ToString();
                    row["第三轮"] = info.ThirdRoundscore == -1 ? "" : info.ThirdRoundscore.ToString();
                    if (dt.Columns["第四轮"] != null)
                    {
                        row["第四轮"] = info.FourthRoundscore == -1 ? "" : info.FourthRoundscore.ToString();
                    }
                    if (dt.Columns["第五轮"] != null)
                    {
                        row["第五轮"] = info.FifthRoundscore == -1 ? "" : info.FifthRoundscore.ToString();
                    }
                    if (dt.Columns["第六轮"] != null)
                    {
                        row["第六轮"] = info.SixRoundscore == -1 ? "" : info.SixRoundscore.ToString();
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

        private void MatchPersonInfoForm_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count == 0)
            {
                return;
            }
            var data = this.dataGridViewX1.SelectedRows[0];
            int nCompLunNo = this.dataGridViewX1.CurrentCell.ColumnIndex - 4;
            bool compareIsZero = false;
            // 判断是否是2的倍数
            if (Convert.ToInt32(data.Cells[0].Value.ToString()) % 2 != 0)
            {
                if (this.dataGridViewX1.Rows[data.Index + 1].Cells[1].Value.ToString() == "轮空")
                {
                    compareIsZero = true;
                }
            }
            else
            {
                if (this.dataGridViewX1.Rows[data.Index - 1].Cells[1].Value.ToString() == "轮空")
                {
                    compareIsZero = true;
                }
            }

            SetScore form = new SetScore(data, compareIsZero, nCompLunNo);
            form.ShowDialog();

        }

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //判断双击的是否为标题
                if (e.RowIndex >= 0)
                {
                    if (dataGridViewX1.SelectedRows.Count == 0)
                    {
                        return;
                    }
                    var data = this.dataGridViewX1.SelectedRows[0];
                    int nCompLunNo = this.dataGridViewX1.CurrentCell.ColumnIndex - 4;
                    if (data.Cells[1].Value.ToString() == "轮空")
                    {
                        return;
                    }
                    bool compareIsZero = false;
                    if (Convert.ToInt32(data.Cells[0].Value.ToString()) % 2 != 0)
                    {
                        if (this.dataGridViewX1.Rows[data.Index + 1].Cells[1].Value.ToString() == "轮空")
                        {
                            compareIsZero = true;
                        }
                    }
                    else
                    {
                        if (this.dataGridViewX1.Rows[data.Index - 1].Cells[1].Value.ToString() == "轮空")
                        {
                            compareIsZero = true;
                        }
                    }

                    SetScore form = new SetScore(data, compareIsZero, nCompLunNo);
                    form.ShowDialog();
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                WriteStream();
                var infos = GetCachedPersonMatchInfo(PersonMatchInfoPath);
                var time = DateTime.Now.ToString("yyyyMMddHHmmss");
                if (infos.Count == 8)
                {
                    NPOIHelper.Export8PersonMatchInfo(infos, $"{ParentProjectInfo.Cells[0].Value.ToString()}_{time}");
                }
                else if (infos.Count == 32)
                {
                    NPOIHelper.Export32PersonMatchInfo(infos, $"{ParentProjectInfo.Cells[0].Value.ToString()}_{time}");
                }
                else if (infos.Count == 64)
                {
                    NPOIHelper.Export64PersonMatchInfo(infos, $"{ParentProjectInfo.Cells[0].Value.ToString()}_{time}");
                }

                // 修改label显示序号
                labelX10.Text = $"报表已生成，序号为：{time}";

                // 打开生成的报表
                System.Diagnostics.Process.Start($@"C:\Report\{ParentProjectInfo.Cells[0].Value.ToString()}_{time}.pdf");

            }
            catch (Exception ex)
            {
                MessageBox.Show("程序出现异常：" + ex, "提示信息");
            }
        }

        private List<PersonInfoEntity> GetCachedPersonMatchInfo(string path)
        {
            List<PersonInfoEntity> infos = new List<PersonInfoEntity>();
            try
            {
                using (StreamReader stream = new StreamReader(path))
                {
                    var line = stream.ReadLine();
                    while (line != null)
                    {
                        PersonInfoEntity info = new PersonInfoEntity();
                        var fields = line.Split(';');
                        info.MatchDisplayNum = Convert.ToInt32(fields[0]);
                        info.Name = fields[1];
                        info.Organization = fields[2];
                        info.Num = Convert.ToInt32(fields[3]);
                        info.SeedNum = Convert.ToInt32(fields[4]);
                        info.FirstRoundscore = string.IsNullOrWhiteSpace(fields[5]) ? -1 : Convert.ToDouble(fields[5]);
                        info.SecondRoundscore = string.IsNullOrWhiteSpace(fields[6]) ? -1 : Convert.ToDouble(fields[6]);
                        info.ThirdRoundscore = string.IsNullOrWhiteSpace(fields[7]) ? -1 : Convert.ToDouble(fields[7]);
                        if (fields.Length >= 9)
                        {
                            info.FourthRoundscore = string.IsNullOrWhiteSpace(fields[8]) ? -1 : Convert.ToDouble(fields[8]);
                        }
                        if (fields.Length >= 10)
                        {
                            info.FifthRoundscore = string.IsNullOrWhiteSpace(fields[9]) ? -1 : Convert.ToDouble(fields[9]);
                        }
                        if (fields.Length == 11)
                        {
                            info.SixRoundscore = string.IsNullOrWhiteSpace(fields[10]) ? -1 : Convert.ToDouble(fields[10]);
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

        private void labelX9_MouseHover(object sender, EventArgs e)
        {
            this.labelX9.ForeColor = Color.Red;
        }

        private void labelX9_MouseLeave(object sender, EventArgs e)
        {
            this.labelX9.ForeColor = Color.Gray;
        }

        private void labelX9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Report\");
        }

        private void lbCoverReset_Click(object sender, EventArgs e)
        {
            Password pwdFrom = new Password();
            pwdFrom.ShowDialog();
            if (pwdFrom.InputPassword.Trim() != "123456")
            {
                return;
            }
            this.lbCoverReset.Width = 0;
            this.buttonReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;

        }

        private void lbCoverGenerate_Click(object sender, EventArgs e)
        {
            this.lbCoverGenerate.Width = 0;
            this.buttonGenerate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
        }
    }
}
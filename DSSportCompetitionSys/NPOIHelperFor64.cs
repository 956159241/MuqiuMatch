using DSSportCompetitionSys.Entity;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSSportCompetitionSys
{
    public static partial class NPOIHelper
    {      
        // 导入数据时，获取人员名单
        public static List<PersonInfoEntity> GetPersonInfo(string path)
        {
            IWorkbook workbook = null;
            ISheet CurrentSheet;
            var extension = Path.GetExtension(path);
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            if (extension.ToUpper() == ".XLS") // 2003版本
            {
                workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
            }
            else if (extension.ToUpper() == ".XLSX") // 2007版本
            {
                workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook
            }
            CurrentSheet = workbook.GetSheetAt(0);  //获取第一个工作表

            List<PersonInfoEntity> result = new List<PersonInfoEntity>();
            IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据
            for (int i = 1; i <= CurrentSheet.LastRowNum; i++)  //对工作表每一行
            {
                row = CurrentSheet.GetRow(i);   //row读入第i行数据
                if (row != null)
                {
                    PersonInfoEntity info = new PersonInfoEntity();
                    if (row.GetCell(0) != null)
                        info.Name = row.GetCell(0).ToString();
                    if (row.GetCell(1) != null)
                        info.Sex = row.GetCell(1).ToString();
                    if (row.GetCell(2) != null)
                        info.Organization = row.GetCell(2).ToString();
                    if (row.GetCell(3) != null)
                        info.Group = row.GetCell(3).ToString();

                    result.Add(info);
                }
            }
            return result;
        }


        // 导出比赛人员名单及分数信息
        public static void Export64PersonMatchInfo(List<PersonInfoEntity> infos, string fileName)
        {
            XSSFWorkbook workbook = null;
            ISheet CurrentSheet;
            // 读取模板信息
            var tempPath = Path.Combine(Directory.GetCurrentDirectory(), @"Template\64Template.xlsx");
            var extension = Path.GetExtension(tempPath);
            FileStream fileStream = new FileStream(tempPath, FileMode.Open, FileAccess.Read);
            if (extension.ToUpper() == ".XLS") // 2003版本
            {
                //workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
            }
            else if (extension.ToUpper() == ".XLSX") // 2007版本
            {
                workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook
            }
            CurrentSheet = workbook.GetSheetAt(0);  //获取第一个工作表

            // 设置表头
            var headerContent = fileName.Split('_')[0];
            CurrentSheet.GetRow(0).GetCell(0).SetCellValue(headerContent);
            CurrentSheet.GetRow(64).GetCell(0).SetCellValue(headerContent);

            // 设置时间和序号
            CurrentSheet.GetRow(1).GetCell(31).SetCellValue(fileName.Split('_')[1]);
            CurrentSheet.GetRow(12).GetCell(31).SetCellValue(DateTime.ParseExact(fileName.Split('_')[1], "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss"));

            // 设置姓名和序号和第一轮分数
            SetNameAndFirstScoreFor64(infos, CurrentSheet);

            // 设置第二轮分数
            SetSecondScoreFor64(infos, CurrentSheet, workbook);

            //设置第三轮分数
            SetThirdScoreFor64(infos, CurrentSheet, workbook);

            // 设置第四轮分数
            SetFourthScoreFor64(infos, CurrentSheet, workbook);

            // 设置第五轮分数
            SetFifthScoreFor64(infos, CurrentSheet);

            // 设置第六轮分数
            SetSixthScoreFor64(infos, CurrentSheet);

            // 导出Excel文件
            // 如果目录不存在，创建目录
            string path = $@"C:\Report\{fileName}.xlsx";
            string pdfath = $@"C:\Report\{fileName}.pdf";
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            using (FileStream fileStream1 = File.OpenWrite(path))
            {
                workbook.Write(fileStream1);
            }

            // 导出PDF版本
            //载入Excel文档
            Workbook spireWorkbook = new Workbook();
            spireWorkbook.LoadFromFile(path);

            // 获取第一个张工作表
            Worksheet spireSheet = spireWorkbook.Worksheets[0];

            // 设置打印区域
            spireSheet.PageSetup.PrintArea = "A1:AN128";
            spireSheet.PageSetup.PaperSize = PaperSizeType.PaperDSheet;

            // 将指定范围的单元格保存位PDF
            spireSheet.SaveToPdf(pdfath);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("报表已生成");
            sb.AppendLine($"路径：{Path.GetDirectoryName(path)}");
            sb.AppendLine($"Excel版：{fileName}.xlsx");
            sb.AppendLine($"PDF版：{fileName}.pdf");

            MessageBox.Show(sb.ToString(), "提示");
        }

        private static void SetSixthScoreFor64(List<PersonInfoEntity> infos, ISheet CurrentSheet)
        {
            var sixthPersons = infos.Where(x => x.SixRoundscore >= 0).OrderByDescending(x => x.MatchDisplayNum).ToList();
            for (int i = 0; i < sixthPersons.Count(); i++)
            {
                CurrentSheet.GetRow(31).GetCell(5).SetCellValue(sixthPersons[i].SixRoundscore);
                var scoreOne = sixthPersons[i].SixRoundscore;
                var nameOne = sixthPersons[i].Name;
                i++;
                try
                {
                    CurrentSheet.GetRow(93).GetCell(5).SetCellValue(sixthPersons[i].SixRoundscore);
                    var scoreTwo = sixthPersons[i].SixRoundscore;
                    var nameTwo = sixthPersons[i].Name;

                    if (scoreOne <= scoreTwo)
                    {
                        CurrentSheet.GetRow(66).GetCell(6).SetCellValue($"{nameOne}");
                        CurrentSheet.GetRow(66).GetCell(4).SetCellValue($"{nameTwo}");
                    }
                    else
                    {
                        CurrentSheet.GetRow(66).GetCell(4).SetCellValue($"{nameOne}");
                        CurrentSheet.GetRow(66).GetCell(6).SetCellValue($"{nameTwo}");

                    }
                }
                catch (Exception ex)
                {
                    // 避免只输入一个人的分数，而没输入对手分数，找不到数据填充，造成异常        
                }

            }
        }

        private static void SetFifthScoreFor64(List<PersonInfoEntity> infos, ISheet CurrentSheet)
        {
            var rowNum = 16;
            var fifthPersons = infos.Where(x => x.FifthRoundscore >= 0).OrderByDescending(x => x.MatchDisplayNum).ToList();
            for (int i = 0; i < fifthPersons.Count(); i++)
            {
                CurrentSheet.GetRow(rowNum).GetCell(7).SetCellValue(fifthPersons[i].FifthRoundscore);
                var scoreOne = fifthPersons[i].FifthRoundscore;
                var nameOne = fifthPersons[i].Name;
                i++;

                try
                {
                    CurrentSheet.GetRow(rowNum + 30).GetCell(7).SetCellValue(fifthPersons[i].FifthRoundscore);
                    var scoreTwo = fifthPersons[i].FifthRoundscore;
                    var nameTwo = fifthPersons[i].Name;

                    if (scoreOne <= scoreTwo)
                    {
                        CurrentSheet.GetRow(rowNum + 15).GetCell(7).SetCellValue(nameOne);
                    }
                    else
                    {
                        CurrentSheet.GetRow(rowNum + 15).GetCell(7).SetCellValue(nameTwo);
                    }
                }
                catch (Exception ex)
                {
                    // 避免只输入一个人的分数，而没输入对手分数，找不到数据填充，造成异常        
                }


                rowNum += 64;
            }
        }

        private static void SetFourthScoreFor64(List<PersonInfoEntity> infos, ISheet CurrentSheet, XSSFWorkbook book)
        {
            ICellStyle style = SetWinScoreStyle(book);

            var rowNum = 8;
            var fourthPersons = infos.Where(x => x.FourthRoundscore >= 0).OrderByDescending(x => x.MatchDisplayNum).ToList();
            for (int i = 0; i < fourthPersons.Count(); i++)
            {
                CurrentSheet.GetRow(rowNum).GetCell(9).SetCellValue(fourthPersons[i].FourthRoundscore);
                try
                {
                    CurrentSheet.GetRow(rowNum + 14).GetCell(9).SetCellValue(fourthPersons[i + 1].FourthRoundscore);
                }
                catch (Exception ex)
                {
                    // 避免只输入一个人的分数，而没输入对手分数，找不到数据填充，造成异常        
                }

                // 设置样式和赢的人的姓名
                string winName = string.Empty;
                double firstScore = Convert.ToDouble(fourthPersons[i].FourthRoundscore);
                double secondScore = Convert.ToDouble(fourthPersons[i + 1].FourthRoundscore);
                if (secondScore > firstScore)
                {
                    // 如果第一个人的分数小于第二个人的分数， 输入第一个人的名字
                    CurrentSheet.GetRow(rowNum).GetCell(8).SetCellValue(fourthPersons[i + 1].Name);

                    // 设置第二个人的分数样式
                    CurrentSheet.GetRow(rowNum + 14).GetCell(9).CellStyle = style;
                }
                else
                {
                    // 如果第一个人的分数大于第二个人的分数， 输入第一个人的名字
                    CurrentSheet.GetRow(rowNum).GetCell(8).SetCellValue(fourthPersons[i].Name);

                    // 设置第一个人的分数样式
                    CurrentSheet.GetRow(rowNum).GetCell(9).CellStyle = style;
                }



                i++;
                rowNum += 32;
            }
        }

        private static void SetThirdScoreFor64(List<PersonInfoEntity> infos, ISheet CurrentSheet, XSSFWorkbook book)
        {
            ICellStyle style = SetWinScoreStyle(book);

            // 设置第三轮分数
            var rowNum = 4;
            var thirdPersons = infos.Where(x => x.ThirdRoundscore >= 0).OrderByDescending(x => x.MatchDisplayNum).ToList();
            for (int i = 0; i < thirdPersons.Count(); i++)
            {
                CurrentSheet.GetRow(rowNum).GetCell(11).SetCellValue(thirdPersons[i].ThirdRoundscore);
                try
                {
                    CurrentSheet.GetRow(rowNum + 6).GetCell(11).SetCellValue(thirdPersons[i + 1].ThirdRoundscore);
                }
                catch (Exception ex)
                {
                    // 避免只输入一个人的分数，而没输入对手分数，找不到数据填充，造成异常        
                }

                // 设置样式和赢的人的姓名
                string winName = string.Empty;
                double firstScore = Convert.ToDouble(thirdPersons[i].ThirdRoundscore);
                double secondScore = Convert.ToDouble(thirdPersons[i + 1].ThirdRoundscore);
                if (secondScore > firstScore)
                {
                    // 如果第一个人的分数小于第二个人的分数， 输入第一个人的名字
                    CurrentSheet.GetRow(rowNum).GetCell(10).SetCellValue(thirdPersons[i + 1].Name);

                    // 设置第二个人的分数样式
                    CurrentSheet.GetRow(rowNum + 6).GetCell(11).CellStyle = style;
                }
                else
                {
                    // 如果第一个人的分数大于第二个人的分数， 输入第一个人的名字
                    CurrentSheet.GetRow(rowNum).GetCell(10).SetCellValue(thirdPersons[i].Name);

                    // 设置第一个人的分数样式
                    CurrentSheet.GetRow(rowNum).GetCell(11).CellStyle = style;
                }


                i++;
                rowNum += 16;
            }
        }

        private static void SetSecondScoreFor64(List<PersonInfoEntity> infos, ISheet CurrentSheet, XSSFWorkbook book)
        {
            ICellStyle style = SetWinScoreStyle(book);

            // 设置第二轮分数
            var rowNum = 2;
            var secondePersons = infos.Where(x => x.SecondRoundscore >= 0).OrderByDescending(x => x.MatchDisplayNum).ToList();
            for (int i = 0; i < secondePersons.Count(); i++)
            {
                CurrentSheet.GetRow(rowNum).GetCell(13).SetCellValue(secondePersons[i].SecondRoundscore);
                try
                {
                    CurrentSheet.GetRow(rowNum + 2).GetCell(13).SetCellValue(secondePersons[i + 1].SecondRoundscore);
                }
                catch (Exception ex)
                {
                    // 避免只输入一个人的分数，而没输入对手分数，找不到数据填充，造成异常              
                }

                // 设置样式和赢的人的姓名
                string winName = string.Empty;
                double firstScore = Convert.ToDouble(secondePersons[i].SecondRoundscore);
                double secondScore = Convert.ToDouble(secondePersons[i + 1].SecondRoundscore);
                if (secondScore > firstScore)
                {
                    // 如果第一个人的分数小于第二个人的分数， 输入第一个人的名字
                    CurrentSheet.GetRow(rowNum).GetCell(12).SetCellValue(secondePersons[i + 1].Name);

                    // 设置第二个人的分数样式
                    CurrentSheet.GetRow(rowNum + 2).GetCell(13).CellStyle = style;
                }
                else
                {
                    // 如果第一个人的分数大于第二个人的分数， 输入第一个人的名字
                    CurrentSheet.GetRow(rowNum).GetCell(12).SetCellValue(secondePersons[i].Name);

                    // 设置第一个人的分数样式
                    CurrentSheet.GetRow(rowNum).GetCell(13).CellStyle = style;
                }

                i++; // 一次循环处理两个人员信息
                rowNum += 8;
            }
        }

        private static ICellStyle SetWinScoreStyle(XSSFWorkbook book)
        {
            ICellStyle style = book.CreateCellStyle();
            style.Rotation = (short)90;

            IFont font = book.CreateFont();
            font.Color = 10;
            font.FontHeightInPoints = 9;
            font.IsBold = true;
            font.IsItalic = true;

            style.SetFont(font);
            return style;
        }

        private static void SetNameAndFirstScoreFor64(List<PersonInfoEntity> infos, ISheet CurrentSheet)
        {
            // 设置姓名和序号和第一轮分数
            var RowNum = 0;
            foreach (var person in infos.OrderByDescending(x => x.MatchDisplayNum))
            {               
                if (person.Name == "轮空")
                {
                    CurrentSheet.GetRow(RowNum).GetCell(18).SetCellValue($"{person.Name}");
                    CurrentSheet.GetRow(RowNum).GetCell(17).SetCellValue("");
                }
                else
                {
                    CurrentSheet.GetRow(RowNum).GetCell(18).SetCellValue($"{person.Name}({person.Organization})");
                    CurrentSheet.GetRow(RowNum).GetCell(17).SetCellValue(person.Num);
                }

                if (person.MatchDisplayNum % 2 == 0)
                {
                    // 如果分数为-1， 不填入分数
                    if (person.FirstRoundscore >= 0)
                    {
                        CurrentSheet.GetRow(RowNum + 1).GetCell(15).SetCellValue(person.FirstRoundscore);
                    }
                    else
                    {
                        CurrentSheet.GetRow(RowNum + 1).GetCell(15).SetCellValue("");
                    }

                }
                else
                {
                    if (person.FirstRoundscore >= 0)
                    {
                        CurrentSheet.GetRow(RowNum).GetCell(15).SetCellValue(person.FirstRoundscore);
                    }
                    else
                    {
                        CurrentSheet.GetRow(RowNum).GetCell(15).SetCellValue("");
                    }
                }

                RowNum += 2;
            }
        }
    }
}
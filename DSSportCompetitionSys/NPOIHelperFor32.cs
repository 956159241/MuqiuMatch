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
        public static void Export32PersonMatchInfo(List<PersonInfoEntity> infos, string fileName)
        {
            XSSFWorkbook workbook = null;
            ISheet CurrentSheet;
            // 读取模板信息
            var tempPath = Path.Combine(Directory.GetCurrentDirectory(), @"Template\32Template.xlsx");
            var extension = Path.GetExtension(tempPath);
            FileStream fileStream = new FileStream(tempPath, FileMode.Open, FileAccess.Read);
            if (extension.ToUpper() == ".XLS") // 2003版本
            {
               // workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
            }
            else if (extension.ToUpper() == ".XLSX") // 2007版本
            {
                workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook
            }
            CurrentSheet = workbook.GetSheetAt(0);  //获取第一个工作表

            // 设置表头
            var headerContent = fileName.Split('_')[0];
            CurrentSheet.GetRow(0).GetCell(0).SetCellValue(headerContent);

            // 设置时间和序号
            CurrentSheet.GetRow(1).GetCell(31).SetCellValue(fileName.Split('_')[1]);
            CurrentSheet.GetRow(12).GetCell(31).SetCellValue(DateTime.ParseExact(fileName.Split('_')[1], "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss"));

            // 设置姓名和序号和第一轮分数
            SetNameAndFirstScoreFor32(infos, CurrentSheet);

            // 设置第二轮分数
            SetSecondScoreFor32(infos, CurrentSheet, workbook);

            //设置第三轮分数
            SetThirdScoreFor32(infos, CurrentSheet, workbook);

            // 设置第四轮分数
            SetFourthScoreFor32(infos, CurrentSheet);

            // 设置第五轮分数
            SetFifthScoreFor32(infos, CurrentSheet);

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
            spireSheet.PageSetup.PrintArea = "A1:AF67";
           // spireSheet.PageSetup.PaperSize = PaperSizeType.PaperDSheet;
            spireSheet.PageSetup.PaperSize = PaperSizeType.A2Paper;

            // 将指定范围的单元格保存位PDF
            spireSheet.SaveToPdf(pdfath);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("报表已生成");
            sb.AppendLine($"路径：{Path.GetDirectoryName(path)}");
            sb.AppendLine($"Excel版：{fileName}.xlsx");
            sb.AppendLine($"PDF版：{fileName}.pdf");

            MessageBox.Show(sb.ToString(), "提示");
        }

        private static void SetFifthScoreFor32(List<PersonInfoEntity> infos, ISheet CurrentSheet)
        {
            var rowNum = 16;
            var fifthPersons = infos.Where(x => x.FifthRoundscore >= 0).OrderByDescending(x => x.MatchDisplayNum).ToList();
            for (int i = 0; i < fifthPersons.Count(); i++)
            {
                CurrentSheet.GetRow(rowNum).GetCell(7).SetCellValue(fifthPersons[i].FifthRoundscore);
                var scoreOne = Convert.ToDouble(fifthPersons[i].FifthRoundscore);
                var nameOne = fifthPersons[i].Name;
                i++;
                try
                {
                    CurrentSheet.GetRow(rowNum + 30).GetCell(7).SetCellValue(fifthPersons[i].FifthRoundscore);
                    var scoreTwo = Convert.ToDouble(fifthPersons[i].FifthRoundscore);
                    var nameTwo = fifthPersons[i].Name;

                    if (scoreOne <= scoreTwo)
                    {
                        CurrentSheet.GetRow(29).GetCell(7).SetCellValue(nameOne);
                        CurrentSheet.GetRow(27).GetCell(5).SetCellValue(nameTwo);
                    }
                    else
                    {
                        CurrentSheet.GetRow(29).GetCell(7).SetCellValue(nameTwo);
                        CurrentSheet.GetRow(27).GetCell(5).SetCellValue(nameOne);
                    }
                }
                catch (Exception ex)
                { 
                
                }
            }
        }

        private static void SetFourthScoreFor32(List<PersonInfoEntity> infos, ISheet CurrentSheet)
        {
            var rowNum = 8;
            var fourthPersons = infos.Where(x => x.FourthRoundscore >= 0).OrderByDescending(x => x.MatchDisplayNum).ToList();
            for (int i = 0; i < fourthPersons.Count(); i++)
            {
                CurrentSheet.GetRow(rowNum).GetCell(9).SetCellValue(fourthPersons[i].FourthRoundscore);
                var scoreOne = Convert.ToDouble(fourthPersons[i].FourthRoundscore);
                var nameOne = fourthPersons[i].Name;
                i++;
                try
                {
                    CurrentSheet.GetRow(rowNum + 14).GetCell(9).SetCellValue(fourthPersons[i].FourthRoundscore);
                    var scoreTwo = Convert.ToDouble(fourthPersons[i].FourthRoundscore);
                    var nameTwo = fourthPersons[i].Name;

                    if (scoreOne < scoreTwo)
                    {
                        CurrentSheet.GetRow(rowNum + 6).GetCell(9).SetCellValue($"{nameOne}");
                    }
                    else
                    {
                        CurrentSheet.GetRow(rowNum + 6).GetCell(9).SetCellValue($"{nameTwo}");
                    }
                }
                catch(Exception ex)
                {

                }


                rowNum += 32;
            }
        }

        private static void SetThirdScoreFor32(List<PersonInfoEntity> infos, ISheet CurrentSheet, XSSFWorkbook book)
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

        private static void SetSecondScoreFor32(List<PersonInfoEntity> infos, ISheet CurrentSheet, XSSFWorkbook book)
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


                i++;
                rowNum += 8;
            }
        }

        private static void SetNameAndFirstScoreFor32(List<PersonInfoEntity> infos, ISheet CurrentSheet)
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
                    // 如果分数为0， 不填入分数
                    if (person.FirstRoundscore != -1)
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
                    if (person.FirstRoundscore != -1)
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

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
        // 导出比赛人员名单及分数信息
        public static void Export8PersonMatchInfo(List<PersonInfoEntity> infos, string fileName)
        {
            IWorkbook workbook = null;
            ISheet CurrentSheet;
            // 读取模板信息
            var tempPath = Path.Combine(Directory.GetCurrentDirectory(), @"Template\8Template.xlsx");
            var extension = Path.GetExtension(tempPath);
            FileStream fileStream = new FileStream(tempPath, FileMode.Open, FileAccess.Read);
            if (extension.ToUpper() == ".XLS") // 2003版本
            {
                workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook
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
            CurrentSheet.GetRow(1).GetCell(17).SetCellValue(fileName.Split('_')[1]);
            CurrentSheet.GetRow(12).GetCell(17).SetCellValue(DateTime.ParseExact(fileName.Split('_')[1], "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss"));

            // 设置姓名和序号和第一轮分数
            SetNameAndFirstScoreFor8(infos, CurrentSheet);

            // 设置第二轮分数
            SetSecondScoreFor8(infos, CurrentSheet);

            //设置第三轮分数
            SetThirdScoreFor8(infos, CurrentSheet);

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
            spireSheet.PageSetup.PrintArea = "A1:R45";
            spireSheet.PageSetup.PaperSize = PaperSizeType.A2Paper;

            // 将指定范围的单元格保存位PDF
            spireSheet.SaveToPdf(pdfath);
        }

        private static void SetThirdScoreFor8(List<PersonInfoEntity> infos, ISheet CurrentSheet)
        {
            // 设置第三轮分数
            var rowNum = 18;
            var thirdPersons = infos.Where(x => x.ThirdRoundscore > 0).OrderByDescending(x => x.MatchDisplayNum).ToList();
            for (int i = 0; i < thirdPersons.Count(); i++)
            {
                CurrentSheet.GetRow(rowNum).GetCell(6).SetCellValue(thirdPersons[i].ThirdRoundscore);
                var scoreOne =Convert.ToDouble(thirdPersons[i].ThirdRoundscore);
                var nameOne = thirdPersons[i].Name;
                i++;
                try
                {
                    CurrentSheet.GetRow(rowNum + 7).GetCell(6).SetCellValue(thirdPersons[i].ThirdRoundscore);
                    var scoreTwo = Convert.ToDouble(thirdPersons[i].ThirdRoundscore);
                    var nameTwo = thirdPersons[i].Name;

                    if (scoreOne <= scoreTwo)
                    {
                        CurrentSheet.GetRow(rowNum + 1).GetCell(6).SetCellValue($"{nameOne}");
                        CurrentSheet.GetRow(19).GetCell(4).SetCellValue($"{nameTwo}");
                    }
                    else
                    {
                        CurrentSheet.GetRow(rowNum + 1).GetCell(6).SetCellValue($"{nameOne}");
                        CurrentSheet.GetRow(19).GetCell(4).SetCellValue($"{nameTwo}");

                    }
                }
                catch (Exception ex)
                { 
                
                }

            }
        }

        private static void SetSecondScoreFor8(List<PersonInfoEntity> infos, ISheet CurrentSheet)
        {
            // 设置第二轮分数
            var rowNum = 16;
            var secondePersons = infos.Where(x => x.SecondRoundscore > 0).OrderByDescending(x => x.MatchDisplayNum).ToList();
            for (int i = 0; i < secondePersons.Count(); i++)
            {
                CurrentSheet.GetRow(rowNum).GetCell(8).SetCellValue(secondePersons[i].SecondRoundscore);
                var scoreOne = Convert.ToDouble(secondePersons[i].SecondRoundscore);
                var nameOne = secondePersons[i].Name;
                i++;
                try
                {
                    CurrentSheet.GetRow(rowNum + 3).GetCell(8).SetCellValue(secondePersons[i].SecondRoundscore);
                    var scoreTwo = Convert.ToDouble(secondePersons[i].SecondRoundscore);
                    var nameTwo = secondePersons[i].Name;

                    if (scoreOne < scoreTwo)
                    {
                        CurrentSheet.GetRow(rowNum + 1).GetCell(8).SetCellValue($"3rd {nameOne}");
                    }
                    else
                    {
                        CurrentSheet.GetRow(rowNum + 1).GetCell(8).SetCellValue($"3rd {nameTwo}");
                    }
                }
                catch (Exception ex)
                { 
                }

                rowNum += 8;
            }
        }

        private static void SetNameAndFirstScoreFor8(List<PersonInfoEntity> infos, ISheet CurrentSheet)
        {
            // 设置姓名和序号和第一轮分数
            var RowNum = 14;
            foreach (var person in infos.OrderByDescending(x => x.MatchDisplayNum))
            {
                if (person.Name == "轮空")
                {
                    CurrentSheet.GetRow(RowNum).GetCell(13).SetCellValue($"{person.Name}");
                    CurrentSheet.GetRow(RowNum).GetCell(12).SetCellValue("");
                }
                else
                {
                    CurrentSheet.GetRow(RowNum).GetCell(13).SetCellValue($"{person.Name}({person.Organization})");
                    CurrentSheet.GetRow(RowNum).GetCell(12).SetCellValue(person.Num);
                }

                if (person.MatchDisplayNum % 2 == 0)
                {
                    // 如果分数为-1， 不填入分数
                    if (person.FirstRoundscore != -1)
                    {
                        CurrentSheet.GetRow(RowNum + 1).GetCell(10).SetCellValue(person.FirstRoundscore);
                    }
                    else
                    {
                        CurrentSheet.GetRow(RowNum + 1).GetCell(10).SetCellValue("");
                    }

                }
                else
                {
                    if (person.FirstRoundscore != -1)
                    {
                        CurrentSheet.GetRow(RowNum).GetCell(10).SetCellValue(person.FirstRoundscore);
                    }
                    else
                    {
                        CurrentSheet.GetRow(RowNum).GetCell(10).SetCellValue("");
                    }
                }

                RowNum += 2;
            }
        }

    }
}

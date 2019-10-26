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
    public enum ExcelHeader { 序号, 姓名, 性别, 单位,分组,种子号,第一轮,第二轮,第三轮,第四轮,第五轮,第六轮,原始序号}
    public static class NPOIHelper
    {      
        // 导入数据时，获取人员名单
        public static List<PersonInfo> GetPersonInfo(string path)
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

            List<PersonInfo> result = new List<PersonInfo>();
            IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据
            for (int i = 1; i <= CurrentSheet.LastRowNum; i++)  //对工作表每一行
            {
                row = CurrentSheet.GetRow(i);   //row读入第i行数据
                if (row != null)
                {
                    PersonInfo info = new PersonInfo();
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
        public static void Export64PersonMatchInfo(List<PersonInfo> infos, string fileName)
        {
            IWorkbook workbook = null;
            ISheet CurrentSheet;
            // 读取模板信息
            var tempPath = Path.Combine(Directory.GetCurrentDirectory(), @"Template\64Template.xlsx");
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

            // 设置姓名和序号
            var RowNum = 0;
            foreach (var person in infos.OrderByDescending(x => x.MatchDisplayNum))
            {
                CurrentSheet.GetRow(RowNum).GetCell(18).SetCellValue(person.Name);
                if (person.Name == "轮空")
                {
                    CurrentSheet.GetRow(RowNum).GetCell(17).SetCellValue("");
                }
                else
                {
                    CurrentSheet.GetRow(RowNum).GetCell(17).SetCellValue(person.Num);
                }

                RowNum += 2;
            }



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
            spireSheet.PageSetup.PrintArea = "A1:AE128";
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
    }
}
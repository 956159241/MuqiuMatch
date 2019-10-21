using DSSportCompetitionSys.Entity;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DSSportCompetitionSys
{
    public class NPOIHelper
    {
        IWorkbook workbook = null;
        private static ISheet CurrentSheet;

        public NPOIHelper(string path)
        {
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
        }

        public List<PersonInfo> GetPersonInfo()
        {
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

    }
}

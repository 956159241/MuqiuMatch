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
    public partial class GroupsManage : Form
    {
        private static string  GroupsPath = Path.Combine(Directory.GetCurrentDirectory(), $@"Temp\groups.txt");

        public GroupsManage()
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(string));
            dt.Columns.Add("组名", typeof(string));

            GetGroups(dt);

            dataGridViewX1.DataSource = dt;
        }

        private void GetGroups(DataTable dt)
        {
            try
            {
                using (StreamReader stream = new StreamReader(GroupsPath))
                {
                    var line = stream.ReadLine();
                    while (line != null)
                    {
                        var fields = line.Split(';');
                        var row = dt.NewRow();
                        row["序号"] = fields[0];
                        row["组名"] = fields[1];

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dt = dataGridViewX1.DataSource as DataTable;

            if (!string.IsNullOrWhiteSpace(tbGroup.Text))
            {
                var row = dt.NewRow();
                row["序号"] = (dt.Rows.Count + 1).ToString();
                row["组名"] = tbGroup.Text;

                dt.Rows.Add(row);
            }
        }

        private void GroupsManage_FormClosing(object sender, FormClosingEventArgs e)
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

                info.AppendLine();
            }
            // 如果目录不存在，创建目录
            if (!Directory.Exists(Path.GetDirectoryName(GroupsPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(GroupsPath));
            }
            using (StreamWriter stream = new StreamWriter(path: GroupsPath, append: false))
            {
                stream.Write(info.ToString());
            }
        }

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                // 获取当前行
                var selectRows = dataGridViewX1.SelectedRows[0];
                var dt = dataGridViewX1.DataSource as DataTable;
                if (MessageBox.Show("删除选中行？", "友情提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    dt.Rows.Remove(dt.Rows[selectRows.Index]);

                    // 重新排序
                    for (int i = 0; i < dataGridViewX1.Rows.Count; i++)
                    {
                        dataGridViewX1.Rows[i].Cells[0].Value = (i + 1).ToString();
                    
                    }
                }
            }
            catch (Exception ex)
            { 
            
            }
        
        }
    }
}

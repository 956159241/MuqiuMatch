using System.Drawing;

namespace DSSportCompetitionSys
{
    partial class MatchPersonInfoForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatchPersonInfoForm));
            this.buttonReset = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.buttonGenerate = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.buttonReset.SuspendLayout();
            this.buttonGenerate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonReset
            // 
            this.buttonReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonReset.BackColor = System.Drawing.Color.Transparent;
            this.buttonReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonReset.Controls.Add(this.labelX2);
            this.buttonReset.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonReset.Location = new System.Drawing.Point(724, 12);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(203, 64);
            this.buttonReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonReset.TabIndex = 3;
            this.buttonReset.Text = "重置对阵表";
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.EnableMarkup = false;
            this.labelX2.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.Location = new System.Drawing.Point(3, 38);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(196, 23);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "Reset Match Table";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonGenerate.BackColor = System.Drawing.Color.Transparent;
            this.buttonGenerate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonGenerate.Controls.Add(this.labelX1);
            this.buttonGenerate.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonGenerate.Location = new System.Drawing.Point(724, 82);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(203, 64);
            this.buttonGenerate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonGenerate.TabIndex = 4;
            this.buttonGenerate.Text = "生成对阵表";
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.EnableMarkup = false;
            this.labelX1.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(3, 38);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(196, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Generate Match Table";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(13, 12);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowHeadersVisible = false;
            this.dataGridViewX1.RowHeadersWidth = 51;
            this.dataGridViewX1.RowTemplate.Height = 27;
            this.dataGridViewX1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewX1.Size = new System.Drawing.Size(705, 494);
            this.dataGridViewX1.TabIndex = 1;
            this.dataGridViewX1.DataSourceChanged += new System.EventHandler(this.dataGridViewX1_DataSourceChanged);
            this.dataGridViewX1.DefaultCellStyleChanged += new System.EventHandler(this.dataGridViewX1_DefaultCellStyleChanged);
            this.dataGridViewX1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellValueChanged);
            this.dataGridViewX1.ColumnSortModeChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridViewX1_ColumnSortModeChanged);
            this.dataGridViewX1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_RowHeaderMouseClick);
            this.dataGridViewX1.Sorted += new System.EventHandler(this.dataGridViewX1_Sorted);
            this.dataGridViewX1.ContextMenuStripChanged += new System.EventHandler(this.dataGridViewX1_ContextMenuStripChanged);
            this.dataGridViewX1.MouseHover += new System.EventHandler(this.dataGridViewX1_MouseHover);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("隶书", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(724, 167);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(203, 23);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "使用说明";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(746, 221);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(177, 79);
            this.labelX6.TabIndex = 13;
            this.labelX6.Text = "2.手动输入每轮分数后，点击其它单元格，确认输入完毕后即可关闭窗口。";
            this.labelX6.WordWrap = true;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(746, 295);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(177, 66);
            this.labelX4.TabIndex = 14;
            this.labelX4.Text = "3.若对阵表已生成，想重新生成对阵表，请先重置原始对阵表。";
            this.labelX4.WordWrap = true;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(746, 196);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(177, 25);
            this.labelX5.TabIndex = 15;
            this.labelX5.Text = "1.红色背景为种子选手";
            this.labelX5.WordWrap = true;
            // 
            // MatchPersonInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(935, 534);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.dataGridViewX1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MatchPersonInfoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "木球竞赛系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MatchPersonInfoForm_FormClosing);
            this.Load += new System.EventHandler(this.MatchPersonInfoForm_Load);
            this.buttonReset.ResumeLayout(false);
            this.buttonGenerate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX buttonReset;
        private DevComponents.DotNetBar.ButtonX buttonGenerate;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
    }
}


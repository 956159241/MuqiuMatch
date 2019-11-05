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
            this.lbCoverReset = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.buttonGenerate = new DevComponents.DotNetBar.ButtonX();
            this.lbCoverGenerate = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            // 
            // buttonReset
            // 
            this.buttonReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonReset.BackColor = System.Drawing.Color.Transparent;
            this.buttonReset.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.buttonReset.Controls.Add(this.lbCoverReset);
            this.buttonReset.Controls.Add(this.labelX2);
            this.buttonReset.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonReset.Location = new System.Drawing.Point(724, 12);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.buttonReset.Size = new System.Drawing.Size(203, 64);
            this.buttonReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonReset.TabIndex = 3;
            this.buttonReset.Text = "重置对阵表";
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // lbCoverReset
            // 
            // 
            // 
            // 
            this.lbCoverReset.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbCoverReset.Location = new System.Drawing.Point(3, 1);
            this.lbCoverReset.Name = "lbCoverReset";
            this.lbCoverReset.Size = new System.Drawing.Size(200, 60);
            this.lbCoverReset.TabIndex = 1;
            this.lbCoverReset.Text = "点击后生效";
            this.lbCoverReset.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lbCoverReset.TextLineAlignment = System.Drawing.StringAlignment.Far;
            this.lbCoverReset.Click += new System.EventHandler(this.lbCoverReset_Click);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Enabled = false;
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
            this.buttonGenerate.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
            this.buttonGenerate.Controls.Add(this.lbCoverGenerate);
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
            // lbCoverGenerate
            // 
            // 
            // 
            // 
            this.lbCoverGenerate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbCoverGenerate.Location = new System.Drawing.Point(4, 1);
            this.lbCoverGenerate.Name = "lbCoverGenerate";
            this.lbCoverGenerate.Size = new System.Drawing.Size(199, 63);
            this.lbCoverGenerate.TabIndex = 1;
            this.lbCoverGenerate.Text = "点击后生效";
            this.lbCoverGenerate.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lbCoverGenerate.TextLineAlignment = System.Drawing.StringAlignment.Far;
            this.lbCoverGenerate.Click += new System.EventHandler(this.lbCoverGenerate_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Enabled = false;
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
            this.dataGridViewX1.MultiSelect = false;
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.ReadOnly = true;
            this.dataGridViewX1.RowHeadersVisible = false;
            this.dataGridViewX1.RowHeadersWidth = 51;
            this.dataGridViewX1.RowTemplate.Height = 27;
            this.dataGridViewX1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX1.Size = new System.Drawing.Size(705, 494);
            this.dataGridViewX1.TabIndex = 1;
            this.dataGridViewX1.DataSourceChanged += new System.EventHandler(this.dataGridViewX1_DataSourceChanged);
            this.dataGridViewX1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewX1_CellMouseDoubleClick);
            this.dataGridViewX1.Sorted += new System.EventHandler(this.dataGridViewX1_Sorted);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("隶书", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(724, 338);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(203, 23);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "使用说明";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(746, 455);
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
            this.labelX5.Location = new System.Drawing.Point(746, 367);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(177, 25);
            this.labelX5.TabIndex = 15;
            this.labelX5.Text = "1.红色背景为种子选手";
            this.labelX5.WordWrap = true;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Controls.Add(this.labelX7);
            this.buttonX1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX1.Location = new System.Drawing.Point(724, 152);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(203, 64);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 16;
            this.buttonX1.Text = "录入分数";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Enabled = false;
            this.labelX7.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX7.Location = new System.Drawing.Point(3, 40);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(200, 23);
            this.labelX7.TabIndex = 0;
            this.labelX7.Text = "Set The Score";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(746, 386);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(177, 74);
            this.labelX8.TabIndex = 17;
            this.labelX8.Text = "2. 录入分数时，选中参赛选手，点击录入分数。 也可双击单元格，直接修改分数。";
            this.labelX8.WordWrap = true;
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.BackColor = System.Drawing.Color.Transparent;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Controls.Add(this.labelX6);
            this.buttonX2.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX2.Location = new System.Drawing.Point(724, 222);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(203, 64);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 18;
            this.buttonX2.Text = "生成比赛报表";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Enabled = false;
            this.labelX6.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.Location = new System.Drawing.Point(3, 38);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(200, 23);
            this.labelX6.TabIndex = 0;
            this.labelX6.Text = "Generate Competition Report";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Font = new System.Drawing.Font("隶书", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX9.ForeColor = System.Drawing.Color.Gray;
            this.labelX9.Location = new System.Drawing.Point(724, 312);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(203, 23);
            this.labelX9.TabIndex = 19;
            this.labelX9.Text = "打开历史报表";
            this.labelX9.Click += new System.EventHandler(this.labelX9_Click);
            this.labelX9.MouseLeave += new System.EventHandler(this.labelX9_MouseLeave);
            this.labelX9.MouseHover += new System.EventHandler(this.labelX9_MouseHover);
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(727, 289);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(200, 23);
            this.labelX10.TabIndex = 20;
            // 
            // MatchPersonInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(935, 534);
            this.Controls.Add(this.labelX10);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.dataGridViewX1);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.buttonReset);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MatchPersonInfoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "木球竞赛系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MatchPersonInfoForm_FormClosing);
            this.Load += new System.EventHandler(this.MatchPersonInfoForm_Load);
            this.SizeChanged += new System.EventHandler(this.MatchPersonInfoForm_SizeChanged);
            this.buttonReset.ResumeLayout(false);
            this.buttonGenerate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.buttonX1.ResumeLayout(false);
            this.buttonX2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX buttonReset;
        private DevComponents.DotNetBar.ButtonX buttonGenerate;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX lbCoverReset;
        private DevComponents.DotNetBar.LabelX lbCoverGenerate;
    }
}


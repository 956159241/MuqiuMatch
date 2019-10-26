using System.Drawing;

namespace DSSportCompetitionSys
{
    partial class ImportPersonInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportPersonInfoForm));
            this.buttonImportPersonInfo = new DevComponents.DotNetBar.ButtonX();
            this.buttonResetPersonInfo = new DevComponents.DotNetBar.ButtonX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.comboSex = new System.Windows.Forms.ComboBox();
            this.comboGroup = new System.Windows.Forms.ComboBox();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonImportPersonInfo
            // 
            this.buttonImportPersonInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonImportPersonInfo.BackColor = System.Drawing.Color.Transparent;
            this.buttonImportPersonInfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonImportPersonInfo.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonImportPersonInfo.Location = new System.Drawing.Point(658, 127);
            this.buttonImportPersonInfo.Name = "buttonImportPersonInfo";
            this.buttonImportPersonInfo.Size = new System.Drawing.Size(241, 61);
            this.buttonImportPersonInfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonImportPersonInfo.TabIndex = 3;
            this.buttonImportPersonInfo.Text = "导入参赛名单";
            this.buttonImportPersonInfo.Click += new System.EventHandler(this.buttonImportPersonInfo_Click);
            // 
            // buttonResetPersonInfo
            // 
            this.buttonResetPersonInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonResetPersonInfo.BackColor = System.Drawing.Color.Transparent;
            this.buttonResetPersonInfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonResetPersonInfo.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonResetPersonInfo.Location = new System.Drawing.Point(658, 197);
            this.buttonResetPersonInfo.Name = "buttonResetPersonInfo";
            this.buttonResetPersonInfo.Size = new System.Drawing.Size(241, 61);
            this.buttonResetPersonInfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonResetPersonInfo.TabIndex = 4;
            this.buttonResetPersonInfo.Text = "重置参赛名单";
            this.buttonResetPersonInfo.Click += new System.EventHandler(this.buttonResetPersonInfo_Click);
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
            this.dataGridViewX1.Size = new System.Drawing.Size(600, 494);
            this.dataGridViewX1.TabIndex = 1;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(658, 51);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(122, 23);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "设置导入条件";
            // 
            // comboSex
            // 
            this.comboSex.FormattingEnabled = true;
            this.comboSex.Location = new System.Drawing.Point(658, 80);
            this.comboSex.Name = "comboSex";
            this.comboSex.Size = new System.Drawing.Size(121, 23);
            this.comboSex.TabIndex = 6;
            // 
            // comboGroup
            // 
            this.comboGroup.FormattingEnabled = true;
            this.comboGroup.Location = new System.Drawing.Point(785, 80);
            this.comboGroup.Name = "comboGroup";
            this.comboGroup.Size = new System.Drawing.Size(121, 23);
            this.comboGroup.TabIndex = 7;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(658, 298);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(89, 23);
            this.labelX2.TabIndex = 8;
            this.labelX2.Text = "可导入样式";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(690, 327);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(209, 23);
            this.labelX3.TabIndex = 9;
            this.labelX3.Text = "1.姓名、性别、单位、组别";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(690, 356);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(209, 58);
            this.labelX4.TabIndex = 10;
            this.labelX4.Text = "2.按序输入种子号，输入完毕后点击空白处确认输入完成后关闭窗口";
            this.labelX4.WordWrap = true;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(658, 420);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(105, 23);
            this.labelX5.TabIndex = 11;
            this.labelX5.Text = "参考种子数：";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(690, 449);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(177, 23);
            this.labelX6.TabIndex = 12;
            this.labelX6.Text = "8位：设置2个种子";
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(690, 478);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(177, 23);
            this.labelX7.TabIndex = 13;
            this.labelX7.Text = "32位：设置8个种子";
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(690, 507);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(177, 23);
            this.labelX8.TabIndex = 14;
            this.labelX8.Text = "64位：设置16个种子";
            // 
            // ImportPersonInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(935, 534);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.comboGroup);
            this.Controls.Add(this.comboSex);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.buttonResetPersonInfo);
            this.Controls.Add(this.buttonImportPersonInfo);
            this.Controls.Add(this.dataGridViewX1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ImportPersonInfoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "木球竞赛系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX buttonImportPersonInfo;
        private DevComponents.DotNetBar.ButtonX buttonResetPersonInfo;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.ComboBox comboSex;
        private System.Windows.Forms.ComboBox comboGroup;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX8;
    }
}


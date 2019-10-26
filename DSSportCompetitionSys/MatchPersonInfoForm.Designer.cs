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
            this.buttonGenerate = new DevComponents.DotNetBar.ButtonX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonReset
            // 
            this.buttonReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonReset.BackColor = System.Drawing.Color.Transparent;
            this.buttonReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonReset.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonReset.Location = new System.Drawing.Point(733, 32);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(166, 40);
            this.buttonReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonReset.TabIndex = 3;
            this.buttonReset.Text = "重置对阵表";
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonGenerate.BackColor = System.Drawing.Color.Transparent;
            this.buttonGenerate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonGenerate.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonGenerate.Location = new System.Drawing.Point(733, 102);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(166, 40);
            this.buttonGenerate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonGenerate.TabIndex = 4;
            this.buttonGenerate.Text = "生成对阵表";
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
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
            // MatchPersonInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(935, 534);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX buttonReset;
        private DevComponents.DotNetBar.ButtonX buttonGenerate;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
    }
}


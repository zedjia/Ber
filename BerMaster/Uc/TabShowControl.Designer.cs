namespace BerMaster.Uc
{
    partial class TabShowControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbllastUpdateTitle = new System.Windows.Forms.Label();
            this.lbllastUpdate = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.moneysection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buymoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buyscal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sellmoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sellscal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diffmoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalmoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAliseTitle = new System.Windows.Forms.Label();
            this.lblAlise = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbllastUpdateTitle
            // 
            this.lbllastUpdateTitle.AutoSize = true;
            this.lbllastUpdateTitle.Location = new System.Drawing.Point(231, 19);
            this.lbllastUpdateTitle.Name = "lbllastUpdateTitle";
            this.lbllastUpdateTitle.Size = new System.Drawing.Size(83, 12);
            this.lbllastUpdateTitle.TabIndex = 0;
            this.lbllastUpdateTitle.Text = "最新数据时间:";
            // 
            // lbllastUpdate
            // 
            this.lbllastUpdate.AutoSize = true;
            this.lbllastUpdate.Location = new System.Drawing.Point(320, 19);
            this.lbllastUpdate.Name = "lbllastUpdate";
            this.lbllastUpdate.Size = new System.Drawing.Size(23, 12);
            this.lbllastUpdate.TabIndex = 0;
            this.lbllastUpdate.Text = "xxx";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(644, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(359, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "结束时间";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(82, 55);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(418, 55);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.moneysection,
            this.buymoney,
            this.buyscal,
            this.sellmoney,
            this.sellscal,
            this.diffmoney,
            this.totalmoney});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 123);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(904, 294);
            this.dataGridView1.TabIndex = 5;
            // 
            // moneysection
            // 
            this.moneysection.DataPropertyName = "moneysection";
            this.moneysection.HeaderText = "资金范围";
            this.moneysection.Name = "moneysection";
            this.moneysection.Width = 150;
            // 
            // buymoney
            // 
            this.buymoney.DataPropertyName = "buymoney";
            this.buymoney.HeaderText = "买入金额(万)";
            this.buymoney.Name = "buymoney";
            // 
            // buyscal
            // 
            this.buyscal.DataPropertyName = "buyscal";
            this.buyscal.HeaderText = "买入占比";
            this.buyscal.Name = "buyscal";
            // 
            // sellmoney
            // 
            this.sellmoney.DataPropertyName = "sellmoney";
            this.sellmoney.HeaderText = "卖出金额(万)";
            this.sellmoney.Name = "sellmoney";
            // 
            // sellscal
            // 
            this.sellscal.DataPropertyName = "sellscal";
            this.sellscal.HeaderText = "卖出占比";
            this.sellscal.Name = "sellscal";
            // 
            // diffmoney
            // 
            this.diffmoney.DataPropertyName = "diffmoney";
            this.diffmoney.HeaderText = "差值(万)";
            this.diffmoney.Name = "diffmoney";
            // 
            // totalmoney
            // 
            this.totalmoney.DataPropertyName = "totalmoney";
            this.totalmoney.HeaderText = "总金额(万)";
            this.totalmoney.Name = "totalmoney";
            // 
            // lblAliseTitle
            // 
            this.lblAliseTitle.AutoSize = true;
            this.lblAliseTitle.Location = new System.Drawing.Point(54, 19);
            this.lblAliseTitle.Name = "lblAliseTitle";
            this.lblAliseTitle.Size = new System.Drawing.Size(35, 12);
            this.lblAliseTitle.TabIndex = 0;
            this.lblAliseTitle.Text = "币名:";
            // 
            // lblAlise
            // 
            this.lblAlise.AutoSize = true;
            this.lblAlise.Location = new System.Drawing.Point(95, 19);
            this.lblAlise.Name = "lblAlise";
            this.lblAlise.Size = new System.Drawing.Size(23, 12);
            this.lblAlise.TabIndex = 0;
            this.lblAlise.Text = "xxx";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(298, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 24);
            this.button2.TabIndex = 6;
            this.button2.Text = "<<>>";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TabShowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblAlise);
            this.Controls.Add(this.lbllastUpdate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAliseTitle);
            this.Controls.Add(this.lbllastUpdateTitle);
            this.Name = "TabShowControl";
            this.Size = new System.Drawing.Size(904, 417);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbllastUpdateTitle;
        private System.Windows.Forms.Label lbllastUpdate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblAliseTitle;
        private System.Windows.Forms.Label lblAlise;
        private System.Windows.Forms.DataGridViewTextBoxColumn moneysection;
        private System.Windows.Forms.DataGridViewTextBoxColumn buymoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn buyscal;
        private System.Windows.Forms.DataGridViewTextBoxColumn sellmoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn sellscal;
        private System.Windows.Forms.DataGridViewTextBoxColumn diffmoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalmoney;
        private System.Windows.Forms.Button button2;
    }
}

namespace TransCoinMaster
{
    partial class TransCoinMaster
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabbrowser = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblbrowserversion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelbrowser = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtLogContainer = new System.Windows.Forms.RichTextBox();
            this.txtUrlBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGoUrl = new System.Windows.Forms.Button();
            this.btnDevTools = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabbrowser.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabbrowser);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 172);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1364, 813);
            this.tabControl1.TabIndex = 0;
            // 
            // tabbrowser
            // 
            this.tabbrowser.Controls.Add(this.groupBox1);
            this.tabbrowser.Controls.Add(this.panelbrowser);
            this.tabbrowser.Location = new System.Drawing.Point(4, 22);
            this.tabbrowser.Name = "tabbrowser";
            this.tabbrowser.Padding = new System.Windows.Forms.Padding(3);
            this.tabbrowser.Size = new System.Drawing.Size(1356, 787);
            this.tabbrowser.TabIndex = 0;
            this.tabbrowser.Text = "页面浏览";
            this.tabbrowser.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnDevTools);
            this.groupBox1.Controls.Add(this.btnGoUrl);
            this.groupBox1.Controls.Add(this.txtUrlBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblbrowserversion);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1350, 195);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作控制";
            // 
            // lblbrowserversion
            // 
            this.lblbrowserversion.AutoSize = true;
            this.lblbrowserversion.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblbrowserversion.Location = new System.Drawing.Point(83, 167);
            this.lblbrowserversion.Name = "lblbrowserversion";
            this.lblbrowserversion.Size = new System.Drawing.Size(59, 12);
            this.lblbrowserversion.TabIndex = 0;
            this.lblbrowserversion.Text = "检测中...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(6, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "浏览器版本:";
            // 
            // panelbrowser
            // 
            this.panelbrowser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelbrowser.Location = new System.Drawing.Point(3, 198);
            this.panelbrowser.Name = "panelbrowser";
            this.panelbrowser.Size = new System.Drawing.Size(1350, 586);
            this.panelbrowser.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtLogContainer);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1253, 787);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "操作日志";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtLogContainer
            // 
            this.txtLogContainer.DetectUrls = false;
            this.txtLogContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogContainer.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtLogContainer.Location = new System.Drawing.Point(3, 3);
            this.txtLogContainer.Name = "txtLogContainer";
            this.txtLogContainer.ReadOnly = true;
            this.txtLogContainer.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtLogContainer.ShortcutsEnabled = false;
            this.txtLogContainer.Size = new System.Drawing.Size(1247, 781);
            this.txtLogContainer.TabIndex = 0;
            this.txtLogContainer.Text = "";
            // 
            // txtUrlBox
            // 
            this.txtUrlBox.Location = new System.Drawing.Point(47, 21);
            this.txtUrlBox.Name = "txtUrlBox";
            this.txtUrlBox.Size = new System.Drawing.Size(1022, 21);
            this.txtUrlBox.TabIndex = 1;
            this.txtUrlBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtUrlBox_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "网址:";
            // 
            // btnGoUrl
            // 
            this.btnGoUrl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGoUrl.Location = new System.Drawing.Point(1075, 19);
            this.btnGoUrl.Name = "btnGoUrl";
            this.btnGoUrl.Size = new System.Drawing.Size(75, 23);
            this.btnGoUrl.TabIndex = 2;
            this.btnGoUrl.Text = "跳转";
            this.btnGoUrl.UseVisualStyleBackColor = true;
            this.btnGoUrl.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDevTools
            // 
            this.btnDevTools.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDevTools.Location = new System.Drawing.Point(1156, 19);
            this.btnDevTools.Name = "btnDevTools";
            this.btnDevTools.Size = new System.Drawing.Size(75, 23);
            this.btnDevTools.TabIndex = 2;
            this.btnDevTools.Text = "DevTools";
            this.btnDevTools.UseVisualStyleBackColor = true;
            this.btnDevTools.Click += new System.EventHandler(this.btnDevTools_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1228, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1228, 149);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "test2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TransCoinMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 985);
            this.Controls.Add(this.tabControl1);
            this.Name = "TransCoinMaster";
            this.Text = "TransCoinMaster";
            this.tabControl1.ResumeLayout(false);
            this.tabbrowser.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabbrowser;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelbrowser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblbrowserversion;
        private System.Windows.Forms.RichTextBox txtLogContainer;
        private System.Windows.Forms.TextBox txtUrlBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGoUrl;
        private System.Windows.Forms.Button btnDevTools;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}


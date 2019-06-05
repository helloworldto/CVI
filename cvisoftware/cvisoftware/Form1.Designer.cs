namespace cvisoftware
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.systemInfoBtn = new System.Windows.Forms.Button();
            this.curvePanel = new System.Windows.Forms.Panel();
            this.showCurve = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // systemInfoBtn
            // 
            this.systemInfoBtn.Location = new System.Drawing.Point(80, 34);
            this.systemInfoBtn.Name = "systemInfoBtn";
            this.systemInfoBtn.Size = new System.Drawing.Size(160, 40);
            this.systemInfoBtn.TabIndex = 0;
            this.systemInfoBtn.Text = "信息";
            this.systemInfoBtn.UseVisualStyleBackColor = true;
            this.systemInfoBtn.Click += new System.EventHandler(this.systemInfoBtn_Click);
            // 
            // curvePanel
            // 
            this.curvePanel.Location = new System.Drawing.Point(13, 131);
            this.curvePanel.Name = "curvePanel";
            this.curvePanel.Size = new System.Drawing.Size(900, 418);
            this.curvePanel.TabIndex = 7;
            // 
            // showCurve
            // 
            this.showCurve.Location = new System.Drawing.Point(274, 34);
            this.showCurve.Name = "showCurve";
            this.showCurve.Size = new System.Drawing.Size(160, 40);
            this.showCurve.TabIndex = 8;
            this.showCurve.Text = "显示曲线";
            this.showCurve.UseVisualStyleBackColor = true;
            this.showCurve.Click += new System.EventHandler(this.showCurve_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(518, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "修改labcode";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(636, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 10;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 561);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.showCurve);
            this.Controls.Add(this.curvePanel);
            this.Controls.Add(this.systemInfoBtn);
            this.Name = "Form1";
            this.Text = "CVI实验";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button systemInfoBtn;
        private System.Windows.Forms.Panel curvePanel;
        private System.Windows.Forms.Button showCurve;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}


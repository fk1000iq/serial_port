namespace serial_port
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
			this.comboBoxPorts = new System.Windows.Forms.ComboBox();
			this.label_serial_name = new System.Windows.Forms.Label();
			this.richTextBox_log = new System.Windows.Forms.RichTextBox();
			this.button_serial_onoff = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// comboBoxPorts
			// 
			this.comboBoxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxPorts.DropDownWidth = 200;
			this.comboBoxPorts.FormattingEnabled = true;
			this.comboBoxPorts.Location = new System.Drawing.Point(58, 41);
			this.comboBoxPorts.Name = "comboBoxPorts";
			this.comboBoxPorts.Size = new System.Drawing.Size(121, 20);
			this.comboBoxPorts.TabIndex = 0;
			// 
			// label_serial_name
			// 
			this.label_serial_name.AutoSize = true;
			this.label_serial_name.Location = new System.Drawing.Point(13, 44);
			this.label_serial_name.Name = "label_serial_name";
			this.label_serial_name.Size = new System.Drawing.Size(29, 12);
			this.label_serial_name.TabIndex = 1;
			this.label_serial_name.Text = "串口";
			// 
			// richTextBox_log
			// 
			this.richTextBox_log.Location = new System.Drawing.Point(227, 41);
			this.richTextBox_log.Name = "richTextBox_log";
			this.richTextBox_log.Size = new System.Drawing.Size(467, 185);
			this.richTextBox_log.TabIndex = 2;
			this.richTextBox_log.Text = "";
			// 
			// button_serial_onoff
			// 
			this.button_serial_onoff.Location = new System.Drawing.Point(104, 203);
			this.button_serial_onoff.Name = "button_serial_onoff";
			this.button_serial_onoff.Size = new System.Drawing.Size(75, 23);
			this.button_serial_onoff.TabIndex = 3;
			this.button_serial_onoff.Text = "打开";
			this.button_serial_onoff.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.button_serial_onoff);
			this.Controls.Add(this.richTextBox_log);
			this.Controls.Add(this.label_serial_name);
			this.Controls.Add(this.comboBoxPorts);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.Label label_serial_name;
        private System.Windows.Forms.RichTextBox richTextBox_log;
        private System.Windows.Forms.Button button_serial_onoff;
    }
}


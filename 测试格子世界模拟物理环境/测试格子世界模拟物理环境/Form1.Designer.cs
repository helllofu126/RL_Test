namespace 测试格子世界模拟物理环境
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.skglControlGridWorld = new SkiaSharp.Views.Desktop.SKGLControl();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.textBoxEnvId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxAlgorithmType = new System.Windows.Forms.ComboBox();
            this.textBoxPolicy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxLeaningRate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxGamma = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxDevice = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxTotalTimeSteps = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBoxIsRefresh = new System.Windows.Forms.CheckBox();
            this.textBoxRequestTime = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // skglControlGridWorld
            // 
            this.skglControlGridWorld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skglControlGridWorld.BackColor = System.Drawing.Color.Black;
            this.skglControlGridWorld.Location = new System.Drawing.Point(0, 48);
            this.skglControlGridWorld.Margin = new System.Windows.Forms.Padding(8, 11, 8, 11);
            this.skglControlGridWorld.Name = "skglControlGridWorld";
            this.skglControlGridWorld.Size = new System.Drawing.Size(509, 505);
            this.skglControlGridWorld.TabIndex = 0;
            this.skglControlGridWorld.VSync = false;
            this.skglControlGridWorld.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.skglControlGridWorld_PaintSurface);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP：";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxIP.Location = new System.Drawing.Point(62, 13);
            this.textBoxIP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(138, 29);
            this.textBoxIP.TabIndex = 2;
            this.textBoxIP.Text = "127.0.0.1";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPort.Location = new System.Drawing.Point(250, 13);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(90, 29);
            this.textBoxPort.TabIndex = 4;
            this.textBoxPort.Text = "8081";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(206, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port：";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(346, 9);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(103, 35);
            this.buttonConnect.TabIndex = 5;
            this.buttonConnect.Text = "连接服务端";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(455, 16);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(44, 21);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "离线";
            // 
            // textBoxEnvId
            // 
            this.textBoxEnvId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEnvId.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEnvId.Location = new System.Drawing.Point(590, 45);
            this.textBoxEnvId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxEnvId.Name = "textBoxEnvId";
            this.textBoxEnvId.Size = new System.Drawing.Size(138, 29);
            this.textBoxEnvId.TabIndex = 8;
            this.textBoxEnvId.Text = "CustomEnv_V1";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(520, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "环境id：";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(520, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "算法：";
            // 
            // comboBoxAlgorithmType
            // 
            this.comboBoxAlgorithmType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAlgorithmType.FormattingEnabled = true;
            this.comboBoxAlgorithmType.Location = new System.Drawing.Point(590, 89);
            this.comboBoxAlgorithmType.Name = "comboBoxAlgorithmType";
            this.comboBoxAlgorithmType.Size = new System.Drawing.Size(138, 23);
            this.comboBoxAlgorithmType.TabIndex = 11;
            // 
            // textBoxPolicy
            // 
            this.textBoxPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPolicy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPolicy.Location = new System.Drawing.Point(590, 122);
            this.textBoxPolicy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxPolicy.Name = "textBoxPolicy";
            this.textBoxPolicy.Size = new System.Drawing.Size(138, 29);
            this.textBoxPolicy.TabIndex = 13;
            this.textBoxPolicy.Text = "MlpPolicy";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(520, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "策略：";
            // 
            // textBoxLeaningRate
            // 
            this.textBoxLeaningRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLeaningRate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLeaningRate.Location = new System.Drawing.Point(590, 162);
            this.textBoxLeaningRate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxLeaningRate.Name = "textBoxLeaningRate";
            this.textBoxLeaningRate.Size = new System.Drawing.Size(138, 29);
            this.textBoxLeaningRate.TabIndex = 15;
            this.textBoxLeaningRate.Text = "0.001";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(520, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 19);
            this.label6.TabIndex = 14;
            this.label6.Text = "学习率：";
            // 
            // textBoxGamma
            // 
            this.textBoxGamma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGamma.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGamma.Location = new System.Drawing.Point(590, 201);
            this.textBoxGamma.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxGamma.Name = "textBoxGamma";
            this.textBoxGamma.Size = new System.Drawing.Size(138, 29);
            this.textBoxGamma.TabIndex = 17;
            this.textBoxGamma.Text = "0.99";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(520, 204);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 19);
            this.label7.TabIndex = 16;
            this.label7.Text = "gamma：";
            // 
            // textBoxDevice
            // 
            this.textBoxDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDevice.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDevice.Location = new System.Drawing.Point(590, 241);
            this.textBoxDevice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxDevice.Name = "textBoxDevice";
            this.textBoxDevice.Size = new System.Drawing.Size(138, 29);
            this.textBoxDevice.TabIndex = 19;
            this.textBoxDevice.Text = "auto";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(520, 244);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 19);
            this.label8.TabIndex = 18;
            this.label8.Text = "设备：";
            // 
            // textBoxTotalTimeSteps
            // 
            this.textBoxTotalTimeSteps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTotalTimeSteps.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTotalTimeSteps.Location = new System.Drawing.Point(590, 284);
            this.textBoxTotalTimeSteps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxTotalTimeSteps.Name = "textBoxTotalTimeSteps";
            this.textBoxTotalTimeSteps.Size = new System.Drawing.Size(138, 29);
            this.textBoxTotalTimeSteps.TabIndex = 21;
            this.textBoxTotalTimeSteps.Text = "200000";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(520, 287);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 19);
            this.label9.TabIndex = 20;
            this.label9.Text = "训练次数：";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(524, 368);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(195, 40);
            this.button1.TabIndex = 22;
            this.button1.Text = "训练模型";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(520, 481);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(208, 40);
            this.button2.TabIndex = 23;
            this.button2.Text = "向模型请求动作";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBoxIsRefresh
            // 
            this.checkBoxIsRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIsRefresh.AutoSize = true;
            this.checkBoxIsRefresh.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxIsRefresh.Location = new System.Drawing.Point(523, 329);
            this.checkBoxIsRefresh.Name = "checkBoxIsRefresh";
            this.checkBoxIsRefresh.Size = new System.Drawing.Size(196, 23);
            this.checkBoxIsRefresh.TabIndex = 24;
            this.checkBoxIsRefresh.Text = "训练时是否同步在界面显示";
            this.checkBoxIsRefresh.UseVisualStyleBackColor = true;
            // 
            // textBoxRequestTime
            // 
            this.textBoxRequestTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRequestTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRequestTime.Location = new System.Drawing.Point(638, 445);
            this.textBoxRequestTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxRequestTime.Name = "textBoxRequestTime";
            this.textBoxRequestTime.Size = new System.Drawing.Size(90, 29);
            this.textBoxRequestTime.TabIndex = 26;
            this.textBoxRequestTime.Text = "500";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(520, 453);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 15);
            this.label10.TabIndex = 25;
            this.label10.Text = "请求间隔（毫秒）：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 557);
            this.Controls.Add(this.textBoxRequestTime);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.checkBoxIsRefresh);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxTotalTimeSteps);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxDevice);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxGamma);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxLeaningRate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxPolicy);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxAlgorithmType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxEnvId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.skglControlGridWorld);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private SkiaSharp.Views.Desktop.SKGLControl skglControlGridWorld;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.TextBox textBoxEnvId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxAlgorithmType;
        private System.Windows.Forms.TextBox textBoxPolicy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxLeaningRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxGamma;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxDevice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxTotalTimeSteps;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBoxIsRefresh;
        private System.Windows.Forms.TextBox textBoxRequestTime;
        private System.Windows.Forms.Label label10;
    }
}


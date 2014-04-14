namespace TNSConfig
{
    partial class FrmTNSConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTNSConfig));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTestConnect = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbSID = new System.Windows.Forms.RadioButton();
            this.rdbSERVICE_NAME = new System.Windows.Forms.RadioButton();
            this.chkDedicated = new System.Windows.Forms.CheckBox();
            this.lnkTNS = new System.Windows.Forms.LinkLabel();
            this.btnUpdateConfig = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSID = new System.Windows.Forms.TextBox();
            this.txtAlisName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoadOraInfo = new System.Windows.Forms.Button();
            this.lnkClear = new System.Windows.Forms.LinkLabel();
            this.rtxtMsg = new System.Windows.Forms.RichTextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.btnLoadOraInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lnkClear);
            this.splitContainer1.Panel2.Controls.Add(this.rtxtMsg);
            this.splitContainer1.Size = new System.Drawing.Size(580, 411);
            this.splitContainer1.SplitterDistance = 264;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTestConnect);
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtPwd);
            this.groupBox2.Location = new System.Drawing.Point(12, 289);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 113);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测试连接";
            // 
            // btnTestConnect
            // 
            this.btnTestConnect.Location = new System.Drawing.Point(64, 78);
            this.btnTestConnect.Name = "btnTestConnect";
            this.btnTestConnect.Size = new System.Drawing.Size(168, 31);
            this.btnTestConnect.TabIndex = 30;
            this.btnTestConnect.Text = "测试连接";
            this.btnTestConnect.UseVisualStyleBackColor = true;
            this.btnTestConnect.Click += new System.EventHandler(this.btnTestConnect_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(64, 24);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(168, 21);
            this.txtUser.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "连接用户";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "密码";
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(64, 51);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(168, 21);
            this.txtPwd.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbSID);
            this.groupBox1.Controls.Add(this.rdbSERVICE_NAME);
            this.groupBox1.Controls.Add(this.chkDedicated);
            this.groupBox1.Controls.Add(this.lnkTNS);
            this.groupBox1.Controls.Add(this.btnUpdateConfig);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtSID);
            this.groupBox1.Controls.Add(this.txtAlisName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 232);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Net 配置";
            // 
            // rdbSID
            // 
            this.rdbSID.AutoSize = true;
            this.rdbSID.Location = new System.Drawing.Point(17, 169);
            this.rdbSID.Name = "rdbSID";
            this.rdbSID.Size = new System.Drawing.Size(41, 16);
            this.rdbSID.TabIndex = 32;
            this.rdbSID.Text = "SID";
            this.rdbSID.UseVisualStyleBackColor = true;
            // 
            // rdbSERVICE_NAME
            // 
            this.rdbSERVICE_NAME.AutoSize = true;
            this.rdbSERVICE_NAME.Checked = true;
            this.rdbSERVICE_NAME.Location = new System.Drawing.Point(17, 151);
            this.rdbSERVICE_NAME.Name = "rdbSERVICE_NAME";
            this.rdbSERVICE_NAME.Size = new System.Drawing.Size(95, 16);
            this.rdbSERVICE_NAME.TabIndex = 32;
            this.rdbSERVICE_NAME.TabStop = true;
            this.rdbSERVICE_NAME.Text = "SERVICE_NAME";
            this.rdbSERVICE_NAME.UseVisualStyleBackColor = true;
            // 
            // chkDedicated
            // 
            this.chkDedicated.AutoSize = true;
            this.chkDedicated.Location = new System.Drawing.Point(132, 151);
            this.chkDedicated.Name = "chkDedicated";
            this.chkDedicated.Size = new System.Drawing.Size(48, 16);
            this.chkDedicated.TabIndex = 31;
            this.chkDedicated.Text = "专用";
            this.chkDedicated.UseVisualStyleBackColor = true;
            // 
            // lnkTNS
            // 
            this.lnkTNS.AutoSize = true;
            this.lnkTNS.Location = new System.Drawing.Point(7, 21);
            this.lnkTNS.Name = "lnkTNS";
            this.lnkTNS.Size = new System.Drawing.Size(11, 12);
            this.lnkTNS.TabIndex = 30;
            this.lnkTNS.TabStop = true;
            this.lnkTNS.Text = ".";
            this.lnkTNS.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTNS_LinkClicked);
            // 
            // btnUpdateConfig
            // 
            this.btnUpdateConfig.Location = new System.Drawing.Point(64, 189);
            this.btnUpdateConfig.Name = "btnUpdateConfig";
            this.btnUpdateConfig.Size = new System.Drawing.Size(168, 31);
            this.btnUpdateConfig.TabIndex = 29;
            this.btnUpdateConfig.Text = "更新配置";
            this.btnUpdateConfig.UseVisualStyleBackColor = true;
            this.btnUpdateConfig.Click += new System.EventHandler(this.btnUpdateConfig_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(80, 71);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(152, 21);
            this.txtPort.TabIndex = 21;
            this.txtPort.Text = "1521";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "IP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "端口";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(80, 44);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(152, 21);
            this.txtIP.TabIndex = 20;
            this.txtIP.TextChanged += new System.EventHandler(this.txtIP_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "数据库SID";
            // 
            // txtSID
            // 
            this.txtSID.Location = new System.Drawing.Point(80, 98);
            this.txtSID.Name = "txtSID";
            this.txtSID.Size = new System.Drawing.Size(152, 21);
            this.txtSID.TabIndex = 22;
            this.txtSID.Text = "ORCL";
            // 
            // txtAlisName
            // 
            this.txtAlisName.Location = new System.Drawing.Point(80, 125);
            this.txtAlisName.Name = "txtAlisName";
            this.txtAlisName.Size = new System.Drawing.Size(152, 21);
            this.txtAlisName.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "本地服务名";
            // 
            // btnLoadOraInfo
            // 
            this.btnLoadOraInfo.Location = new System.Drawing.Point(29, 12);
            this.btnLoadOraInfo.Name = "btnLoadOraInfo";
            this.btnLoadOraInfo.Size = new System.Drawing.Size(163, 23);
            this.btnLoadOraInfo.TabIndex = 26;
            this.btnLoadOraInfo.Text = "重新获取Oracle信息";
            this.btnLoadOraInfo.UseVisualStyleBackColor = true;
            this.btnLoadOraInfo.Click += new System.EventHandler(this.btnLoadOraInfo_Click);
            // 
            // lnkClear
            // 
            this.lnkClear.AutoSize = true;
            this.lnkClear.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lnkClear.Location = new System.Drawing.Point(1, 1);
            this.lnkClear.Name = "lnkClear";
            this.lnkClear.Size = new System.Drawing.Size(77, 12);
            this.lnkClear.TabIndex = 1;
            this.lnkClear.TabStop = true;
            this.lnkClear.Text = "清除提示信息";
            this.lnkClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClear_LinkClicked);
            // 
            // rtxtMsg
            // 
            this.rtxtMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtMsg.Location = new System.Drawing.Point(0, 0);
            this.rtxtMsg.Name = "rtxtMsg";
            this.rtxtMsg.Size = new System.Drawing.Size(312, 411);
            this.rtxtMsg.TabIndex = 0;
            this.rtxtMsg.Text = "";
            // 
            // FrmTNSConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 411);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTNSConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ORACLE本地服务名配置 V1.0";
            this.Load += new System.EventHandler(this.FrmTNSConfig_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rtxtMsg;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAlisName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnLoadOraInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUpdateConfig;
        private System.Windows.Forms.Button btnTestConnect;
        private System.Windows.Forms.LinkLabel lnkTNS;
        private System.Windows.Forms.CheckBox chkDedicated;
        private System.Windows.Forms.RadioButton rdbSERVICE_NAME;
        private System.Windows.Forms.RadioButton rdbSID;
        private System.Windows.Forms.LinkLabel lnkClear;
    }
}


namespace yltl.Tools
{
    partial class FrmOraTnsname
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtxtMsg = new System.Windows.Forms.RichTextBox();
            this.btnEnvironmentPath = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAlisName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSplitTNSName = new System.Windows.Forms.Button();
            this.btnUpdateTNSName = new System.Windows.Forms.Button();
            this.btnBackupTNSName = new System.Windows.Forms.Button();
            this.btnOraVersion = new System.Windows.Forms.Button();
            this.btnConfigFormat = new System.Windows.Forms.Button();
            this.btnTNSName = new System.Windows.Forms.Button();
            this.btnOraclePath = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxtMsg
            // 
            this.rtxtMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtMsg.Location = new System.Drawing.Point(316, 12);
            this.rtxtMsg.Name = "rtxtMsg";
            this.rtxtMsg.Size = new System.Drawing.Size(500, 494);
            this.rtxtMsg.TabIndex = 0;
            this.rtxtMsg.Text = "";
            // 
            // btnEnvironmentPath
            // 
            this.btnEnvironmentPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEnvironmentPath.Location = new System.Drawing.Point(0, 0);
            this.btnEnvironmentPath.Name = "btnEnvironmentPath";
            this.btnEnvironmentPath.Size = new System.Drawing.Size(286, 44);
            this.btnEnvironmentPath.TabIndex = 1;
            this.btnEnvironmentPath.Text = "系统环境变量";
            this.btnEnvironmentPath.UseVisualStyleBackColor = true;
            this.btnEnvironmentPath.Click += new System.EventHandler(this.btnEnvironmentPath_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnOraVersion);
            this.panel1.Controls.Add(this.btnConfigFormat);
            this.panel1.Controls.Add(this.btnTNSName);
            this.panel1.Controls.Add(this.btnOraclePath);
            this.panel1.Controls.Add(this.btnEnvironmentPath);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(286, 494);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtPwd);
            this.panel2.Controls.Add(this.txtUser);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtAlisName);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtSID);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtPort);
            this.panel2.Controls.Add(this.txtIP);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnSplitTNSName);
            this.panel2.Controls.Add(this.btnUpdateTNSName);
            this.panel2.Controls.Add(this.btnBackupTNSName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 220);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(286, 250);
            this.panel2.TabIndex = 6;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(183, 219);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(82, 21);
            this.txtPwd.TabIndex = 15;
            this.txtPwd.Text = "USER_BASK_APP";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(77, 219);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 21);
            this.txtUser.TabIndex = 14;
            this.txtUser.Text = "USER_BASK_APP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "用户密码";
            // 
            // txtAlisName
            // 
            this.txtAlisName.Location = new System.Drawing.Point(77, 191);
            this.txtAlisName.Name = "txtAlisName";
            this.txtAlisName.Size = new System.Drawing.Size(100, 21);
            this.txtAlisName.TabIndex = 13;
            this.txtAlisName.Text = "TESTABCD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "配置服务名";
            // 
            // txtSID
            // 
            this.txtSID.Location = new System.Drawing.Point(77, 164);
            this.txtSID.Name = "txtSID";
            this.txtSID.Size = new System.Drawing.Size(100, 21);
            this.txtSID.TabIndex = 12;
            this.txtSID.Text = "YLTL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "数据库SID";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(183, 138);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(82, 21);
            this.txtPort.TabIndex = 11;
            this.txtPort.Text = "1521";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(77, 138);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 21);
            this.txtIP.TabIndex = 10;
            this.txtIP.Text = "192.168.0.170";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "IP及端口";
            // 
            // btnSplitTNSName
            // 
            this.btnSplitTNSName.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSplitTNSName.Location = new System.Drawing.Point(0, 88);
            this.btnSplitTNSName.Name = "btnSplitTNSName";
            this.btnSplitTNSName.Size = new System.Drawing.Size(286, 44);
            this.btnSplitTNSName.TabIndex = 8;
            this.btnSplitTNSName.Text = "分解TNSName";
            this.btnSplitTNSName.UseVisualStyleBackColor = true;
            this.btnSplitTNSName.Click += new System.EventHandler(this.btnSplitTNSName_Click);
            // 
            // btnUpdateTNSName
            // 
            this.btnUpdateTNSName.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUpdateTNSName.Location = new System.Drawing.Point(0, 44);
            this.btnUpdateTNSName.Name = "btnUpdateTNSName";
            this.btnUpdateTNSName.Size = new System.Drawing.Size(286, 44);
            this.btnUpdateTNSName.TabIndex = 7;
            this.btnUpdateTNSName.Text = "更新TNSName";
            this.btnUpdateTNSName.UseVisualStyleBackColor = true;
            this.btnUpdateTNSName.Click += new System.EventHandler(this.btnUpdateTNSName_Click);
            // 
            // btnBackupTNSName
            // 
            this.btnBackupTNSName.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBackupTNSName.Location = new System.Drawing.Point(0, 0);
            this.btnBackupTNSName.Name = "btnBackupTNSName";
            this.btnBackupTNSName.Size = new System.Drawing.Size(286, 44);
            this.btnBackupTNSName.TabIndex = 6;
            this.btnBackupTNSName.Text = "备份TNSName";
            this.btnBackupTNSName.UseVisualStyleBackColor = true;
            this.btnBackupTNSName.Click += new System.EventHandler(this.btnBackupTNSName_Click);
            // 
            // btnOraVersion
            // 
            this.btnOraVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOraVersion.Location = new System.Drawing.Point(0, 176);
            this.btnOraVersion.Name = "btnOraVersion";
            this.btnOraVersion.Size = new System.Drawing.Size(286, 44);
            this.btnOraVersion.TabIndex = 5;
            this.btnOraVersion.Text = "ORCLE版本";
            this.btnOraVersion.UseVisualStyleBackColor = true;
            this.btnOraVersion.Click += new System.EventHandler(this.btnOraVersion_Click);
            // 
            // btnConfigFormat
            // 
            this.btnConfigFormat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnConfigFormat.Location = new System.Drawing.Point(0, 132);
            this.btnConfigFormat.Name = "btnConfigFormat";
            this.btnConfigFormat.Size = new System.Drawing.Size(286, 44);
            this.btnConfigFormat.TabIndex = 4;
            this.btnConfigFormat.Text = "TNS配置内容";
            this.btnConfigFormat.UseVisualStyleBackColor = true;
            this.btnConfigFormat.Click += new System.EventHandler(this.btnConfigFormat_Click);
            // 
            // btnTNSName
            // 
            this.btnTNSName.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTNSName.Location = new System.Drawing.Point(0, 88);
            this.btnTNSName.Name = "btnTNSName";
            this.btnTNSName.Size = new System.Drawing.Size(286, 44);
            this.btnTNSName.TabIndex = 3;
            this.btnTNSName.Text = "TNSNAME文件";
            this.btnTNSName.UseVisualStyleBackColor = true;
            this.btnTNSName.Click += new System.EventHandler(this.btnTNSName_Click);
            // 
            // btnOraclePath
            // 
            this.btnOraclePath.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOraclePath.Location = new System.Drawing.Point(0, 44);
            this.btnOraclePath.Name = "btnOraclePath";
            this.btnOraclePath.Size = new System.Drawing.Size(286, 44);
            this.btnOraclePath.TabIndex = 2;
            this.btnOraclePath.Text = "ORACLE路径";
            this.btnOraclePath.UseVisualStyleBackColor = true;
            this.btnOraclePath.Click += new System.EventHandler(this.btnOraclePath_Click);
            // 
            // FrmOraTnsname
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 518);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rtxtMsg);
            this.Name = "FrmOraTnsname";
            this.Text = "Oracle-TnsName配置";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtMsg;
        private System.Windows.Forms.Button btnEnvironmentPath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOraclePath;
        private System.Windows.Forms.Button btnTNSName;
        private System.Windows.Forms.Button btnConfigFormat;
        private System.Windows.Forms.Button btnOraVersion;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnBackupTNSName;
        private System.Windows.Forms.Button btnUpdateTNSName;
        private System.Windows.Forms.Button btnSplitTNSName;
        private System.Windows.Forms.TextBox txtSID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAlisName;
        private System.Windows.Forms.Label label2;
    }
}
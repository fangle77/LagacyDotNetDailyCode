namespace yltl.Tools
{
    partial class FrmDwdyStatistical
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDwdyStatistical));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnCount = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cmbCNW = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnTestConnect = new System.Windows.Forms.Button();
            this.cmbTable = new System.Windows.Forms.ComboBox();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.cmbDept = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDept = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDBPwd = new System.Windows.Forms.TextBox();
            this.txtDBUser = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtxtSql = new System.Windows.Forms.RichTextBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.linkClear = new System.Windows.Forms.LinkLabel();
            this.rtxtMsg = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.chkAllDept = new System.Windows.Forms.CheckBox();
            this.chkSheng = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkShi = new System.Windows.Forms.CheckBox();
            this.chkEjj = new System.Windows.Forms.CheckBox();
            this.chkZgx = new System.Windows.Forms.CheckBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.chkZgx);
            this.splitContainer1.Panel1.Controls.Add(this.chkEjj);
            this.splitContainer1.Panel1.Controls.Add(this.chkShi);
            this.splitContainer1.Panel1.Controls.Add(this.chkSheng);
            this.splitContainer1.Panel1.Controls.Add(this.chkAllDept);
            this.splitContainer1.Panel1.Controls.Add(this.btnCount);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.btnSelectPath);
            this.splitContainer1.Panel1.Controls.Add(this.txtPath);
            this.splitContainer1.Panel1.Controls.Add(this.cmbCNW);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.btnExport);
            this.splitContainer1.Panel1.Controls.Add(this.btnTestConnect);
            this.splitContainer1.Panel1.Controls.Add(this.cmbTable);
            this.splitContainer1.Panel1.Controls.Add(this.dtpEnd);
            this.splitContainer1.Panel1.Controls.Add(this.dtpBegin);
            this.splitContainer1.Panel1.Controls.Add(this.cmbDept);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.lblDept);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.txtDB);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txtDBPwd);
            this.splitContainer1.Panel1.Controls.Add(this.txtDBUser);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(804, 529);
            this.splitContainer1.SplitterDistance = 143;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnCount
            // 
            this.btnCount.Location = new System.Drawing.Point(645, 73);
            this.btnCount.Name = "btnCount";
            this.btnCount.Size = new System.Drawing.Size(75, 23);
            this.btnCount.TabIndex = 37;
            this.btnCount.Text = "统计";
            this.btnCount.UseVisualStyleBackColor = true;
            this.btnCount.Click += new System.EventHandler(this.btnCount_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 36;
            this.label8.Text = "保存路径";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(357, 104);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(26, 23);
            this.btnSelectPath.TabIndex = 35;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(69, 105);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(281, 21);
            this.txtPath.TabIndex = 34;
            this.txtPath.DoubleClick += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // cmbCNW
            // 
            this.cmbCNW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCNW.FormattingEnabled = true;
            this.cmbCNW.Items.AddRange(new object[] {
            "全部",
            "城网",
            "农网"});
            this.cmbCNW.Location = new System.Drawing.Point(404, 74);
            this.cmbCNW.Name = "cmbCNW";
            this.cmbCNW.Size = new System.Drawing.Size(87, 20);
            this.cmbCNW.TabIndex = 33;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(357, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 32;
            this.label7.Text = "城农网";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(507, 73);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(132, 23);
            this.btnExport.TabIndex = 31;
            this.btnExport.Text = "查询并导出月数据";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnTestConnect
            // 
            this.btnTestConnect.Location = new System.Drawing.Point(467, 10);
            this.btnTestConnect.Name = "btnTestConnect";
            this.btnTestConnect.Size = new System.Drawing.Size(132, 23);
            this.btnTestConnect.TabIndex = 29;
            this.btnTestConnect.Text = "连接并读取单位";
            this.btnTestConnect.UseVisualStyleBackColor = true;
            this.btnTestConnect.Click += new System.EventHandler(this.btnTestConnect_Click);
            // 
            // cmbTable
            // 
            this.cmbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTable.FormattingEnabled = true;
            this.cmbTable.Items.AddRange(new object[] {
            "调整和原始",
            "原始",
            "调整"});
            this.cmbTable.Location = new System.Drawing.Point(265, 74);
            this.cmbTable.Name = "cmbTable";
            this.cmbTable.Size = new System.Drawing.Size(87, 20);
            this.cmbTable.TabIndex = 27;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(146, 74);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.ShowUpDown = true;
            this.dtpEnd.Size = new System.Drawing.Size(74, 21);
            this.dtpEnd.TabIndex = 26;
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "yyyy-MM";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.Location = new System.Drawing.Point(53, 74);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.ShowUpDown = true;
            this.dtpBegin.Size = new System.Drawing.Size(74, 21);
            this.dtpBegin.TabIndex = 25;
            // 
            // cmbDept
            // 
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.Location = new System.Drawing.Point(53, 44);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.Size = new System.Drawing.Size(132, 20);
            this.cmbDept.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(132, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(230, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "数据";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "时间";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Location = new System.Drawing.Point(18, 46);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(29, 12);
            this.lblDept.TabIndex = 18;
            this.lblDept.Text = "单位";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "服务名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(323, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "密码";
            // 
            // txtDB
            // 
            this.txtDB.Location = new System.Drawing.Point(53, 12);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(100, 21);
            this.txtDB.TabIndex = 14;
            this.txtDB.Text = "USER_BASK_APP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "用户名";
            // 
            // txtDBPwd
            // 
            this.txtDBPwd.Location = new System.Drawing.Point(361, 12);
            this.txtDBPwd.Name = "txtDBPwd";
            this.txtDBPwd.Size = new System.Drawing.Size(100, 21);
            this.txtDBPwd.TabIndex = 15;
            this.txtDBPwd.Text = "USER_BASK_APP";
            // 
            // txtDBUser
            // 
            this.txtDBUser.Location = new System.Drawing.Point(206, 12);
            this.txtDBUser.Name = "txtDBUser";
            this.txtDBUser.Size = new System.Drawing.Size(100, 21);
            this.txtDBUser.TabIndex = 16;
            this.txtDBUser.Text = "USER_BASK_APP";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1Collapsed = true;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(804, 382);
            this.splitContainer2.SplitterDistance = 172;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtxtSql);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sql语句";
            // 
            // rtxtSql
            // 
            this.rtxtSql.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtSql.Location = new System.Drawing.Point(3, 17);
            this.rtxtSql.Name = "rtxtSql";
            this.rtxtSql.Size = new System.Drawing.Size(166, 80);
            this.rtxtSql.TabIndex = 0;
            this.rtxtSql.Text = "";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer3.Panel2Collapsed = true;
            this.splitContainer3.Size = new System.Drawing.Size(804, 382);
            this.splitContainer3.SplitterDistance = 371;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.linkClear);
            this.groupBox2.Controls.Add(this.rtxtMsg);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(804, 382);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "提示信息";
            // 
            // linkClear
            // 
            this.linkClear.AutoSize = true;
            this.linkClear.Location = new System.Drawing.Point(67, 2);
            this.linkClear.Name = "linkClear";
            this.linkClear.Size = new System.Drawing.Size(29, 12);
            this.linkClear.TabIndex = 1;
            this.linkClear.TabStop = true;
            this.linkClear.Text = "清空";
            this.linkClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkClear_LinkClicked);
            // 
            // rtxtMsg
            // 
            this.rtxtMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtMsg.Location = new System.Drawing.Point(3, 17);
            this.rtxtMsg.Name = "rtxtMsg";
            this.rtxtMsg.Size = new System.Drawing.Size(798, 362);
            this.rtxtMsg.TabIndex = 0;
            this.rtxtMsg.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvData);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(429, 405);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询结果";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(3, 17);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(423, 385);
            this.dgvData.TabIndex = 0;
            // 
            // chkAllDept
            // 
            this.chkAllDept.AutoSize = true;
            this.chkAllDept.Location = new System.Drawing.Point(185, 46);
            this.chkAllDept.Name = "chkAllDept";
            this.chkAllDept.Size = new System.Drawing.Size(48, 16);
            this.chkAllDept.TabIndex = 38;
            this.chkAllDept.Text = "全选";
            this.chkAllDept.UseVisualStyleBackColor = true;
            this.chkAllDept.CheckedChanged += new System.EventHandler(this.chkAllDept_CheckedChanged);
            // 
            // chkSheng
            // 
            this.chkSheng.AutoSize = true;
            this.chkSheng.Location = new System.Drawing.Point(677, 34);
            this.chkSheng.Name = "chkSheng";
            this.chkSheng.Size = new System.Drawing.Size(36, 16);
            this.chkSheng.TabIndex = 39;
            this.chkSheng.Text = "省";
            this.chkSheng.UseVisualStyleBackColor = true;
            this.chkSheng.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(605, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 40;
            this.label9.Text = "载入单位选择：";
            this.label9.Visible = false;
            // 
            // chkShi
            // 
            this.chkShi.AutoSize = true;
            this.chkShi.Checked = true;
            this.chkShi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShi.Location = new System.Drawing.Point(677, 12);
            this.chkShi.Name = "chkShi";
            this.chkShi.Size = new System.Drawing.Size(36, 16);
            this.chkShi.TabIndex = 39;
            this.chkShi.Text = "市";
            this.chkShi.UseVisualStyleBackColor = true;
            this.chkShi.Visible = false;
            // 
            // chkEjj
            // 
            this.chkEjj.AutoSize = true;
            this.chkEjj.Location = new System.Drawing.Point(719, 34);
            this.chkEjj.Name = "chkEjj";
            this.chkEjj.Size = new System.Drawing.Size(60, 16);
            this.chkEjj.TabIndex = 39;
            this.chkEjj.Text = "二级局";
            this.chkEjj.UseVisualStyleBackColor = true;
            this.chkEjj.Visible = false;
            // 
            // chkZgx
            // 
            this.chkZgx.AutoSize = true;
            this.chkZgx.Location = new System.Drawing.Point(719, 12);
            this.chkZgx.Name = "chkZgx";
            this.chkZgx.Size = new System.Drawing.Size(60, 16);
            this.chkZgx.TabIndex = 39;
            this.chkZgx.Text = "直管县";
            this.chkZgx.UseVisualStyleBackColor = true;
            this.chkZgx.Visible = false;
            // 
            // FrmDwdyStatistical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 529);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDwdyStatistical";
            this.Text = "电网电压统计工具";
            this.Load += new System.EventHandler(this.FrmDwdyStatistical_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cmbTable;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Windows.Forms.ComboBox cmbDept;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDept;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDBPwd;
        private System.Windows.Forms.TextBox txtDBUser;
        private System.Windows.Forms.Button btnTestConnect;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtxtSql;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtxtMsg;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ComboBox cmbCNW;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.LinkLabel linkClear;
        private System.Windows.Forms.Button btnCount;
        private System.Windows.Forms.CheckBox chkAllDept;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkSheng;
        private System.Windows.Forms.CheckBox chkEjj;
        private System.Windows.Forms.CheckBox chkShi;
        private System.Windows.Forms.CheckBox chkZgx;
    }
}
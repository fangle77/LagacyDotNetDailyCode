namespace yltl.Tools
{
    partial class FrmReg
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtxtReg = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenNew = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chkInTime = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rtxtText = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtxtResult = new System.Windows.Forms.RichTextBox();
            this.chkHideGroup = new System.Windows.Forms.CheckBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(790, 484);
            this.splitContainer1.SplitterDistance = 394;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer2.Size = new System.Drawing.Size(394, 484);
            this.splitContainer2.SplitterDistance = 215;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtxtReg);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(394, 171);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "正则表达式";
            // 
            // rtxtReg
            // 
            this.rtxtReg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtReg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtReg.Location = new System.Drawing.Point(3, 17);
            this.rtxtReg.Name = "rtxtReg";
            this.rtxtReg.Size = new System.Drawing.Size(388, 151);
            this.rtxtReg.TabIndex = 1;
            this.rtxtReg.Text = "";
            this.rtxtReg.TextChanged += new System.EventHandler(this.rtxtReg_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpenNew);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.chkInTime);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选项";
            // 
            // btnOpenNew
            // 
            this.btnOpenNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOpenNew.Location = new System.Drawing.Point(47, 15);
            this.btnOpenNew.Name = "btnOpenNew";
            this.btnOpenNew.Size = new System.Drawing.Size(75, 23);
            this.btnOpenNew.TabIndex = 2;
            this.btnOpenNew.Text = "新建窗口";
            this.btnOpenNew.UseVisualStyleBackColor = true;
            this.btnOpenNew.Click += new System.EventHandler(this.btnOpenNew_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(250, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "匹  配";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkInTime
            // 
            this.chkInTime.AutoSize = true;
            this.chkInTime.Checked = true;
            this.chkInTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInTime.Location = new System.Drawing.Point(148, 18);
            this.chkInTime.Name = "chkInTime";
            this.chkInTime.Size = new System.Drawing.Size(84, 16);
            this.chkInTime.TabIndex = 0;
            this.chkInTime.Text = "书写时匹配";
            this.chkInTime.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rtxtText);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(394, 265);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "匹配内容";
            // 
            // rtxtText
            // 
            this.rtxtText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtText.Location = new System.Drawing.Point(3, 17);
            this.rtxtText.Name = "rtxtText";
            this.rtxtText.Size = new System.Drawing.Size(388, 245);
            this.rtxtText.TabIndex = 1;
            this.rtxtText.Text = "";
            this.rtxtText.TextChanged += new System.EventHandler(this.rtxtReg_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkHideGroup);
            this.groupBox3.Controls.Add(this.rtxtResult);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(392, 484);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "匹配结果";
            // 
            // rtxtResult
            // 
            this.rtxtResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtResult.Location = new System.Drawing.Point(3, 17);
            this.rtxtResult.Name = "rtxtResult";
            this.rtxtResult.Size = new System.Drawing.Size(386, 464);
            this.rtxtResult.TabIndex = 0;
            this.rtxtResult.Text = "";
            // 
            // chkHideGroup
            // 
            this.chkHideGroup.AutoSize = true;
            this.chkHideGroup.Location = new System.Drawing.Point(68, 0);
            this.chkHideGroup.Name = "chkHideGroup";
            this.chkHideGroup.Size = new System.Drawing.Size(90, 16);
            this.chkHideGroup.TabIndex = 1;
            this.chkHideGroup.Text = "不显示group";
            this.chkHideGroup.UseVisualStyleBackColor = true;
            // 
            // FrmReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 484);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmReg";
            this.Text = "正则表达式匹配";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rtxtResult;
        private System.Windows.Forms.RichTextBox rtxtReg;
        private System.Windows.Forms.CheckBox chkInTime;
        private System.Windows.Forms.RichTextBox rtxtText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOpenNew;
        private System.Windows.Forms.CheckBox chkHideGroup;
    }
}
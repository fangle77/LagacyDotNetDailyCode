namespace yltl.Tools
{
    partial class FrmStringUtil
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
            this.rtxtInput = new System.Windows.Forms.RichTextBox();
            this.btnFind_rn = new System.Windows.Forms.Button();
            this.btnRSAPkcs8 = new System.Windows.Forms.Button();
            this.btnRemove_rn = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.rtxtOutput = new System.Windows.Forms.RichTextBox();
            this.txtSrc = new System.Windows.Forms.TextBox();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.chkCaseInsense = new System.Windows.Forms.CheckBox();
            this.btnReplace = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtxtOutput);
            this.splitContainer1.Size = new System.Drawing.Size(757, 540);
            this.splitContainer1.SplitterDistance = 252;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.rtxtInput);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnReplace);
            this.splitContainer2.Panel2.Controls.Add(this.chkCaseInsense);
            this.splitContainer2.Panel2.Controls.Add(this.txtReplace);
            this.splitContainer2.Panel2.Controls.Add(this.txtSrc);
            this.splitContainer2.Panel2.Controls.Add(this.btnFind_rn);
            this.splitContainer2.Panel2.Controls.Add(this.btnRSAPkcs8);
            this.splitContainer2.Panel2.Controls.Add(this.btnRemove_rn);
            this.splitContainer2.Panel2.Controls.Add(this.btnExecute);
            this.splitContainer2.Size = new System.Drawing.Size(757, 252);
            this.splitContainer2.SplitterDistance = 443;
            this.splitContainer2.TabIndex = 0;
            // 
            // rtxtInput
            // 
            this.rtxtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtInput.Location = new System.Drawing.Point(0, 0);
            this.rtxtInput.Name = "rtxtInput";
            this.rtxtInput.Size = new System.Drawing.Size(443, 252);
            this.rtxtInput.TabIndex = 1;
            this.rtxtInput.Text = "";
            // 
            // btnFind_rn
            // 
            this.btnFind_rn.Location = new System.Drawing.Point(3, 146);
            this.btnFind_rn.Name = "btnFind_rn";
            this.btnFind_rn.Size = new System.Drawing.Size(134, 27);
            this.btnFind_rn.TabIndex = 2;
            this.btnFind_rn.Text = "Find \\r\\n";
            this.btnFind_rn.UseVisualStyleBackColor = true;
            this.btnFind_rn.Click += new System.EventHandler(this.btnFind_rn_Click);
            // 
            // btnRSAPkcs8
            // 
            this.btnRSAPkcs8.Location = new System.Drawing.Point(3, 104);
            this.btnRSAPkcs8.Name = "btnRSAPkcs8";
            this.btnRSAPkcs8.Size = new System.Drawing.Size(134, 27);
            this.btnRSAPkcs8.TabIndex = 2;
            this.btnRSAPkcs8.Text = "RSA PKCS8";
            this.btnRSAPkcs8.UseVisualStyleBackColor = true;
            this.btnRSAPkcs8.Click += new System.EventHandler(this.btnRSAPkcs8_Click);
            // 
            // btnRemove_rn
            // 
            this.btnRemove_rn.Location = new System.Drawing.Point(3, 60);
            this.btnRemove_rn.Name = "btnRemove_rn";
            this.btnRemove_rn.Size = new System.Drawing.Size(134, 27);
            this.btnRemove_rn.TabIndex = 1;
            this.btnRemove_rn.Text = "Remove \\r\\n";
            this.btnRemove_rn.UseVisualStyleBackColor = true;
            this.btnRemove_rn.Click += new System.EventHandler(this.btnRemove_rn_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(3, 12);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(134, 27);
            this.btnExecute.TabIndex = 0;
            this.btnExecute.Text = "Code Sql";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtOutput.Location = new System.Drawing.Point(0, 0);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.Size = new System.Drawing.Size(757, 284);
            this.rtxtOutput.TabIndex = 0;
            this.rtxtOutput.Text = "";
            // 
            // txtSrc
            // 
            this.txtSrc.Location = new System.Drawing.Point(183, 18);
            this.txtSrc.Name = "txtSrc";
            this.txtSrc.Size = new System.Drawing.Size(100, 20);
            this.txtSrc.TabIndex = 3;
            // 
            // txtReplace
            // 
            this.txtReplace.Location = new System.Drawing.Point(183, 60);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(100, 20);
            this.txtReplace.TabIndex = 3;
            // 
            // chkCaseInsense
            // 
            this.chkCaseInsense.AutoSize = true;
            this.chkCaseInsense.Location = new System.Drawing.Point(183, 104);
            this.chkCaseInsense.Name = "chkCaseInsense";
            this.chkCaseInsense.Size = new System.Drawing.Size(50, 17);
            this.chkCaseInsense.TabIndex = 4;
            this.chkCaseInsense.Text = "Case";
            this.chkCaseInsense.UseVisualStyleBackColor = true;
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(173, 146);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(134, 27);
            this.btnReplace.TabIndex = 5;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // FrmStringUtil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 540);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmStringUtil";
            this.Text = "String Util";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox rtxtInput;
        private System.Windows.Forms.RichTextBox rtxtOutput;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnRemove_rn;
        private System.Windows.Forms.Button btnRSAPkcs8;
        private System.Windows.Forms.Button btnFind_rn;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.TextBox txtSrc;
        private System.Windows.Forms.CheckBox chkCaseInsense;
        private System.Windows.Forms.Button btnReplace;
    }
}
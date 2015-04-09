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
            this.rtxtResult = new System.Windows.Forms.RichTextBox();
            this.rtxtInput = new System.Windows.Forms.RichTextBox();
            this.btnExcute = new System.Windows.Forms.Button();
            this.chkRemoveNewLine = new System.Windows.Forms.CheckBox();
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
            this.splitContainer1.Panel1.Controls.Add(this.chkRemoveNewLine);
            this.splitContainer1.Panel1.Controls.Add(this.btnExcute);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(664, 382);
            this.splitContainer1.SplitterDistance = 70;
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
            this.splitContainer2.Panel2.Controls.Add(this.rtxtResult);
            this.splitContainer2.Size = new System.Drawing.Size(664, 308);
            this.splitContainer2.SplitterDistance = 332;
            this.splitContainer2.TabIndex = 0;
            // 
            // rtxtResult
            // 
            this.rtxtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtResult.Location = new System.Drawing.Point(0, 0);
            this.rtxtResult.Name = "rtxtResult";
            this.rtxtResult.Size = new System.Drawing.Size(328, 308);
            this.rtxtResult.TabIndex = 0;
            this.rtxtResult.Text = "";
            // 
            // rtxtInput
            // 
            this.rtxtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtInput.Location = new System.Drawing.Point(0, 0);
            this.rtxtInput.Name = "rtxtInput";
            this.rtxtInput.Size = new System.Drawing.Size(332, 308);
            this.rtxtInput.TabIndex = 1;
            this.rtxtInput.Text = "";
            // 
            // btnExcute
            // 
            this.btnExcute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcute.Location = new System.Drawing.Point(494, 24);
            this.btnExcute.Name = "btnExcute";
            this.btnExcute.Size = new System.Drawing.Size(158, 23);
            this.btnExcute.TabIndex = 0;
            this.btnExcute.Text = "Excute";
            this.btnExcute.UseVisualStyleBackColor = true;
            this.btnExcute.Click += new System.EventHandler(this.btnExcute_Click);
            // 
            // chkRemoveNewLine
            // 
            this.chkRemoveNewLine.AutoSize = true;
            this.chkRemoveNewLine.Checked = true;
            this.chkRemoveNewLine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemoveNewLine.Location = new System.Drawing.Point(44, 24);
            this.chkRemoveNewLine.Name = "chkRemoveNewLine";
            this.chkRemoveNewLine.Size = new System.Drawing.Size(88, 17);
            this.chkRemoveNewLine.TabIndex = 1;
            this.chkRemoveNewLine.Text = "Remove \\r\\n";
            this.chkRemoveNewLine.UseVisualStyleBackColor = true;
            // 
            // FrmStringUtil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 382);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmStringUtil";
            this.Text = "FrmStringUtil";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnExcute;
        private System.Windows.Forms.RichTextBox rtxtInput;
        private System.Windows.Forms.RichTextBox rtxtResult;
        private System.Windows.Forms.CheckBox chkRemoveNewLine;
    }
}
namespace yltl.Tools
{
    partial class FrmAccessDept
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
            this.txtBaseDB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstDepts = new System.Windows.Forms.ListBox();
            this.btnReadDept = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtBaseDB
            // 
            this.txtBaseDB.Location = new System.Drawing.Point(12, 27);
            this.txtBaseDB.Name = "txtBaseDB";
            this.txtBaseDB.Size = new System.Drawing.Size(383, 21);
            this.txtBaseDB.TabIndex = 0;
            this.txtBaseDB.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "原始数据库";
            // 
            // lstDepts
            // 
            this.lstDepts.FormattingEnabled = true;
            this.lstDepts.ItemHeight = 12;
            this.lstDepts.Location = new System.Drawing.Point(12, 54);
            this.lstDepts.Name = "lstDepts";
            this.lstDepts.Size = new System.Drawing.Size(383, 244);
            this.lstDepts.TabIndex = 2;
            // 
            // btnReadDept
            // 
            this.btnReadDept.Location = new System.Drawing.Point(12, 304);
            this.btnReadDept.Name = "btnReadDept";
            this.btnReadDept.Size = new System.Drawing.Size(142, 40);
            this.btnReadDept.TabIndex = 3;
            this.btnReadDept.Text = "读单位";
            this.btnReadDept.UseVisualStyleBackColor = true;
            this.btnReadDept.Click += new System.EventHandler(this.btnReadDept_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(253, 304);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(142, 40);
            this.btnGenerate.TabIndex = 3;
            this.btnGenerate.Text = "生成数据库";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(413, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(248, 431);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // FrmAccessDept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 455);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnReadDept);
            this.Controls.Add(this.lstDepts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBaseDB);
            this.Name = "FrmAccessDept";
            this.Text = "FrmAccessDept";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBaseDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstDepts;
        private System.Windows.Forms.Button btnReadDept;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}
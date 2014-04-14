namespace ExcelExportorForWin
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnExport = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.numCols = new System.Windows.Forms.NumericUpDown();
            this.numTables = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.panList = new System.Windows.Forms.Panel();
            this.lblTestDrag = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTables)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnExport);
            this.splitContainer1.Panel1.Controls.Add(this.richTextBox1);
            this.splitContainer1.Panel1.Controls.Add(this.numCols);
            this.splitContainer1.Panel1.Controls.Add(this.numTables);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panList);
            this.splitContainer1.Panel2.Controls.Add(this.lblTestDrag);
            this.splitContainer1.Size = new System.Drawing.Size(874, 519);
            this.splitContainer1.SplitterDistance = 54;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(289, 15);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(752, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(119, 48);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // numCols
            // 
            this.numCols.Location = new System.Drawing.Point(456, 15);
            this.numCols.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numCols.Name = "numCols";
            this.numCols.Size = new System.Drawing.Size(64, 21);
            this.numCols.TabIndex = 1;
            this.numCols.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numTables
            // 
            this.numTables.Location = new System.Drawing.Point(386, 14);
            this.numTables.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numTables.Name = "numTables";
            this.numTables.Size = new System.Drawing.Size(64, 21);
            this.numTables.TabIndex = 1;
            this.numTables.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(529, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "生成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panList
            // 
            this.panList.Location = new System.Drawing.Point(133, 169);
            this.panList.Name = "panList";
            this.panList.Size = new System.Drawing.Size(554, 280);
            this.panList.TabIndex = 1;
            // 
            // lblTestDrag
            // 
            this.lblTestDrag.AutoSize = true;
            this.lblTestDrag.Location = new System.Drawing.Point(12, 23);
            this.lblTestDrag.Name = "lblTestDrag";
            this.lblTestDrag.Size = new System.Drawing.Size(53, 12);
            this.lblTestDrag.TabIndex = 0;
            this.lblTestDrag.Text = "拖放测试";
            this.lblTestDrag.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTestDrag_MouseMove);
            this.lblTestDrag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTestDrag_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 519);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numCols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTables)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.NumericUpDown numCols;
        private System.Windows.Forms.NumericUpDown numTables;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblTestDrag;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel panList;

    }
}


namespace WebServerManager
{
    partial class FrmOpenFile
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
            this.btnTop = new System.Windows.Forms.Button();
            this.txtOraginalFile = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.lnkCloseTabPage = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnTop);
            this.splitContainer1.Panel1.Controls.Add(this.txtOraginalFile);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearch);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lnkCloseTabPage);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(603, 422);
            this.splitContainer1.SplitterDistance = 52;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnTop
            // 
            this.btnTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTop.Location = new System.Drawing.Point(577, 13);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(22, 23);
            this.btnTop.TabIndex = 2;
            this.btnTop.Text = "顶";
            this.btnTop.UseVisualStyleBackColor = true;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // txtOraginalFile
            // 
            this.txtOraginalFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOraginalFile.Location = new System.Drawing.Point(15, 15);
            this.txtOraginalFile.Name = "txtOraginalFile";
            this.txtOraginalFile.Size = new System.Drawing.Size(472, 20);
            this.txtOraginalFile.TabIndex = 1;
            this.txtOraginalFile.TextChanged += new System.EventHandler(this.txtOraginalFile_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(493, 13);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "确定";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(603, 366);
            this.tabControl1.TabIndex = 0;
            // 
            // lnkCloseTabPage
            // 
            this.lnkCloseTabPage.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
            this.lnkCloseTabPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkCloseTabPage.AutoSize = true;
            this.lnkCloseTabPage.Location = new System.Drawing.Point(583, 1);
            this.lnkCloseTabPage.Name = "lnkCloseTabPage";
            this.lnkCloseTabPage.Size = new System.Drawing.Size(14, 13);
            this.lnkCloseTabPage.TabIndex = 3;
            this.lnkCloseTabPage.TabStop = true;
            this.lnkCloseTabPage.Text = "X";
            this.lnkCloseTabPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCloseTabPage_LinkClicked);
            // 
            // FrmOpenFile
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 422);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmOpenFile";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtOraginalFile;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnTop;
        private System.Windows.Forms.LinkLabel lnkCloseTabPage;
    }
}
namespace WebServerManager
{
    partial class FrmWebDev
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWebDev));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnFileForm = new System.Windows.Forms.Button();
            this.btnFreshState = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtVSDev = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWebDev = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panSites = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnFileForm);
            this.splitContainer1.Panel1.Controls.Add(this.btnFreshState);
            this.splitContainer1.Panel1.Controls.Add(this.btnSave);
            this.splitContainer1.Panel1.Controls.Add(this.btnUpdate);
            this.splitContainer1.Panel1.Controls.Add(this.txtVSDev);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txtWebDev);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panSites);
            this.splitContainer1.Size = new System.Drawing.Size(745, 535);
            this.splitContainer1.SplitterDistance = 113;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnFileForm
            // 
            this.btnFileForm.Location = new System.Drawing.Point(522, 80);
            this.btnFileForm.Name = "btnFileForm";
            this.btnFileForm.Size = new System.Drawing.Size(75, 23);
            this.btnFileForm.TabIndex = 8;
            this.btnFileForm.Text = "打开文件";
            this.btnFileForm.UseVisualStyleBackColor = true;
            this.btnFileForm.Click += new System.EventHandler(this.btnFileForm_Click);
            // 
            // btnFreshState
            // 
            this.btnFreshState.Location = new System.Drawing.Point(396, 80);
            this.btnFreshState.Name = "btnFreshState";
            this.btnFreshState.Size = new System.Drawing.Size(75, 23);
            this.btnFreshState.TabIndex = 7;
            this.btnFreshState.Text = "刷新状态";
            this.btnFreshState.UseVisualStyleBackColor = true;
            this.btnFreshState.Click += new System.EventHandler(this.btnFreshState_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(266, 80);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存站点信息";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(605, 36);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(126, 23);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "更新配置";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtVSDev
            // 
            this.txtVSDev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVSDev.Location = new System.Drawing.Point(161, 37);
            this.txtVSDev.Name = "txtVSDev";
            this.txtVSDev.Size = new System.Drawing.Size(436, 20);
            this.txtVSDev.TabIndex = 4;
            this.txtVSDev.TextChanged += new System.EventHandler(this.txtWebDev_TextChanged);
            this.txtVSDev.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtWebDev_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "VS编译程序";
            // 
            // txtWebDev
            // 
            this.txtWebDev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWebDev.Location = new System.Drawing.Point(161, 11);
            this.txtWebDev.Name = "txtWebDev";
            this.txtWebDev.Size = new System.Drawing.Size(436, 20);
            this.txtWebDev.TabIndex = 4;
            this.txtWebDev.TextChanged += new System.EventHandler(this.txtWebDev_TextChanged);
            this.txtWebDev.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtWebDev_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "WebDev.WebServer程序";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(161, 80);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "添加站点";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panSites
            // 
            this.panSites.AutoScroll = true;
            this.panSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panSites.Location = new System.Drawing.Point(0, 0);
            this.panSites.Name = "panSites";
            this.panSites.Size = new System.Drawing.Size(741, 414);
            this.panSites.TabIndex = 0;
            // 
            // FrmWebDev
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 535);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmWebDev";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dev站点";
            this.Load += new System.EventHandler(this.FrmWebDev_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panSites;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtWebDev;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnFreshState;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnFileForm;
        private System.Windows.Forms.TextBox txtVSDev;
        private System.Windows.Forms.Label label1;
    }
}
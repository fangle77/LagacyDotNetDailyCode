namespace yltl.UIComponent
{
    partial class ViewForm
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
            this.mainSplit = new System.Windows.Forms.SplitContainer();
            this.panConditions = new System.Windows.Forms.Panel();
            this.panButtons = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnExportAll = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.chkCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mainSplit.Panel1.SuspendLayout();
            this.mainSplit.Panel2.SuspendLayout();
            this.mainSplit.SuspendLayout();
            this.panButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // mainSplit
            // 
            this.mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplit.Location = new System.Drawing.Point(0, 0);
            this.mainSplit.Name = "mainSplit";
            this.mainSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplit.Panel1
            // 
            this.mainSplit.Panel1.Controls.Add(this.panConditions);
            this.mainSplit.Panel1.Controls.Add(this.panButtons);
            // 
            // mainSplit.Panel2
            // 
            this.mainSplit.Panel2.Controls.Add(this.dgvData);
            this.mainSplit.Size = new System.Drawing.Size(649, 420);
            this.mainSplit.SplitterDistance = 77;
            this.mainSplit.TabIndex = 0;
            // 
            // panConditions
            // 
            this.panConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panConditions.Location = new System.Drawing.Point(0, 0);
            this.panConditions.Name = "panConditions";
            this.panConditions.Size = new System.Drawing.Size(649, 52);
            this.panConditions.TabIndex = 0;
            // 
            // panButtons
            // 
            this.panButtons.Controls.Add(this.btnExit);
            this.panButtons.Controls.Add(this.btnHelp);
            this.panButtons.Controls.Add(this.btnExportAll);
            this.panButtons.Controls.Add(this.btnExportExcel);
            this.panButtons.Controls.Add(this.btnDelete);
            this.panButtons.Controls.Add(this.btnUpdate);
            this.panButtons.Controls.Add(this.btnAdd);
            this.panButtons.Controls.Add(this.btnSearch);
            this.panButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panButtons.Location = new System.Drawing.Point(0, 52);
            this.panButtons.Name = "panButtons";
            this.panButtons.Size = new System.Drawing.Size(649, 25);
            this.panButtons.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Location = new System.Drawing.Point(525, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnHelp.Location = new System.Drawing.Point(450, 0);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 25);
            this.btnHelp.TabIndex = 0;
            this.btnHelp.Text = "帮助";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnExportAll
            // 
            this.btnExportAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExportAll.Location = new System.Drawing.Point(375, 0);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.Size = new System.Drawing.Size(75, 25);
            this.btnExportAll.TabIndex = 0;
            this.btnExportAll.Text = "导出全部";
            this.btnExportAll.UseVisualStyleBackColor = true;
            this.btnExportAll.Click += new System.EventHandler(this.btnExportAll_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExportExcel.Location = new System.Drawing.Point(300, 0);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(75, 25);
            this.btnExportExcel.TabIndex = 0;
            this.btnExportExcel.Text = "导出Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDelete.Location = new System.Drawing.Point(225, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnUpdate.Location = new System.Drawing.Point(150, 0);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 25);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "修改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.Location = new System.Drawing.Point(75, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSearch.Location = new System.Drawing.Point(0, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkCheck});
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(649, 339);
            this.dgvData.TabIndex = 0;
            // 
            // chkCheck
            // 
            this.chkCheck.FalseValue = "N";
            this.chkCheck.HeaderText = "全选";
            this.chkCheck.IndeterminateValue = "Y";
            this.chkCheck.Name = "chkCheck";
            this.chkCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chkCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.chkCheck.TrueValue = "Y";
            // 
            // ViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 420);
            this.Controls.Add(this.mainSplit);
            this.Name = "ViewForm";
            this.Text = "查看";
            this.Load += new System.EventHandler(this.ViewForm_Load);
            this.mainSplit.Panel1.ResumeLayout(false);
            this.mainSplit.Panel2.ResumeLayout(false);
            this.mainSplit.ResumeLayout(false);
            this.panButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.SplitContainer mainSplit;
        protected System.Windows.Forms.Panel panButtons;
        protected System.Windows.Forms.Panel panConditions;
        protected System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkCheck;
        protected System.Windows.Forms.Button btnExportAll;
        protected System.Windows.Forms.Button btnExportExcel;
        protected System.Windows.Forms.Button btnDelete;
        protected System.Windows.Forms.Button btnUpdate;
        protected System.Windows.Forms.Button btnAdd;
        protected System.Windows.Forms.Button btnSearch;
        protected System.Windows.Forms.Button btnHelp;
        protected System.Windows.Forms.Button btnExit;
    }
}
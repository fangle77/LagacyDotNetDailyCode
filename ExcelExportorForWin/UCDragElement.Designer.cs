namespace ExcelExportorForWin
{
    partial class UCDragElement
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lstDestionation = new System.Windows.Forms.ListBox();
            this.lstMapping = new System.Windows.Forms.ListBox();
            this.lstSource = new System.Windows.Forms.ListBox();
            this.gpbSource = new System.Windows.Forms.GroupBox();
            this.gpbDestination = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gpbSource.SuspendLayout();
            this.gpbDestination.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstDestionation
            // 
            this.lstDestionation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDestionation.FormattingEnabled = true;
            this.lstDestionation.ItemHeight = 12;
            this.lstDestionation.Location = new System.Drawing.Point(3, 17);
            this.lstDestionation.Name = "lstDestionation";
            this.lstDestionation.Size = new System.Drawing.Size(163, 364);
            this.lstDestionation.TabIndex = 4;
            this.lstDestionation.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstSource_DragDrop);
            this.lstDestionation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstSource_MouseMove);
            this.lstDestionation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstSource_MouseDown);
            this.lstDestionation.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstSource_DragEnter);
            // 
            // lstMapping
            // 
            this.lstMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMapping.FormattingEnabled = true;
            this.lstMapping.ItemHeight = 12;
            this.lstMapping.Location = new System.Drawing.Point(3, 17);
            this.lstMapping.Name = "lstMapping";
            this.lstMapping.Size = new System.Drawing.Size(164, 364);
            this.lstMapping.TabIndex = 3;
            this.lstMapping.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstSource_MouseMove);
            this.lstMapping.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstSource_MouseDown);
            // 
            // lstSource
            // 
            this.lstSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSource.FormattingEnabled = true;
            this.lstSource.ItemHeight = 12;
            this.lstSource.Location = new System.Drawing.Point(3, 17);
            this.lstSource.Name = "lstSource";
            this.lstSource.Size = new System.Drawing.Size(163, 364);
            this.lstSource.TabIndex = 2;
            this.lstSource.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstSource_DragDrop);
            this.lstSource.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstSource_MouseMove);
            this.lstSource.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstSource_MouseDown);
            this.lstSource.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstSource_DragEnter);
            // 
            // gpbSource
            // 
            this.gpbSource.Controls.Add(this.lstSource);
            this.gpbSource.Dock = System.Windows.Forms.DockStyle.Left;
            this.gpbSource.Location = new System.Drawing.Point(0, 0);
            this.gpbSource.Name = "gpbSource";
            this.gpbSource.Size = new System.Drawing.Size(169, 384);
            this.gpbSource.TabIndex = 5;
            this.gpbSource.TabStop = false;
            this.gpbSource.Text = "源列名";
            // 
            // gpbDestination
            // 
            this.gpbDestination.Controls.Add(this.lstDestionation);
            this.gpbDestination.Dock = System.Windows.Forms.DockStyle.Left;
            this.gpbDestination.Location = new System.Drawing.Point(169, 0);
            this.gpbDestination.Name = "gpbDestination";
            this.gpbDestination.Size = new System.Drawing.Size(169, 384);
            this.gpbDestination.TabIndex = 6;
            this.gpbDestination.TabStop = false;
            this.gpbDestination.Text = "目标列名";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstMapping);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(338, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(170, 384);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "对应关系";
            // 
            // UCDragElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gpbDestination);
            this.Controls.Add(this.gpbSource);
            this.Name = "UCDragElement";
            this.Size = new System.Drawing.Size(508, 384);
            this.gpbSource.ResumeLayout(false);
            this.gpbDestination.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstDestionation;
        private System.Windows.Forms.ListBox lstMapping;
        private System.Windows.Forms.ListBox lstSource;
        private System.Windows.Forms.GroupBox gpbSource;
        private System.Windows.Forms.GroupBox gpbDestination;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

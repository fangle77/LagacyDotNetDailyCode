namespace MySharper
{
    partial class FrmMain
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
            this.txtKeyWord = new System.Windows.Forms.TextBox();
            this.lblElapseTime = new System.Windows.Forms.Label();
            this.panelResult = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnTop = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtKeyWord
            // 
            this.txtKeyWord.Location = new System.Drawing.Point(3, 16);
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Size = new System.Drawing.Size(292, 20);
            this.txtKeyWord.TabIndex = 0;
            this.txtKeyWord.TextChanged += new System.EventHandler(this.txtKeyWord_TextChanged);
            this.txtKeyWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyWord_KeyDown);
            // 
            // lblElapseTime
            // 
            this.lblElapseTime.AutoSize = true;
            this.lblElapseTime.Location = new System.Drawing.Point(303, 24);
            this.lblElapseTime.Name = "lblElapseTime";
            this.lblElapseTime.Size = new System.Drawing.Size(13, 13);
            this.lblElapseTime.TabIndex = 2;
            this.lblElapseTime.Text = "0";
            // 
            // panelResult
            // 
            this.panelResult.AutoScroll = true;
            this.panelResult.AutoSize = true;
            this.panelResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResult.Location = new System.Drawing.Point(0, 39);
            this.panelResult.Name = "panelResult";
            this.panelResult.Size = new System.Drawing.Size(365, 58);
            this.panelResult.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(340, 1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 20);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnTop
            // 
            this.btnTop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnTop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTop.Image = global::MySharper.Properties.Resources.Bluepin;
            this.btnTop.Location = new System.Drawing.Point(307, 1);
            this.btnTop.Margin = new System.Windows.Forms.Padding(0);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(28, 20);
            this.btnTop.TabIndex = 5;
            this.btnTop.Text = ".";
            this.btnTop.UseVisualStyleBackColor = true;
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.btnTop);
            this.panel1.Controls.Add(this.txtKeyWord);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 39);
            this.panel1.TabIndex = 4;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(365, 97);
            this.Controls.Add(this.panelResult);
            this.Controls.Add(this.lblElapseTime);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmMain";
            this.Opacity = 0.75D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MySharper";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtKeyWord;
        private System.Windows.Forms.Label lblElapseTime;
        private System.Windows.Forms.Panel panelResult;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnTop;
        private System.Windows.Forms.Panel panel1;
    }
}


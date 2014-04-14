using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace yltl.ExcelHelper
{
    internal partial class SheetExcelForm : Form
    {
        public SheetExcelForm()
        {
            InitializeComponent();
        }

        private void AddValue(int current, int total)
        {
            if (total <= 0) total = 1;
            if (current > total) current = total;
            progressBar1.Maximum = total;
            progressBar1.Value = current;

            lblProcess.Text = string.Format("{0}/{1}    {2}%", current, total, (((double)current / total) * 100).ToString("0.0"));
        }

        public void AddProcess(int current, int total)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    AddValue(current, total);
                }));
            }
            else AddValue(current, total);
        }
    }
}

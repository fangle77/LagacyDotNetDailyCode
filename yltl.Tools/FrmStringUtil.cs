using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace yltl.Tools
{
    public partial class FrmStringUtil : Form
    {
        public FrmStringUtil()
        {
            InitializeComponent();
        }

        private void btnExcute_Click(object sender, EventArgs e)
        {
            string input = rtxtInput.Text.Trim();
            if (chkRemoveNewLine.Checked)
            {
                input = input.Replace("\r", "").Replace("\n", "");
            }
            rtxtResult.Text = input;
        }
    }
}

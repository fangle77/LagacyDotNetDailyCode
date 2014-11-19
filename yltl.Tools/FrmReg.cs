using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace yltl.Tools
{
    public partial class FrmReg : Form
    {
        public FrmReg()
        {
            InitializeComponent();
        }

        private void rtxtReg_TextChanged(object sender, EventArgs e)
        {
            if (chkInTime.Checked) Match();
        }

        private void Match()
        {
            rtxtResult.Text = Match(rtxtReg.Text, rtxtText.Text);
        }

        private string Match(string regular, string input)
        {
            if (string.IsNullOrEmpty(regular) || string.IsNullOrEmpty(input))
            {
                return "";
            }
            try
            {
                var reg = new Regex(regular, RegexOptions.IgnoreCase);

                var matchs = reg.Matches(input);
                if (matchs == null || matchs.Count == 0)
                {
                    return "";
                }

                StringBuilder sb = new StringBuilder(input.Length);
                for (int j = 0; j < matchs.Count; j++)
                {
                    if (matchs[j].Success)
                    {
                        if (matchs[j].Value != null)
                        {
                            sb.AppendLine("V[" + j + "]:\t" + matchs[j].Value);
                        }
                        if (chkHideGroup.Checked == false)
                        {
                            var groups = matchs[j].Groups;
                            for (int i = 0; i < groups.Count; i++)
                            {
                                var g = groups[i];
                                sb.AppendLine("G[" + i + "]:\t" + g.Value);
                            }
                        }
                    }
                }

                return sb.ToString();
            }
            catch { return ""; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Match();
        }


        public static int InstanceCount = 0;
        private void btnOpenNew_Click(object sender, EventArgs e)
        {
            FrmReg frm = new FrmReg();
            InstanceCount++;
            frm.WindowState = FormWindowState.Normal;
            frm.StartPosition = FormStartPosition.WindowsDefaultLocation;
            frm.Text = "[" + InstanceCount + "]" + frm.Text;
            frm.Show();
            frm.BringToFront();
        }
    }
}

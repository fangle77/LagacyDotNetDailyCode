using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace yltl.Tools
{
    public partial class FrmStringUtil : Form
    {
        public FrmStringUtil()
        {
            InitializeComponent();
        }

        const String pemprivheader = "-----BEGIN RSA PRIVATE KEY-----";
        const String pemprivfooter = "-----END RSA PRIVATE KEY-----";
        const String pempubheader = "-----BEGIN PUBLIC KEY-----";
        const String pempubfooter = "-----END PUBLIC KEY-----";
        const String pemp8header = "-----BEGIN PRIVATE KEY-----";
        const String pemp8footer = "-----END PRIVATE KEY-----";
        const String pemp8encheader = "-----BEGIN ENCRYPTED PRIVATE KEY-----";
        const String pemp8encfooter = "-----END ENCRYPTED PRIVATE KEY-----";

        private string InputString { get { return rtxtInput.Text.Trim(); } }
        private string OutputString { set { rtxtOutput.Text = value; } }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            string temp = InputString.Replace("\\\"", "\\-/");
            temp = temp.Replace("\\r", "").Replace("\\n", "").Replace("\r", "").Replace("\n", "\r\n").Replace("+", " ").Replace("\"", " ");
            OutputString = temp.Replace("\\-/", "\\\"");
        }

        private string ReplaceSql(string input)
        {
            string temp = input.Replace("\\\"", "\\-/");
            temp = temp.Replace("\\r", "").Replace("\\n", "").Replace("\r", "").Replace("\n", "\r\n").Replace("+", " ").Replace("\"", " ");
            return temp.Replace("\\-/", "\\\"");
        }

        private void btnRemove_rn_Click(object sender, EventArgs e)
        {
            OutputString = InputString.Replace("\r", "").Replace("\n", "");
        }

        private void btnRSAPkcs8_Click(object sender, EventArgs e)
        {
            string x = InputString.Replace("\r", "").Replace("\n", "");
            StringBuilder sb = new StringBuilder();

            sb.Append("\n");
            int length = 0;
            while (length < x.Length)
            {
                sb.Append(x.Substring(length, Math.Min(x.Length - length, 64)));
                sb.Append('\n');
                length += 64;
            }

            sb.Insert(0, pemp8header);
            sb.Append(pemp8footer);

            OutputString = sb.ToString();
        }

        private void btnFind_rn_Click(object sender, EventArgs e)
        {
            OutputString = InputString.Replace("\r", "{\\r}\r").Replace("\n", "{\\n}\n");
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            string src = txtSrc.Text.Trim();
            string replace = txtReplace.Text.Trim();
            if (replace == "\\r\\n") replace = "\r\n";
            bool caseSensitive = chkCaseInsense.Checked;

            string input = rtxtInput.Text.Trim();
            if (caseSensitive)
            {
                rtxtOutput.Text = input.Replace(src, replace);
            }
            else
            {
                Regex reg = new Regex(src, RegexOptions.IgnoreCase);
                rtxtOutput.Text = reg.Replace(input, replace);
            }
        }
    }
}

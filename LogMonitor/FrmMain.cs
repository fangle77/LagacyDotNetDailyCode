using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LogMonitor
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            var dir = @"\\local-www\ExceptionLog\Diapers\2014\5\19";
            List<ResultItem> ris = new List<ResultItem>();
            var dif = new DirectoryInfo(dir);
            var matcher = new ErrorMatcher();
            int i = 0;
            foreach (var file in dif.GetFiles())
            {
                if (i++ > 20) break;
                var r = FileContentFinder.Find(file.FullName, matcher);
                if (r != null) ris.AddRange(r);
            }
            this.Tag = ris;
        }
    }
}

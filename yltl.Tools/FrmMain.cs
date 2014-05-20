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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            InitTabPage();
        }

        private Dictionary<string, Form> _FormCollection = new Dictionary<string, Form>()
            {
                //{"电网电压统计", new FrmDwdyStatistical()},
                //{"TNSName", new FrmOraTnsname()},
                {"正则表达式", new FrmReg()},
                {"字符串处理", new FrmStringUtil()},
                //{"生成单位数据库", new FrmAccessDept()},
                //{"数据表迁移", new FrmTableTransport()},
                {"文件清理", new FormClear()}
            };

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void InitTabPage()
        {
            List<TabPage> list = new List<TabPage>();
            foreach (string name in _FormCollection.Keys)
            {
                var tp = new TabPage(name);
                tp.Name = name;

                list.Add(tp);
            }
            tabMain.TabPages.AddRange(list.ToArray());
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab != tpMain && tabMain.SelectedTab.Controls.Count == 0)
            {
                string name = tabMain.SelectedTab.Name;

                if (_FormCollection.ContainsKey(name))
                {
                    var frm = _FormCollection[name];
                    if (frm != null)
                    {
                        frm.FormBorderStyle = FormBorderStyle.None;
                        frm.TopMost = false;
                        frm.TopLevel = false;
                        frm.Dock = DockStyle.Fill;
                        tabMain.SelectedTab.Controls.Add(frm);
                        frm.Show();
                    }
                }
            }
        }
    }
}

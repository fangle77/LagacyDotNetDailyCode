using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace yltl.Tools
{
    public partial class FrmAccessDept : Form
    {
        public FrmAccessDept()
        {
            InitializeComponent();
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.mdb|*.mdb";
            ofd.ShowDialog();
            string file = ofd.FileName;
            if (string.IsNullOrEmpty(file))
            {
                return;
            }
            txtBaseDB.Text = file;
        }

        private void AddInfo(string info)
        {
            richTextBox1.BeginInvoke(new MethodInvoker(() =>
            {
                richTextBox1.AppendText(info + "\r\n");
            }));
        }

        private Dictionary<string, string> _depts;

        private void btnReadDept_Click(object sender, EventArgs e)
        {
            var th = new Thread(new ThreadStart(() =>
            {
                try
                {
                    string oraConnect = "Data Source=user_sxdy_sheng;User ID=user_sxdy_sheng;Password=user_sxdy_sheng";
                    var dbo = yltl.DBUtility.DBFactory.Create(yltl.DBUtility.eDBType.Oracle, oraConnect);

                    var dt = dbo.GetDataTable("select dept_name,dept_id from sys_dept where val_xfield4='2' order by dept_name");

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        AddInfo("没找到单位");
                        return;
                    }

                    List<string> ss = new List<string>(dt.Rows.Count);

                    _depts = new Dictionary<string, string>(dt.Rows.Count);

                    foreach (DataRow dr in dt.Rows)
                    {
                        ss.Add(string.Format("{0}\t{1}", dr["dept_id"], dr["dept_name"]));

                        _depts.Add(dr["dept_id"].ToString(),dr["dept_name"].ToString());
                    }

                    BeginInvoke(new MethodInvoker(() =>
                    {
                        lstDepts.Items.Clear();
                        lstDepts.Items.AddRange(ss.ToArray());
                    }));
                }
                catch (Exception ex)
                {
                    AddInfo(ex.Message);
                }
            }));
            th.Start();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string baseDb = txtBaseDB.Text.Trim();
            if (string.IsNullOrEmpty(baseDb))
            {
                AddInfo("请选择原始数据库");
                return;
            }
            if (File.Exists(baseDb) == false)
            {
                AddInfo("原始数据库不存在：" + baseDb);
                return;
            }

            var th = new Thread(new ThreadStart(() =>
            {

                try
                {
                    string baseDir = Application.StartupPath.TrimEnd('\\') + "\\";

                    foreach (string deptid in _depts.Keys)
                    {
                        string deptName = _depts[deptid];
                        string dir = baseDir + deptName.Substring(0, 3);

                        AddInfo("开始生成：" + deptName);

                        CreateDir(dir);

                        string deptMdb = CopyMdb(dir, baseDb);

                        string conn = BuildAccessConnectString(deptMdb);

                        var dbo = yltl.DBUtility.DBFactory.Create(yltl.DBUtility.eDBType.Access, conn);

                        dbo.ExecuteSql(string.Format("update T_DY_BSCSB set dwmc='{0}',dept_id='{1}'", deptName, deptid));

                        AddInfo("生成完成，路径：" + deptMdb);
                    }
                }
                catch (Exception ex)
                {
                    AddInfo(ex.Message);
                }

            }));
            th.Start();
        }

        private string BuildAccessConnectString(string mdb)
        {
            return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};jet OleDB:Database Password=123456", mdb);
        }

        private void CreateDir(string name)
        {
            if (Directory.Exists(name) == false)
            {
                Directory.CreateDirectory(name);
            }
        }

        private string CopyMdb(string dir, string src)
        {
            string dest = dir.TrimEnd('\\') + "\\" + "voltagedb_c.mdb";

            File.Copy(src, dest, true);

            return dest;
        }
    }
}

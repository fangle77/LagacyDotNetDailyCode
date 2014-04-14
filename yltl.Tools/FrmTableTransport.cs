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
    public partial class FrmTableTransport : Form
    {
        public FrmTableTransport()
        {
            InitializeComponent();
        }

        private bool _stop = false;

        private string _DYCD = @"SELECT * FROM T_DY_DYCD WHERE DEPT_ID IN(
SELECT DEPT_ID FROM SYS_DEPT START WITH DEPT_ID='692F0FC4F11D01B2E0430A8EA81001B20000' CONNECT BY PRIOR DEPT_ID=pDEPT_ID
)";
        private string _DYJCY = @"select * from t_sb_dyjcy where obj_id in (
 SELECT sb_id FROM T_DY_DYCD WHERE DEPT_ID IN(
 SELECT DEPT_ID FROM SYS_DEPT START WITH DEPT_ID='692F0FC4F11D01B2E0430A8EA81001B20000' CONNECT BY PRIOR DEPT_ID=pDEPT_ID
))";

        private string _SCADASB = @"select * from t_sb_scadasb where obj_id in (
SELECT sb_id FROM T_DY_DYCD WHERE DEPT_ID IN(
SELECT DEPT_ID FROM SYS_DEPT START WITH DEPT_ID='692F0FC4F11D01B2E0430A8EA81001B20000' CONNECT BY PRIOR DEPT_ID=pDEPT_ID
))";

        private string _Data = @"select * from {0} where cd_id in(
SELECT obj_id FROM T_DY_DYCD WHERE DEPT_ID IN(
SELECT DEPT_ID FROM SYS_DEPT START WITH DEPT_ID='692F0FC4F11D01B2E0430A8EA81001B20000' CONNECT BY PRIOR DEPT_ID=pDEPT_ID
)) and realyear>'2011'";

        private void button1_Click(object sender, EventArgs e)
        {
            _stop = false;

            var th = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    TransportTable("T_SB_DYJCY", _DYJCY);
                    TransportTable("T_DY_DYCD", _DYCD);
                    TransportTable("T_SB_SCADASB", _SCADASB);
                    TransportTable("T_DY_MONTH", string.Format(_Data, "T_DY_MONTH"));
                    TransportTable("T_DY_MONTHTRUE", string.Format(_Data, "T_DY_MONTHTRUE"));
                    TransportTable("T_DY_DAY", string.Format(_Data, "T_DY_DAY"));
                    TransportTable("T_DY_DAYTRUE", string.Format(_Data, "T_DY_DAYTRUE"));
                }
                catch (Exception ex)
                {
                    AddMsg(ex.Message);
                }
            }));

            th.Start();
        }

        private void AddMsg(string msg)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                richTextBox1.AppendText(msg + "\r\n");
                richTextBox1.ScrollToCaret();
            }));
        }

        private void TransportTable(string tableName, string sql)
        {
            if (_stop) return;
            AddMsg("===================");
            AddMsg("表：" + tableName);

            var dbora = yltl.DBUtility.DBFactory.CreateOracle("user_bask_app", "user_bask_app", "user_bask_app");
            var dbacc = yltl.DBUtility.DBFactory.CreateAccess(textBox1.Text, "123456");

            AddMsg("查询源表");
            DataTable source = dbora.GetDataTable(sql);
            AddMsg("查询目标表");
            dbacc.ExecuteSql("delete from " + tableName);
            DataTable dest = dbacc.GetDataTable(string.Format("select * from {0} where 1=0", tableName));
            AddMsg("分析表");
            DataTable dt = DiffrentOfColumns(source, dest);

            AddMsg("构建语句");
            string format = BuildSql(dest, tableName);

            AddMsg("开始插入");
            for (int i = 0; i < source.Rows.Count; i++)
            {
                if (_stop) break;
                string insert = "";
                try
                {
                    insert = string.Format(format, source.Rows[i].ItemArray);
                    insert = insert.Replace(",,,,", ",0,0,0,").Replace(",,,", ",0,0,").Replace(",,", ",0,");
                    dbacc.ExecuteSql(insert);
                    AddMsg(i + "/" + source.Rows.Count);
                }
                catch (Exception ex)
                {
                    AddMsg(ex.Message);
                    AddMsg(insert);
                }
            }
        }

        private string BuildSql(DataTable destTable, string tableName)
        {
            StringBuilder sbColumns = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();

            //sb.AppendFormat("insert into {0}(", tableName);
            for (int i = 0; i < destTable.Columns.Count; i++)
            {
                var dc = destTable.Columns[i];
                sbColumns.Append(dc.ColumnName + ",");

                var type = dc.DataType;

                if (type == typeof(int)
                    || type == typeof(double)
                    || type == typeof(decimal))
                {
                    sbValues.AppendFormat("{0},", "{" + i + "}");
                }
                else
                {
                    sbValues.AppendFormat("'{0}',", "{" + i + "}");
                }
            }

            sbColumns = sbColumns.Remove(sbColumns.Length - 1, 1);
            sbValues = sbValues.Remove(sbValues.Length - 1, 1);

            return string.Format("insert into {0} ({1}) values ({2})", tableName, sbColumns.ToString(), sbValues.ToString());
        }

        private DataTable DiffrentOfColumns(DataTable source, DataTable dest)
        {
            List<string> toremoves = new List<string>();
            foreach (DataColumn dc in source.Columns)
            {
                if (dest.Columns.Contains(dc.ColumnName) == false)
                {
                    toremoves.Add(dc.ColumnName);
                }
            }
            toremoves.ForEach(n => { source.Columns.Remove(n); });

            source.AcceptChanges();

            for (int i = 0; i < dest.Columns.Count; i++)
            {
                source.Columns[dest.Columns[i].ColumnName].SetOrdinal(i);
            }
            source.AcceptChanges();
            return source;
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            textBox1.Text = ofd.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _stop = true;
        }
    }
}

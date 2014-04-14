using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using yltl.DBUtility;
using yltl.Common;
using yltl.ExcelHelper;

namespace yltl.Tools
{
    /// <summary>
    /// 电网电压统计工具
    /// </summary>
    public partial class FrmDwdyStatistical : Form
    {
        public FrmDwdyStatistical()
        {
            InitializeComponent();
        }

        private void FrmDwdyStatistical_Load(object sender, EventArgs e)
        {
            if (cmbTable.Items.Count > 0)
            {
                cmbTable.SelectedIndex = 0;
            }
            if (cmbCNW.Items.Count > 0)
            {
                cmbCNW.SelectedIndex = 0;
            }
        }

        #region 查询语句

        string _selectFormat = @"select 
 STATION_NAME as 测点名称,
                MAXV_MINV as 考核范围,
                VOLTAGE_TYPE as 测点类型,
                REALYEAR as 年,
                REALMONTH as 月,
                TO_CHAR(TOTALTIME) as 总运行时间,
                TO_CHAR(HIGHTIME) as 超高时间,
                TO_CHAR(LOWTIME) as 超低时间,
                TO_CHAR(RATETIME) as 合格时间,
                TO_CHAR(STATION_MON_VOLTAGE_RATE) as 合格率,
                TO_CHAR(STATION_MON_VOLTAGE_CSXRATE) as 超上限率,
                TO_CHAR(STATION_MON_VOLTAGE_CXXRATE) as 超下限率,
                TO_CHAR(STATION_MON_RUN_RATE) as 运行合格率,
                TO_CHAR(MAXV) as 最大值,
                MAXVTIME as 最大值时间,
                TO_CHAR(MINV) as 最小值,
                MINVTIME as 最大值时间,
                TO_CHAR(VOLTAGE_AVERAGE) as 平均电压, 
                cdsx as 测点属性
 from {0} b ,(select
  OBJ_ID,STATION_NAME,VOLTAGE_TYPE,MAXV_MINV,cdsx
  ,(select link_id from t_dwsb_tree b where b.obj_id=dept_id) as dept_like
  ,(select link_id from t_dwsb_tree b where b.obj_id=direid) as direid_like
    from t_dy_dycd) u
  where u.OBJ_ID=b.CD_ID  and realyear ||  realmonth  ='{1}' 
  AND u.DEPT_LIKE like '{2}'||'%' {3} order by VOLTAGE_TYPE";//{3} (u.cdsx='城网' or u.cdsx='城农网') 

        #endregion

        private void AddInfo(string info)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                rtxtMsg.AppendText(info + "\r\n");
                rtxtMsg.ScrollToCaret();
                Delay(5);
            }));
        }
        private void Delay(int ms)
        {
            int count = System.Environment.TickCount;
            while (System.Environment.TickCount < ms + count) Application.DoEvents();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dtpBegin.Value.Date > dtpEnd.Value.Date)
            {
                MessageBox.Show("开始时间大于结束时间");
                return;
            }

            if (!string.IsNullOrEmpty(txtPath.Text) && System.IO.Directory.Exists(txtPath.Text) == false)
            {
                MessageBox.Show("保存路径不存在");
                return;
            }

            if (chkAllDept.Checked)//导出全部单位的数据
            {
                DataTable dt = ValidateInput();
                if (dt == null) return;

                foreach (DataRow dr in dt.Rows)
                {
                    Export(dr[cmbDept.ValueMember].ToStringNull(), dr[cmbDept.DisplayMember].ToStringNull().Replace("电业局", "局"));
                    Delay(50);
                }
            }
            else
            {
                if (cmbDept.SelectedValue == null)
                {
                    MessageBox.Show("没有选择单位，请先点击测试连接，以载入单位信息");
                    return;
                }

                Export(cmbDept.SelectedValue.ToString(), cmbDept.Text.Replace("电业局", "局"));
            }
        }

        private DataTable ValidateInput()
        {
            if (cmbDept.Items.Count == 0)
            {
                MessageBox.Show("没有载入单位，请先点击测试连接，以载入单位信息");
                return null;
            }

            DataTable dt = (DataTable)cmbDept.DataSource;
            if (dt == null) return null;

            if (System.IO.Directory.Exists(txtPath.Text.Trim()) == false)
            {
                MessageBox.Show("导出多个单位，请输入存在的路径，以便保存导出的文件");
                return null;
            }
            return dt;
        }

        private Dictionary<string, string> GetMonthTables()
        {
            Dictionary<string, string> dicName_Table = new Dictionary<string, string>();

            switch (cmbTable.Text)
            {
                case "原始":
                    dicName_Table.Add("原始数据", "T_DY_MONTHTRUE");
                    break;
                case "调整":
                    dicName_Table.Add("调整数据", "T_DY_MONTH");
                    break;
                case "原始和调整":
                default:
                    dicName_Table.Add("调整数据", "T_DY_MONTH");
                    dicName_Table.Add("原始数据", "T_DY_MONTHTRUE");
                    break;
            }
            return dicName_Table;
        }

        private string GetCNWCondition()
        {
            string cnw = "";
            switch (cmbCNW.Text)
            {
                case "城网":
                    cnw = " and (u.cdsx='城网' or u.cdsx='城农网') ";
                    break;
                case "农网":
                    cnw = " and (u.cdsx='农网' or u.cdsx='城农网') ";
                    break;
                case "全部":
                default: break;
            }
            return cnw;
        }

        private void Export(string linkId, string deptName)
        {
            if (string.IsNullOrEmpty(linkId))
            {
                AddInfo("linkid为空，终止");
                return;
            }

            AddInfo(string.Format("=======准备导出====[{0}]", deptName));
            Dictionary<string, string> dicCountName_sql = GetNameSqlDic(linkId);

            var dbo = GetDBO(false);
            if (dbo == null) return;

            DataSet ds = new DataSet();
            AddInfo("查询数据……");
            foreach (string name in dicCountName_sql.Keys)
            {
                DataTable dt1 = dbo.GetDataTable(dicCountName_sql[name]);
                dt1.TableName = name;
                ds.Tables.Add(dt1);
            }

            AddInfo("开始导出数据……");
            string fileName = null;
            if (string.IsNullOrEmpty(txtPath.Text.Trim()) == false)
            {
                fileName = string.Format("{0}\\{1}电网电压月数据.xls", txtPath.Text.Trim().TrimEnd('\\'), deptName);
            }

            WinFormExcelExporter.DataSetToExcel(ds, fileName);

            AddInfo("导出完成");
        }

        private Dictionary<string, string> GetNameSqlDic(string linkid)
        {
            Dictionary<string, string> dicName_Table = GetMonthTables();
            //and (u.cdsx='城网' or u.cdsx='城农网')
            string cnw = GetCNWCondition();

            Dictionary<string, string> dicCountName_sql = new Dictionary<string, string>();
            DateTime begin = dtpBegin.Value;
            DateTime end = dtpEnd.Value;
            while (begin.Date <= end.Date)
            {
                foreach (string name in dicName_Table.Keys)
                {
                    string table = dicName_Table[name];

                    string sql = string.Format(_selectFormat, table, begin.ToString("yyyyMM"), linkid, cnw);

                    string key = begin.ToString("yyyyMM") + name;
                    if (dicCountName_sql.ContainsKey(key) == false)
                    {
                        dicCountName_sql.Add(key, sql);
                    }
                }
                begin = begin.AddMonths(1);
            }
            return dicCountName_sql;
        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            var dbo = GetDBO(true);
            if (dbo != null)
            {
                GetDeptInfo();
            }
        }

        private IDBOperator GetDBO(bool showSuc)
        {
            string db = txtDB.Text.Trim();
            string user = txtDBUser.Text.Trim();
            string pwd = txtDBPwd.Text.Trim();

            string connectionString = string.Format("Data Source={0};User ID={1};Password={2}", db, user, pwd);

            var dbo = DBFactory.Create(eDBType.Oracle, connectionString);

            string msg;
            bool isSuc = dbo.TestConnection(out msg);

            if (isSuc == false)
            {
                AddInfo(msg);
                return null;
            }
            else
            {
                if (showSuc) AddInfo(msg);
                return dbo;
            }
        }

        private void btnExcuteSql_Click(object sender, EventArgs e)
        {

        }

        private void GetDeptInfo()
        {
            var dbo = GetDBO(false);
            if (dbo == null) return;

            var dt = dbo.GetDataTable("select obj_name,link_id from t_dwsb_tree where  obj_type='dept' and length(link_id)=10 and link_id<'0000100010'");
            cmbDept.DisplayMember = "obj_name";
            cmbDept.ValueMember = "link_id";
            cmbDept.DataSource = dt;
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            var dr = fbd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtPath.Text = fbd.SelectedPath;
            }
        }

        private void linkClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rtxtMsg.Clear();
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            if (dtpBegin.Value.Date > dtpEnd.Value.Date)
            {
                MessageBox.Show("开始时间大于结束时间");
                return;
            }

            DataSet ds = new DataSet();
            if (chkAllDept.Checked)
            {
                if (cmbDept.Items.Count == 0)
                {
                    MessageBox.Show("没有载入单位，请先点击测试连接，以载入单位信息");
                    return;
                }

                DataTable dt = (DataTable)cmbDept.DataSource;
                if (dt == null) return;

                foreach (DataRow dr in dt.Rows)
                {
                    // or CountHorizontal
                    DataTable t = Count(dr[cmbDept.ValueMember].ToStringNull(), dr[cmbDept.DisplayMember].ToStringNull().Replace("电业局", ""));
                    ds.Tables.Add(t);
                    Delay(10);
                }
            }
            else
            {
                if (cmbDept.SelectedValue == null)
                {
                    AddInfo("未选择单位");
                    return;
                }
                string linkid = cmbDept.SelectedValue.ToString();
                string deptName = cmbDept.Text.Replace("电业局", "");
                DataTable dt = Count(linkid, deptName);//CountHorizontal
                ds.Tables.Add(dt);
            }
            AddInfo("\r\n开始导出……");

            string fileName = null;
            if (!string.IsNullOrEmpty(txtPath.Text.Trim())
                && System.IO.Directory.Exists(txtPath.Text.Trim()))
            {
                if (chkAllDept.Checked) fileName = txtPath.Text.TrimEnd('\\') + "\\" + "月数据统计.xls";
                else fileName = txtPath.Text.TrimEnd('\\') + "\\" + cmbDept.Text + "月数据统计.xls";
            }

            yltl.ExcelHelper.WinFormExcelExporter.DataSetToExcel(ds, fileName);
            AddInfo("导出完成");
        }

        private DataTable Count(string linkid, string deptName)
        {
            AddInfo(string.Format("=====准备统计=====[{0}]", deptName));

            DataTable dt = new DataTable();
            dt.TableName = deptName + "统计";
            dt.Columns.Add(deptName);
            dt.Columns.Add("类型");
            dt.Columns.Add(string.Format("{0}({1})", deptName, "个数"));
            dt.Columns.Add(string.Format("{0}({1})", deptName, "合格率"));
            dt.Columns.Add(string.Format("{0}({1})", deptName, "个数/合格率"));
            dt.Columns.Add(string.Format("{0}({1})", deptName, "合格率/个数"));

            var dic = GetNameSqlDic(linkid);

            List<string> types = new List<string>() { "综合", "A", "B", "C", "D" };
            foreach (string key in dic.Keys)//key 示例:201201调整数据
            {
                string sql = dic[key];

                AddInfo("统计:" + key);
                List<string> hgls = JS_HGL(sql);
                Delay(10);
                List<string> counts = JS_GS(sql);
                Delay(10);
                //201201调整数据，类型 ，个数，合格率
                for (int i = 0; i < hgls.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = key;
                    dr[1] = types[i];
                    dr[2] = counts[i];
                    dr[3] = hgls[i];
                    dr[4] = string.Format("{0}/{1}", counts[i], hgls[i]);
                    dr[5] = string.Format("{0}/{1}", hgls[i], counts[i]);
                    dt.Rows.Add(dr);
                }
                Delay(10);
                DataRow drEmpty = dt.NewRow();
                dt.Rows.Add(drEmpty);
            }
            AddInfo("统计完成");
            return dt;
        }
        private DataTable CountHorizontal(string linkid, string deptName)
        {
            AddInfo(string.Format("=====准备统计=====[{0}]", deptName));

            DataTable dt = new DataTable();
            dt.TableName = deptName + "统计";
            dt.Columns.Add(deptName);
            dt.Columns.Add("综合");
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C");
            dt.Columns.Add("D");

            var dic = GetNameSqlDic(linkid);

            List<string> types = new List<string>() { "个数", "合格率", "个数/合格率", "合格率/个数" };
            foreach (string key in dic.Keys)//key 示例:201201调整数据
            {
                string sql = dic[key];

                AddInfo("统计:" + key);
                List<string> hgls = JS_HGL(sql);
                Delay(10);
                List<string> counts = JS_GS(sql);
                Delay(10);

                DataRow drKey = dt.NewRow();
                drKey[0] = key;
                dt.Rows.Add(drKey);

                string type = types[0];
                AddHRow(dt, null, counts, type);
                type = types[1];
                AddHRow(dt, hgls, null, type);
                type = types[2];
                AddHRow(dt, hgls, counts, type);
                type = types[3];
                AddHRow(dt, counts, hgls, type);

                Delay(10);
                DataRow drEmpty = dt.NewRow();
                dt.Rows.Add(drEmpty);
            }
            AddInfo("统计完成");
            return dt;
        }

        private static void AddHRow(DataTable dt, List<string> hgls, List<string> counts, string type)
        {
            DataRow dr = dt.NewRow();//个数
            dr[0] = type;

            bool hasHgls = hgls != null && hgls.Count > 0;
            bool hasCounts = counts != null && counts.Count > 0;
            string split = hasHgls && hasCounts ? "/" : "";

            int count = hasHgls ? hgls.Count : counts.Count;

            for (int j = 0; j < count; j++)
            {
                dr[j + 1] = string.Format("{0}{1}{2}", hasCounts ? counts[j] : "", split, hasHgls ? hgls[j] : "");
            }

            dt.Rows.Add(dr);
        }


        private string JoinList(List<string> list, string joiner)
        {
            if (list == null || list.Count == 0) return joiner;
            return list.Join(joiner);
        }

        #region 统计月数据

        /// <summary>
        /// 根据sql语句返回 综合、A\B\C\D 类合格率
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="CXSJK">查询数据库</param>
        /// <returns></returns>
        private List<string> JS_HGL(string sql)
        {
            var dbo = GetDBO(false);
            if (dbo == null) return new List<string>();

            string hglA = "0";
            string hglB = "0";
            string hglC = "0";
            string hglD = "0";
            string sqls = "";

            sqls = "select sum(总运行时间) as zs,sum(超高时间) as cg,sum(超低时间) as cd,测点类型 from (" + sql + " ) group by 测点类型 order by 测点类型";


            DataTable dt = dbo.GetDataTable(sqls);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string VOLTAGE_TYPE = dt.Rows[i]["测点类型"].ToString();
                if (VOLTAGE_TYPE == "A")
                    hglA = Station_Mon_Voltage_Rate(dt.Rows[i]["zs"].ToString(), dt.Rows[i]["cg"].ToString(), dt.Rows[i]["cd"].ToString());
                if (VOLTAGE_TYPE == "B")
                    hglB = Station_Mon_Voltage_Rate(dt.Rows[i]["zs"].ToString(), dt.Rows[i]["cg"].ToString(), dt.Rows[i]["cd"].ToString());
                if (VOLTAGE_TYPE == "C")
                    hglC = Station_Mon_Voltage_Rate(dt.Rows[i]["zs"].ToString(), dt.Rows[i]["cg"].ToString(), dt.Rows[i]["cd"].ToString());
                if (VOLTAGE_TYPE == "D")
                    hglD = Station_Mon_Voltage_Rate(dt.Rows[i]["zs"].ToString(), dt.Rows[i]["cg"].ToString(), dt.Rows[i]["cd"].ToString());

            }

            string ZH_HGL = "";
            ZH_HGL = CalculateTotal(hglA, hglB, hglC, hglD);
            if (ZH_HGL == "")
                ZH_HGL = "0";
            //string hgl = "【A类】：" + hglA + " 【B类】：" + hglB + " 【C类】：" + hglC + " 【D类】：" + hglD
            //    + " 【综合合格率】：" + ZH_HGL;
            //return hgl;
            return new List<string>() { ZH_HGL, hglA, hglB, hglC, hglD };
        }

        /// <summary>
        /// 根据sql语句返回 综合、A\B\C\D 类测点数统计
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="CXSJK">查询数据库</param>
        /// <returns></returns>
        private List<string> JS_GS(string sql)
        {
            var dbo = GetDBO(false);
            if (dbo == null) return new List<string>();

            string hglA = "0";
            string hglB = "0";
            string hglC = "0";
            string hglD = "0";
            string sqls = "";

            sqls = "select count(*) as GS,测点类型 from (" + sql + " ) group by 测点类型 order by 测点类型";

            DataTable dt = dbo.GetDataTable(sqls);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string VOLTAGE_TYPE = dt.Rows[i]["测点类型"].ToString();
                if (VOLTAGE_TYPE == "A")
                    hglA = dt.Rows[i]["GS"].ToString();
                if (VOLTAGE_TYPE == "B")
                    hglB = dt.Rows[i]["GS"].ToString();
                if (VOLTAGE_TYPE == "C")
                    hglC = dt.Rows[i]["GS"].ToString();
                if (VOLTAGE_TYPE == "D")
                    hglD = dt.Rows[i]["GS"].ToString();
            }

            //string ZH_HGL = "";
            ////ZH_HGL = CalculateTotal(hglA, hglB, hglC, hglD);
            ////if (ZH_HGL == "")
            ////    ZH_HGL = "0";
            //string hgl = "【A类】：" + hglA + " 【B类】：" + hglB + " 【C类】：" + hglC + " 【D类】：" + hglD;
            //hgl += "【总数】：" + (int.Parse(hglA) + int.Parse(hglB) + int.Parse(hglC) + int.Parse(hglD)).ToString();
            //return hgl;
            string zh = (int.Parse(hglA) + int.Parse(hglB) + int.Parse(hglC) + int.Parse(hglD)).ToString();
            return new List<string>() { zh, hglA, hglB, hglC, hglD };
        }

        /// <summary>
        /// 计算综合合格率
        /// </summary>
        /// 创建者：YCF
        /// 创建时间：20100128
        /// 主要功能：计算综合合格率
        /// 参数说明：各类合格率
        /// 返回值说明：返回综合合格率
        /// <example>
        /// 调用事例：
        /// <code>
        ///   ZH_HGL = CalculateTotal(hglA, hglB, hglC, hglD);
        /// </code>
        /// 返回类型<c>string </c>,返回值：综合合格率
        /// </example>
        /// <param name="strRateA">A类合格率</param>
        /// <param name="strRateB">B类合格率</param>
        /// <param name="strRateC">C类合格率</param>
        /// <param name="strRateD">D类合格率</param>
        /// <returns>返回综合合格率</returns>
        private string CalculateTotal(string strRateA, string strRateB, string strRateC, string strRateD)
        {
            double di = 0, dTotal = 0;
            if (strRateB != "0" && strRateB != "0.000" && strRateB != "-1" && strRateB.Trim() != "")
            {
                di = di + 1;
                dTotal = dTotal + Convert.ToDouble(strRateB);
            }

            if (strRateC != "0" && strRateC != "0.000" && strRateC != "-1" && strRateC.Trim() != "")
            {
                di = di + 1;
                dTotal = dTotal + Convert.ToDouble(strRateC);
            }

            if (strRateD != "0" && strRateD != "0.000" && strRateD != "-1" && strRateD.Trim() != "")
            {
                di = di + 1;
                dTotal = dTotal + Convert.ToDouble(strRateD);
            }

            if ((di > 0) && (dTotal > 0))// BCD  不全为0 
            {
                if (strRateA == "0" || strRateA == "0.000")//A等于0
                {
                    return RetRound2(dTotal / di, 3);
                }
                else
                {
                    return RetRound2(0.5 * ((dTotal / di) + Convert.ToDouble(strRateA)), 3);
                }
            }
            else
            {
                if (strRateA != "0")
                {
                    return strRateA;
                }
                else
                {
                    return "0";
                }
            }
        }

        /// <summary>
        /// 计算时间合格率
        /// 修改记录（可以有多组）
        /// 修改人：ycf
        /// 修改时间：20100323
        /// 修改原因：合格率计算公式变更
        /// 修改内容概述：合格率计算公式改为 hgl =(1-(超高时间+超低时间)/总运行时间)*100% 并且截取小数点要四舍五入
        /// </summary>
        /// <param name="TotalTime">总运行时间</param>
        /// <param name="HighTime">超高时间</param>
        /// <param name="LowTime">超低时间</param>
        /// <returns>时间合格率</returns>
        private string Station_Mon_Voltage_Rate(string TotalTime, string HighTime, string LowTime)
        {
            double run_rate = 0;
            if (TotalTime == "")
                TotalTime = "0";
            try
            {
                if (TotalTime != "0")
                {
                    //修 改 人： ycf
                    //修改时间：20100323
                    //修改原因： 合格率计算公式变更
                    //修改内容概述： 合格率计算公式改为 hgl =(1-(超高时间+超低时间)/总运行时间)*100% 并且截取小数点要四舍五入
                    if (HighTime == "")
                        HighTime = "0";
                    if (LowTime == "")
                        LowTime = "0";

                    run_rate = (1 - (double.Parse(HighTime) + double.Parse(LowTime)) / double.Parse(TotalTime)) * 100;
                    run_rate = Round45(run_rate, 3);

                }
                if (run_rate > 100)
                    run_rate = 100;
                if (run_rate < 0)
                    run_rate = 0;
            }
            catch
            {

            }

            return run_rate.ToString();
        }

        /// <summary>
        /// 保留小数点后多少位四舍五入 
        /// [创 建 人] YCF [创建时间] 2010-03-23
        /// <example>
        /// 调用示例：
        /// <code>
        ///   run_rate = Round45(run_rate, digit_Rate);
        /// </code>
        /// 返回类型<c>double </c>,返回值： 四舍五入后的数字
        /// </example>
        /// </summary>
        /// <param name="d">待处理数字</param>
        /// <param name="i">小数点保留位数</param>
        /// <returns></returns>
        private double Round45(double d, int i)
        {
            try
            {
                if (d >= 0)
                {
                    d += 5 * Math.Pow(10, -(i + 1));
                }
                else
                {
                    d += -5 * Math.Pow(10, -(i + 1));
                }
                string str = d.ToString();
                string[] strs = str.Split('.');
                int idot = str.IndexOf('.');
                string prestr = strs[0];
                string poststr = strs[1];
                if (poststr.Length > i)
                {
                    poststr = str.Substring(idot + 1, i);
                }
                string strd = prestr + "." + poststr;
                d = Double.Parse(strd);
            }
            catch
            {
                d = Math.Round(d, i);
            }
            return d;
        }

        /// <summary>
        /// 保留小数点后多少位只舍不进 用于计算超限率 合格率 [创 建 人] YCF [创建时间] 2009-10-28
        /// <example>
        /// 调用事例：
        /// <code>
        ///  string hgl = RetRound2(96.6589,3);
        /// </code>
        /// 返回类型<c>string </c>,返回值：96.658
        /// </example>
        /// </summary>
        /// <param name="Rate">待处理数字</param>
        /// <param name="i">小数点位数</param>
        /// <returns>处理后的数字</returns>
        private string RetRound2(double Rate, int i)
        {
            if (Rate > 100)
                Rate = 100;
            if (Rate < 0)
                Rate = 0;
            string tmp = Rate.ToString();
            int t = tmp.IndexOf(".");
            if (t > 0 && (tmp.Length - t > i))
                tmp = tmp.Substring(0, tmp.IndexOf(".") + i + 1);

            return tmp;
        }

        /// <summary>
        /// 保留小数点后多少位只舍不进 用于计算超限率 合格率 [创 建 人] YCF [创建时间] 2009-10-28
        /// <example>
        /// 调用事例：
        /// <code>
        ///  string hgl = RetRound(96.6589,3);
        /// </code>
        /// 返回类型<c>string </c>,返回值：96.658
        /// </example>
        /// </summary>
        /// <param name="Rate">待处理数字</param>
        /// <param name="i">小数点位数</param>
        /// <returns>处理后的数字</returns>
        private double RetRound(double Rate, int i)
        {
            if (Rate < 0)
                Rate = 0;
            string tmp = Rate.ToString();
            int t = tmp.IndexOf(".");
            if (t > 0 && (tmp.Length - t > i))
                tmp = tmp.Substring(0, tmp.IndexOf(".") + i + 1);

            return double.Parse(tmp);
        }

        #endregion

        private void chkAllDept_CheckedChanged(object sender, EventArgs e)
        {
            cmbDept.Enabled = !chkAllDept.Checked;
        }
    }
}

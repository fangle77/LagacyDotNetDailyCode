using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            dataGridView1.DataError += dataGridView1_DataError;
            dataGridView2.DataError += dataGridView1_DataError;
            textBox1.KeyDown += textBox1_KeyDown;

            textBox1.Text = "558764,917110,1022979,115174,917138,803583,5994,1,1006731,104671,92938,87030,1023066,917139,917145,1023075,1022789,27055,917140,16646,917141,18633,1008497";
        }

        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Filter_Changed(sender, null);
            }
        }

        private DataTable SourceData = null;
        private HashSet<string> AllAttr = new HashSet<string>();
        HashSet<string> NeedAttr = new HashSet<string>(new string[] { "buyers_month", "buyers_week", "freshness" }, StringComparer.OrdinalIgnoreCase);

        protected override void OnLoad(EventArgs e)
        {
            //var dir = @"\\local-www\ExceptionLog\Diapers\2014\5\19";
            //List<ResultItem> ris = new List<ResultItem>();
            //var dif = new DirectoryInfo(dir);
            //var matcher = new ErrorMatcher();
            //int i = 0;
            //foreach (var file in dif.GetFiles())
            //{
            //    if (i++ > 20) break;
            //    var r = FileContentFinder.Find(file.FullName, matcher);
            //    if (r != null) ris.AddRange(r);
            //}
            //this.Tag = ris;

            Task.Run(() =>
            {
                InitSourceData();
                InitAttr();
                BindData();
            });
        }

        private void InitSourceData()
        {
            if (SourceData != null) return;
            SourceData = new DataTable();
            SourceData.Columns.Add("ProductId", typeof(int));
            SourceData.Columns.Add("Attr", typeof(string));
            SourceData.Columns.Add("Value", typeof(decimal));

            var ss = File.ReadAllLines("product_stats.txt");
            int count = 0;
            HashSet<string> distinct = new HashSet<string>(ss);
            foreach (string s in distinct)
            {
                count++;
                if (count % 100 == 0) UpdateStatistics(count, distinct.Count);
                var ll = s.Split('|');
                if (ll.Length < 3) continue;
                DataRow dr = SourceData.NewRow();
                dr[0] = ll[0];
                dr[1] = ll[1];
                dr[2] = ll[2];
                AllAttr.Add(dr[1].ToString());
                SourceData.Rows.Add(dr);
            }
            UpdateStatistics(count, distinct.Count);
            SourceData.AcceptChanges();
        }

        private void InvokeAction(Action action)
        {
            if (this.InvokeRequired) this.Invoke(new MethodInvoker(action));
            else
            {
                action();
            }
        }

        private DataTable BindData()
        {
            var dv = SourceData.DefaultView;
            dv.RowFilter = GetFilters();
            dv.Sort = "Attr asc";
            var dt = dv.ToTable();
            InvokeAction(() =>
            {
                dataGridView1.ClearSelection();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dv;
                UpdateStatistics(dt.Rows.Count, dv.Table.Rows.Count);
            });
            return dt;
        }

        private void UpdateStatistics(int statistics, int total)
        {
            InvokeAction(() => { label1.Text = statistics.ToString() + "/" + total.ToString(); });
        }

        private void InitAttr()
        {
            InvokeAction(() =>
                             {
                                 foreach (string s in AllAttr)
                                 {
                                     AddCheckBox(s);
                                 }
                             });
        }

        private void AddCheckBox(string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            CheckBox chk = new CheckBox();
            chk.Text = value;
            chk.Checked = NeedAttr.Contains(value);
            chk.Dock = DockStyle.Left;
            chk.CheckedChanged += Filter_Changed;
            panel1.Controls.Add(chk);
        }

        private string GetFilters()
        {
            string result = string.Empty;
            InvokeAction(() =>
            {
                List<string> attrs = new List<string>();
                foreach (var control in panel1.Controls)
                {
                    var chk = control as CheckBox;
                    if (chk == null) continue;
                    if (chk.Checked)
                    {
                        attrs.Add("'" + chk.Text + "'");
                    }
                }
                if (attrs.Count > 0) result = string.Format("Attr in({0})", string.Join(",", attrs));
                if (attrs.Count == panel1.Controls.Count) result = string.Empty;
            });


            if (textBox1.Text.Trim().Length > 0)
            {
                string input = textBox1.Text.Trim();
                var productids = new Regex(@"\D").Split(input).ToList();
                productids.RemoveAll(string.IsNullOrEmpty);
                string pids = string.Join(",", productids);
                if (result.Length > 0 && productids.Count > 0)
                {
                    result = string.Format("ProductId in({0}) and ({1})", pids, result);
                }
                else if (productids.Count > 0)
                {
                    result = string.Format("ProductId in ({0})", pids);
                }
            }

            return result;
        }

        void Filter_Changed(object sender, EventArgs e)
        {
            Task.Run(() => BindData());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
                         {
                             var dt = BindData();
                             Task.Run(() =>
                             {
                                 var bs = CalcBestSelling(dt);
                                 InvokeAction(() =>
                                                  {
                                                      dataGridView2.DataSource = null;
                                                      dataGridView2.DataSource = bs;
                                                  });
                             });
                         });
        }

        private DataTable CalcBestSelling(DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count == 0) return null;

            DataTable bestSelling = new DataTable();
            bestSelling.Columns.Add("ProductId", typeof(int));
            bestSelling.Columns.Add("buyers_month", typeof(decimal));
            bestSelling.Columns.Add("buyers_week", typeof(decimal));
            bestSelling.Columns.Add("freshness", typeof(decimal));

            Dictionary<int, DataRow> dataRows = new Dictionary<int, DataRow>(dataTable.Rows.Count / 3);
            foreach (DataRow dr in dataTable.Rows)
            {
                string attr = dr["Attr"].ToString();
                if (!NeedAttr.Contains(attr)) continue;

                int productid;
                decimal v;
                if (int.TryParse(dr["ProductId"].ToString(), out productid))
                {
                    DataRow tdr;
                    if (dataRows.ContainsKey(productid)) tdr = dataRows[productid];
                    else
                    {
                        tdr = bestSelling.NewRow();
                        dataRows.Add(productid, tdr);
                        bestSelling.Rows.Add(tdr);
                    }
                    tdr["ProductId"] = productid;
                    decimal.TryParse(dr["Value"].ToString(), out v);
                    tdr[attr] = v;
                }
            }

            foreach (DataRow dr in bestSelling.Rows)
            {
                foreach (string attr in NeedAttr)
                {
                    if (dr[attr] == null || dr[attr].ToString().Trim().Length == 0) dr[attr] = 0.0;

                    //decimal v = (decimal)dr[attr];
                    //int b = attr.Equals("freshness", StringComparison.OrdinalIgnoreCase) ? 2000 : 5000;
                    //dr[attr] = v >= b ? 1.0m : (v <= 0 ? 0m : v / b);
                }
            }

            int baseWeight = 10;
            SetTableSortColumn(ref bestSelling, "buyers_month", "buyers_month asc,buyers_week asc,freshness asc", baseWeight * baseWeight * baseWeight * 8d);
            SetTableSortColumn(ref bestSelling, "buyers_week", "buyers_week asc,buyers_month asc,freshness asc", baseWeight * baseWeight * 1d);
            SetTableSortColumn(ref bestSelling, "freshness", "freshness asc,buyers_month asc,buyers_week asc", baseWeight * 1d);

            bestSelling.Columns.Add("Rank", typeof(double), "0.8*buyers_month_Rank + 0.1*buyers_week_Rank + 0.1*freshness_Rank");
            bestSelling.AcceptChanges();
            var dv = bestSelling.DefaultView;
            dv.Sort = "Rank desc";
            bestSelling = dv.ToTable();
            return bestSelling;
        }

        private void SetTableSortColumn(ref DataTable bestSelling, string column, string sort, double weight)
        {
            var dvMonth = bestSelling.DefaultView;
            dvMonth.Sort = sort;
            bestSelling = dvMonth.ToTable();

            string columnName = column + "_Rank";
            bestSelling.Columns.Add(columnName, typeof(double));
            int i = 1;
            foreach (DataRow dr in bestSelling.Rows)
            {
                dr[columnName] = weight * i++;
            }
            bestSelling.AcceptChanges();
        }
    }
}

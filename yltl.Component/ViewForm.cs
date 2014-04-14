using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace yltl.UIComponent
{
    public partial class ViewForm : Form
    {
        public ViewForm()
        {
            InitializeComponent();
            InitGrid(dgvData);
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        #region DataGridView 相关
        /// <summary>
        /// 获取选中与未选中的值
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="checkboxColumnIndex"></param>
        /// <param name="trueValue"></param>
        /// <param name="falseValue"></param>
        /// <returns></returns>
        protected bool GetTrueAndFalseValue(DataGridView dgv, int checkboxColumnIndex, out string trueValue, out string falseValue)
        {
            trueValue = "Y";
            falseValue = "N";
            var chkCol = dgv.Columns[checkboxColumnIndex] as DataGridViewCheckBoxColumn;
            if (chkCol == null) return false;
            trueValue = chkCol.TrueValue.ToString();
            falseValue = chkCol.FalseValue.ToString();
            return true;
        }

        /// <summary>
        /// 获取复选框列的列索引
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        protected int GetCheckBoxColumnIndex(DataGridView dgv)
        {
            int index = -1;
            foreach (DataGridViewColumn dc in dgv.Columns)
            {
                if ((dc as DataGridViewCheckBoxColumn) == null) continue;
                index = dc.Index;
                break;
            }
            return index;
        }

        /// <summary>
        /// 单击列头事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            var dgv = sender as DataGridView;
            if (dgv == null) return;

            int index = e.ColumnIndex;

            string trueValue;
            string falseValue;
            if (GetTrueAndFalseValue(dgv, index, out trueValue, out falseValue) == false) return;

            if (dgv.CurrentCell != null)
            {//该段解决在点击全选时，被选中的复选框勾不会及时更新的问题
                if (dgv.CurrentCell.ColumnIndex == index)
                {
                    int rowindex = dgv.CurrentCell.RowIndex;
                    for (int i = index + 1; i < dgv.Columns.Count; i++)
                    {
                        if (dgv.Columns[i].Visible)
                        {
                            dgv.CurrentCell = dgv.Rows[rowindex].Cells[i];
                            break;
                        }
                    }
                }
            }

            //第一遍，先判断是否全选
            bool isSelectedAll = true;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var value = row.Cells[index].Value;
                if (value == null || value.ToString() == falseValue)
                {
                    isSelectedAll = false; break;//只要找到一个未选择的则认为没有全选
                }
            }

            //第二遍，将状态设置为 当前相反的状态
            string selected = isSelectedAll ? falseValue : trueValue;
            foreach (DataGridViewRow r in dgv.Rows)
            {
                var v = r.Cells[index];
                if (v == null) continue;
                if (v.Value == null || v.Value.ToString() != selected)
                    v.Value = selected;
            }
        }

        /// <summary>
        /// 列表行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv == null) return;
            using (SolidBrush b = new SolidBrush(dgv.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b,
                    e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 5);
            }
        }

        /// <summary>
        /// 获取选中的行
        /// </summary>
        /// <returns></returns>
        protected List<DataGridViewRow> GetSelectedRows()
        {
            return GetSelectedRows(dgvData);
        }
        /// <summary>
        /// 获取选择行
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        private List<DataGridViewRow> GetSelectedRows(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0) return null;
            int index = GetCheckBoxColumnIndex(dgv);
            if (index < 0) return null;

            string trueValue;
            string falseValue;
            GetTrueAndFalseValue(dgv, index, out trueValue, out falseValue);

            var rowList = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                var check = row.Cells[index].Value;
                if (check != null && check.ToString() == trueValue)
                    rowList.Add(row);
            }

            return rowList;
        }

        /// <summary>
        /// 设置列表样式
        /// </summary>
        /// <param name="dgv"></param>
        protected void SetGridStyle(DataGridView dgv)
        {
            if (dgv == null) return;
            Color bColor = Color.FromArgb(191, 210, 251);// Color.FromArgb(216, 228, 248);
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = bColor;
            //Font ft = new Font("宋体", 9, FontStyle.Bold);
            //dgv.ColumnHeadersDefaultCellStyle.Font = ft;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.Highlight;

            dgv.RowHeadersDefaultCellStyle.BackColor = bColor;

            dgv.AutoGenerateColumns = false;

            dgv.BackgroundColor = System.Drawing.Color.White;
        }

        /// <summary>
        /// 初始化DataGridView，包括样式、ColumnHeaderMouseClick、RowPostPaint事件
        /// </summary>
        /// <param name="dgv"></param>
        protected void InitGrid(DataGridView dgv)
        {

            SetGridStyle(dgv);
            dgv.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dgv_ColumnHeaderMouseClick);
            dgv.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dgv_RowPostPaint);
        }

        private Dictionary<string, string> _colDataAndName_ColHeaderText_Pair;
        /// <summary>
        /// 列的键值对：列对应的字段名-列标题，如果某列不显示则列标题为空。
        /// </summary>
        public Dictionary<string, string> ColDataAndName_ColHeaderText_Pair
        {
            get { return _colDataAndName_ColHeaderText_Pair; }
            set
            {
                _colDataAndName_ColHeaderText_Pair = value;
                AddColumns();
            }
        }

        /// <summary>
        /// 添加列
        /// </summary>
        private void AddColumns()
        {
            DataGridViewColumn[] dgc = BuildGridColumns();
            if (dgc == null) return;
            dgvData.Columns.Clear();
            dgvData.Columns.AddRange(BuildGridColumns());
        }

        /// <summary>
        /// 构建表格列，需要设置的属性：ColDataAndName_ColHeaderText_Pair
        /// </summary>
        /// <returns></returns>
        private DataGridViewColumn[] BuildGridColumns()
        {
            if (ColDataAndName_ColHeaderText_Pair == null) return null;

            int colCount = ColDataAndName_ColHeaderText_Pair.Count;
            if (colCount == 0) return null;

            DataGridViewColumn[] dgcc = new DataGridViewColumn[colCount + 1];

            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            chkCol.TrueValue = "Y";
            chkCol.FalseValue = "N";
            chkCol.HeaderText = "";
            chkCol.Name = checkboxColumnName;
            chkCol.Width = 30;
            chkCol.Frozen = true;
            chkCol.ReadOnly = false;
            chkCol.Resizable = DataGridViewTriState.False;
            dgcc[0] = chkCol;

            chkCol.Visible = ShowCheckBoxColumn;

            int col = 1;
            foreach (string key in ColDataAndName_ColHeaderText_Pair.Keys)
            {
                DataGridViewTextBoxColumn dgvTbc = new DataGridViewTextBoxColumn();
                dgvTbc.Name = key;
                dgvTbc.DataPropertyName = key;
                string headerText = ColDataAndName_ColHeaderText_Pair[key];
                if (string.IsNullOrEmpty(headerText))
                {
                    dgvTbc.Visible = false;
                }
                dgvTbc.HeaderText = headerText;
                dgvTbc.ReadOnly = true;
                //dgvTbc.Width = 110;
                dgcc[col] = dgvTbc;
                col++;
            }
            if (KeyColumnName == null)
            {
                KeyColumnName = ColDataAndName_ColHeaderText_Pair.Keys.First();
            }
            return dgcc;
        }

        private string checkboxColumnName = "selectCol";
        /// <summary>
        /// 获取或者设置复选框列的列名
        /// </summary>
        protected string CheckBoxColumnName
        {
            get { return checkboxColumnName; }
            set { checkboxColumnName = value; }
        }

        private bool _showCheckBoxColumn = true;
        /// <summary>
        /// 获取或设置一个值，该值指示是否显示复选框列。
        /// </summary>
        protected bool ShowCheckBoxColumn
        {
            get { return _showCheckBoxColumn; }
            set
            {
                _showCheckBoxColumn = value;
                if (dgvData.Columns[checkboxColumnName] != null)
                {
                    dgvData.Columns[checkboxColumnName].Visible = value;
                }
                //this.导出选择ToolStripMenuItem.Visible = value;
            }
        }

        /// <summary>
        /// DataGridView的主键列名
        /// </summary>
        protected string KeyColumnName { get; set; }

        #endregion

        #region 按钮动作

        protected Action OnSearch { get; set; }
        protected Action OnAdd { get; set; }
        protected Action OnUpdate { get; set; }
        protected Action OnDelete { get; set; }
        protected Func<bool> OnBeforeClose { get; set; }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearch != null) OnSearch();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (OnAdd != null)
            {
                OnAdd();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (OnUpdate != null)
            {
                OnUpdate();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OnDelete != null)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            bool canExit = true;
            if (OnBeforeClose != null) canExit = OnBeforeClose();
            if (canExit) this.Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnExportAll_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

namespace yltl.Common
{
    /// <summary>
    /// DataGridView的扩展方法
    /// </summary>
    public static class DataGridViewExtendFunc
    {
        /// <summary>
        /// datagridViewRow转换为对应类型的List,需满足类型T的字段名是DatagridView的PropertyName属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static List<T> GridToList<T>(this List<System.Windows.Forms.DataGridViewRow> drs) where T : class, new()
        {
            if (drs == null || drs.Count == 0) return null;
            var dgv = drs[0].DataGridView;
            if (dgv == null) return null;

            BindingFlags flag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetField
                | BindingFlags.SetProperty;

            Type t = typeof(T);

            PropertyInfo[] pis = t.GetProperties(flag);

            //查找出属性名与 DataPropertyName 相等的 对应的列的索引
            List<string> piNames = new List<string>();
            foreach (var pi in pis)
            {
                if (pi.CanWrite)
                    piNames.Add(pi.Name.ToUpper());
            }

            Dictionary<string, int> col_indexDic = new Dictionary<string, int>();
            foreach (System.Windows.Forms.DataGridViewColumn col in dgv.Columns)
            {
                string proName = col.DataPropertyName.ToUpper();
                if (piNames.Contains(proName) && col_indexDic.ContainsKey(proName) == false)
                {
                    col_indexDic.Add(proName, col.Index);
                }
            }

            List<T> list = new List<T>();
            foreach (System.Windows.Forms.DataGridViewRow row in drs)
            {
                bool isAllCellNull = true;//全部单元格都为空，则不添加该行

                T instance = new T();
                foreach (var pi in pis)
                {
                    string name = pi.Name.ToUpper();
                    if (col_indexDic.ContainsKey(name))
                    {
                        object value = row.Cells[col_indexDic[name]].Value;
                        if (value != null && (value is DBNull) == false) { isAllCellNull = false; }
                        else continue;
                        DataTableExtendFunc.SetPropertyValue<T>(instance, pi, value);
                    }
                }
                if (isAllCellNull == false)
                    list.Add(instance);
            }
            return list;
        }

        /// <summary>
        /// 将一个DataGridViewRow转换为对应实体类，需满足类型T的字段名是DatagridView的PropertyName属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T ToModel<T>(this System.Windows.Forms.DataGridViewRow dr) where T : class, new()
        {
            if (dr == null) return null;
            var list = new List<System.Windows.Forms.DataGridViewRow>() { dr }.GridToList<T>();
            if (list == null || list.Count == 0) return null;
            return list[0];
        }

        /// <summary>
        /// datagridView导出到List,需满足类型T的字段名是DatagridView的PropertyName属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static List<T> GridToList<T>(this System.Windows.Forms.DataGridView dgv) where T : class, new()
        {
            if (dgv == null) return null;
            var list = new List<System.Windows.Forms.DataGridViewRow>();
            foreach (System.Windows.Forms.DataGridViewRow dr in dgv.Rows)
            {
                list.Add(dr);
            }
            return list.GridToList<T>();
        }

        /// <summary>
        /// 将Datatable添加到datagridView,以一行一行的添加，而非直接绑定
        /// ：需满足Datatable列名是datagridView的DataPropertyName属性值
        /// </summary>
        /// <param name="dt">要添加的DataTable</param>
        /// <param name="dgv"></param>
        public static void BindTableAsItems(this System.Windows.Forms.DataGridView dgv, DataTable dt)
        {
            if (dgv == null) return;
            dgv.Rows.Clear();
            if (dt == null || dt.Rows.Count == 0) return;

            Dictionary<string, int> col_dgvIndexDic = new Dictionary<string, int>();
            foreach (DataColumn dc in dt.Columns)
            {
                string dcname = dc.ColumnName.ToUpper();
                foreach (System.Windows.Forms.DataGridViewColumn dgvc in dgv.Columns)
                {
                    string name = dgvc.DataPropertyName.ToUpper();
                    if (name == dcname)
                    {
                        if (col_dgvIndexDic.Keys.Contains(dc.ColumnName) == false)
                        {
                            col_dgvIndexDic.Add(dc.ColumnName, dgvc.Index);
                        }
                    }
                }
            }

            int index = dgv.Rows.Add();

            foreach (DataRow dr in dt.Rows)
            {
                int i = dgv.Rows.AddCopy(index);

                foreach (string key in col_dgvIndexDic.Keys)
                {
                    dgv.Rows[i].Cells[col_dgvIndexDic[key]].Value = dr[key];
                }
            }
            dgv.Rows.RemoveAt(index);
        }
    }
}

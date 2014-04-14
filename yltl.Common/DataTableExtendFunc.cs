using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Reflection;

namespace yltl.Common
{
    /// <summary>
    /// DataTable扩展方法
    /// </summary>
    public static class DataTableExtendFunc
    {
        /// <summary>
        /// 合并两个DataTable，依据两个列的值相等进行关联
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2">该DataTable被合并</param>
        /// <param name="dt1Column">合并DataTable用于关联的列名</param>
        /// <param name="dt2Column">被合并Datatable的用于关联的列名</param>
        /// <param name="coverTable1">如果列名相同，后面的是否覆盖前面的值,true：覆盖</param>
        public static void InnerJoin(this DataTable dt1, DataTable dt2, string dt1Column, string dt2Column, bool coverTable1)
        {
            if (dt1 == null) return;
            if (dt2 == null) return;
            List<string> dt1InitColumns = new List<string>();
            foreach (DataColumn dc in dt2.Columns)
            {
                if (dt1.Columns[dc.ColumnName] != null)
                {
                    dt1InitColumns.Add(dc.ColumnName);
                    continue;
                }
                dt1.Columns.Add(dc.ColumnName);
            }

            if (dt1.Rows.Count == 0 || dt2.Rows.Count == 0) return;
            if (string.IsNullOrEmpty(dt1Column) || string.IsNullOrEmpty(dt2Column)) return;
            if (dt1.Columns[dt1Column] == null) return;
            if (dt2.Columns[dt2Column] == null) return;

            var toRemoveRows = new List<DataRow>();
            foreach (DataRow dr in dt1.Rows)
            {
                var value = dr[dt1Column];
                if (value == null) continue;
                DataRow[] drs = dt2.Select(string.Format("{0}='{1}'", dt2Column, value));
                if (drs == null || drs.Length == 0)
                {
                    toRemoveRows.Add(dr);
                    continue;
                }

                var dt2Row = drs[0];
                for (int i = 0; i < dt2.Columns.Count; i++)
                {
                    string name = dt2.Columns[i].ColumnName;
                    if (!coverTable1 && dt1InitColumns.Contains(name)) continue;
                    dr[dt2.Columns[i].ColumnName] = dt2Row[dt2.Columns[i].ColumnName];
                }
            }

            if (toRemoveRows.Count > 0)
            {
                foreach (DataRow r in toRemoveRows)
                {
                    dt1.Rows.Remove(r);
                }
            }
        }

        /// <summary>
        /// 将一个DataTable转换为List
        /// </summary>
        /// <typeparam name="T">转换的类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            if (dt == null || dt.Columns.Count == 0) return null;

            if (dt.Rows.Count == 0) return new List<T>();

            List<T> list = new List<T>(dt.Rows.Count);

            BindingFlags flag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetField
               | BindingFlags.SetProperty;

            Type t = typeof(T);

            PropertyInfo[] pis = t.GetProperties(flag);
            List<PropertyInfo> proInTable = new List<PropertyInfo>(pis.Length);
            foreach (var pi in pis)
            {
                if (dt.Columns[pi.Name] == null) continue;
                if(pi.CanWrite==false) continue;
                proInTable.Add(pi);
            }

            foreach (DataRow dr in dt.Rows)
            {
                T instance = new T();

                foreach (PropertyInfo pi in proInTable)
                {
                    //var value = dr[pi.Name];
                    //if (value == null || value is DBNull) continue;
                    pi.SetValue(instance, dr[pi.Name], null);
                    //SetPropertyValue<T>(instance, pi, value);
                }

                list.Add(instance);
            }

            return list;
        }

        /// <summary>
        /// 利用反射设置属性的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="pi"></param>
        /// <param name="value"></param>
        internal static void SetPropertyValue<T>(T instance, PropertyInfo pi, object v) where T : class, new()
        {
            string value = v.ToString();
            //if (string.IsNullOrEmpty(value)) return;
            var datatype = pi.PropertyType.ToString().ToLower();
            //if (datatype.IndexOf("system.nullable") >= 0)
            //{
            //    //system.nullable`1[system.int32]
            //    int index = datatype.IndexOf('[');
            //    datatype = datatype.Substring(index + 1, datatype.Length - index - 2);
            //    //var ty = pi.PropertyType.GetGenericArguments();
            //    //datatype = ty[0].ToString().ToLower();
            //}
            SetPropertyValue<T>(instance, pi, value, datatype);
        }

        private static void SetPropertyValue<T>(T instance, PropertyInfo pi, string value, string datatype) where T : class, new()
        {
            switch (datatype)
            {
                case "system.string":
                    pi.SetValue(instance, value, null);
                    break;
                case "system.int32":
                case "system.int":
                    int i = 0;
                    int.TryParse(value, out i);
                    pi.SetValue(instance, i, null);
                    break;
                case "system.int64":
                    long l = 0;
                    long.TryParse(value, out l);
                    pi.SetValue(instance, l, null);
                    break;
                case "system.double":
                    double d = 0.0;
                    double.TryParse(value, out d);
                    pi.SetValue(instance, d, null);
                    break;
                case "system.decimal":
                    Decimal dc = default(decimal);
                    Decimal.TryParse(value, out dc);
                    pi.SetValue(instance, dc, null);
                    break;
                case "system.datetime":
                    DateTime dt = default(DateTime);
                    DateTime.TryParse(value, out dt);
                    pi.SetValue(instance, dt, null);
                    break;
                case "system.boolean":
                    bool b = default(bool);
                    Boolean.TryParse(value, out b);
                    pi.SetValue(instance, b, null);
                    break;
                case "system.dbnull":
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 查询数据并返回结果
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="col_valueDic"></param>
        /// <returns></returns>
        public static DataRow[] Select(this DataTable dt, Dictionary<string, string> col_valueDic)
        {
            string condition = BuildSelectExpress(dt, col_valueDic);
            if (string.IsNullOrEmpty(condition)) return null;

            return dt.Select(condition);
        }

        /// <summary>
        /// 构造该DataTable的查询条件表达式
        /// </summary>
        /// <param name="dt">要查询的表，主要用于检查列名是否存在</param>
        /// <param name="col_valueDic">列-值对，如果值为'not null'则查询非空</param>
        /// <returns></returns>
        public static string BuildSelectExpress(this DataTable dt, Dictionary<string, string> col_valueDic)
        {
            if (dt == null) return null;
            if (col_valueDic == null || col_valueDic.Count == 0) return null;

            string condition = string.Empty;
            foreach (string col in col_valueDic.Keys)
            {
                if (dt.Columns[col] == null) continue;
                if (col_valueDic[col] == "not null")
                {
                    condition += string.Format(" and ([{0}] is not null or [{0}]<>'')", col);
                }
                else
                {
                    condition += string.Format(" and [{0}]='{1}'", col, col_valueDic[col].Replace("'", "''"));
                }
            }
            if (!string.IsNullOrEmpty(condition))
            {
                condition = condition.Substring(4);
            }

            return condition;
        }

        /// <summary>
        /// 查找指定列的不重复数据,并返回不重复数据行的List,如果未指定列名则默认比较全部列
        /// </summary>
        /// <param name="SourceTable"></param>
        /// <param name="FieldNames"></param>
        /// <returns></returns>
        public static List<DataRow> SelectDistinct(this DataTable sourceTable, params string[] fieldNames)
        {
            object[] lastValues;
            DataTable newTable;
            DataRow[] orderedRows;

            if (fieldNames == null || fieldNames.Length == 0)
            {
                //throw new ArgumentNullException("FieldNames");
                if (fieldNames == null) fieldNames = new string[sourceTable.Columns.Count];
                for (int i = 0; i < sourceTable.Columns.Count; i++)
                {
                    fieldNames[i] = sourceTable.Columns[i].ColumnName;
                }
            }

            lastValues = new object[fieldNames.Length];
            newTable = new DataTable();

            foreach (string fieldName in fieldNames)
                newTable.Columns.Add(fieldName, sourceTable.Columns[fieldName].DataType);

            orderedRows = sourceTable.Select("", string.Join(", ", fieldNames));

            List<DataRow> list = new List<DataRow>();
            foreach (DataRow row in orderedRows)
            {
                if (!FieldValuesAreEqual(lastValues, row, fieldNames))
                {
                    //newTable.Rows.Add(createRowClone(row, newTable.NewRow(), FieldNames));
                    list.Add(row);
                    SetLastValues(lastValues, row, fieldNames);
                }
            }

            return list;
        }

        /// <summary>
        /// 比较指定的列值是否相等
        /// </summary>
        /// <param name="lastValues"></param>
        /// <param name="currentRow"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        private static bool FieldValuesAreEqual(object[] lastValues, DataRow currentRow, string[] fieldNames)
        {
            bool areEqual = true;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (lastValues[i] == null || !lastValues[i].Equals(currentRow[fieldNames[i]]))
                {
                    areEqual = false;
                    break;
                }
            }

            return areEqual;
        }

        /// <summary>
        /// 克隆行数据为新的DataRow
        /// </summary>
        /// <param name="sourceRow"></param>
        /// <param name="newRow"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        private static DataRow CreateRowClone(DataRow sourceRow, DataRow newRow, string[] fieldNames)
        {
            foreach (string field in fieldNames)
                newRow[field] = sourceRow[field];

            return newRow;
        }

        private static void SetLastValues(object[] lastValues, DataRow sourceRow, string[] fieldNames)
        {
            for (int i = 0; i < fieldNames.Length; i++)
                lastValues[i] = sourceRow[fieldNames[i]];
        }
    }
}

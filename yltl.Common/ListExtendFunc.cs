using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Reflection;

namespace yltl.Common
{
    /// <summary>
    /// List扩展方法
    /// </summary>
    public static class ListExtendFunc
    {
        /// <summary>
        /// 转换为属性名为列，的datatable集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataTable ToTableStructure<T>() where T : class
        {
            Type t = typeof(T);

            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance;

            PropertyInfo[] pis = t.GetProperties(flag);

            DataTable dt = new DataTable();

            foreach (var pi in pis)
            {
                if (pi.PropertyType.ToString().ToLower().IndexOf("system.nullable") >= 0)
                {
                    var ty = pi.PropertyType.GetGenericArguments();
                    if (ty != null && ty.Length > 0)
                    {
                        dt.Columns.Add(pi.Name, ty[0]);
                    }
                }
                else if (pi.PropertyType == typeof(DBNull))
                {
                    dt.Columns.Add(pi.Name, typeof(object));
                }
                else
                {
                    dt.Columns.Add(pi.Name, pi.PropertyType);
                }
            }
            dt.AcceptChanges();
            return dt;
        }

        /// <summary>
        /// 将一个List转换为Datatable
        /// </summary>
        /// <typeparam name="T">转换的类型</typeparam>
        /// <param name="list">List</param>
        /// <returns></returns>
        public static DataTable ToTable<T>(this List<T> list) where T : class
        {
            if (list == null) return null;

            Type t = typeof(T);

            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo[] pis = t.GetProperties(flag);
            DataTable dt = ToTableStructure<T>();
            dt.TableName = t.Name;

            foreach (T l in list)
            {
                DataRow dr = dt.NewRow();

                foreach (var pi in pis)
                {
                    string name = pi.Name;
                    object value = pi.GetValue(l, null);

                    dr[name] = value;
                }

                dt.Rows.Add(dr);
            }

            dt.AcceptChanges();

            return dt;
        }
    }
}
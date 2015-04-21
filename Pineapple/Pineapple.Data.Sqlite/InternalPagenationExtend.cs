using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Pineapple.Model;
using Dapper;

namespace Pineapple.Data.Sqlite
{
    internal static class InternalPagenationExtend
    {
        public static List<T> PageQuery<T>(this IDbConnection cnn, Pagination page, string sql, object parameter = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select count(*) from ({0})", sql);
            int total = cnn.Query<int>(sb.ToString(), parameter).FirstOrDefault();
            if (total == 0)
            {
                return new List<T>();
            }

            page.TotalRecords = total;

            sb.Clear();
            sb.AppendFormat("{0} limit {1},{2}", sql, page.Offset, page.SizePerPage);
            return cnn.Query<T>(sb.ToString(), parameter).ToList();
        }
    }
}

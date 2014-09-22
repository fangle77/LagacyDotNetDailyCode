using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace Pineapple.Data.Sqlite
{
    public static class SqliteDapperExtention
    {
        public static string GetSqliteInsertSql(this object model, params string[] ignoreFields)
        {
            StringBuilder sql = new StringBuilder(model.GetInsertSql(ignoreFields));
            sql.Append("SELECT last_insert_rowid();");
            return sql.ToString();
        }
    }
}

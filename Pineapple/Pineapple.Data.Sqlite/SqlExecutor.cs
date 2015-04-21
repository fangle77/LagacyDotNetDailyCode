using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Dapper;

namespace Pineapple.Data.Sqlite
{
    public class SqlExecutor : ISqlExecutor
    {
        public bool Execute(string connectionString, string sql)
        {
            using (var cnn = new SQLiteConnection(connectionString))
            {
                cnn.Open();
                return cnn.Execute(sql) > 0;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Dapper;
using log4net;

namespace Pineapple.Data.Sqlite
{
    public class ActionDb
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ActionDb));

        private static string DbFile
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\App_Data\\pineapple.action.db"; }
        }

        internal static SQLiteConnection DbConnection()
        {
            var cnn = new SQLiteConnection(string.Format("Data Source=\"{0}\";Pooling=True;", DbFile));
            cnn.Open();
            return cnn;
        }
    }
}

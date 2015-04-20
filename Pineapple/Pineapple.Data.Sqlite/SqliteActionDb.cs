using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Dapper;
using log4net;

namespace Pineapple.Data.Sqlite
{
    public class SqliteActionDb
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SqliteActionDb));

        private static string DbFile
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\App_Data\\action.sqlite"; }
        }

        internal static SQLiteConnection DbConnection()
        {
            var cnn = new SQLiteConnection(string.Format("Data Source=\"{0}\";Pooling=True;", DbFile));
            cnn.Open();
            return cnn;
        }

        internal static void CreateTables()
        {
            using (var cnn = DbConnection())
            {
                cnn.Execute(@"Create table if not exists VisitLog
 (
     VisitorId     			TEXT,
	 VisitTime				TEXT,
	 ClientIp				TEXT,
	 UserAgent				TEXT,
	 SessionId				TEXT
 )");

                cnn.Execute(@"Create table if not exists Visitor
 (
     VisitorId     			TEXT PRIMARY KEY,
     FirstVisitTime    		TEXT,
	 EnterUrl				TEXT,
	 RefererUrl				TEXT,
	 UserName				TEXT
 )");
            }
        }
    }
}

using System;
using System.Data.SQLite;
using System.IO;
using Dapper;
using log4net;

namespace Pineapple.Data.Sqlite
{
    internal class DataDb
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DataDb));

        private static string DbFile
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\App_Data\\pineapple.data.db"; }
        }

        internal static SQLiteConnection DbConnection()
        {
            var cnn = new SQLiteConnection(string.Format("Data Source=\"{0}\";Pooling=True;", DbFile));
            cnn.Open();
            return cnn;
        }
    }
}
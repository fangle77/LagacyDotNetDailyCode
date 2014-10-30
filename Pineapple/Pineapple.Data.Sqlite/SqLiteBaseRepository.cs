using System;
using System.Data.SQLite;
using System.IO;
using Dapper;

namespace Pineapple.Data.Sqlite
{
    internal class SqLiteBaseRepository
    {
        private static string DbFile
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\App_Data\\Pineapple.sqlite"; }
        }

        internal static SQLiteConnection DbConnection()
        {
            var cnn = new SQLiteConnection(string.Format("Data Source={0};Pooling=True;", DbFile));
            cnn.Open();
            return cnn;
        }

        internal static SQLiteConnection DbReadOnlyConnection()
        {
            var cnn = new SQLiteConnection(string.Format("Data Source={0};Pooling=True;", DbFile));
            cnn.Open();
            return cnn;
        }

        internal static void InitDataBase()
        {
            if (!File.Exists(DbFile))
            {
                CreateDatabase();
            }
        }

        private static void CreateDatabase()
        {
            using (var cnn = DbConnection())
            {
                cnn.Execute(
                    @"create table Visitor
              (
                 ID                                  integer primary key AUTOINCREMENT,
                 VisitDate                           varchar(30) not null
              )");
            }

            SqliteTables st = new SqliteTables();
            st.CreateTables();
        }
    }
}
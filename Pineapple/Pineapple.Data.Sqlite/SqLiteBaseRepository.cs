using System.Data.SQLite;
using System.IO;
using Dapper;

namespace Pineapple.Data.Sqlite
{
    public class SqLiteBaseRepository
    {
        private static string DbFile
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\App_Data\\Pineapple.sqlite"; }
        }

        internal static SQLiteConnection DbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
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
                cnn.Open();
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
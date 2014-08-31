using System.Data.SQLite;
using System.IO;
using System.Web;
using Dapper;

namespace Data
{
    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return HttpRuntime.AppDomainAppPath.TrimEnd('\\') + "\\App_Data\\SimpleDb.sqlite"; }
        }

        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }


        public static void InitDataBase()
        {
            if (!File.Exists(DbFile))
            {
                CreateDatabase();
            }
        }

        private static void CreateDatabase()
        {
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                cnn.Execute(
                    @"create table Visitor
              (
                 ID                                  integer primary key AUTOINCREMENT,
                 VisitDate                           varchar(30) not null
              )");
            }
        }
    }
}
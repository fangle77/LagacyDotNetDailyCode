using System.Collections.Generic;
using System.Linq;
using Dapper;
using Pineapple.Core.Cache;
using Pineapple.Model;

namespace Pineapple.Data.Sqlite
{
    public class VisitorData : IVisitorData
    {
        public Visitor AddVisitor(Visitor visitor)
        {
            using (var cnn = SqliteActionDb.DbConnection())
            {
                cnn.Execute(visitor.GetSqliteInsertSql(null), visitor);
                return visitor;
            }
        }

        public Visitor GetVisitor(string visitorId)
        {
            using (var cnn = SqliteActionDb.DbConnection())
            {
                return cnn.Query<Visitor>(typeof(Visitor).GetSelectSql("VisitorId=@VisitorId"), new { VisitorId = visitorId }).FirstOrDefault();
            }
        }

        public VisitLog AddVisitLog(VisitLog visiLog)
        {
            using (var cnn = SqliteActionDb.DbConnection())
            {
                visiLog.GetSqliteInsertSql(null);
                return visiLog;
            }
        }

        public List<Visitor> LoadVisitors()
        {
            using (var cnn = SqliteActionDb.DbConnection())
            {
                return cnn.Query<Visitor>("select * from visitor order by FirstVisitTime desc").ToList();
            }
        }
    }
}

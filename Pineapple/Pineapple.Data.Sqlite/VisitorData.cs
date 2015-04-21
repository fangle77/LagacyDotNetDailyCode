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
            using (var cnn = ActionDb.DbConnection())
            {
                cnn.Execute(visitor.GetSqliteInsertSql(null), visitor);
                return visitor;
            }
        }

        public Visitor GetVisitor(string visitorId)
        {
            using (var cnn = ActionDb.DbConnection())
            {
                return cnn.Query<Visitor>(typeof(Visitor).GetSelectSql("VisitorId=@VisitorId"), new { VisitorId = visitorId }).FirstOrDefault();
            }
        }

        public VisitLog AddVisitLog(VisitLog visiLog)
        {
            using (var cnn = ActionDb.DbConnection())
            {
                visiLog.GetSqliteInsertSql(null);
                return visiLog;
            }
        }

        public List<VisitLog> LoadVisitorLogs()
        {
            using (var cnn = ActionDb.DbConnection())
            {
                return cnn.Query<VisitLog>("select * from VisitLog order by VisitTimeInMs desc").ToList();
            }
        }
    }
}

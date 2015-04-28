using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Pineapple.Model;
using Dapper;

namespace Pineapple.Data.Sqlite
{
    public class CasesData : ICasesData
    {
        private DbConnection Db
        {
            get { return DataDb.DbConnection(); }
        }

        private readonly string[] casesIngnoreFields = new string[] { "CaseItems" };
        private readonly string[] casesItemIngnoreFields = new string[] { "Attachment" };
        private readonly string caseKeyField = "CaseId";
        private readonly string caseItemKeyField = "CaseItemId";

        public Cases GetCase(int caseId)
        {
            using (var db = Db)
            {
                return db.Query<Cases>(typeof(Cases).GetSelectSql("CaseId=" + caseId, casesIngnoreFields)).FirstOrDefault();
            }
        }

        public List<Cases> LoadCases(Model.Pagination pagination)
        {
            using (var db = Db)
            {
                return db.PageQuery<Cases>(pagination, typeof(Cases).GetSelectSql("", casesIngnoreFields) + " order by DisplayOrder desc, TimeInMs desc");
            }
        }

        public List<CaseItem> LoadCaseItemsByCaseId(int caseId)
        {
            using (var db = Db)
            {
                return db.Query<CaseItem>(typeof(CaseItem).GetSelectSql("CaseId=" + caseId), casesItemIngnoreFields).ToList();
            }
        }

        public Cases SaveCase(Cases cases)
        {
            using (var db = Db)
            {
                if (cases.CaseId > 0)
                {
                    db.Execute(typeof(Cases).GetUpdateSql(caseKeyField, caseKeyField), cases);
                    return cases;
                }
                else
                {
                    cases.CaseId = db.Query<int>(typeof(Cases).GetSqliteInsertSql(casesIngnoreFields), cases).FirstOrDefault();
                    return cases;
                }
            }
        }

        public List<CaseItem> SaveCaseItems(List<CaseItem> caseItems)
        {
            using (var db = Db)
            {
                foreach (var caseItem in caseItems)
                {
                    if (caseItem.CaseItemId > 0)
                    {
                        db.Execute(typeof(CaseItem).GetUpdateSql(caseItemKeyField, casesItemIngnoreFields), caseItem);
                    }
                    else
                    {
                        caseItem.CaseItemId = db.Query<int>(typeof(CaseItem).GetSqliteInsertSql(casesItemIngnoreFields), caseItem).FirstOrDefault();
                    }
                }
            }
            return caseItems;
        }

        public bool DeleteCase(int caseId)
        {
            using (var db = Db)
            {
                int i = db.Execute("delete from Cases where CaseId=" + caseId);
                i += db.Execute("delete from CaseItem where CaseId=" + caseId);
                return i > 0;
            }
        }
        
        public List<CaseItem> LoadCaseItemsByCaseIds(List<int> caseIds)
        {
            using (var db = Db)
            {
                return db.Query<CaseItem>(typeof(CaseItem).GetSelectSql("CaseId in @ids", casesItemIngnoreFields)
                    , new { ids = caseIds.ToArray() }).ToList();
            }
        }
    }
}

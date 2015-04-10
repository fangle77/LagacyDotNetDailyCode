using System;
using System.Collections.Generic;
using Dapper;
using Pineapple.Model;
using System.Linq;
using System.Text;

namespace Pineapple.Data.Sqlite
{
    public class CatalogData : ICatalogData
    {
        private readonly string KeyFiled = "CatalogId";
        private readonly string[] InsertIgnore = { "CatalogId" };
        private readonly string[] UpdateIgnore = { "CatalogId" };

        public Catalog SaveCatalog(Catalog catalog)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                if (catalog.CatalogId == null || catalog.CatalogId <= 0)
                {
                    catalog.CatalogId = cnn.Query<int>(catalog.GetSqliteInsertSql(InsertIgnore), catalog).FirstOrDefault();
                    return catalog;
                }
                else
                {
                    cnn.Execute(catalog.GetUpdateSql(KeyFiled, UpdateIgnore), catalog);
                    return catalog;
                }
            }
        }

        public List<Catalog> CatalogLoadAll()
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                return cnn.Query<Catalog>(typeof(Catalog).GetSelectSql()).ToList<Catalog>();
            }
        }

        public Catalog GetCatalogById(int catalogId)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                return cnn.Query<Catalog>(typeof(Catalog).GetSelectSql("CatalogId=@CatalogId"), new { CatalogId = catalogId }).FirstOrDefault();
            }
        }

        public bool Delete(int catalogId)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                return cnn.Execute("Delete from Catalog where CatalogId=@CatalogId", new { CatalogId = catalogId }) > 0;
            }
        }


        public List<Catalog> LoadCatalogsByIdCatalogIds(IEnumerable<int> catalogIds)
        {
            string ids = string.Join(",", catalogIds).TrimEnd(',');
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                return cnn.Query<Catalog>(typeof(Catalog).GetSelectSql("CatalogId in (@CatalogIds)"), new { CatalogIds = ids }).ToList();
            }
        }
    }
}

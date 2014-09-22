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
            if (catalog.CatalogId == null || catalog.CatalogId <= 0)
            {
                return AddCatalog(catalog);
            }
            else
            {
                return UpdateCatalog(catalog);
            }
        }

        private Catalog AddCatalog(Catalog catalog)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                cnn.Open();
                catalog.CatalogId = cnn.Query<int>(catalog.GetSqliteInsertSql(InsertIgnore), catalog).FirstOrDefault();
                return catalog;
            }
        }

        private Catalog UpdateCatalog(Catalog catalog)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                cnn.Open();
                cnn.Execute(catalog.GetUpdateSql(KeyFiled, InsertIgnore), catalog);
                return catalog;
            }
        }

        public List<Catalog> CatalogLoadAll()
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                cnn.Open();
                return cnn.Query<Catalog>(typeof(Catalog).GetSelectSql()).ToList<Catalog>();
            }
        }

        public Catalog GetCatalogById(int catalogId)
        {
            throw new NotImplementedException();
        }
    }
}

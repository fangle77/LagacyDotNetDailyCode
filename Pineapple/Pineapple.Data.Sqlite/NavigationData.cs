using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Model;
using Dapper;

namespace Pineapple.Data.Sqlite
{
    public class NavigationData : INavigationData
    {
        private readonly string[] NavigationInsertIgnore = { "NavigationId" };
        private readonly string NavigationKeyField = "NavigationId";
        private readonly string[] NavigationUpdateIgnore = { "NavigationId" };

        public Navigation SaveNavigation(Navigation navigation)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                if (navigation.NavigationId == null)
                {
                    navigation.NavigationId = cnn.Query<int>(navigation.GetSqliteInsertSql(NavigationInsertIgnore), navigation).FirstOrDefault();
                    return navigation;
                }
                else
                {
                    cnn.Execute(navigation.GetUpdateSql(NavigationKeyField, NavigationUpdateIgnore), navigation);
                    return navigation;
                }
            }
        }

        public Navigation GetNavigationById(int navigationId)
        {
            using (var cnn = SqLiteBaseRepository.DbReadOnlyConnection())
            {
                return cnn.Query<Navigation>(typeof(Navigation).GetSelectSql("NavigationId=@NavigationId"), new { NavigationId = navigationId }).FirstOrDefault();
            }
        }

        public List<Navigation> LoadNavigationByCatalogId(int catalogId)
        {
            using (var cnn = SqLiteBaseRepository.DbReadOnlyConnection())
            {
                return cnn.Query<Navigation>(@"select n.* from Navigation as n inner join CatalogNavigationMapping as nm
                    on n.NavigationId=nm.NavigationId where nm.CatalogId=@CatalogId", new { CatalogId = catalogId }).ToList();
            }
        }

        public bool DeleteNavigation(int navigationId)
        {
            using (var cnn = SqLiteBaseRepository.DbReadOnlyConnection())
            {
                return cnn.Execute("delete from Navigation where NavigationId=@NavigationId", new { NavigationId = navigationId }) > 0;
            }
        }
    }
}

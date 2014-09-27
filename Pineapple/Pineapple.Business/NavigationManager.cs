using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Data;
using Pineapple.Model;

namespace Pineapple.Business
{
    public class NavigationManager
    {
        [Dependency]
        public INavigationData NavigationData { protected get; set; }

        [Dependency]
        public IMappingData MappingData { protected get; set; }

        public Navigation SaveNavigation(Navigation navigation)
        {
            return NavigationData.SaveNavigation(navigation);
        }

        public Navigation GetNavigationById(int navigationId)
        {
            return NavigationData.GetNavigationById(navigationId);
        }

        public List<Navigation> LoadNavigationsByCatalogId(int catalogId)
        {
            return NavigationData.LoadNavigationByCatalogId(catalogId);
        }

        public List<Navigation> LoadNavigationByNavigationIds(IEnumerable<int> navigationids)
        {
            if (navigationids == null || navigationids.Count() == 0) return null;
            return NavigationData.LoadNavigationByNavigationIds(navigationids.Distinct());
        }

        public List<Navigation> LoadNavigationsByCategoryId(int categoryId)
        {
            var mapping = MappingData.GetMappingByKey(new CategoryNavigationMapping(), categoryId);
            return LoadNavigationByNavigationIds(mapping.Values);
        }

        public bool DeleteNavigation(int navigationId)
        {
            bool navigationDeleted = NavigationData.DeleteNavigation(navigationId);
            if (navigationDeleted)
            {
                MappingData.DeleteMappingByValue(new CatalogNavigationMapping(), navigationId);
                MappingData.DeleteMappingByValue(new CategoryNavigationMapping(), navigationId);
            }
            return navigationDeleted;
        }
    }
}
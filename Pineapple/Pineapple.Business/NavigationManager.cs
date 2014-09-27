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

        public List<Navigation> LoadNavigationByCatalogId(int catalogId)
        {
            return NavigationData.LoadNavigationByCatalogId(catalogId);
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

        public CatalogNavigationMapping GetCatalogNavigationMappingByNavigationId(int navigationId)
        {
            return (CatalogNavigationMapping)MappingData.GetMappingByValue(new CatalogNavigationMapping(), navigationId);
        }

        public CategoryNavigationMapping GetCategoryNavigationMappingByNavigationId(int navigationId)
        {
            return (CategoryNavigationMapping)MappingData.GetMappingByValue(new CategoryNavigationMapping(), navigationId);
        }
    }
}
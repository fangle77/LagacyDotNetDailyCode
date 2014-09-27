using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Model;
using Pineapple.View;
using Pineapple.Business;

namespace Pineapple.Service
{
    public class NavigationService
    {
        [Dependency]
        public NavigationManager NavigationManager { protected get; set; }

        [Dependency]
        public CatalogManager CatalogManager { protected get; set; }

        [Dependency]
        public CategoryManager CategoryManager { protected get; set; }

        public Navigation SaveNavigation(Navigation navigation)
        {
            return NavigationManager.SaveNavigation(navigation);
        }

        public bool DeleteNavigation(int navigationId)
        {
            return NavigationManager.DeleteNavigation(navigationId);
        }

        public NavigationView GetNavigationView(int navigationId)
        {
            NavigationView view = new NavigationView();
            view.Navigation = NavigationManager.GetNavigationById(navigationId);
            if (view.Navigation == null) return null;

            view.Catalogs = CatalogManager.LoadCatalogByNavigationId(navigationId);
            view.Categories = CategoryManager.LoadCategoriesByNavigationId(navigationId);
            return view;
        }
    }
}

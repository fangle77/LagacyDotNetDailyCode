using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Business;
using Pineapple.Model;
using Pineapple.View;

namespace Pineapple.Service
{
    public class CatalogService
    {
        [Dependency]
        public CatalogManager CatalogManager { protected get; set; }

        [Dependency]
        public CategoryManager CategoryManager { protected get; set; }

        [Dependency]
        public NavigationManager NavigationManager { protected get; set; }

        public virtual Catalog SaveCatalog(Catalog catalog)
        {
            return CatalogManager.SaveCatalog(catalog);
        }

        public virtual List<Catalog> CatalogLoadAll()
        {
            return CatalogManager.CatalogLoadAll();
        }

        public virtual CatalogView GetCatalogById(int catalogId)
        {
            CatalogView view = new CatalogView();
            view.Catalog = CatalogManager.GetCatalogById(catalogId);
            if (view.Catalog == null) return null;

            view.Categories = CategoryManager.LoadCategoriesByCatalogId(catalogId);
            view.Navigations = NavigationManager.LoadNavigationsByCatalogId(catalogId);
            return view;
        }
    }
}

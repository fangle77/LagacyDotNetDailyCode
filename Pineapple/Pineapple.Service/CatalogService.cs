using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Business;
using Pineapple.Model;

namespace Pineapple.Service
{
    public class CatalogService
    {
        [Dependency]
        public CatalogManager CatalogManager { protected get; set; }

        public virtual Catalog SaveCatalog(Catalog catalog)
        {
            return CatalogManager.SaveCatalog(catalog);
        }

        public virtual List<Catalog> CatalogLoadAll()
        {
            return CatalogManager.CatalogLoadAll();
        }

        public virtual Catalog GetCatalogById(int catalogId)
        {
            return CatalogManager.GetCatalogById(catalogId);
        }
    }
}

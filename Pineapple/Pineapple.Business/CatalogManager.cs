using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Data;
using Pineapple.Model;

namespace Pineapple.Business
{
    public class CatalogManager
    {
        [Dependency]
        public ICatalogData CatalogData { protected get; set; }

        public Catalog SaveCatalog(Catalog catalog)
        {
            return CatalogData.SaveCatalog(catalog);
        }

        public List<Catalog> CatalogLoadAll()
        {
            return CatalogData.CatalogLoadAll();
        }

        public Catalog GetCatalogById(int catalogId)
        {
            return CatalogData.GetCatalogById(catalogId);
        }
    }
}
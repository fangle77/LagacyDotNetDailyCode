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

        public virtual Catalog SaveCatalog(Catalog catalog)
        {
            return CatalogData.SaveCatalog(catalog);
        }

        public virtual List<Catalog> CatalogLoadAll()
        {
            return CatalogData.CatalogLoadAll();
        }

        public virtual Catalog GetCatalogById(int catalogId)
        {
            return CatalogData.GetCatalogById(catalogId);
        }
    }
}
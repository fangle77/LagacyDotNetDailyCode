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

        [Dependency]
        public IMappingData MappingData { protected get; set; }

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

        public List<Catalog> LoadCatalogsByIdCatalogIds(IEnumerable<int> catalogIds)
        {
            if (catalogIds == null || catalogIds.Count() == 0) return null;
            return CatalogData.LoadCatalogsByIdCatalogIds(catalogIds.Distinct());
        }

        public List<Catalog> LoadCatalogByCategoryId(int categoryId)
        {
            var mapping = MappingData.GetMappingByValue(new CatalogCategoryMapping(), categoryId);
            return LoadCatalogsByIdCatalogIds(mapping.Keys);
        }

        public List<Catalog> LoadCatalogByNavigationId(int navigationId)
        {
            var mapping = MappingData.GetMappingByValue(new CatalogNavigationMapping(), navigationId);
            return LoadCatalogsByIdCatalogIds(mapping.Keys);
        }
    }
}
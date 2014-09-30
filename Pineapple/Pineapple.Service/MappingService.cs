using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Model;
using Pineapple.Business;

namespace Pineapple.Service
{
    public class MappingService
    {
        [Dependency]
        public MappingManager MappingManager { protected get; set; }

        private bool SaveMapping<T>(int key, int value) where T : Mapping<int, int>, new()
        {
            var mapping = new T();
            mapping.AddItem(new MappingItem<int, int>(key, value));
            var m = MappingManager.SaveMapping(mapping);
            return m != null && m.Items.Count > 0 && m.Items[0].MappingId > 0;
        }

        public bool SaveCatalogCategoryMapping(int catalogId, int categoryId)
        {
            return SaveMapping<CatalogCategoryMapping>(catalogId, categoryId);
        }

        public bool SaveCatalogNavigationMapping(int catalogId, int navigationId)
        {
            return SaveMapping<CatalogNavigationMapping>(catalogId, navigationId);
        }

        public bool SaveCategoryNavigationMapping(int categoryId, int navigationId)
        {
            return SaveMapping<CategoryNavigationMapping>(categoryId, navigationId);
        }

        public bool SaveCategoryItemMapping(int categoryId, int categoryItemId)
        {
            return SaveMapping<CategoryItemMapping>(categoryId, categoryItemId);
        }

        public bool SaveCategoryTemplateMapping(int categoryId, int templateId)
        {
            return SaveMapping<CategoryTemplateMapping>(categoryId, templateId);
        }
    }
}

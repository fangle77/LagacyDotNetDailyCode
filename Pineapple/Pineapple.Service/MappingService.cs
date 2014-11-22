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

        #region Save

        private bool SaveMapping<T>(int key, int value) where T : Mapping<int, int>, new()
        {
            var mapping = new T();
            mapping.AddItem(new MappingItem<int, int>(key, value));
            var m = MappingManager.SaveMapping(mapping);
            return m != null && m.Items.Count > 0 && m.Items[0].MappingId > 0;
        }

        public bool SaveCatalogCategoryMapping(int catalogId, int categoryId)
        {
            MappingManager.DeleteMappingByValue(new CatalogCategoryMapping(), categoryId);
            return SaveMapping<CatalogCategoryMapping>(catalogId, categoryId);
        }

        public bool SaveCatalogNavigationMapping(int catalogId, int navigationId)
        {
            MappingManager.DeleteMappingByValue(new CatalogNavigationMapping(), navigationId);
            return SaveMapping<CatalogNavigationMapping>(catalogId, navigationId);
        }

        public bool SaveCategoryNavigationMapping(int categoryId, int navigationId)
        {
            return SaveMapping<CategoryNavigationMapping>(categoryId, navigationId);
        }

        public bool SaveCategoryItemMapping(int categoryId, int categoryItemId)
        {
            MappingManager.DeleteMappingByValue(new CategoryItemMapping(), categoryItemId);
            return SaveMapping<CategoryItemMapping>(categoryId, categoryItemId);
        }

        public bool SaveCategoryTemplateMapping(int categoryId, int templateId)
        {
            return SaveMapping<CategoryTemplateMapping>(categoryId, templateId);
        }

        #endregion

        #region Delete

        private bool DeleteMapping<T>(int key, int value) where T : Mapping<int, int>, new()
        {
            var mapping = new T();
            mapping.AddItem(new MappingItem<int, int>(key, value));
            return MappingManager.DeleteMapping(mapping);
        }

        public bool DeleteCatalogCategoryMapping(int catalogId, int categoryId)
        {
            return DeleteMapping<CatalogCategoryMapping>(catalogId, categoryId);
        }

        public bool DeleteCatalogNavigationMapping(int catalogId, int navigationId)
        {
            return DeleteMapping<CatalogNavigationMapping>(catalogId, navigationId);
        }

        public bool DeleteCategoryNavigationMapping(int categoryId, int navigationId)
        {
            return DeleteMapping<CategoryNavigationMapping>(categoryId, navigationId);
        }

        public bool DeleteCategoryItemMapping(int categoryId, int categoryItemId)
        {
            return DeleteMapping<CategoryItemMapping>(categoryId, categoryItemId);
        }

        public bool DeleteCategoryTemplateMapping(int categoryId, int templateId)
        {
            return DeleteMapping<CategoryTemplateMapping>(categoryId, templateId);
        }

        #endregion
    }
}

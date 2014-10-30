using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Model;
using Pineapple.Data;

namespace Pineapple.Business
{
    public class CategoryManager
    {
        [Dependency]
        public ICategoryData CategoryData { protected get; set; }

        [Dependency]
        public IMappingData MappingData { protected get; set; }

        public virtual Category SaveCategory(Category category)
        {
            return CategoryData.SaveCategory(category);
        }

        public virtual Category GetCategoryById(int categoryId)
        {
            return CategoryData.GetCategoryById(categoryId);
        }

        public virtual List<Category> LoadAllCategories()
        {
            return CategoryData.LoaddAllCategories();
        } 

        public virtual List<Category> LoadCategoriesByCatalogId(int catalogId)
        {
            return CategoryData.LoadCategoriesByCatalogId(catalogId);
        }

        public List<Category> LoadCategoriesByCategoryIds(IEnumerable<int> categoryIds)
        {
            if (categoryIds == null || categoryIds.Count() == 0) return null;
            return CategoryData.LoadCategoriesByCategoryIds(categoryIds.Distinct());
        }

        public List<Category> LoadCategoriesByTemplateId(int templateId)
        {
            var mapping = MappingData.GetMappingByValue(new CategoryTemplateMapping(), templateId);
            return LoadCategoriesByCategoryIds(mapping.Keys);
        }

        public List<Category> LoadCategoriesByNavigationId(int navigationId)
        {
            var mapping = MappingData.GetMappingByValue(new CategoryNavigationMapping(), navigationId);
            return LoadCategoriesByCategoryIds(mapping.Keys);
        }

        public virtual bool DeleteCategory(int categoryId)
        {
            bool deleted = CategoryData.DeleteCategory(categoryId);
            if (deleted)
            {
                MappingData.DeleteMappingByValue(new CatalogCategoryMapping(), categoryId);
                MappingData.DeleteMappingByKey(new CategoryItemMapping(), categoryId);
                MappingData.DeleteMappingByKey(new CategoryNavigationMapping(), categoryId);
                MappingData.DeleteMappingByKey(new CategoryTemplateMapping(), categoryId);
            }
            return deleted;
        }

        public virtual CategoryItem SaveCategoryItem(CategoryItem categoryItem)
        {
            return CategoryData.SaveCategoryItem(categoryItem);
        }

        public virtual CategoryItem GetCategoryItemById(int categoryItemId)
        {
            return CategoryData.GetCategoryItemById(categoryItemId);
        }

        public  List<CategoryItem> LoadAllCategoryItems()
        {
            return CategoryData.LoadAllCategoryItems();
        }

        public virtual List<CategoryItem> LoadCategoryItemsByCategoryId(int categoryId)
        {
            return CategoryData.LoadCategoryItemsByCategoryId(categoryId);
        }

        public virtual bool DeleteCategoryItems(IEnumerable<int> categoryItemIds)
        {
            bool itemDelted = CategoryData.DeleteCategoryItems(categoryItemIds);
            if (itemDelted)
            {
                CategoryItemMapping mapping = new CategoryItemMapping();
                foreach (int categoryItemId in categoryItemIds)
                {
                    MappingData.DeleteMappingByValue(mapping, categoryItemId);
                }
            }
            return itemDelted;
        }
    }
}

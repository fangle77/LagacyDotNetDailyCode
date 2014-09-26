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

        public virtual Category SaveCategory(Category category)
        {
            return CategoryData.SaveCategory(category);
        }

        public virtual Category GetCategoryById(int categoryId)
        {
            return CategoryData.GetCategoryById(categoryId);
        }

        public virtual List<Category> LoadCategoriesByCatalogId(int catalogId)
        {
            return CategoryData.LoadCategoriesByCatalogId(catalogId);
        }

        public virtual bool DeleteCategory(int categoryId)
        {
            return CategoryData.DeleteCategory(categoryId);
        }

        public virtual CategoryItem SaveCategoryItem(CategoryItem categoryItem)
        {
            return CategoryData.SaveCategoryItem(categoryItem);
        }

        public virtual CategoryItem GetCategoryItemById(int categoryItemId)
        {
            return CategoryData.GetCategoryItemById(categoryItemId);
        }

        public virtual List<CategoryItem> LoadCategoryItemsByCategoryId(int categoryId)
        {
            return CategoryData.LoadCategoryItemsByCategoryId(categoryId);
        }

        public virtual bool DeleteCategoryItems(IEnumerable<int> categoryItemIds)
        {
            return CategoryData.DeleteCategoryItems(categoryItemIds);
        }
    }
}

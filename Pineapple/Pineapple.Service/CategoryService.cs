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
    public class CategoryService
    {
        [Dependency]
        public CategoryManager CategoryManager { protected get; set; }

        [Dependency]
        public CatalogManager CatalogManager { protected get; set; }

        [Dependency]
        public NavigationManager NavigationManager { protected get; set; }

        public List<Category> LoadAllCategoriesByCatalogId(int catalogId)
        {
            return CategoryManager.LoadCategoriesByCatalogId(catalogId);
        }

        public List<Category> LoadAllCategories()
        {
            return CategoryManager.LoadAllCategories();
        }

        public virtual Category SaveCategory(Category category)
        {
            return CategoryManager.SaveCategory(category);
        }

        public virtual bool DeleteCategory(int categoryId)
        {
            return CategoryManager.DeleteCategory(categoryId);
        }

        public virtual CategoryItem SaveCategoryItem(CategoryItem categoryItem)
        {
            return CategoryManager.SaveCategoryItem(categoryItem);
        }

        public List<CategoryItem> LoadAllCategoryItems()
        {
            return CategoryManager.LoadAllCategoryItems();
        }

        public virtual CategoryItem GetCategoryItemById(int categoryItemId)
        {
            return CategoryManager.GetCategoryItemById(categoryItemId);
        }

        public virtual bool DeleteCategoryItems(IEnumerable<int> categoryItemIds)
        {
            return CategoryManager.DeleteCategoryItems(categoryItemIds);
        }

        public CategoryView GetCategoryViewByCategryid(int categoryId)
        {
            CategoryView view = new CategoryView();
            view.Category = CategoryManager.GetCategoryById(categoryId);
            if (view.Category == null) return CategoryView.EmptyView;

            view.Catalogs = CatalogManager.LoadCatalogByCategoryId(categoryId);
            view.Navigations = NavigationManager.LoadNavigationsByCategoryId(categoryId);
            view.CategoryItems = CategoryManager.LoadCategoryItemsByCategoryId(categoryId);

            return view;
        }
    }
}

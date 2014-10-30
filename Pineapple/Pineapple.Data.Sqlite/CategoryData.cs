using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Model;
using Dapper;

namespace Pineapple.Data.Sqlite
{
    public class CategoryData : ICategoryData
    {
        private readonly string[] CategoryInsertIgnore = { "CategoryId" };
        private readonly string CategoryKeyField = "CategoryId";
        private readonly string[] CategoryUpdateIgnore = { "CategoryId" };

        private readonly string[] CategoryItemInsertIgnore = { "CategoryItemId" };
        private readonly string CategoryItemKeyField = "CategoryItemId";

        public Category SaveCategory(Category category)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                if (category.CategoryId == null)
                {
                    category.CategoryId = cnn.Query<int>(category.GetSqliteInsertSql(CategoryInsertIgnore), category).FirstOrDefault();
                    return category;
                }
                else
                {
                    cnn.Execute(category.GetUpdateSql(CategoryKeyField, CategoryUpdateIgnore), category);
                    return category;
                }
            }
        }

        public Category GetCategoryById(int categoryId)
        {
            using (var cnn = SqLiteBaseRepository.DbReadOnlyConnection())
            {
                return cnn.Query<Category>(typeof(Category).GetSelectSql("CategoryId=@CategoryId"), new { CategoryId = categoryId }).FirstOrDefault();
            }
        }

        public List<Category> LoaddAllCategories()
        {
            using (var cnn = SqLiteBaseRepository.DbReadOnlyConnection())
            {
                return cnn.Query<Category>(@"select c.* from Category as c").ToList();
            }
        }

        public List<Category> LoadCategoriesByCatalogId(int catalogId)
        {
            using (var cnn = SqLiteBaseRepository.DbReadOnlyConnection())
            {
                return cnn.Query<Category>(@"select c.* from Category as c inner join CatalogCategoryMapping as cm on c.CategoryId=cm.CategoryId
                where cm.CatalogId = @CatalogId", new { CatalogId = catalogId }).ToList();
            }
        }

        public List<Category> LoadCategoriesByCategoryIds(IEnumerable<int> categoryIds)
        {
            string ids = string.Join(",", categoryIds).TrimEnd(',');
            using (var cnn = SqLiteBaseRepository.DbReadOnlyConnection())
            {
                return cnn.Query<Category>(typeof(Category).GetSelectSql("CategoryId in(@CategoryIds)"), new { CategoryIds = ids }).ToList();
            }
        }

        public bool DeleteCategory(int categoryId)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                return cnn.Execute("delete from Category where CategoryId=@CategoryId", new { CategoryId = categoryId }) > 0;
            }
        }

        public CategoryItem SaveCategoryItem(CategoryItem categoryItem)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                if (categoryItem.CategoryItemId == null)
                {
                    categoryItem.CategoryItemId = cnn.Query<int>(categoryItem.GetSqliteInsertSql(CategoryItemInsertIgnore), categoryItem).FirstOrDefault();
                    return categoryItem;
                }
                else
                {
                    cnn.Execute(categoryItem.GetUpdateSql(CategoryItemKeyField), categoryItem);
                    return categoryItem;
                }
            }
        }

        public CategoryItem GetCategoryItemById(int categoryItemId)
        {
            using (var cnn = SqLiteBaseRepository.DbReadOnlyConnection())
            {
                return cnn.Query<CategoryItem>(typeof(CategoryItem).GetSelectSql("CategoryItemId=@CategoryItemId"), new { CategoryItemId = categoryItemId }).FirstOrDefault();
            }
        }

        public  List<CategoryItem> LoadAllCategoryItems()
        {
        	using (var cnn = SqLiteBaseRepository.DbReadOnlyConnection())
            {
                return cnn.Query<CategoryItem>(@"select c.* from CategoryItem as c").ToList();
            }
        }
        
        public List<CategoryItem> LoadCategoryItemsByCategoryId(int categoryId)
        {
            using (var cnn = SqLiteBaseRepository.DbReadOnlyConnection())
            {
                return cnn.Query<CategoryItem>(@"select c.* from CategoryItem as c inner join CatalogItemMapping as cm on c.CategoryItemId=cm.CategoryItemId
                where cm.CategoryId = @CategoryId", new { CategoryId = categoryId }).ToList();
            }
        }

        public bool DeleteCategoryItems(IEnumerable<int> categoryItemIds)
        {
            using (var cnn = SqLiteBaseRepository.DbConnection())
            {
                int total = categoryItemIds.Sum(id => cnn.Execute("delete from CategoryItem where CategoryItemId=@CategoryItemId", new { CategoryItemId = id }));
                return total > 0;
            }
        }
    }
}
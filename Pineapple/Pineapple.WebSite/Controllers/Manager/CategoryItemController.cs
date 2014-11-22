using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Pineapple.Model;
using Pineapple.View;
using Pineapple.Service;

namespace Pineapple.WebSite.Controllers.Manager
{
    public class CategoryItemController : ManagerController
    {
        [Dependency]
        public CategoryService CategoryService { protected get; set; }

        [Dependency]
        public MappingService MappingService { protected get; set; }

        protected override string ManagerName
        {
            get { return "CategoryItem"; }
        }

        //
        // GET: /CategoryItem/

        public ActionResult Index()
        {
            ViewBag.Categories = CategoryService.LoadAllCategories();
            int categoryId = 0;
            if (int.TryParse(base.Request.QueryString["category"], out categoryId) && categoryId > 0)
            {
                ViewBag.CategoryItems = CategoryService.LoadCategoryItemsByCategoryId(categoryId);
            }
            else
            {
                ViewBag.CategoryItems = CategoryService.LoadAllCategoryItems();
            }
            ViewBag.CategoryId = categoryId;
            return View("Index");
        }

        //
        // GET: /CategoryItem/Details/5

        public ActionResult Details(int id)
        {
            ViewBag.CategoryItem = CategoryService.GetCategoryItemById(id);
            ViewBag.Category = CategoryService.GetCategoryByCategoryItemId(id);
            return View("Detail");
        }

        //
        // GET: /CategoryItem/Create

        public ActionResult Create()
        {
            ViewBag.Title = "Create New Category Item";
            ViewBag.CategoryItem = new CategoryItem();
            return View("Edit");
        }

        //
        // POST: /CategoryItem/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var categoryItem = new CategoryItem();
            TryUpdateModel(categoryItem);
            CategoryService.SaveCategoryItem(categoryItem);
            SaveCategoryItemMapping(categoryItem.CategoryItemId, collection);

            return RedirectToAction("Index");
        }

        //
        // GET: /CategoryItem/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Category Item";
            var item = CategoryService.GetCategoryItemById(id);
            if (item == null) item = new CategoryItem();
            ViewBag.CategoryItem = item;

            ViewBag.Category = CategoryService.GetCategoryByCategoryItemId(id);

            return View("Edit");
        }

        //
        // POST: /CategoryItem/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var categoryItem = new CategoryItem();
            TryUpdateModel(categoryItem);
            CategoryService.SaveCategoryItem(categoryItem);

            SaveCategoryItemMapping(categoryItem.CategoryItemId, collection);

            return RedirectToAction("Index");
        }

        private void SaveCategoryItemMapping(int? categoryItemId, FormCollection collection)
        {
            int categoryId = 0;
            if (int.TryParse(collection["CategoryId"], out categoryId) && categoryItemId != null)
            {
                MappingService.SaveCategoryItemMapping(categoryId, categoryItemId.Value);
            }
        }

        //
        // GET: /CategoryItem/Delete/5
        public ActionResult Delete(int id)
        {
            CategoryService.DeleteCategoryItems(new int[] { id });
            return RedirectToAction("Index");
        }
    }
}

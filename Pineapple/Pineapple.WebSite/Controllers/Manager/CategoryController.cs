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
    public class CategoryController : ManagerController
    {
        [Dependency]
        public CategoryService CategoryService { protected get; set; }

        [Dependency]
        public MappingService MappingService { protected get; set; }

        [Dependency]
        public CatalogService CatalogService { protected get; set; }

        protected override string ManagerName
        {
            get { return "Category"; }
        }

        //
        // GET: /Category/

        public ActionResult Index()
        {
            int catalogid = 0;
            if (int.TryParse(base.Request.QueryString["catalog"], out catalogid) && catalogid > 0)
            {
                ViewBag.Categorys = CategoryService.LoadAllCategoriesByCatalogId(catalogid);
            }
            else
            {
                ViewBag.Categorys = CategoryService.LoadAllCategories();
            }
            ViewBag.Catalogs = CatalogService.CatalogLoadAll();
            ViewBag.CatalogId = catalogid;
            return View("Index");
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id)
        {
            ViewBag.CategoryView = CategoryService.GetCategoryViewByCategryid(id);
            return View("Detail");
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            ViewBag.Title = "Create New Category";
            ViewBag.CategoryView = CategoryView.EmptyView;
            return View("Edit");
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var category = new Category();
            TryUpdateModel(category);
            CategoryService.SaveCategory(category);
            return RedirectToAction("Index");
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Category";
            ViewBag.CategoryView = CategoryService.GetCategoryViewByCategryid(id);
            return View("Edit");
        }

        //
        // POST: /Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var category = new Category();
            TryUpdateModel(category);
            CategoryService.SaveCategory(category);

            int catalogId = 1;
            if (int.TryParse(collection["catalogId"], out catalogId) && category.CategoryId != null)
            {
                MappingService.SaveCatalogCategoryMapping(catalogId, category.CategoryId.Value);
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Category/Delete/5
        public ActionResult Delete(int id)
        {
            CategoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}

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

        protected override string ManagerName
        {
            get { return "Category"; }
        }

        //
        // GET: /Category/

        public ActionResult Index()
        {
            ViewBag.Categorys = CategoryService.LoadAllCategories();
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
            return RedirectToAction("Index");
        }

        ////
        //// GET: /Category/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Category/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}

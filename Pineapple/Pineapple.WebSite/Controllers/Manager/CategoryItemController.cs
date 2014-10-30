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

        protected override string ManagerName
        {
            get { return "CategoryItem"; }
        }

        //
        // GET: /Category/

        public ActionResult Index()
        {
            ViewBag.CategoryItems = CategoryService.LoadAllCategoryItems();
            return View("Index");
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id)
        {
            ViewBag.CategoryItem = CategoryService.GetCategoryItemById(id);
            return View("Detail");
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
        	ViewBag.Title = "Create New Category Item";
        	ViewBag.CategoryItem = new CategoryItem();
            return View("Edit");
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var categoryItem = new CategoryItem();
            TryUpdateModel(categoryItem);
            CategoryService.SaveCategoryItem(categoryItem);
            return RedirectToAction("Index");
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id)
        {
        	ViewBag.Title = "Edit Category Item";
            var item = CategoryService.GetCategoryItemById(id);
            if(item == null) item = new CategoryItem();
            ViewBag.CategoryItem = item;
            return View("Edit");
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var categoryItem = new CategoryItem();
            TryUpdateModel(categoryItem);
            CategoryService.SaveCategoryItem(categoryItem);
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

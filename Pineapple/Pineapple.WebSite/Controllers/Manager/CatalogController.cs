using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Pineapple.Service;
using Pineapple.Model;
using Pineapple.View;

namespace Pineapple.WebSite.Controllers.Manager
{
    public class CatalogController : ManagerController
    {
        [Dependency]
        public CatalogService CatalogService { protected get; set; }

        protected override string ManagerName
        {
            get { return "Catalog"; }
        }

        //
        // GET: /Catalog/

        public ActionResult Index()
        {
            ViewBag.Catalogs = CatalogService.CatalogLoadAll();

            return View("Index");
        }

        //
        // GET: /Catalog/Details/5

        public ActionResult Details(int id)
        {
            ViewBag.CatalogView = CatalogService.GetCatalogById(id);

            return View("Detail");
        }

        //
        // GET: /Catalog/Create

        public ActionResult Create()
        {
            ViewBag.CatalogView = CatalogView.EmptyView;
            ViewBag.Title = "Create New Catalog";
            return View("Edit");
        }

        //
        // POST: /Catalog/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var catalog = new Catalog();
            TryUpdateModel(catalog);
            CatalogService.SaveCatalog(catalog);
            return RedirectToAction("Index");
        }

        //
        // GET: /Catalog/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Catalog";
            ViewBag.CatalogView = CatalogService.GetCatalogById(id);
            return View("Edit");
        }

        //
        // POST: /Catalog/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var catalog = new Catalog();
            TryUpdateModel(catalog);
            CatalogService.SaveCatalog(catalog);
            return RedirectToAction("Index");
        }

        //
        // GET: /Catalog/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Catalog/Delete/5

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

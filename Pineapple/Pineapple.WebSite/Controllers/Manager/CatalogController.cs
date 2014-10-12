using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Pineapple.Service;
using Pineapple.Model;

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

            return View("Index.cshtml");
        }

        //
        // GET: /Catalog/Details/5

        public ActionResult Details(int id)
        {
            ViewBag.CatalogView = CatalogService.GetCatalogById(id);

            return View("Detail.cshtml");
        }

        //
        // GET: /Catalog/Create

        public ActionResult Create()
        {
            ViewBag.Title = "Create New Catalog";
            return View("Edit.cshtml");
        }

        //
        // POST: /Catalog/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                CatalogService.SaveCatalog(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Title = "Create New Catalog";
                return View("Edit.cshtml");
            }
        }

        //
        // GET: /Catalog/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Catalog";
            ViewBag.Catalog = CatalogService.GetCatalogById(id);
            return View("Edit.cshtml");
        }

        //
        // POST: /Catalog/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                CatalogService.SaveCatalog(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Title = "Edit Catalog";
                return View("Edit.cshtml");
            }
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

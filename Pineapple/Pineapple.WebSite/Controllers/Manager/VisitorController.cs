using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using Microsoft.Practices.Unity;
using Pineapple.Model;
using Pineapple.Service;

namespace Pineapple.WebSite.Controllers.Manager
{
    public class VisitorController : ManagerController
    {
        //
        // GET: /Visitor/
        [Dependency]
        public VisitorService VisitorService { protected get; set; }

        public ActionResult Index()
        {
        	ViewBag.Visitors = VisitorService.LoadVisitors();
            return View("Index");
        }

        ////
        //// GET: /Visitor/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        ////
        //// GET: /Visitor/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /Visitor/Create

        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Visitor/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Visitor/Edit/5

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Visitor/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Visitor/Delete/5

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

        protected override string ManagerName
        {
            get { return "Visitor"; }
        }
    }
}

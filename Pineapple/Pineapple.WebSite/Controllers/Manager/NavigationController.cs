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
    public class NavigationController : ManagerController
    {
        [Dependency]
        public NavigationService NavigationService { protected get; set; }

        protected override string ManagerName
        {
            get { return "Navigation"; }
        }

        //
        // GET: /Navigation/

        public ActionResult Index()
        {
        	ViewBag.Navigations = NavigationService.LoadAllNavigations();
            return View("Index");
        }

        //
        // GET: /Navigation/Details/5

        public ActionResult Details(int id)
        {
        	ViewBag.Title = "Navigation Detail";
        	ViewBag.NavigationView = NavigationService.GetNavigationView(id);
            return View("Detail");
        }

        //
        // GET: /Navigation/Create

        public ActionResult Create()
        {
        	ViewBag.Title = "Create New Navigation";
            ViewBag.NavigationView = NavigationView.EmtpyView;
            return View("Edit");
        }

        //
        // POST: /Navigation/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var navigation = new Navigation();
            TryUpdateModel(navigation);
            NavigationService.SaveNavigation(navigation);
            return RedirectToAction("Index");
        }

        //
        // GET: /Navigation/Edit/5

        public ActionResult Edit(int id)
        {
        	ViewBag.Title = "Edit Navigation";
            ViewBag.NavigationView = NavigationService.GetNavigationView(id);
            return View("Edit");
        }

        //
        // POST: /Navigation/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var navigation = new Navigation();
            TryUpdateModel(navigation);
            NavigationService.SaveNavigation(navigation);
            return RedirectToAction("Index");
        }

        //
        // GET: /Navigation/Delete/5
        public ActionResult Delete(int id)
        {
        	NavigationService.DeleteNavigation(id);
            return RedirectToAction("Index");
        }
    }
}

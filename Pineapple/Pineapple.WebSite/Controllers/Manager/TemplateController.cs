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
    public class TemplateController : ManagerController
    {
        [Dependency]
        public TemplateService TemplateService { protected get; set; }

        protected override string ManagerName
        {
            get { return "Template"; }
        }

        //
        // GET: /Template/

        public ActionResult Index()
        {
        	ViewBag.Templates = TemplateService.LoadAllTemplates();
            return View("Index");
        }

        //
        // GET: /Template/Details/5

        public ActionResult Details(int id)
        {
        	ViewBag.Title = "Template Detail";
        	ViewBag.TemplateView = TemplateService.GetTemplateView(id);
            return View("Detail");
        }

        //
        // GET: /Template/Create

        public ActionResult Create()
        {
        	ViewBag.Title = "Create New Template";
            ViewBag.TemplateView = TemplateView.EmptyView;
            return View("Edit");
        }

        //
        // POST: /Template/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var template = new Template();
            TryUpdateModel(template);
            TemplateService.SaveTemplate(template);
            return RedirectToAction("Index");
        }

        //
        // GET: /Template/Edit/5

        public ActionResult Edit(int id)
        {
        	ViewBag.Title = "Edit Template";
            ViewBag.TemplateView = TemplateService.GetTemplateView(id);
            return View("Edit");
        }

        //
        // POST: /Template/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var template = new Template();
            TryUpdateModel(template);
            TemplateService.SaveTemplate(template);
            return RedirectToAction("Index");
        }

        //
        // GET: /Template/Delete/5
        public ActionResult Delete(int id)
        {
        	TemplateService.DeleteTemplate(id);
            return RedirectToAction("Index");
        }
    }
}

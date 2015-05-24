using System;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Pineapple.Model;
using Pineapple.Service;
using Pineapple.Core.Util;

namespace Pineapple.WebSite.Controllers.Manager
{
    /// <summary>
    /// Description of CasesController.
    /// </summary>
    public abstract class BaseCasesController : ManagerController
    {
        [Dependency]
        public CasesService CasesService { protected get; set; }

        protected override string ManagerName
        {
            get { return "Cases"; }
        }

        protected abstract CaseType CaseType { get; }

        public ActionResult Index()
        {
            ViewBag.Title = NavigationName;

            var pagination = base.GetPage();
            ViewBag.Cases = CasesService.LoadSimpleCases(pagination.Pagination, CaseType);
            BuildPagination(pagination);
            return View("Index");
        }

        public ActionResult Create()
        {
            ViewBag.Cases = new Cases();
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Save(Cases cases)
        {
            if (cases != null) cases.CaseType = (int)CaseType;
            cases = CasesService.SaveCase(cases);
            return RedirectToAction("Edit", new RouteValueDictionary(new { id = cases.CaseId }));
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Cases = CasesService.GetCase(id);
            return View("Edit");
        }

        public ActionResult Delete(int id)
        {
            CasesService.DeleteCase(id);
            return RedirectToAction("Index");
        }

        public ActionResult EditItems()
        {
            int caseid = Request.QueryString["caseid"].ToInt();
            if (caseid <= 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Cases = CasesService.GetCase(caseid);
                return View("EditItems");
            }
        }

        [HttpPost]
        public ActionResult SaveItem(CaseItem item)
        {
            bool result = CasesService.SaveCaseItem(item) != null;
            return Json(result.ToString().ToLower());
        }

        [HttpPost]
        public ActionResult DeleteItem(int id)
        {
            bool result = CasesService.DeleteCaseItem(id);
            return Json(result.ToString().ToLower());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Pineapple.Service;
using Microsoft.Practices.Unity;

namespace Pineapple.WebSite.Controllers.Manager
{
    public abstract class ManagerController : Controller
    {
        [Dependency]
        public ManagerNavigationService ManagerNavigationService { protected get; set; }

        protected string Master { get { return "~/Views/Manager/_Layout.cshtml"; } }

        protected abstract string ManagerName
        {
            get;
        }

        protected string AddManageBase(string viewName)
        {
            return string.Format("~/Views/Manager/{0}{1}{2}", ManagerName, string.IsNullOrEmpty(ManagerName) ? string.Empty : "/", viewName);
        }

        protected new ViewResult View(string viewName)
        {
            BuildLeftNavigation();
            BuildBreadCrumb();
            return base.View(AddManageBase(viewName), Master);
        }

        protected void BuildLeftNavigation()
        {
            ViewBag.Navigations = ManagerNavigationService.LoadManagerNavigatoin(ManagerName);
        }

        protected void BuildBreadCrumb()
        {
            string controller = (string)RouteData.Values["controller"];
            string action = (string)RouteData.Values["action"];
            ViewBag.BreadCrumbs = ManagerNavigationService.BuildBreadCrumbs(controller, action);
        }
    }
}

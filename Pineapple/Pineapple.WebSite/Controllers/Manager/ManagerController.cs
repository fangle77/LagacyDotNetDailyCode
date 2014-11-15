using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Pineapple.Service;
using Microsoft.Practices.Unity;

namespace Pineapple.WebSite.Controllers.Manager
{
    [ValidateInput(false)]
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
            return string.Format("~/Views/Manager/{0}{1}{2}.cshtml", ManagerName, string.IsNullOrEmpty(ManagerName) ? string.Empty : "/", viewName);
        }

        protected new ActionResult View(string viewName)
        {
            BuildCommonVariable();
            BuildBreadCrumb();
            if (IsAjaxRequest())
            {
                return base.View(viewName);
            }
            else
            {
                BuildLeftNavigation();
                return base.View(AddManageBase(viewName), Master);
            }
        }

        protected void BuildLeftNavigation()
        {
            ViewBag.LeftNavigations = ManagerNavigationService.LoadManagerNavigatoin(ManagerName);
        }

        protected void BuildBreadCrumb()
        {
            string controller = (string)RouteData.Values["controller"];
            string action = (string)RouteData.Values["action"];
            ViewBag.BreadCrumbs = ManagerNavigationService.BuildBreadCrumbs(controller, action);
        }

        protected bool IsAjaxRequest()
        {
            //X-Requested-With:XMLHttpRequest
            var xmlHeaders = HttpContext.Request.Headers.GetValues("X-Requested-With");
            return xmlHeaders != null &&
                   xmlHeaders.Any(x => x.IndexOf("XMLHttpRequest", StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void BuildCommonVariable()
        {
            ViewBag.TableClass = "table table-hover table-bordered";
        }
    }
}

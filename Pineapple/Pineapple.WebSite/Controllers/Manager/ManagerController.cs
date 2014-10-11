using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Pineapple.WebSite.Controllers.Manager
{
    public abstract class ManagerController : Controller
    {
        protected string Master { get { return "~/Views/Manager/_Layout.cshtml"; } }

        protected string AddManageBase(string viewName)
        {
            return "~/Views/Manager/" + viewName;
        }

        protected new ViewResult View(string viewName)
        {
            return base.View(AddManageBase(viewName), Master);
        }
    }
}

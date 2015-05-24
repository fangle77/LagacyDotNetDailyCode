using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pineapple.Model;

namespace Pineapple.WebSite.Controllers.Manager
{
    public class ServiceController : BaseCasesController
    {

        protected override CaseType CaseType
        {
            get { return CaseType.Service; }
        }

        protected override string NavigationName
        {
            get { return "Service"; }
        }
    }
}

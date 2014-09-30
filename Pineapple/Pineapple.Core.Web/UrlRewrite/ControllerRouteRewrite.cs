using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Core.Web.UrlRewrite
{
    public class ControllerRouteRewrite : IUrlRewriteAdaptee
    {
        public bool RewritePath(System.Web.HttpContext httpContext)
        {
            return false;
        }
    }
}

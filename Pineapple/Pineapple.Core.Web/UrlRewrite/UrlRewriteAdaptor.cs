using System.Collections.Generic;
using System.Web;
using Microsoft.Practices.Unity;

namespace Pineapple.Core.Web.UrlRewrite
{
    public class UrlRewriteAdaptor
    {
        private List<IUrlRewriteAdaptee> urlRewriteAdaptees = new List<IUrlRewriteAdaptee>();

        [Dependency]
        public IUrlRewriteAdaptee[] UrlRewriteAdaptees
        {
            set { urlRewriteAdaptees.AddRange(value); }
        }

        public bool RewriteUrl(HttpContext context)
        {
            if (context == null) return false;
            foreach (var adaptee in urlRewriteAdaptees)
            {
                if (adaptee.RewritePath(context))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

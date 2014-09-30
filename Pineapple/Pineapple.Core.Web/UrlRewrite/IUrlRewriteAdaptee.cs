using System.Web;

namespace Pineapple.Core.Web.UrlRewrite
{
    public interface IUrlRewriteAdaptee
    {
        bool RewritePath(HttpContext httpContext);
    }
}
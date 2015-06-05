using System;
using System.Web;
using System.Web.Mvc;

namespace Pineapple.WebSite.Controllers
{
	/// <summary>
	/// Description of BaseController.
	/// </summary>
	public abstract class BaseController : Controller
	{
		[Dependency("DefaultViewTemplate")]
		public string DefaultViewDir
		{
			protected get; set;
		}
		
		public virtual string ViewTemplate()
		{
				string cookieView =	base.ControllerContext.RequestContext.HttpContext.Request.Cookies["_v_t_"];
				if(String.IsNullOrEmpty(cookieView)){
					cookieView = DefaultViewDir;
					HttpCookie cookie = new HttpCookie("_v_t_",cookieView);
					cookie.Expires = DateTime.Now.AddDays(30);
					base.ControllerContext.RequestContext.HttpContext.Response.SetCookie(cookie);
				}
				return cookieView;
		}
		
		protected string Master { get { return String.Format("~/Views/{0}/_Layout.cshtml",ViewTemplate()); } }
		
		protected string AddManageBase(string viewName)
        {
			string viewTemplate = ViewTemplate();
            return string.Format("~/Views/{0}/{1}.cshtml", viewTemplate, viewName);
        }
		
		protected override ViewResult View(string viewName, string masterName, object model)
		{
			viewName = AddManageBase(viewName);
			return base.View(viewName, Master, model);
		}
	}
}
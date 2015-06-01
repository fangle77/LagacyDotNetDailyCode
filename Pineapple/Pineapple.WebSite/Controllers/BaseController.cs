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
		[Dependency("DefaultViewDir")]
		public string DefaultViewDir
		{
			protected get; set;
		}
		
		protected string ViewDir
		{
			get
			{
				string cookieView =	base.ControllerContext.RequestContext.HttpContext.Request.Cookies["_v_"];
				if(String.IsNullOrEmpty(cookieView)){
					cookieView = DefaultViewDir;
					HttpCookie cookie = new HttpCookie("_v_",cookieView);
					cookie.Expires = DateTime.Now.AddYears(1);
					base.ControllerContext.RequestContext.HttpContext.Response.SetCookie(cookie);
				}
				return cookieView;
			}
		}
		
		
	}
}
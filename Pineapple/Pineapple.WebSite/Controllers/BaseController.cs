using System;
using System.Web.Mvc;

namespace Pineapple.WebSite.Controllers
{
	/// <summary>
	/// Description of BaseController.
	/// </summary>
	public abstract class BaseController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
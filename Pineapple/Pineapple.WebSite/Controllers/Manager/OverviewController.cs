using System;
using System.Web.Mvc;

namespace Pineapple.WebSite.Controllers.Manager
{
	/// <summary>
	/// Description of OverviewController.
	/// </summary>
	public class OverviewController : ManagerController
	{
		protected override string ManagerName
		{
			get { return string.Empty; }
		}
		
		public ActionResult Index()
		{
			return View("Index");
		}
	}
}
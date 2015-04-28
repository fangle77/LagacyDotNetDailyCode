using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Pineapple.Model;
using Pineapple.Service;

namespace Pineapple.WebSite.Controllers.Manager
{
	/// <summary>
	/// Description of CasesController.
	/// </summary>
	public class CasesController : ManagerController
	{
		[Dependency]
		public CasesService CasesService{protected get;set;}
		
		public ActionResult Index()
		{
			var pagination = base.GetPage();
			ViewBag.Cases = CasesService.LoadCases(pagination.Pagination);
			BuildPagination(pagination);
			return View("Index");
		}
		
		public ActionResult Create()
		{
			ViewBag.Cases = new Cases();
			return View("Edit");
		}
		
		protected override string ManagerName
		{
			get { return "Cases"; }
		}
	}
}
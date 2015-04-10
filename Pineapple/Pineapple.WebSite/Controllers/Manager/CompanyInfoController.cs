using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Pineapple.Model;
using Pineapple.Service;
using Pineapple.WebSite.Controllers.Manager;

namespace Pineapple.WebSite.Controllers.Manager
{
	/// <summary>
	/// Description of CompanyInfoController.
	/// </summary>
	public class CompanyInfoController : ManagerController
	{
		[Dependency]
        public CompanyInfoService CompanyInfoService { protected get; set; }
		
		protected override string ManagerName
		{
			get 
			{
				return "CompanyInfo";
			}
		}
		
		public ActionResult Index()
		{
			ViewBag.Campany = CompanyInfoService.LoadCompanyInfo();
			return View("Index");
		}

		[HttpPost]
		public ActionResult Create(FormCollection values)
		{
			return View();
		}
		
		[HttpPost]
		public ActionResult Edit(FormCollection values)
		{
			return View();
		}
		
		[HttpPost]
		public ActionResult Delete(string key)
		{
			return View();
		}
	}
}
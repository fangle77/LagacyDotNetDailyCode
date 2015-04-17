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
			ViewBag.Company = CompanyInfoService.LoadCompanyInfo();
			return View("Index");
		}
		
		[HttpPost]
		public ActionResult Save(string key, string content)
		{
			bool result = CompanyInfoService.SaveCompanyInfo(key,content);
			return Json(result.ToString().ToLower());
		}
		
		[HttpPost]
		public ActionResult Delete(string key)
		{
			bool result = CompanyInfoService.DeleteCompanyInfo(key);
			return Json(result.ToString().ToLower());
		}
	}
}
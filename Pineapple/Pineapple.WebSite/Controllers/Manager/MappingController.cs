using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pineapple.Model;
using Pineapple.Service;
using Pineapple.View;

namespace Pineapple.WebSite.Controllers.Manager
{
    public class MappingController : ManagerController
    {

    	public MappingService MappingService { protected get; set; }
    	
    	protected override string ManagerName
        {
    	 	get { return "Mapping"; }
        }
    	
        public ActionResult Index()
        {
            return View("Index");
        }
        
        [ActionName("Catalog-Category")]
        public ActionResult CatalogCategory()
        {
        	return View("Catalog-Category");
        }
        
        [ActionName("Catalog-Navigation")]
        public ActionResult CatalogNavigation()
        {
        	return View("Catalog-Navigation");
        }
        
        [ActionName("Category-Navigation")]
        public ActionResult CategoryNavigation()
        {
        	return View("Category-Navigation");
        }
        
        [ActionName("Category-Template")]
        public ActionResult CategoryTemplate()
        {
        	return View("Category-Template");
        }
        
        [ActionName("Category-CategoryItem")]
        public ActionResult CategoryCategoryItem()
        {
        	return View("Category-CategoryItem");
        }
    }
}
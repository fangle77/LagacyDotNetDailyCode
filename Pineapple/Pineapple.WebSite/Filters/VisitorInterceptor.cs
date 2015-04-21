using System;
using System.Web.Mvc;
using System.Web;
using Pineapple.Core;
using Microsoft.Practices.Unity;
using Pineapple.Model;
using Pineapple.Service;
using Pineapple.Core.Util;

namespace Pineapple.WebSite.Filters
{
	/// <summary>
	/// Description of VisitorInterceptor.
	/// </summary>
	public class VisitorInterceptor : IActionFilter
	{
		[Dependency]
		public VisitorService VisitorService {private get; set;}
		
		public VisitorInterceptor()
		{
			Container.UnityContainer.BuildUp(this);
		}
		
		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
		    var vidCookie = filterContext.RequestContext.HttpContext.Request.Cookies["vid"];
		    string vid = vidCookie==null? string.Empty : vidCookie.Value;
		    if(VisitorService.IsValidVisitorId(vid))
		    {
		    	bool inSession = VisitorService.GetVisitorFromSession(vid) != null;
		    	if(!inSession)
		    	{
		    		Visitor visitor = VisitorService.GetVisitor(vid);
		    		if(visitor == null)
		    		{
		    		    visitor = CreateNewVisitor(filterContext);
		    			AddVisitLog(filterContext, visitor.VisitorId);
		    		}
		    		else
		    		{
		    			AddVisitLog(filterContext, vid);
		    		}
		    	}
		    }
		    else
		    {
		    	Visitor visitor = CreateNewVisitor(filterContext);
		    	AddVisitLog(filterContext, visitor.VisitorId);
		    }
		}
		
		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			
		}
		
		private Visitor CreateNewVisitor(ActionExecutingContext filterContext)
		{
			Visitor visitor = new Visitor();
			visitor.VisitorId = VisitorService.CreateNewVisitorId();
			
			VisitorService.AddVisitor(visitor);
			
			HttpCookie cookie = new HttpCookie("vid",visitor.VisitorId);
			cookie.Expires = DateTime.Now.AddDays(365);
			filterContext.HttpContext.Response.Cookies.Add(cookie);
		    
			return visitor;
		}
		
		private void AddVisitLog(ActionExecutingContext filterContext, string vid)
		{
			VisitLog log = new VisitLog();
			log.VisitorId = vid;
			log.VisitTime = DateTime.Now;
			log.ClientIp = filterContext.RequestContext.HttpContext.Request.UserHostAddress;
			log.UserAgent = filterContext.RequestContext.HttpContext.Request.UserAgent;
			log.EnterUrl = string.Concat(filterContext.RequestContext.HttpContext.Request.Url);
			log.RefererUrl = string.Concat(filterContext.RequestContext.HttpContext.Request.UrlReferrer);
			log.VisitTimeInMs = TimeUtility.ToUtcTimeInMillis(log.VisitTime);
			VisitorService.AddVisitLog(log);
		}
	}
}

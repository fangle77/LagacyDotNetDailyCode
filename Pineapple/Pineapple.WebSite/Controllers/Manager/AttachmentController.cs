using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.Practices.Unity;
using Pineapple.Model;
using Pineapple.Service;


namespace Pineapple.WebSite.Controllers.Manager
{
	/// <summary>
	/// Description of AttachmentController.
	/// </summary>
	public class AttachmentController : ManagerController
	{
		[Dependency]
		public AttachmentService  AttachmentService { get; set; }
		
		protected override string ManagerName 
		{
			get { return "Attachment"; }
		}
		
		public ActionResult Index()
		{
			return View("Upload");
		}
		
		[HttpPost]
		public ActionResult Upload()
		{
			if(Request.Files != null )
			{
				for(int i = 0; i < Request.Files.Count; i++ )
				{
					var file = Request.Files[i];
					if(file != null && file.ContentLength > 0)
					{
						string fileName = Path.GetFileName(file.FileName);
					    Attachment attachment =	AttachmentService.SaveAttachment(file.ContentLength, file.ContentType, fileName);
					    file.SaveAs(attachment.FilePath);
					}
				}
			}
			return RedirectToAction("Index");
		}
	}
}
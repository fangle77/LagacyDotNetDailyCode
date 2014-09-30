using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Practices.Unity;
using Pineapple.Core.Web.UrlRewrite;

namespace Pineapple.Core.Web.Modules
{
    public class UrlRewriteModule : IHttpModule
    {
        [Dependency]
        public UrlRewriteAdaptor UrlRewriteAdaptor { private get; set; }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
        }

        void Context_BeginRequest(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            if(application==null) return;

            UrlRewriteAdaptor.RewriteUrl(application.Context);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace MVCHttpHandler
{
    public class MVCHttpHandler : IHttpHandler, IRequiresSessionState
    {

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string rawurl = context.Request.RawUrl;
            string w = rawurl;
            context.Response.Write(w);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Business;
using Pineapple.Model;
using Microsoft.Practices.Unity;

namespace Pineapple.Service
{
    public class VisitorService
    {
        [Dependency]
        public VisitorManager VisitorManager { private get; set; }

        public Visitor AddVisitor(Visitor visitor)
        {
            return VisitorManager.AddVisitor(visitor);
        }

        public string CreateNewVisitorId()
        {
            return VisitorManager.CreateNewVisitorId();
        }

        public bool IsValidVisitorId(string visitorId)
        {
            return VisitorManager.IsValidVisitorId(visitorId);
        }

        public Visitor GetVisitor(string visitorId)
        {
            return VisitorManager.GetVisitor(visitorId);
        }

        public virtual Visitor GetVisitorFromSession(string visitorId)
        {
            return VisitorManager.GetVisitorFromSession(visitorId);
        }

        public VisitLog AddVisitLog(VisitLog visiLog)
        {
            return VisitorManager.AddVisitLog(visiLog);
        }

        public List<VisitLog> LoadVisitors()
        {
            return VisitorManager.LoadVisitorLogs();
        }
    }
}

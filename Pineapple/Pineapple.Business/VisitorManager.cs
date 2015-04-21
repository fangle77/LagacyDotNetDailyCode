using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Core.Cache;
using Pineapple.Data;
using Microsoft.Practices.Unity;
using Pineapple.Model;

namespace Pineapple.Business
{
    public class VisitorManager
    {
        [Dependency]
        public IVisitorData VisitorData { private get; set; }

        public virtual Visitor AddVisitor(Visitor visitor)
        {
            ClearVisitorCache(visitor.VisitorId);
            return VisitorData.AddVisitor(visitor);
        }

        public string CreateNewVisitorId()
        {
            return Guid.NewGuid().ToString();
        }

        public bool IsValidVisitorId(string visitorId)
        {
            Guid g;
            return Guid.TryParse(visitorId, out g);
        }

        [Cache("Action", "Visitor", CacheMode.Session, CacheType.Clear)]
        public virtual void ClearVisitorCache([CacheKey] string visitorId)
        {
        }

        [Cache("Action", "Visitor", CacheMode.Session)]
        public virtual Visitor GetVisitorFromSession([CacheKey]string visitorId)
        {
            return null;
        }

        [Cache("Action", "Visitor", CacheMode.Session)]
        public virtual Visitor GetVisitor([CacheKey]string visitorId)
        {
            return VisitorData.GetVisitor(visitorId);
        }

        public virtual VisitLog AddVisitLog(VisitLog visiLog)
        {
            ClearVisitorCache(visiLog.VisitorId);
            return VisitorData.AddVisitLog(visiLog);
        }

        public List<VisitLog> LoadVisitorLogs()
        {
            return VisitorData.LoadVisitorLogs();
        }
    }
}

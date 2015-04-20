using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Core.Cache;
using Pineapple.Model;

namespace Pineapple.Data
{
    public interface IVisitorData
    {
        Visitor AddVisitor(Visitor visitor);

        Visitor GetVisitor(string visitorId);

        VisitLog AddVisitLog(VisitLog visiLog);

        List<Visitor> LoadVisitors();
    }
}

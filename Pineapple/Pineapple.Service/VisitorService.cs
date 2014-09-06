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

        public List<Visitor> LoadLatestNVisitors(int latestN)
        {
            return VisitorManager.LoadLatestNVisitors(latestN);
        }

        public long GetTotalVisitors()
        {
            return VisitorManager.GetTotalVisitors();
        }
    }
}

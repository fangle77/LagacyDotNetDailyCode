using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Data;
using Microsoft.Practices.Unity;
using Pineapple.Model;

namespace Pineapple.Business
{
    public class VisitorManager
    {
        [Dependency]
        public IVisitorData VisitorData { private get; set; }

        public Visitor AddVisitor(Visitor visitor)
        {
            return VisitorData.AddVisitor(visitor);
        }

        public List<Visitor> LoadLatestNVisitors(int latestN)
        {
            return VisitorData.LoadLatestNVisitors(latestN);
        }

        public long GetTotalVisitors()
        {
            return VisitorData.GetTotalVisitors();
        }
    }
}

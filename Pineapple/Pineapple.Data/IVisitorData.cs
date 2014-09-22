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

        [Cache("Visitor", "LatestN")]
        List<Visitor> LoadLatestNVisitors(int latestN);

        long GetTotalVisitors();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Core.Cache
{
    public enum CacheMode
    {
        None,
        Request,
        Session,
        Local,
        Remote
    }
}
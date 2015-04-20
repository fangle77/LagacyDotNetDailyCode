using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Pineapple.Core.Cache
{
    public class CacheProxy
    {
        [Dependency(CacheConstants.LocalCache)]
        public ICache LocalCache { private get; set; }

        [Dependency(CacheConstants.SessionCache)]
        public ICache SessionCache { private get; set; }

        [Dependency(CacheConstants.RequestCache)]
        public ICache RequestCache { private get; set; }

        public virtual ICache GetCacheHandler(CacheMode cacheMode)
        {
            switch (cacheMode)
            {
                case CacheMode.Local:
                    return LocalCache;
                case CacheMode.Session:
                    return SessionCache;
                case CacheMode.Request:
                    return RequestCache;
                default:
                    throw new NotImplementedException("CacheMode =" + cacheMode);
            }
        }
    }
}
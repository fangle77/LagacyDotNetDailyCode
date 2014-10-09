using System;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Pineapple.Core.Cache
{
    public class CacheCallHandler : ICallHandler
    {
        LocalCache localCache = new LocalCache();

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var cacheAttr = GetAttribute(input);
            if (cacheAttr == null) return getNext()(input, getNext);

            if (localCache.Contain(cacheAttr.Group, cacheAttr.CacheKey))
            {
                return input.CreateMethodReturn(localCache.Get(cacheAttr.Group, cacheAttr.CacheKey));
            }
            else
            {
                var r = getNext()(input, getNext);
                localCache.Add(cacheAttr.Group, cacheAttr.CacheKey, r.ReturnValue);
                return r;
            }
        }

        public int Order { get; set; }

        private CacheAttribute GetAttribute(IMethodInvocation input)
        {
            var attr = Attribute.GetCustomAttributes(input.MethodBase, typeof(CacheAttribute));
            if (attr.Length == 0) return null;
            return attr[0] as CacheAttribute;
        }
    }
}

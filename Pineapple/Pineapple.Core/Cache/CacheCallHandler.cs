using System;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Pineapple.Core.Cache
{
    public class CacheCallHandler : ICallHandler
    {
        LocalCache localCache = new LocalCache();

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            string cacheKey = GetCacheKey(input);
            if (cacheKey == null) return getNext()(input, getNext);

            if (localCache.Contain(cacheKey))
            {
                return input.CreateMethodReturn(localCache.Get(cacheKey));
            }
            else
            {
                var r = getNext()(input, getNext);
                localCache.Add(cacheKey, r.ReturnValue);
                return r;
            }
        }

        private string GetCacheKey(IMethodInvocation input)
        {
            var attr = Attribute.GetCustomAttributes(input.MethodBase, typeof(CacheAttribute));
            if (attr.Length == 0) return null;
            var cacheAttribute = attr[0] as CacheAttribute;
            return cacheAttribute != null ? cacheAttribute.CacheKey : null;
        }

        public int Order { get; set; }
    }
}

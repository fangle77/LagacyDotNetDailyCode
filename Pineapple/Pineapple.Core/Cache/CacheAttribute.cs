using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Pineapple.Core.Cache
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CacheAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return container.Resolve<CacheCallHandler>();
        }

        public CacheAttribute(string group, string name)
        {
            Group = group;
            Name = name;
        }

        public CacheAttribute(string group, string name, CacheType cacheType)
        {
            Group = group;
            Name = name;
            CacheType = cacheType;
        }

        public string Group { get; private set; }

        public string Name { get; private set; }

        public CacheType CacheType { get; private set; }

        public string CacheKey { get { return Group + ";" + Name; } }
    }
}

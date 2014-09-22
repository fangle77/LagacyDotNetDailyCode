using System;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Pineapple.Core.Cache
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CacheAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(Microsoft.Practices.Unity.IUnityContainer container)
        {
            return new CacheCallHandler();
        }

        public CacheAttribute(string group, string name)
        {
            this.Group = group;
            this.Name = name;
        }

        public string Group { get; private set; }

        public string Name { get; private set; }

        public string CacheKey { get { return Group + Name; } }
    }
}

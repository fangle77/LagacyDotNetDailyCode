using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Core.Cache;
using Pineapple.Core.Web.Cache;

namespace Pineapple.Core.Web
{
    public class WebInitializer
    {
        public static void Init()
        {
            Container.UnityContainer.RegisterType<ICache, SessionCache>(CacheConstants.SessionCache, new ContainerControlledLifetimeManager());
            Container.UnityContainer.RegisterType<ICache, RequestCache>(CacheConstants.RequestCache, new ContainerControlledLifetimeManager());
        }
    }
}

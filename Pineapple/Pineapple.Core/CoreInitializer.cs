using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Core.Cache;

namespace Pineapple.Core
{
    public class CoreInitializer
    {
        public static void Init()
        {
            Container.UnityContainer.RegisterType<CacheCallHandler>(new ContainerControlledLifetimeManager());
        }
    }
}

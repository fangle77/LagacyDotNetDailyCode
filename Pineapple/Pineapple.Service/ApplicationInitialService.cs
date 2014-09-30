using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Pineapple.Core;
using Pineapple.Core.Cache;

namespace Pineapple.Service
{
    public class ApplicationInitialService
    {
        private static readonly string DataDll = "Pineapple.Data";
        private static readonly string DataSqliteDll = "Pineapple.Data.Sqlite";
        private static readonly string BusinessDll = "Pineapple.Business";
        private static readonly string ServiceDll = "Pineapple.Service";

        public static void RegisterContainer()
        {
            Container.RegisterAssemblyInterface(DataDll, DataSqliteDll);
            Container.ResisterAssemblyType(BusinessDll, ServiceDll);

            RegisterCache();
        }

        private static void RegisterCache()
        {
            var matchType = new Func<Type, bool>(type => type.GetMethods().Any(methodInfo => methodInfo.GetCustomAttributes(typeof(CacheAttribute), true).Length > 0));

            Container.RegisterAssemblyVirtualMethodInterceptor(matchType, BusinessDll, ServiceDll);
            Container.RegisterAssemblyVirtualMethodInterceptor(matchType, DataSqliteDll);
        }
    }
}

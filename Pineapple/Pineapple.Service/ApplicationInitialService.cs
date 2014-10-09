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
        private ApplicationInitialService(){}

        private readonly string DataDll = "Pineapple.Data";
        private readonly string DataSqliteDll = "Pineapple.Data.Sqlite";
        private readonly string BusinessDll = "Pineapple.Business";
        private readonly string ServiceDll = "Pineapple.Service";

        public static void RegisterContainer()
        {
            new ApplicationInitialService().InnerRegisterContainer();
        }

        private void InnerRegisterContainer()
        {
            Container.RegisterAssemblyInterface(DataDll, DataSqliteDll);
            Container.ResisterAssemblyType(BusinessDll, ServiceDll);

            RegisterCache();
        }

        private void RegisterCache()
        {
            var matchType = new Func<Type, bool>(type => type.GetMethods().Any(methodInfo => methodInfo.GetCustomAttributes(typeof(CacheAttribute), true).Length > 0));

            Container.RegisterAssemblyVirtualMethodInterceptor(matchType, BusinessDll, ServiceDll);
            Container.RegisterAssemblyVirtualMethodInterceptor(matchType, DataSqliteDll);
        }
    }
}

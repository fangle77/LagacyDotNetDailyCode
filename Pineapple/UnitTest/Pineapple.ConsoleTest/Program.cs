using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Pineapple.Core;

namespace Pineapple.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            InitInjection();

            //Core.ContainerTest.RegisterAssemblyTest();
            //Service.VisitorServiceTest.InjectionTest();
            Service.VisitorServiceTest.CacheInterceptorTest();

            Console.WriteLine("\r\nFinish!");
            Console.Read();
        }

        private static void InitInjection()
        {
            Container.RegisterAssemblyInterface("Pineapple.Data", "Pineapple.Data.Sqlite");
            Container.ResisterAssemblyType("Pineapple.Business", "Pineapple.Service");
        }
    }
}

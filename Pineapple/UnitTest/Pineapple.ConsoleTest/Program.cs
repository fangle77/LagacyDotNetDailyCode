using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

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
            Pineapple.Core.Container.ResisterAssemblyType("Pineapple.Data"
                , "Pineapple.Business", "Pineapple.Service");
        }
    }
}

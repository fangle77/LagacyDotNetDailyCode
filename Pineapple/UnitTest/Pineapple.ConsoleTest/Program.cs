using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Pineapple.Core;
using Pineapple.Service;

namespace Pineapple.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationInitialService.RegisterContainer();


            //Core.ContainerTest.RegisterAssemblyTest();
            //Service.VisitorServiceTest.InjectionTest();
            Service.VisitorServiceTest.CacheInterceptorTest();

            Console.WriteLine("\r\nFinish!");
            Console.Read();
        }
    }
}

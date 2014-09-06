using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            InitInjection();

            //Core.ContainerTest.RegisterAssemblyTest();
            Service.VisitorServiceTest.InjectionTest();

            Console.WriteLine("Finish!");
            Console.Read();
        }

        private static void InitInjection()
        {
            Pineapple.Core.Container.ResisterAssemblyType("Pineapple.Data"
                , "Pineapple.Business", "Pineapple.Service");
        }
    }
}

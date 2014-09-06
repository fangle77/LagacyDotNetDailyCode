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
            Core.ContainerTest.RegisterAssemblyTest();

            Console.Read();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Core;
using Pineapple.Data;

namespace Pineapple.ConsoleTest.Core
{
    class ContainerTest
    {
        public static void RegisterAssemblyTest()
        {
            Container.ResisterAssemblyType("Pineapple.Data");
            var data = Container.Resolve<VisitorData>();

            Console.WriteLine(data == null);
            if (data != null) Console.WriteLine(data.GetType());
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Util;
using System.Data;


namespace ConsoleApplication1
{
    class Program
    {

        static void TestAction()
        {
           ProductCenterKeyword.Start();
            
        }


        static void Main(string[] args)
        {
            string q = string.Empty;
            while (!"q".Equals(q, StringComparison.OrdinalIgnoreCase))
            {
                DateTime TimeStart = DateTime.Now;
                Console.WriteLine("start " + TimeStart.ToString("HH:mm:ss fff"));

                TestAction();

                Console.WriteLine("done, {0} ms", DateTime.Now.Subtract(TimeStart).TotalMilliseconds);

                Console.Write("input q to exit:\r\n");
                q = Console.ReadLine().Trim();
            }
        }
    }
}

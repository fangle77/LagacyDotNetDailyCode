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
        static void Main(string[] args)
        {
            RepeatRun(() =>
            {
			
			//"[^"]+",?|[^,]{0,},?
                Console.WriteLine(1.CompareTo(2));
                List<int> list = new List<int>(5);
                for(int i=0;i<5;i++) list.Add(i);
                list.Sort((i1,i2)=>
                              {
                                  return -1;
                              });
                list.ForEach(Console.WriteLine);
            });
        }

        static void RepeatRun(Action action)
        {
            string q = string.Empty;
            while (!"q".Equals(q, StringComparison.OrdinalIgnoreCase))
            {
                DateTime TimeStart = DateTime.Now;
                Console.WriteLine("start " + TimeStart.ToString("HH:mm:ss fff"));

                action();

                Console.WriteLine("done, " + DateTime.Now.Subtract(TimeStart).TotalMilliseconds);

                Console.Write("input q to exit:");
                q = Console.ReadLine().Trim();
            }
        }
    }
}

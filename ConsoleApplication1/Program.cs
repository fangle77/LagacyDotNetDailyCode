using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Util;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(@"E:\Code\WWW\DEV\Soap\WebSite\AJAX\AddressAjax.aspx");
            RepeatRun(() =>
            {
                List<int> list = new List<int> { 1, 2, 3, 4, 5 };
                list.Sort((l1, l2) => l1.CompareTo(l2));
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

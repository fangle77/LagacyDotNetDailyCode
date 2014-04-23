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


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(@"E:\Code\WWW\DEV\Soap\WebSite\AJAX\AddressAjax.aspx");
            RepeatRun(() =>
            {
                List<object> list = new List<object>()
                                      {
                                          "",new {A=1,B="waa\"baa'caa\r\ndaa\reaa\nfaa\\<>?/';:|}{[]+=_-)(*&^%$#@!~`",C=@"123123,ab""cd

abcd",D=DateTime.Now}
                                      };

                foreach (var o in list)
                {
                    JsonSerializerTest.SerializeTest(o);
                }
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

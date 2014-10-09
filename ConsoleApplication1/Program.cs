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
                DataTable dt = new DataTable();
                dt.Columns.Add("a");
                var dr = dt.NewRow();
                dr[0] = 1;
                dt.Rows.Add(dr);

                Console.WriteLine(dr["a"]);
                Console.WriteLine(dr.Table.Columns["b"]==null);

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

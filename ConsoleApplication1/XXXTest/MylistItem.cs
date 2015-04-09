using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.XXXTest
{
    class MylistItem
    {
        public string Sku;
        public string LastPurchaseDate;
        public string Frequency;
        public string Consuma;

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}", Sku, LastPurchaseDate, Frequency, Consuma);
        }

        public static List<MylistItem> ReadFromString(string[] content)
        {
            var result = new List<MylistItem>(content.Length);
            foreach (string line in content)
            {
                var fileds = line.Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var item = new MylistItem();
                if (fileds.Length > 0) item.Sku = fileds[0];
                if (fileds.Length > 1) item.LastPurchaseDate = fileds[1];
                if (fileds.Length > 2) item.Frequency = fileds[2];
                if (fileds.Length > 3) item.Consuma = fileds[3];
                result.Add(item);
            }
            return result;
        }

        public static void Compare()
        {
            string autoshipFile = @"E:\temp\autoship.txt";
            string mylistFile = @"E:\temp\mylist.txt";

            var autoshipList = MylistItem.ReadFromString(File.ReadAllLines(autoshipFile));
            var mylist = MylistItem.ReadFromString(File.ReadAllLines(mylistFile));

            StringBuilder sb = new StringBuilder();
            StringBuilder diff = new StringBuilder();
            foreach (var item in autoshipList)
            {
                sb.AppendLine(item.ToString());
                var mi = mylist.Find(i => string.Equals(i.Sku, item.Sku, StringComparison.OrdinalIgnoreCase));
                if (mi != null) sb.AppendLine(mi.ToString());
                sb.AppendLine("--------");

                if (mi == null
                    || !string.Equals(item.LastPurchaseDate, mi.LastPurchaseDate, StringComparison.OrdinalIgnoreCase)
                    || !string.Equals(item.Frequency, mi.Frequency, StringComparison.OrdinalIgnoreCase)
                    || !string.Equals(item.Consuma, mi.Consuma, StringComparison.OrdinalIgnoreCase)
                    )
                {
                    diff.AppendLine(item.ToString());
                    if (mi != null) diff.AppendLine(mi.ToString());
                    diff.AppendLine("-------");
                }
            }
            File.WriteAllText(@"e:\temp\result.txt", sb.ToString());
            File.WriteAllText(@"e:\temp\diffResult.txt", diff.ToString());
        }
    }
}

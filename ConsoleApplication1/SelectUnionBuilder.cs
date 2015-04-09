using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
    class SelectUnionBuilder
    {
        public static void Start()
        {
            BuildSelectUnion(1, @"C:\Users\bowen.zhang\Desktop\diapers.txt");
            BuildSelectUnion(3, @"C:\Users\bowen.zhang\Desktop\bb.txt");
        }

        private static void BuildSelectUnion(int catalog, string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            StringBuilder result = new StringBuilder(lines.Length * 256);

            StringBuilder oneElement = new StringBuilder();
            StringBuilder equel = new StringBuilder();
            foreach (string line in lines)
            {
                var ls = line.Trim().Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (ls.Length > 1)
                {
                    if (ls[0].Equals(ls[1], StringComparison.OrdinalIgnoreCase))
                    {
                        equel.AppendLine(line);
                    }
                    else
                    {
                        result.AppendLine();
                        result.AppendFormat("UNION SELECT {0}, '{1}', '{2}'", catalog, ls[0], ls[1]);
                    }
                }
                else
                {
                    oneElement.AppendLine(line);
                }
            }

            var fi = new FileInfo(fileName);
            File.WriteAllText(fileName.Replace(fi.Name, "") + fi.Name + ".result.txt", result.ToString());
            File.WriteAllText(fileName.Replace(fi.Name, "") + fi.Name + ".oneElement.txt", oneElement.ToString());
            File.WriteAllText(fileName.Replace(fi.Name, "") + fi.Name + ".equel.txt", equel.ToString());
        }
    }
}

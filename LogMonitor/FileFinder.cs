using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LogMonitor
{
    class FileFinder
    {
        public static int FindLine(string file, string content)
        {
            if (!File.Exists(file)) return 0;

            int line = 0;
            bool finded = false;
            using (StreamReader sr = new StreamReader(file))
            {
                string lineContent = sr.ReadLine();
                while ((lineContent = sr.ReadLine()) != null)
                {
                    line++;
                    if (lineContent.IndexOf(content, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        finded = true;
                        break;
                    }
                }
            }
            return finded ? line : 0;
        }

        public static List<string> Find(string direcotry, string content)
        {
            if (Directory.Exists(direcotry) == false) return new List<string>();

            var list = new List<string>();
            foreach (string file in Directory.GetFiles(direcotry))
            {
                int line = FindLine(file, content);
                if (line > 0)
                {
                    list.Add(string.Format("{0}, line {1}", file, line));
                }
            }
            return list;
        }
    }
}

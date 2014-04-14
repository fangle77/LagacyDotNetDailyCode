using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    public class TableAnalyser
    {
        public List<string> GetTableNameFromFile(string file)
        {
            if (File.Exists(file) == false) return null;
            string name = new FileInfo(file).Name;

            Regex reg = new Regex(@"(FROM|JOIN)\s+(DBO.)?\[?([\w]+)\]?");
            List<string> tableList = new List<string>();
            using (var sr = new StreamReader(file))
            {
                string content = sr.ReadToEnd().ToUpper();
                var ms = reg.Matches(content);
                if (ms.Count == 0) return tableList;

                foreach (Match m in ms)
                {
                    if (m.Success && m.Groups.Count > 3)
                    {
                        if (tableList.Contains(m.Groups[3].Value) == false)
                        {
                            tableList.Add(m.Groups[3].Value);
                        }
                    }
                }
            }

            return tableList;
        }

        public List<string> GetTableNameFromDirectory(string directory)
        {
            if (Directory.Exists(directory) == false) return null;
            var di = new DirectoryInfo(directory);
            List<string> tableNameList = new List<string>();
            foreach (var fi in di.GetFiles())
            {
                tableNameList.AddRange(GetTableNameFromFile(fi.FullName));
            }
            return tableNameList;
        }

        public void SaveTableNameFromDirecotry(string directory, string saveFile)
        {
            var list = GetTableNameFromDirectory(directory);
            if (list == null) return;
            list = DistinctList(list);
            list.Sort();
            using (var fs = File.Create(saveFile))
            {
                using (var sw = new StreamWriter(fs))
                {
                    list.ForEach(sw.WriteLine);
                }
            }
        }

        private List<string> DistinctList(List<string> list)
        {
            if (list == null) return null;
            HashSet<string> hs = new HashSet<string>(list);
            return hs.ToList();
        }
    }
}

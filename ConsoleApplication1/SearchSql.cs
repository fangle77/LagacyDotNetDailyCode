using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class SearchSql
    {
        static readonly string Paths = @"E:\Code\WWW\DEV\PrototypeWebsite\Database;E:\Code\Intranet\DEV\Database";
        static readonly string FileNames = @"";

        public static void Start()
        {
            var findReg = new Regex(@"insert\s+into\s+[dbo\.]{0,4}\s*customer_promotion_usage", RegexOptions.IgnoreCase);

            var fileList = new List<string>(FileNames.Split(" \r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            fileList.RemoveAll(string.IsNullOrWhiteSpace);
            var list = fileList.ConvertAll(f => f + ".sql");
            var fileSet = new HashSet<string>(list, StringComparer.OrdinalIgnoreCase);

            var result = new List<string>();
            foreach (string path in Paths.Split(';'))
            {
                var files = GetFilesFromDir(path, fileSet);

                result.AddRange(from f in files let content = File.ReadAllText(f) where findReg.IsMatch(content) select f);
            }

            result.ForEach(Console.WriteLine);
        }

        private static List<string> GetFilesFromDir(string dir, HashSet<string> fileSet)
        {
            var di = new DirectoryInfo(dir);
            if (di.Exists == false) return new List<string>(0);

            var list = new List<string>();
            foreach (var fi in di.GetFiles())
            {
                if (fileSet.Count > 0 && fileSet.Contains(fi.Name))
                {
                    list.Add(fi.FullName);
                }
                else
                {
                    list.Add(fi.FullName);
                }
            }

            foreach (var ddi in di.GetDirectories())
            {
                list.AddRange(GetFilesFromDir(ddi.FullName, fileSet));
            }
            return list;
        }
    }
}

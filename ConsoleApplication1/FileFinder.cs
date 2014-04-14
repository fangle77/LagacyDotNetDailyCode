using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class FileFinder
    {
        public static List<string> Find(string dir, string findContent, IEnumerable<string> fileExtendtions)
        {
            var direcotry = new DirectoryInfo(dir);
            if (direcotry.Exists == false) return null;
            if (fileExtendtions == null || fileExtendtions.Count() == 0)
            {
                return Find(direcotry, findContent, null);
            }
            else
            {
                List<string> results = new List<string>();
                foreach (string extend in fileExtendtions)
                {
                    results.AddRange(Find(direcotry, findContent, extend));
                }
                return results;
            }
        }

        private static List<string> Find(DirectoryInfo directory, string findContent, string fileExtendtion)
        {
            if (directory.Exists == false) return null;

            List<string> results = new List<string>();

            FileInfo[] files = !string.IsNullOrEmpty(fileExtendtion) ? directory.GetFiles(fileExtendtion) : directory.GetFiles();

            var list = FindFiles(files, findContent);
            if (list != null) results.AddRange(list);

            var subFolders = directory.GetDirectories();
            foreach (var d in subFolders)
            {
                var l = Find(d, findContent, fileExtendtion);
                results.AddRange(l);
            }

            return results;
        }

        private static List<string> FindFiles(IEnumerable<FileInfo> files, string findContent)
        {
            if (files == null || files.Count() == 0 || string.IsNullOrEmpty(findContent)) return null;

            List<Action> actions = new List<Action>();
            List<string> results = new List<string>(files.Count());
            foreach (var f in files)
            {
                actions.Add(() =>
                {
                    string result = FindFile(f, findContent);
                    if (!string.IsNullOrEmpty(result)) results.Add(result);
                });
            }
            TaskUtility.ParallelTask(actions, Environment.ProcessorCount);

            return results;
        }

        private static HashSet<string> NotToFindType = new HashSet<string>(".exe,.dll,.png,.jpg,.jpeg,.sln".Split(','), StringComparer.OrdinalIgnoreCase);

        private static string FindFile(FileInfo file, string findContent)
        {
            if (file.Exists == false || string.IsNullOrEmpty(findContent)) return string.Empty;
            if (NotToFindType.Contains(file.Extension)) return string.Empty;
            int i = 0;
            string resultFormat = "{0}({1}) {2} ";
            string result = string.Empty;
            
            var lines = File.ReadAllLines(file.FullName);
            foreach (string line in lines)
            {
                i++;
                if (line.IndexOf(findContent, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result = string.Format(resultFormat, file.FullName, i, line);
                    break;
                }
            }

            return result;
        }
    }

    class WWWFileFinder
    {
        public static void Start()
        {
            Logger log = new Logger("results" + DateTime.Now.ToString("hhmmssfff"));
            var list = FileFinder.Find(@"e:\Code\WWW\dev", "MercadoSearch", new List<string>() { "*.cs", "*.js", "*.vm", "*.svm" });
            list.ForEach(log.Log);
            log.FlushToFile();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class TextFinder
    {
        public static List<string> Find(string path, string searchTearm, bool withSub)
        {
            List<string> list = new List<string>();
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                foreach (var fi in directory.GetFiles())
                {
                    list.AddRange(FindInFile(fi.FullName, searchTearm));
                }
                if (withSub)
                {
                    foreach (DirectoryInfo di in directory.GetDirectories())
                    {
                        list.AddRange(Find(di.FullName, searchTearm, true));
                    }
                }
            }
            else
            {
                list.AddRange(FindInFile(path, searchTearm));
            }
            return list;
        }

        private static List<string> FindInFile(string filePath, string searchTearm)
        {
            if (File.Exists(filePath) == false) return new List<string>();
            if (string.IsNullOrEmpty(searchTearm)) return new List<string>();

            string results = string.Empty;
            using (var sr = new StreamReader(filePath))
            {
                int lineNum = 0;
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    lineNum++;
                    if (line.IndexOf(searchTearm, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        results = string.Format("{0}\t{1}", new FileInfo(filePath).Name, lineNum);
                        break;
                    }
                }
            }
            return new List<string>() { results };
        }
    }

    class FileNameFinder
    {
        private static readonly Dictionary<string, List<FileInfo>> allFileDic = new Dictionary<string, List<FileInfo>>(StringComparer.OrdinalIgnoreCase);

        private static void Initial()
        {
            if (allFileDic.Count > 0) return;

            DirectoryInfo baseDir = new DirectoryInfo(@"e:\code\www\dev");
            AddToDir(baseDir);
        }

        private static void AddToDir(DirectoryInfo di)
        {
            var files = di.GetFiles();

            foreach (var fi in files)
            {
                if (allFileDic.ContainsKey(fi.Name))
                {
                    allFileDic[fi.Name].Add(fi);
                }
                else
                {
                    allFileDic.Add(fi.Name, new List<FileInfo>() { fi });
                }
            }

            var dirs = di.GetDirectories();
            foreach (DirectoryInfo di1 in dirs)
            {
                AddToDir(di1);
            }
        }

        public static List<FileInfo> Find(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return new List<FileInfo>();
            Initial();
            var list = new List<FileInfo>();
            if (allFileDic.ContainsKey(fileName)) list.AddRange(allFileDic[fileName]);

            foreach (string name in allFileDic.Keys)
            {
                if (name.Equals(fileName, StringComparison.OrdinalIgnoreCase)) continue;
                if (name.IndexOf(fileName, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    list.AddRange(allFileDic[name]);
                }
            }
            return list;
        }
    }
}

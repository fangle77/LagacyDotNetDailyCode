using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MySharper.Model;

namespace MySharper.Index
{
    class FilePathIndexer : IIndexer
    {
        public void Index(List<string> args)
        {
            StartInitial(args);
        }

        private static void StartInitial(List<string> initialItems)
        {
            if (initialItems == null || initialItems.Count == 0) return;
            if (!FileItemContainer.IsEmpty) return;

            foreach (string item in initialItems)
            {
                InitialFileItems(GetAllFiles(item));
            }
        }

        private static void InitialFileItems(List<string> allFiles)
        {
            if (allFiles == null || allFiles.Count == 0) return;

            Dictionary<string, List<FileItem>> allFileItems = new Dictionary<string, List<FileItem>>(allFiles.Count, StringComparer.OrdinalIgnoreCase);
            foreach (string file in allFiles)
            {
                int idx = file.LastIndexOf('\\');
                if (idx < 0) continue;
                string fileName = file.Substring(idx + 1);
                FileItem item = new FileItem(fileName, file);
                if (allFileItems.ContainsKey(fileName))
                {
                    allFileItems[fileName].Add(item);
                }
                else
                {
                    allFileItems.Add(fileName, new List<FileItem>() { item });
                }
            }

            foreach (var items in allFileItems.Values)
            {
                items.ForEach(FileItemContainer.AddItem);
            }
        }

        private static List<string> GetAllFiles(string item)
        {
            List<Project> projects = null;
            if (Directory.Exists(item))
            {
                projects = new List<Project> { new Project(eProjectType.FileSystem, item) };
            }
            else if (File.Exists(item))
            {
                if (item.EndsWith(".sln", StringComparison.OrdinalIgnoreCase))
                {
                    projects = Solution.GetAllProjects(item);
                }
                else if (item.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase))
                {
                    projects = new List<Project> { new Project(eProjectType.Project, item) };
                }
            }

            if (projects == null || projects.Count == 0) return null;

            var allFiles = GetAllFiles(projects);
            return allFiles;
        }

        private static List<string> GetAllFiles(List<Project> projects)
        {
            if (projects == null) return new List<string>();
            List<string> allFiles = new List<string>();
            foreach (var p in projects)
            {
                var list = p.GetAllFiles();
                if (list != null) allFiles.AddRange(list);
            }
            return allFiles;
        }

        private static List<string> GetClassStructEnum(string classFile)
        {
            string content = File.ReadAllText(classFile);
            var reg = new Regex(@"class\s+(\w+)|enum\s+(\w+)|struct\s+(\w+)");
            var matchs = reg.Matches(content);
            List<string> list = new List<string>();
            foreach (Match m in matchs)
            {
                if (m.Success)
                {

                }
            }
            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace MySharper.Index
{
    enum eProjectType
    {
        FileSystem = 1,
        Project = 2
    }

    class Project
    {
        private static readonly HashSet<string> UnIndexDirecotry = new HashSet<string>("bin,obj,Log Files,Images,PDF,SearchPerformanceTest,pwr,webfont".Split(','), StringComparer.OrdinalIgnoreCase);
        private static readonly HashSet<string> UnIndexFileType = new HashSet<string>(".exe,.dll,.bin,.cmd,.pdb,.xml,.png,.jpeg,.jpg,.gif,.cur".Split(','), StringComparer.OrdinalIgnoreCase);

        public Project() { }
        public Project(eProjectType projectType, string path)
        {
            this.ProjectType = projectType;
            this.Path = path;
        }

        public eProjectType ProjectType { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return string.Format("ProjectType={0}, Path={1}", ProjectType, Path);
        }

        public List<string> GetAllFiles()
        {
            switch (ProjectType)
            {
                case eProjectType.FileSystem: return GetFileFromFileSystem(Path);
                case eProjectType.Project: return GetFileFromProject(Path);
            }
            return null;
        }

        private static List<string> GetFileFromFileSystem(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            if (dir.Exists == false) return null;

            var files = dir.GetFiles();
            List<string> allFiles = new List<string>(files.Length);
            foreach (var file in files)
            {
                if (UnIndexFileType.Contains(file.Extension)) continue;
                allFiles.Add(file.FullName);
            }

            var dirs = dir.GetDirectories();
            foreach (var d in dirs)
            {
                if (UnIndexDirecotry.Contains(d.Name)) continue;
                var list = GetFileFromFileSystem(d.FullName);
                if (list != null) allFiles.AddRange(list);
            }

            return allFiles;
        }

        private static List<string> GetFileFromProject(string projectFile)
        {
            FileInfo fileInfo = new FileInfo(projectFile);
            if (fileInfo.Exists == false) return null;

            var includeReg = new Regex(@"<Compile Include=""([^""]+)""|<DependentUpon>([\w.]+)</DependentUpon>", RegexOptions.IgnoreCase);

            string content = File.ReadAllText(projectFile);

            var matchs = includeReg.Matches(content);

            List<string> allFiles = new List<string>(matchs.Count);
            foreach (Match m in matchs)
            {
                if (m.Success && !string.IsNullOrEmpty(m.Groups[1].Value))
                {
                    allFiles.Add(System.IO.Path.Combine(fileInfo.DirectoryName, m.Groups[1].Value));
                }
            }
            return allFiles;
        }
    }
}

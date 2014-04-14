using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;

namespace MySharper
{
    class Solution
    {
        public static List<Project> GetAllProjects(string solutionFile)
        {
            FileInfo fi = new FileInfo(solutionFile);
            if (fi.Exists == false) return null;

            string content = File.ReadAllText(fi.FullName);

            return AnalyseProjects(content, fi.DirectoryName);
        }

        private static List<Project> AnalyseProjects(string content, string slnPath)
        {
            if (string.IsNullOrEmpty(content)) return null;

            Regex projectReg = new Regex("\"([^\"]+.csproj)\"", RegexOptions.IgnoreCase);
            Regex websiteReg = new Regex(@".AspNetCompiler.PhysicalPath = ""([\w\\]+)""", RegexOptions.IgnoreCase);

            List<Project> projectList = new List<Project>();

            var matchWebSite = websiteReg.Matches(content);
            HashSet<string> existWebSite = new HashSet<string>();
            foreach (Match m in matchWebSite)
            {
                if (m.Success)
                {
                    string v = m.Groups[1].Value;
                    if (existWebSite.Contains(v)) continue;
                    existWebSite.Add(v);

                    projectList.Add(new Project() { ProjectType = eProjectType.FileSystem, Path = CommonFunc.CombinePath(slnPath, v) });
                }
            }

            var matchProject = projectReg.Matches(content);
            foreach (Match m in matchProject)
            {
                if (m.Success)
                {
                    projectList.Add(new Project() { ProjectType = eProjectType.Project, Path = CommonFunc.CombinePath(slnPath, m.Groups[1].Value) });
                }
            }

            return projectList;
        }
    }
}

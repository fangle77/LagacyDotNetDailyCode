using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServerManager
{
    class MSBuildCommand
    {
        private static string msBuildFileFullPath = @"""C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe""";

        public static string MSBuildFileFullPath
        {
            get { return msBuildFileFullPath; }
            set
            {
                msBuildFileFullPath = value;
                if (!string.IsNullOrEmpty(msBuildFileFullPath))
                {
                    msBuildFileFullPath = "\"" + msBuildFileFullPath.Trim('"') + "\"";
                }
            }
        }

        private static string BuildSolutionCommand = " \"{0}\" ";

        public string BuildSolution(string solutionFile)
        {
            string args = string.Format(BuildSolutionCommand, solutionFile);
            string results = AppProcessor.StartNewCMDProcess(MSBuildFileFullPath.Replace(".exe", ""), args);
            return results;
        }
    }
}

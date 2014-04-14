using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class SVNVersion
    {
        public static void GetVersion()
        {
            string FolderName = @"E:\Code\WWWReview\QA";

            const string DB = "wc.db";
            const string PATTERN = "/!svn/ver/(?'version'[0-9]*)/";
            int maxVer = int.MinValue;
            string SvnSubfolder = FolderName + "\\.svn";

            //DirectoryInfo di = new DirectoryInfo(FolderName);
            //while (di != null)
            //{
            //    string psvn = di.FullName + "\\.svn";
            //    Console.WriteLine(psvn + ",\t" + Directory.Exists(psvn));
            //    di = di.Parent;
            //}

            string EntriesFile = Directory.GetFiles(SvnSubfolder, DB).FirstOrDefault();

            if (!string.IsNullOrEmpty(EntriesFile))
            {
                byte[] fileData = File.ReadAllBytes(EntriesFile);
                string fileDataString = Encoding.Default.GetString(fileData);

                Regex regex = new Regex(PATTERN);

                foreach (Match match in regex.Matches(fileDataString))
                {
                    string version = match.Groups["version"].Value;

                    int curVer;
                    if (int.TryParse(version, out curVer))
                        if (curVer > maxVer)
                            maxVer = curVer;
                }
                Console.WriteLine(maxVer);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace WebServerManager
{
    public class SiteFile
    {
        private static List<string> SiteLists = new List<string>()
                                                    {
                                                        "diapers","beautybar","soap","wag"
                                                        ,"yoyo","casa","jump","green","look","book"
                                                    };

        public static List<FileInfo> GetFilesBySites(string oraginalFile)
        {
            string lowerFile = oraginalFile.ToLower();
            string baseSite = "";
            foreach(string site in SiteLists)
            {
                if(lowerFile.Contains("\\"+site+"\\"))
                {
                    baseSite = site;
                    break;
                }
            }

            var fileInfoList = new List<FileInfo>(SiteLists.Count);
            foreach (string site in SiteLists)
            {
                string siteFile = Regex.Replace(oraginalFile, "\\\\" + baseSite + "\\\\", "\\" + site + "\\", RegexOptions.IgnoreCase);
                if (siteFile != oraginalFile)
                {
                    fileInfoList.Add(new FileInfo(siteFile));
                }
            }
            return fileInfoList;
        }
    }
}

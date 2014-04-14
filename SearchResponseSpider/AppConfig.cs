using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace SearchResponseSpider
{
    class AppConfig
    {
        private static readonly AppConfig config = new AppConfig();

        public static AppConfig Instance
        {
            get { return config; }
        }

        private Regex plpRegex;
        private int threadCount;
        private Dictionary<string, string> taskDomains;
        private Dictionary<string, string> mercadoDomains;
        private Dictionary<string, DirectoryInfo> siteResultsDir;
        private DirectoryInfo urlDirectory;
        private bool logTaskString = false;
        private static List<string> sites;
        private int timeoutSecond;

        private static Dictionary<string, string> AliasSiteNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                                                                       {
                                                                           {"beauty","beautybar"},
                                                                           {"jump","afterschool"},
                                                                           {"green","vine"},
                                                                           {"book","bookworm"},
                                                                       };

        private AppConfig()
        {
            sites = new List<string>()
                        {
                            "Diapers",
                            "Soap",
                            "BeautyBar",
                            "Wag",
                            "Yoyo",
                            "Casa",
                            "AfterSchool",
                            "Vine",
                            "BookWorm",
                            "Look"
                        };
            Initial();
        }

        private void Initial()
        {
            int.TryParse(ConfigurationManager.AppSettings["ThreadCount"], out threadCount);
            if (threadCount <= 0) threadCount = 1;

            List<string> plpPages = ConfigurationManager.AppSettings["plp-page"].Split(',').ToList();
            plpRegex = BuildPLPRegex(plpPages);
            logTaskString = "true".Equals(ConfigurationManager.AppSettings["logTaskString"], StringComparison.OrdinalIgnoreCase);

            taskDomains = BuildTaskConfig();

            mercadoDomains = BuildMercadoConfig(ConfigurationManager.AppSettings["domain"], "mercado-");

            int.TryParse(ConfigurationManager.AppSettings["requestTimeOut"], out timeoutSecond);
            if (timeoutSecond == 0) timeoutSecond = 300;


            urlDirectory = GetDirectory("url-directory");
            InitResultDir();
        }

        private void InitResultDir()
        {
            var dirs = BuildResultConfig(ConfigurationManager.AppSettings["result-directory"], "result-");
            siteResultsDir = new Dictionary<string, DirectoryInfo>(StringComparer.OrdinalIgnoreCase);
            foreach (string key in dirs.Keys)
            {
                siteResultsDir.Add(key, new DirectoryInfo(dirs[key]));
            }
        }

        public int ThreadCount
        {
            get { return threadCount; }
        }

        public int TimeOutSecond
        {
            get { return timeoutSecond; }
        }

        public bool LogTaskString
        {
            get { return logTaskString; }
        }

        public DirectoryInfo UrlDirectory
        {
            get { return urlDirectory; }
        }

        public bool IsPLPUrl(string url)
        {
            return plpRegex != null && !string.IsNullOrEmpty(url) && plpRegex.IsMatch(url);
        }

        public string GetMercadoDomain(string pyramid)
        {
            pyramid = GetPyramid(pyramid);
            if (mercadoDomains == null || mercadoDomains.ContainsKey(pyramid) == false) return string.Empty;
            return mercadoDomains[pyramid];
        }

        public string GetTaskDomain(string pyramid)
        {
            pyramid = GetPyramid(pyramid);
            if (taskDomains == null || taskDomains.ContainsKey(pyramid) == false) return string.Empty;
            return taskDomains[pyramid];
        }

        private Regex BuildPLPRegex(List<string> plpPaths)
        {
            if (plpPaths == null || plpPaths.Count == 0) return null;
            string regFormat = "({0}[^\\w]?)|";
            StringBuilder sb = new StringBuilder();
            plpPaths.ForEach(s =>
            {
                sb.AppendFormat(regFormat, s);
            });
            return new Regex(sb.Remove(sb.Length - 1, 1).ToString());
        }

        private Dictionary<string, string> BuildTaskConfig()
        {
            Dictionary<string, string> tasksDic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (string key in sites)
            {
                string site = ConfigurationManager.AppSettings["task-" + key];
                if (!string.IsNullOrEmpty(site))
                {
                    tasksDic.Add(key, site);
                }
            }

            return tasksDic;
        }

        private Dictionary<string, string> BuildMercadoConfig(string format, string keyPrefix)
        {
            if (string.IsNullOrEmpty(format)) return null;
            Dictionary<string, string> sitesDic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (string key in sites)
            {
                string site = ConfigurationManager.AppSettings[keyPrefix + key];
                if (!string.IsNullOrEmpty(site))
                {
                    sitesDic.Add(key, format.Replace("{site}", site));
                }
            }

            return sitesDic;
        }

        private Dictionary<string, string> BuildResultConfig(string format, string keyPrefix)
        {
            if (string.IsNullOrEmpty(format)) return null;
            Dictionary<string, string> sitesDic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (string key in sites)
            {
                string site = ConfigurationManager.AppSettings[keyPrefix + key];
                if (!string.IsNullOrEmpty(site))
                {
                    sitesDic.Add(key, format.Replace("{site}", site));
                }
            }

            return sitesDic;
        }

        private DirectoryInfo GetDirectory(string appKey)
        {
            string dir = ConfigurationManager.AppSettings[appKey];
            if (string.IsNullOrEmpty(dir) || dir == "\\") return new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            return new DirectoryInfo(dir);
        }

        public DirectoryInfo GetSiteDirectory(string site)
        {
            if (siteResultsDir == null || siteResultsDir.ContainsKey(site) == false)
                return new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            return siteResultsDir[site];
        }

        private string GetPyramid(string pyramid)
        {
            if (string.IsNullOrEmpty(pyramid)) return pyramid;
            if (AliasSiteNames.ContainsKey(pyramid))
            {
                return AliasSiteNames[pyramid];
            }
            return pyramid;
        }

        public string GetWebSiteConfig(string pyramid, out string searchInterface)
        {
            pyramid = GetPyramid(pyramid);
            string siteBase = ConfigurationManager.AppSettings["website-directory"];
            siteBase = siteBase.Replace("{site}", ConfigurationManager.AppSettings["result-" + pyramid]);

            searchInterface = ConfigurationManager.AppSettings["SearchInterface-" + pyramid];

            return siteBase;
        }
    }
}

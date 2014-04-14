using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace WebServerManager
{
    public class WebDevServerBuilder
    {
        public static string WebDev_WebServer_Path = "";
        public static string WebDev_WebServer_Exe = "";

        private static readonly Dictionary<string, Process> SiteProcessDic = new Dictionary<string, Process>();

        public static void Start(Site site)
        {
            if (site == null) return;
            //if (SiteProcessDic.ContainsKey(site.Name)) return;

            //C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0\WebDev.WebServer40 /port:9010 /path: "E:\DEV\WWW\Look\WebSite" /vpath:"/"

            string exe = WebDev_WebServer_Exe.LastIndexOf(".exe", StringComparison.OrdinalIgnoreCase) > 0 ? "" : ".exe";


            string fileName = WebDev_WebServer_Path + WebDev_WebServer_Exe + exe;
            string arguments = string.Format("/port:{0} /path:\"{1}\" /vpath:\"/\" ", site.Port, site.Path);

            var p = AppProcessor.StartNewProcess(fileName, arguments);
            p.WaitForExit(1000);
            p.Close();
        }
    }

    [Serializable]
    public class WebDevInfo
    {
        private string path = @"C:\Program Files (x86)\Common Files\microsoft shared\DevServer\10.0\";
        private string exename = "WebDev.WebServer40.exe";

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        public string ExeName
        {
            get { return exename; }
            set { exename = value; }
        }

        public string GetFullName()
        {
            return Path + ExeName;
        }
        public void SetFullName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return;
            int idx = fullName.LastIndexOf('\\');
            if (idx > 0)
            {
                Path = fullName.Substring(0, idx + 1);
                ExeName = fullName.Substring(idx + 1);
            }
        }

        public string VSDevenvFileFullPath = @"""C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\devenv.exe""";
    }

    public class WebDevBuilderSerializer
    {
        private static readonly string FileName = System.Environment.CurrentDirectory + "\\WebDev.xml";

        public static void Serialize(WebDevInfo info)
        {
            var list = new List<WebDevInfo>() { info };
            string xmlString = Serializer.Serialize<WebDevInfo>(list);
            File.WriteAllText(FileName, xmlString);
        }

        public static WebDevInfo Deserialize()
        {
            if (File.Exists(FileName) == false) return new WebDevInfo();

            string xmlString = File.ReadAllText(FileName);
            var list = Serializer.Deserizlize<WebDevInfo>(xmlString);
            return list != null && list.Count > 0 ? list[0] : new WebDevInfo();
        }
    }
}

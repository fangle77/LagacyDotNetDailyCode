using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApplication1.XmlTest
{
    class AppConfigTest
    {
        private static readonly string ConfigFile = "XmlTest/AppsVersion.config";

        public static AppVersionConfig ReadConfig()
        {
            string xmlContent = File.ReadAllText(ConfigFile);
            return XMLUtility.Deserialize<AppVersionConfig>(xmlContent);
        }

        public static void Test()
        {
            var aConfig = ReadConfig();
            Console.WriteLine(aConfig);
        }
    }


    public class AppVersionConfig
    {
        public List<AppVersion> AppVersions { get; set; }
        public List<AppUrl> AppUrls { get; set; }
    }

    public class AppVersion
    {
        [XmlAttribute("platform")]
        public string Platform { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlElement("Message")]
        public string Message { get; set; }

        [XmlElement("Url")]
        public string Url { get; set; }
    }

    public class AppUrl
    {
        [XmlAttribute("platform")]
        public string Platform { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        private string url;
        [XmlText]
        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                if (url != null) url = url.Trim();
            }
        }
    }
}

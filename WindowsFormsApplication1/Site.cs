using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace WebServerManager
{
    [Serializable]
    public class Site
    {
        public string Name { get; set; }

        public int Port { get; set; }

        public string Path { get; set; }

        public string Solution { get; set; }
    }

    public class SiteSerialize
    {
        private static string FileName = System.Environment.CurrentDirectory + "\\sites.xml";

        public static void Serialize(List<Site> sites)
        {
            string xmlString = Serializer.Serialize<Site>(sites);
            File.WriteAllText(FileName, xmlString);
        }

        public static List<Site> Deserialize()
        {
            if (File.Exists(FileName) == false) return new List<Site>();

            string xmlString = File.ReadAllText(FileName);
            return Serializer.Deserizlize<Site>(xmlString) ?? new List<Site>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Text;
using System.Xml.Serialization;

namespace WebServerManager
{
    public class Serializer
    {
        public static string Serialize<T>(List<T> list)
        {
            if (list == null || list.Count == 0) return string.Empty;

            string xmlString = string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            using (MemoryStream ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, list);
                xmlString = Encoding.UTF8.GetString(ms.ToArray());
            }
            return xmlString;
        }

        public static List<T> Deserizlize<T>(string xmlString)
        {
            if(string.IsNullOrEmpty(xmlString)) return default(List<T>);
            List<T> list = null;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof (List<T>));
            using(Stream xmlStream=new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                using(XmlReader xmlReader=XmlReader.Create(xmlStream))
                {
                    object obj = xmlSerializer.Deserialize(xmlReader);
                    list = (List<T>) obj;
                }
            }
            return list;
        }
    }
}

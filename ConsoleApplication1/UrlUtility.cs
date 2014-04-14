using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class UrlUtility
    {
        public static string AddQuery(string originalUrl, string name, string value)
        {
            if (string.IsNullOrEmpty(originalUrl)) return originalUrl;
            if (string.IsNullOrEmpty(name)) return originalUrl;

            Regex queryReg = new Regex(string.Format(@"[^\w]{0}\s*=\s*{1}", name, value), RegexOptions.IgnoreCase);
            if (queryReg.IsMatch(originalUrl)) return originalUrl;

            string[] parts = originalUrl.Split('#');
            StringBuilder url = new StringBuilder(originalUrl.Length + 10);
            url.AppendFormat("{0}{1}{2}={3}", parts[0].Trim(), parts[0].LastIndexOf('?') >= 0 ? "&" : "?", name, value);
            for (int i = 1; i < parts.Length; i++)
            {
                url.AppendFormat("#{0}", parts[i]);
            }
            return url.ToString();
        }

        public static string GetDomain(string url)
        {
            if (string.IsNullOrEmpty(url)) return url;

            int length = url.Length;
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                if (url[i] == '/' && ((i > 0 && url[i - 1] != '/') && (i < length - 1 && url[i + 1] != '/')))
                {
                    break;
                }
                sb.Append(url[i]);
            }
            return sb.ToString();
        }
        
//        public static na GetUrlInfo(string url)
//        {
//        	if(string.IsNullOrEmpty(url)) return new Dictionary<string,string>();
//        	
//        	url=url.Replace(GetDomain(url),"");
//        	
//        	if(string.IsNullOrEmpty(url)) return new Dictionary<string,string>();
//        	
//        	Dictionary<string,string> dic = new Dictionary<string,string>();
//        	
//        	string[] infos = url.Split("/#&".ToCharArray());
//        	foreach(string info in infos)
//        	{
//        		if(info.Trim()==string.Empty) continue;
//        		
//        	}
//        }
    }
}

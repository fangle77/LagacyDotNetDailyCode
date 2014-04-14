using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TNSConfig
{
    class Sqlnetora
    {
        public static string GetDefaultDomain(string tnsNamePath)
        {
            if (string.IsNullOrEmpty(tnsNamePath)) return "";

            if (File.Exists(tnsNamePath) == false) return "";

            tnsNamePath = tnsNamePath.ToLower();
            if (tnsNamePath.LastIndexOf("tnsnames.ora") < 0) return "";

            string sqlnetora = tnsNamePath.Replace("tnsnames.ora", "sqlnet.ora");

            if (File.Exists(sqlnetora) == false) return "";

            try
            {
                string sqlnet = File.ReadAllText(sqlnetora);

                var reg = new System.Text.RegularExpressions.Regex(@"([#]*)[\t\ ]*NAMES.DEFAULT_DOMAIN\s*=\s*([a-zA-Z0-9_.]+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                var m = reg.Match(sqlnet);
                if (m.Success)
                {
                    if (m.Groups.Count >= 3)
                    {
                        if (m.Groups[1].Value != null && m.Groups[1].Value.IndexOf('#') >= 0) return "";

                        if (m.Groups[2].Value == null) return "";
                        return m.Groups[2].Value.Trim().ToUpper();
                    }
                }
                return "";
            }
            catch { return ""; }
        }
    }
}

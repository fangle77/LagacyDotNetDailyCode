using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace SearchResponseSpider
{
    class ConfigModify
    {
        private readonly string webConfigFile;
        private readonly string log4netFile;
        private readonly string siteSettingFile;
        private readonly string searchInterFace;

        private string restoreLog4Net;
        private string restoreSiteSetting;

        public ConfigModify(string webSiteBaseDir, string searchInterFace)
        {
            this.searchInterFace = searchInterFace;
            webConfigFile = Path.Combine(webSiteBaseDir, "web.config");
            log4netFile = Path.Combine(webSiteBaseDir, @"Config\GeneralConfig\log4net.config");
            siteSettingFile = Path.Combine(webSiteBaseDir, @"Config\SiteSetting.config");
        }

        public void Modify()
        {
            ModifyLog4Net();
            ModifySiteSetting();
            ModifyWebConfigToRestartIIS();
        }

        public void Restore()
        {
            if (!string.IsNullOrEmpty(restoreLog4Net)) File.WriteAllText(log4netFile, restoreLog4Net);
            if (!string.IsNullOrEmpty(restoreSiteSetting)) File.WriteAllText(siteSettingFile, restoreSiteSetting);
            ModifyWebConfigToRestartIIS();
        }

        private void ModifyWebConfigToRestartIIS()
        {
            if (File.Exists(webConfigFile) == false)
            {
                Console.WriteLine("not exist web.config:" + webConfigFile);
                return;
            }

            if (string.IsNullOrEmpty(restoreLog4Net) && string.IsNullOrEmpty(restoreSiteSetting))
            {
                return;
            }

            string config = File.ReadAllText(webConfigFile);
            if (config.LastIndexOfAny(" \r\n".ToCharArray()) > 0)
            {
                config = config.Trim();
            }
            else
            {
                config = config + "  ";
            }
            File.WriteAllText(webConfigFile, config);
        }

        private void ModifyLog4Net()
        {
            if (File.Exists(log4netFile) == false)
            {
                Console.WriteLine("not exist log4net.config" + log4netFile);
                return;
            }
            string log4netContent = File.ReadAllText(log4netFile);
            var debugReg = new Regex(@"<level value=""DEBUG""\s*/>", RegexOptions.IgnoreCase);

            if (debugReg.IsMatch(log4netContent)) return;

            restoreLog4Net = log4netContent;
            Regex reg = new Regex(@"<level\s*value=""\w+""\s*/>");



            log4netContent = reg.Replace(log4netContent, "<level value=\"DEBUG\"/>");

            File.WriteAllText(log4netFile, log4netContent);
        }

        private void ModifySiteSetting()
        {
            if (File.Exists(siteSettingFile) == false)
            {
                Console.WriteLine("not exist siteSetting.config" + siteSettingFile);
                return;
            }

            string siteSettingContent = File.ReadAllText(siteSettingFile);
            var existReg = new Regex("<add\\s+key=\"SearchInterface\"\\s+value=\"" + searchInterFace + "\"\\s*/>",RegexOptions.IgnoreCase);
            if(existReg.IsMatch(siteSettingContent)) return;

            restoreSiteSetting = siteSettingContent;

            Regex reg = new Regex("<add\\s+key=\"SearchInterface\"\\s*value=\"[^\"]+\"\\s*/>");
            siteSettingContent = reg.Replace(siteSettingContent, "<add key=\"SearchInterface\" value=\"" + searchInterFace + "\" />");

            File.WriteAllText(siteSettingFile, siteSettingContent);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Business;

namespace Pineapple.Service
{
    public class CompanyInfoService
    {
        [Dependency]
        public CompanyInfoManager CompanyInfoManager { protected get; set; }

        public dynamic LoadCompanyInfo()
        {
            return CompanyInfoManager.LoadCompanyInfo();
        }

        public bool SaveCompanyInfo(string key, string value)
        {
            return CompanyInfoManager.SaveCompanyInfo(key, value);
        }

        public bool DeleteCompanyInfo(string key)
        {
            return CompanyInfoManager.DeleteCompanyInfo(key);
        }
    }
}

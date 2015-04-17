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
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            {
                return false;
            }
            return CompanyInfoManager.SaveCompanyInfo(key.Trim(), value.Trim());
        }

        public bool DeleteCompanyInfo(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }
            return CompanyInfoManager.DeleteCompanyInfo(key.Trim());
        }
    }
}

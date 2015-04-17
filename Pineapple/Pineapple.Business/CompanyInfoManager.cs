using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Core.Cache;
using Pineapple.Data;

namespace Pineapple.Business
{
    public class CompanyInfoManager
    {
        private INoSqlData noSqlData;

        [Dependency]
        public INoSqlData NoSqlData
        {
            protected get { return CreateCompanyInfo(); }
            set { noSqlData = value; }
        }

        [Cache("NoSqlData", "CompanyInfo", CacheMode.Local)]
        public virtual INoSqlData CreateCompanyInfo()
        {
            return noSqlData.CreateNoSqlData("CompanyInfo");
        }

        [Cache("CompanyInfo", "CompanyInfo", CacheMode.Local)]
        public dynamic LoadCompanyInfo()
        {
            return NoSqlData.LoadDynamicModel();
        }

        [Cache("CompanyInfo", "CompanyInfo", CacheMode.Local, CacheType.Clear)]
        public bool SaveCompanyInfo(string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
            {
                return false;
            }
            return NoSqlData.Save(key.Trim(), value.Trim());
        }

        [Cache("CompanyInfo", "CompanyInfo", CacheMode.Local, CacheType.Clear)]
        public bool DeleteCompanyInfo(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }
            return NoSqlData.Delete(key.Trim());
        }
    }
}

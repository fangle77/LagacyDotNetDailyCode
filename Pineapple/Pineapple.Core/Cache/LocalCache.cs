using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.BackingStoreImplementations;
using Microsoft.Practices.EnterpriseLibrary.Caching.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Pineapple.Core.Cache
{
    public class LocalCache : ICache
    {
        readonly Dictionary<string, ICacheManager> cacheManagers = new Dictionary<string, ICacheManager>();

        public bool Contain(string group, string key)
        {
            var cacheManager = GetCacheManagerByGroupName(group);
            if (cacheManager != null)
            {
                return cacheManager.Contains(key);
            }
            return false;
        }

        public object Get(string group, string key)
        {
            var cacheManager = GetCacheManagerByGroupName(group);
            if (cacheManager != null) { return cacheManager.GetData(key); }
            return null;
        }

        public void Add(string group, string key, object value)
        {
            var cacheManager = GetCacheManagerByGroupName(group);
            if (cacheManager != null)
            {
                cacheManager.Add(key, value);
            }
        }

        public void Remove(string group, string key)
        {
            var cacheManager = GetCacheManagerByGroupName(group);
            if (cacheManager != null)
            {
                cacheManager.Remove(key);
            }
        }

        public void Clear()
        {
            lock (cacheManagers)
            {
                foreach (string key in cacheManagers.Keys)
                {
                    cacheManagers[key].Remove(key);
                }

                cacheManagers.Clear();
            }
        }

        private ICacheManager GetCacheManagerByGroupName(string groupName)
        {
            lock (cacheManagers)
            {
                if (cacheManagers.ContainsKey(groupName))
                    return cacheManagers[groupName];

                InitCacheManagers();

                if (cacheManagers.ContainsKey(groupName))
                    return cacheManagers[groupName];

                var cacheManager = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>();
                if (cacheManager != null)
                    cacheManagers.Add(groupName, cacheManager);
                return cacheManager;
            }
        }

        private void InitCacheManagers()
        {
            if (cacheManagers.Count > 0) return;

            var section = ConfigurationManager.GetSection(CacheManagerSettings.SectionName) as CacheManagerSettings;
            if (section == null) throw new ConfigurationException("Read section" + CacheManagerSettings.SectionName + " failed!");

            foreach (var managerConfig in section.CacheManagers)
            {
                if (cacheManagers.ContainsKey(managerConfig.Name) == false)
                {
                    var cacheManager = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>(managerConfig.Name);
                    if (cacheManager != null)
                    {
                        cacheManagers.Add(managerConfig.Name, cacheManager);
                    }
                }
            }
        }
    }
}

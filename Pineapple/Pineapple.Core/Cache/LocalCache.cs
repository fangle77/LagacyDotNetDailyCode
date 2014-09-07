using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Core.Cache
{
    public class LocalCache : ICache
    {
        private static readonly Dictionary<string, object> CacheObjects =
            new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        public bool Contain(string key)
        {
            return CacheObjects.ContainsKey(key);
        }

        public object Get(string key)
        {
            return CacheObjects.ContainsKey(key) ? CacheObjects[key] : null;
        }

        public void Add(string key, object value)
        {
            if (CacheObjects.ContainsKey(key)) CacheObjects[key] = value;
            else
            {
                CacheObjects.Add(key, value);
            }
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }


        public void Clear()
        {
            CacheObjects.Clear();
        }

        public List<string> CacheKeys
        {
            get { return CacheObjects.Keys.ToList(); }
        }
    }
}

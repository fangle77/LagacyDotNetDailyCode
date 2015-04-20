using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Pineapple.Core.Cache;

namespace Pineapple.Core.Web.Cache
{
    public class SessionCache : ICache
    {
        public bool Contain(string group, string key)
        {
            return HttpContext.Current.Session[BuildKey(group, key)] != null;
        }

        public object Get(string group, string key)
        {
            return HttpContext.Current.Session[BuildKey(group, key)];
        }

        public void Add(string group, string key, object value)
        {
            HttpContext.Current.Session.Add(BuildKey(group, key), value);
        }

        public void Remove(string group, string key)
        {
            HttpContext.Current.Session.Remove(BuildKey(group, key));
        }

        public void Clear()
        {

        }

        private string BuildKey(string group, string key)
        {
            return group + "-" + key;
        }
    }
}

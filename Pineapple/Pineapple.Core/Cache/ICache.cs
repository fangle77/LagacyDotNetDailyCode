using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Core.Cache
{
    public interface ICache
    {
        bool Contain(string group, string key);
        object Get(string group, string key);
        void Add(string group, string key, object value);
        void Remove(string group, string key);
        void Clear();
    }
}

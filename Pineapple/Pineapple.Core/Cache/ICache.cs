using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Core.Cache
{
    public interface ICache
    {
        bool Contain(string key);
        object Get(string key);
        void Add(string key, object value);
        void Remove(string key);
        void Clear();
        List<string> CacheKeys { get; } 
    }
}

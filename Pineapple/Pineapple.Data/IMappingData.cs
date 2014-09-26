using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Model;

namespace Pineapple.Data
{
    public interface IMappingData
    {
        Mapping<TKey, TValue> SaveMapping<TKey, TValue>(Mapping<TKey, TValue> mapping);
        
        bool DeleteMapping<TKey, TValue>(Mapping<TKey, TValue> mapping);
        
        bool DeleteMappingByKey<TKey, TValue>(Mapping<TKey, TValue> mapping, TKey key);
        
        bool DeleteMappingByValue<TKey, TValue>(Mapping<TKey, TValue> mapping, TValue value);
        
        Mapping<TKey, TValue> GetMappingByKey<TKey, TValue>(Mapping<TKey, TValue> mapping, TKey key);
        
        Mapping<TKey, TValue> GetMappingByValue<TKey, TValue>(Mapping<TKey, TValue> mapping, TValue value);
    }
}
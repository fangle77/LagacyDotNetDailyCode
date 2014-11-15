using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Pineapple.Model;
using Pineapple.Data;

namespace Pineapple.Business
{
    public class MappingManager
    {
        [Dependency]
        public IMappingData MappingData { protected get; set; }

        public Mapping<TKey, TValue> SaveMapping<TKey, TValue>(Mapping<TKey, TValue> mapping)
        {
            mapping.Items.ForEach(item => item.MappingId = 0);
            MappingData.DeleteMapping(mapping);
            return MappingData.SaveMapping(mapping);
        }

        public bool DeleteMapping<TKey, TValue>(Mapping<TKey, TValue> mapping)
        {
            return MappingData.DeleteMapping(mapping);
        }
    }
}

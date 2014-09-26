using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Model
{
    public class MappingItem<TKey, TValue>
    {
        public int MappingId { get; set; }
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }

    public class Mapping<TKey, TValue>
    {
        public string MappingName { get; protected set; }
        public string KeyName { get; protected set; }
        public string ValueName { get; protected set; }

        public Mapping() { }
        public Mapping(string mappingName, string keyName, string valueName)
        {
            MappingName = mappingName;
            KeyName = keyName;
            ValueName = valueName;
        }

        private readonly List<MappingItem<TKey, TValue>> _items = new List<MappingItem<TKey, TValue>>();
        private readonly HashSet<string> _existKeyValue = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public List<MappingItem<TKey, TValue>> Items
        {
            get { return _items; }
        }

        public void AddItem(MappingItem<TKey, TValue> item)
        {
            if (item != null)
            {
                string code = string.Format("{0}_{1}", item.Key, item.Value);
                if (!_existKeyValue.Contains(code))
                {
                    _items.Add(item);
                    _existKeyValue.Add(code);
                }
            }
        }

        public void AddItems(IEnumerable<MappingItem<TKey, TValue>> items)
        {
            if (items != null)
            {
                foreach (var item in items) AddItem(item);
            }
        }

        public List<MappingItem<TKey, TValue>> FindItemsByKey(TKey key)
        {
            return _items.FindAll(item => item.Key.Equals(key));
        }

        public List<MappingItem<TKey, TValue>> FindItemsByValue(TValue value)
        {
            return _items.FindAll(item => item.Value.Equals(value));
        }

        public MappingItem<TKey, TValue> GetItemByKey(TKey key)
        {
            return _items.Find(item => item.Key.Equals(key));
        }

        public MappingItem<TKey, TValue> GetItemByValue(TValue value)
        {
            return _items.Find(item => item.Value.Equals(value));
        }
    }
}

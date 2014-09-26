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
        public string MappingName { get; set; }
        public string KeyName { get; set; }
        public string ValueName { get; set; }

        private readonly List<MappingItem<TKey, TValue>> _items = new List<MappingItem<TKey, TValue>>();
        public List<MappingItem<TKey, TValue>> Items
        {
            get { return _items; }
        }

        public void AddItem(MappingItem<TKey, TValue> item)
        {
            if (item != null) _items.Add(item);
        }

        public List<MappingItem<TKey, TValue>> FindItemsItemByKey(TKey key)
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

    public class MappingInt : Mapping<int, int>
    {

    }
}

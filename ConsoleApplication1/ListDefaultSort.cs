using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ListDefaultSort
    {
        /*
If the partition size is fewer than 16 elements, it uses an insertion sort algorithm.
If the number of partitions exceeds 2 * LogN, where N is the range of the input array, it uses a Heapsort algorithm.
Otherwise, it uses a Quicksort algorithm.
         */

        public static void Test16()
        {
            List<ListSortItem> list = new List<ListSortItem>();

            for (int i = 0; i < 20; i++)
            {
                list.Add(new ListSortItem() { Id = i, Value = i % 3 });
            }

            list.Sort();
            Console.WriteLine("==============");
            list.ForEach(Console.WriteLine);
        }

    }

    class ListSortItem : IComparable
    {
        public int Id;
        public int Value;

        public int CompareTo(object obj)
        {
            var item = obj as ListSortItem;
            if (item == null) return 1;
            return this.Value.CompareTo(item.Value);
        }

        public override string ToString()
        {
            return string.Format("Id={0}, Value={1}", Id, Value);
        }
    }
}

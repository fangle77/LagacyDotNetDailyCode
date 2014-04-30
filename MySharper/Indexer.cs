using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MySharper.Index;
using MySharper.Model;

namespace MySharper
{
    class Indexer
    {
        private static readonly List<IIndexer> Indexers = new List<IIndexer>
                                                             {
                                                                 new FilePathIndexer()
                                                             };

        public static void StartIndex(List<string> initialItems)
        {
            foreach (var indexer in Indexers)
            {
                indexer.Index(initialItems);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySharper.Find;
using MySharper.Model;

namespace MySharper
{
    class Finder
    {
        private static readonly List<Find.IFinder> Finders = new List<IFinder>
                                                      {
                                                          new SiteSpecFinder(),
                                                          new FileFider()
                                                      };

        public static List<Model.FileItem> Find(string keyword)
        {
            if (string.IsNullOrEmpty(keyword)) return new List<FileItem>(0);

            var results = new List<Model.FileItem>();

            foreach (IFinder finder in Finders)
            {
                var r = finder.Find(keyword);
                if (r != null && r.Count > 0)
                {
                    results.AddRange(r);

                    if (finder.BreakIfFounded) break;
                }
            }
            return results;
        }
    }
}

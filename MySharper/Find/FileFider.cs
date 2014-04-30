using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySharper.Index;
using MySharper.Model;

namespace MySharper.Find
{
    class FileFider : IFinder
    {
        public bool BreakIfFounded
        {
            get { return false; }
        }

        public List<Model.FileItem> Find(string keyword)
        {
            return FileItemContainer.FindItems(keyword);
        }
    }
}

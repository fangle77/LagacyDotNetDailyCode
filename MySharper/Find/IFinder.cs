using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySharper.Find
{
    interface IFinder
    {
        bool BreakIfFounded { get; }
        
        List<Model.FileItem> Find(string keyword);
    }
}

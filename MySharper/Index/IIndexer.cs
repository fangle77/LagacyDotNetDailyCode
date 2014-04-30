using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySharper.Index
{
    interface IIndexer
    {
        void Index(List<string> args);
    }
}

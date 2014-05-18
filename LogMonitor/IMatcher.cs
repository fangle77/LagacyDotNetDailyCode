using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMonitor
{
    interface IMatcher
    {
        bool BeginMatch(string content);
        bool EndMatch(string content);
    }
}

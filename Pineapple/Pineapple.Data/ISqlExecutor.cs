using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Data
{
    public interface ISqlExecutor
    {
        bool Execute(string connectionString, string sql);
    }
}

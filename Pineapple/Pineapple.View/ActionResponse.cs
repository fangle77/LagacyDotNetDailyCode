using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.View
{
    public class ActionResponse
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}

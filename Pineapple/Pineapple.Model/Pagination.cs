using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Model
{
    public class Pagination
    {
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRecords { get; set; }
        public int SizePerPage { get; set; }
    }
}

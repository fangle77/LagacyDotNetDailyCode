using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Model
{
    public class Navigation
    {
        public int? NavigationId { get; set; }
        public int? DisplayOrder { get; set; }
        public string NavigationName { get; set; }
        public string DisplayText { get; set; }
        public string SubText { get; set; }
        public int? ParentId { get; set; }
    }
}

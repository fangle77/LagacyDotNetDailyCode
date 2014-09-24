using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Model
{
    public class Category
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public int? DisplayOrder { get; set; }
        public string DisplayName { get; set; }
        public string SubDisplayName { get; set; }

        public List<CategoryItem> CategoryItems { get; set; }
    }
}

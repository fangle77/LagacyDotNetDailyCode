using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Model
{
    public class CategoryItem
    {
        public int? CategoryItemId { get; set; }
        public int? DisplayOrder { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Time { get; set; }
        public string DisplayText { get; set; }
        public string Image { get; set; }
    }
}

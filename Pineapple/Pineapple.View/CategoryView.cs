using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Pineapple.Model;

namespace Pineapple.View
{
    public class CategoryView
    {
        public Category Category { get; set; }

        public List<CategoryItem> CategoryItems { get; set; }

        public List<Catalog> Catalogs { get; set; }

        public List<Navigation> Navigations { get; set; }

        public List<Template> Templates { get; set; }

        public static CategoryView EmptyView
        {
            get
            {
                return new CategoryView() { Category = new Category(), IsEmpty = true };
            }
        }

        public bool IsEmpty { get; set; }
    }
}

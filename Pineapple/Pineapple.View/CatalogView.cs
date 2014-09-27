using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Model;

namespace Pineapple.View
{
    public class CatalogView
    {
        public Catalog Catalog { get; set; }

        public List<Category> Categories { get; set; }

        public List<Navigation> Navigations { get; set; }
    }
}

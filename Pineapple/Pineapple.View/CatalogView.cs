using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Model;

namespace Pineapple.View
{
    public class CatalogView
    {
        public static CatalogView EmptyView
        {
            get { return new CatalogView() { Catalog = new Catalog(), IsEmpty = true }; }
        }

        public bool IsEmpty { get; set; }

        public Catalog Catalog { get; set; }

        public List<Category> Categories { get; set; }

        public List<Navigation> Navigations { get; set; }
    }
}

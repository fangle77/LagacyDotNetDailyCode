using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pineapple.Model
{
    public class CatalogNavigationMapping : Mapping<int, int>
    {
        public CatalogNavigationMapping()
            : base("CatalogNavigationMapping", "CatalogId", "NavigationId") { }
    }

    public class CatalogCategoryMapping : Mapping<int, int>
    {
        public CatalogCategoryMapping()
            : base("CatalogCategoryMapping", "CatalogId", "CategoryId") { }
    }

    public class CategoryNavigationMapping : Mapping<int, int>
    {
        public CategoryNavigationMapping()
            : base("CategoryNavigationMapping", "CategoryId", "NavigationId") { }
    }

    public class CategoryItemMapping : Mapping<int, int>
    {
        public CategoryItemMapping()
            : base("CategoryItemMapping", "CategoryId", "CategoryItemId") { }
    }

    public class CategoryTemplateMapping : Mapping<int, int>
    {
        public CategoryTemplateMapping()
            : base("CategoryTemplateMapping", "CategoryId", "TemplateId") { }
    }
}

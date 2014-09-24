using System.Collections.Generic;
using Pineapple.Model;

namespace Pineapple.Data
{
    public interface ICatalogData
    {
        Catalog SaveCatalog(Catalog catalog);
        List<Catalog> CatalogLoadAll();
        Catalog GetCatalogById(int catalogId);
        bool Delete(int catalogId);
    }
}
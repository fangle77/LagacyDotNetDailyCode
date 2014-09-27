using System.Collections.Generic;
using Pineapple.Model;

namespace Pineapple.Data
{
    public interface ICatalogData
    {
        Catalog SaveCatalog(Catalog catalog);
        List<Catalog> CatalogLoadAll();
        List<Catalog> LoadCatalogsByIdCatalogIds(IEnumerable<int> catalogIds);
        Catalog GetCatalogById(int catalogId);
        bool Delete(int catalogId);
    }
}
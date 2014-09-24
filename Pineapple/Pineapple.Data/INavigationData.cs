using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Model;

namespace Pineapple.Data
{
    public interface INavigationData
    {
        Navigation SaveNavigation(Navigation navigation);
        Navigation GetNavigationById(int navigationId);
        List<Navigation> LoadNavigationByCatalogId(int catalogId); 
        bool DeleteNavigation(int navigationId);
    }
}
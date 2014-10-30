using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Model;

namespace Pineapple.View
{
    public class NavigationView
    {
        public Navigation Navigation { get; set; }

        public List<Catalog> Catalogs { get; set; }

        public List<Category> Categories { get; set; }
        
        public bool IsEmpty { get; set; }
        
        public static NavigationView EmtpyView
        {
        	get
        	{
        		return new NavigationView() {Navigation = new Navigation(), IsEmpty = true };
        	}
        }
    }
}

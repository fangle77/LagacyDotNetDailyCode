using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.Model;

namespace Pineapple.View
{
    public class TemplateView
    {
        public Template Template { get; set; }

        public List<Category> Categories { get; set; }
        
        public bool IsEmpty { get; set; }
        
        public static TemplateView EmptyView
        {
        	get
        	{
        		return new TemplateView() { Template = new Template(), IsEmpty = true };
        	}
        }
    }
}

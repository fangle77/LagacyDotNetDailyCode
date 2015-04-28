using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pineapple.View;
using Pineapple.Core;
using Pineapple.Core.Cache;

namespace Pineapple.Service
{
    public class ManagerNavigationService
    {
        public List<ManagerNavigation> LoadManagerNavigatoin(string activeName)
        {
            var list = new List<ManagerNavigation>();
            list.Add(new ManagerNavigation() { Name = "Overview", Link = "/Manager", Text = "Overview" });
            list.Add(new ManagerNavigation() { Name = "CompanyInfo", Link = "/Manager/CompanyInfo", Text = "Company" });
            list.Add(new ManagerNavigation() { Name = "Cases", Link = "/Manager/Cases", Text = "Cases" });
            list.Add(new ManagerNavigation() { Name = "Attachment", Link = "/Manager/Attachment", Text = "Attachment" });

            if (string.IsNullOrEmpty(activeName)) activeName = "Overview";

            var activeNav = list.Find(m => string.Equals(activeName, m.Name, StringComparison.OrdinalIgnoreCase));
            if (activeNav != null) activeNav.Active = "active";

            return list;
        }

        public List<BreadCrumb> BuildBreadCrumbs(string controller, string action)
        {
            var list = new List<BreadCrumb>();
            list.Add(new BreadCrumb() { Text = "Overview", Link = "/Manager" });
            if (!string.IsNullOrEmpty(controller) && !"Overview".Equals(controller, StringComparison.OrdinalIgnoreCase))
            {
                list.Add(new BreadCrumb() { Text = controller, Link = "/Manager/" + controller });
            }
            if (!string.IsNullOrEmpty(action) && !"Index".Equals(action, StringComparison.OrdinalIgnoreCase))
            {
                list.Add(new BreadCrumb() { Text = action, Link = string.Empty, Active = "active" });
            }
            return list;
        }
    }
}

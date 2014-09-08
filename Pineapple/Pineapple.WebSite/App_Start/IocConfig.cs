using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pineapple.WebSite.App_Start
{
    public class IocConfig
    {
        public static void RegisterTypes()
        {
            Pineapple.Core.Container.ResisterAssemblyType("Pineapple.Data"
                , "Pineapple.Business", "Pineapple.Service");
        }
    }
}
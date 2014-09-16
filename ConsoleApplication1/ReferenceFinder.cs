using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;

namespace ConsoleApplication1
{
    class DllConstant
    {
        public const string DllBaseDir = @"E:\Code\WWW\DEV\Diapers\WebSite\Bin\";

        public static readonly string ModelDll = DllBaseDir + @"Model.dll";

        public static readonly string DataDll = DllBaseDir + @"DataAccess.dll";

        public static readonly string BusinessDll = DllBaseDir + @"Business.dll";

        public static readonly string QuidsiWebSiteDll = DllBaseDir + @"QuidsiWebSite.dll";

        public static readonly string QuidsiServiceDll = DllBaseDir + @"QuidsiService.dll";

        public static readonly string WebSiteBaseDll = DllBaseDir + @"WebSiteBase.dll";
    }

    class ReferenceFinder
    {
        /*
         * 1. 获取到dll里的所有类型
         * 2. 获取到类型里的所有property
         * 3. 获取到类型里的所有方法
         * 4. 获取方法的返回值
         * 5. 获取方法的参数
         * 6. 获取方法的方法体
        */

        public static List<Type> GetAllTypes(string dll)
        {
            var ass = Assembly.UnsafeLoadFrom(dll);
            return ass.GetTypes().ToList();
        }
    }
}

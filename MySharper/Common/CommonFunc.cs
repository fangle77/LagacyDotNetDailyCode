using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MySharper
{
    static class CommonFunc
    {
        public static string CombinePath(string basePath, string relativePath)
        {
            DirectoryInfo diBase = new DirectoryInfo(basePath);
            relativePath = relativePath.Replace('/', '\\');

            string[] pathLevels = relativePath.Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            string folder = string.Empty;
            DirectoryInfo di = diBase;
            for (int i = pathLevels.Length - 1; i >= 0; i--)
            {
                if (pathLevels[i] == "..")
                {
                    if (di.Parent == null) break;

                    di = di.Parent;
                }
                else
                {
                    folder = pathLevels[i] + (folder == string.Empty ? "" : ("\\" + folder));
                }
            }

            return Path.Combine(di.FullName, folder);
        }
    }
}

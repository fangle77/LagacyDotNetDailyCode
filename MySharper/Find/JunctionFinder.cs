using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySharper.Model;

namespace MySharper.Find
{
    class JunctionFinder : IFinder
    {
        public bool BreakIfFounded
        {
            get { return true; }
        }

        public List<Model.FileItem> Find(string keyword)
        {
            if (keyword == null || keyword.Length < 20) return null;
            if (File.Exists(keyword) == false) return null;
            var fileInfo = new FileInfo(keyword);
            if (fileInfo.Directory == null) return null;
            if (!HasReparsePoint(fileInfo)) return null;

            List<string> fullPaths = new List<string>();
            fullPaths.Add(fileInfo.Name);

            DirectoryInfo di = fileInfo.Directory;
            while (di != null)
            {
                string originPath = Index.JunctionIndexer.FindOrigin(di.FullName);
                if (!string.IsNullOrEmpty(originPath))
                {
                    fullPaths.Add(originPath);
                    fullPaths.Reverse();
                    return new List<FileItem> { new FileItem(fileInfo.Name, Path.Combine(fullPaths.ToArray())) { OpenFileLocation = true } };
                }
                fullPaths.Add(di.Name);
                di = di.Parent;
            }
            return null;
        }

        private bool HasReparsePoint(FileInfo fi)
        {
            DirectoryInfo di = fi.Directory;
            while (di != null)
            {
                if ((di.Attributes & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint) return true;
                di = di.Parent;
            }
            return false;
        }
    }
}

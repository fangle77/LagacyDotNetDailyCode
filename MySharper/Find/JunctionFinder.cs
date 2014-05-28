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
            if ((fileInfo.Directory.Attributes & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint) return null;

            string originPath = Index.JunctionIndexer.FindOrigin(fileInfo.Directory.FullName);
            if (string.IsNullOrEmpty(originPath)) return null;
            return new List<FileItem> { new FileItem(fileInfo.Name, Path.Combine(originPath, fileInfo.Name)) { OpenFileLocation = true } };
        }
    }
}

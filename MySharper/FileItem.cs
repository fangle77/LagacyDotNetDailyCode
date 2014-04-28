using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySharper
{
    class FileItem : IComparable
    {
        public readonly string FileName;
        public readonly string FullPath;
        public readonly string DisplayText;

        public FileItem(string fileName, string fullPath)
            : this(fileName, fullPath, true)
        {

        }

        public FileItem(string fileName, string fullPath, bool caclDisplayText)
        {
            this.FileName = fileName;
            this.FullPath = fullPath;
            DisplayText = caclDisplayText ? InitDisplayText() : fullPath;
        }

        private string InitDisplayText()
        {
            string[] paths = FullPath.Split('\\');
            int count = Math.Min(paths.Length, 3);

            string r = paths[paths.Length - 1];
            int i = paths.Length - 2;
            for (; count > 0 && i >= 0; i--, count--)
            {
                r = paths[i] + "\\" + r;
            }
            if (i >= 0) return string.Format("{0}...\\{1}", FileName, r);
            else return string.Format("{0}...{1}", FileName, r);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return -1;
            var item = obj as FileItem;
            if (item == null) return -1;

            int c = this.FileName.CompareTo(item.FileName);
            if (c == 0) return this.FullPath.CompareTo(item.FullPath);
            else return c;
        }
    }

    class FileItemContainer
    {
        private static readonly List<FileItem> AllFileItems = new List<FileItem>();

        public static bool IsEmpty
        {
            get { return AllFileItems.Count == 0; }
        }

        public static void AddItem(FileItem item)
        {
            if (item != null) AllFileItems.Add(item);
        }

        public static List<FileItem> FindItems(string fileName)
        {
            var r = new List<FileItem>(20);
            var r2 = new List<FileItem>(20);
            foreach (var item in AllFileItems)
            {
                int i = item.FileName.IndexOf(fileName, StringComparison.OrdinalIgnoreCase);
                if (i == 0)
                {
                    r.Add(item);
                }
                else if (i > 0)
                {
                    r2.Add(item);
                }
                if (r.Count >= 20 || r2.Count >= 20) break;
            }
            r.AddRange(r2);
            return r;
        }
    }
}

using System;
using System.Collections.Generic;

namespace MySharper.Model
{
    class FileItem : IComparable
    {
        public readonly string FileName;
        public readonly string FullPath;

        private readonly bool _calcDisplayText = false;
        private string _displayText;

        public string DisplayText
        {
            get
            {
                return _displayText = _displayText ?? (_calcDisplayText ? InitDisplayText() : FullPath);
            }
        }

        public FileItem(string fileName, string fullPath)
            : this(fileName, fullPath, true)
        {

        }

        public FileItem(string fileName, string fullPath, bool calcDisplayText)
        {
            this.FileName = fileName;
            this.FullPath = fullPath;
            this._calcDisplayText = calcDisplayText;
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
            return c == 0 ? this.FullPath.CompareTo(item.FullPath) : c;
        }
    }
}

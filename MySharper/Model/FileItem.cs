using System;
using System.Collections.Generic;
using System.Text;

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

            if (paths.Length < 5)
            {
                return string.Format("{0}    {1}", FileName, FullPath.Substring(0, FullPath.LastIndexOf('\\')));
            }
            else
            {
                StringBuilder sb = new StringBuilder(FullPath.Length + 4);
                sb.AppendFormat("{0}    \\", FileName);
                for (int i = 4; i >= 1; i--)
                {
                    sb.AppendFormat("{0}\\", paths[paths.Length - i]);
                }
                return sb.ToString();
            }
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

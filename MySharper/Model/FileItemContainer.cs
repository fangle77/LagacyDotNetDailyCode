using System;
using System.Collections.Generic;

namespace MySharper.Model
{
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
            var resultStartWith = new List<FileItem>(20);
            var resultContains = new List<FileItem>(20);
            foreach (var item in AllFileItems)
            {
                int i = item.FileName.IndexOf(fileName, StringComparison.OrdinalIgnoreCase);
                if (i == 0)
                {
                    resultStartWith.Add(item);
                }
                else if (i > 0 && resultContains.Count <= 20)
                {
                    resultContains.Add(item);
                }
                if (resultStartWith.Count >= 20) break;
            }
            resultStartWith.Sort();
            resultContains.Sort();
            resultStartWith.AddRange(resultContains);
            return resultStartWith;
        }
    }
}

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
        private static readonly int CacheSearchCount = 30;

        private static readonly List<FileItem> AllFileItems = new List<FileItem>();
        private static readonly Dictionary<string, List<FileItem>> OneLetterItems = new Dictionary<string, List<FileItem>>(StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<string, List<FileItem>> TwoLetterItems = new Dictionary<string, List<FileItem>>(StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<string, List<FileItem>> ThreeLetterItems = new Dictionary<string, List<FileItem>>(StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<string, List<FileItem>> MoreThanThreeLetterItems = new Dictionary<string, List<FileItem>>(StringComparer.OrdinalIgnoreCase);
        private static readonly List<FileItem> EmptyResult = new List<FileItem>(0);

        public static bool IsEmpty
        {
            get { return AllFileItems.Count == 0; }
        }

        public static void AddItem(FileItem item)
        {
            if (item != null) AllFileItems.Add(item);
            AddToLetterItem(item, OneLetterItems, 1);
            AddToLetterItem(item, TwoLetterItems, 2);
            AddToLetterItem(item, ThreeLetterItems, 3);
        }

        private static void AddToLetterItem(FileItem item, Dictionary<string, List<FileItem>> dic, int letterCount)
        {
            if (string.IsNullOrEmpty(item.FileName)) return;
            if (item.FileName.Length < letterCount) return;
            string letter = item.FileName.Substring(0, letterCount);
            if (dic.ContainsKey(letter)) dic[letter].Add(item);
            else dic.Add(letter, new List<FileItem>() { item });
        }

        public static List<FileItem> FindItems(string fileName)
        {
            if (AllFileItems.Count == 0) return EmptyResult;
            if (fileName == null) return EmptyResult;
            fileName = fileName.Trim().ToLower();
            if (fileName.Length == 0) return EmptyResult;

            List<FileItem> result = null;

            if (fileName.Length == 1)
            {
                if (OneLetterItems.ContainsKey(fileName)) result = OneLetterItems[fileName];
            }
            else if (fileName.Length == 2)
            {
                if (TwoLetterItems.ContainsKey(fileName)) result = TwoLetterItems[fileName];
            }
            else if (fileName.Length == 3)
            {
                if (ThreeLetterItems.ContainsKey(fileName)) result = ThreeLetterItems[fileName];
            }
            else
            {
                result = FindMoreThanThreeLetters(fileName);
            }
            SortFileItem(result);
            return result == null ? EmptyResult : result;
        }

        private static List<FileItem> FindMoreThanThreeLetters(string fileName)
        {
            string startThreeLetter = fileName.Substring(0, 3);
            if (ThreeLetterItems.ContainsKey(startThreeLetter) == false) return EmptyResult;

            string letters = string.Empty;
            List<FileItem> items = null;
            bool exist = false;
            for (int len = fileName.Length; len > 3; len--)
            {
                letters = fileName.Substring(0, len);
                if (MoreThanThreeLetterItems.ContainsKey(letters))
                {
                    items = MoreThanThreeLetterItems[letters];
                    exist = true;
                    break;
                }
            }
            if (exist && letters.Length == fileName.Length) return items;

            if (exist && (items == null || items.Count == 0)) return items;

            items = items == null ? ThreeLetterItems[startThreeLetter] : items;
            List<FileItem> newItems = new List<FileItem>();
            items.ForEach(t =>
            {
                if (t.FileName.StartsWith(fileName, StringComparison.OrdinalIgnoreCase))
                {
                    newItems.Add(t);
                }
            });
            AddToMoreDictionary(fileName, newItems);
            return newItems;
        }

        private static void AddToMoreDictionary(string key, List<FileItem> value)
        {
            if (MoreThanThreeLetterItems.ContainsKey(key)) return;
            if (MoreThanThreeLetterItems.Count >= CacheSearchCount)
            {
                MoreThanThreeLetterItems.Remove(MoreThanThreeLetterItems.Keys.First());
            }
            MoreThanThreeLetterItems.Add(key, value);
        }

        private static void SortFileItem(List<FileItem> items)
        {
            if (items == null) return;
            items.Sort((item1, item2) =>
            {
                return item2.CompareTo(item1);
            });
        }
    }
}

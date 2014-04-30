using System;
using System.Collections.Generic;
using System.IO;
using MySharper.Model;

namespace MySharper.Find
{
    class SiteSpecFinder : IFinder
    {
        private static List<string> SiteSpecs = new List<string>() { "diapers", "beautybar", "soap", "casa", "green", "wag", "book", "look", "jump", "yoyo" };

        public static List<FileItem> FindItems(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return null;
            if (File.Exists(fileName) == false) return null;


            FileInfo fi = new FileInfo(fileName);
            string name = fi.Name;

            fileName = fileName.ToLower();
            string site = string.Empty;
            foreach (string s in SiteSpecs)
            {
                if (fileName.IndexOf(s, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    site = s;
                    break;
                }
            }
            if (string.IsNullOrEmpty(site)) return null;
            List<FileItem> items = new List<FileItem>(SiteSpecs.Count);
            foreach (string s in SiteSpecs)
            {
                string full = fileName.Replace(site, s);
                if (File.Exists(full))
                {
                    items.Add(new FileItem(name, full, false));
                }
            }
            return items;
        }

        public bool BreakIfFounded { get { return true; } }

        public List<FileItem> Find(string keyword)
        {
            if (keyword == null || keyword.Length < 20) return null;

            return FindItems(keyword);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class PrimarySiteWeight
    {
        public static void AddPrimarySiteWeight(string product_attribute, string product_stat, string catalogCode)
        {
            if (File.Exists(product_attribute) == false || File.Exists(product_stat) == false) return;
            if (string.IsNullOrEmpty(catalogCode)) return;

            HashSet<string> existLine = new HashSet<string>();
            using (var sr = new StreamReader(product_stat))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.IndexOf("PrimarySite", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        existLine.Add(line);
                    }
                }
            }

            using (var sw = new StreamWriter(product_stat, true))
            {
                using (var sr = new StreamReader(product_attribute))
                {
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.IndexOf("PrimarySite", StringComparison.OrdinalIgnoreCase) >= 0
                            && line.IndexOf(catalogCode, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            string productid = line.Split('|')[0];
                            string statLine = string.Format("{0}|PrimarySite|1||", productid);
                            if (existLine.Contains(statLine) == false)
                            {
                                sw.WriteLine(statLine);
                            }
                        }
                    }
                }
            }
        }

        public static void AddAtributeWeight(string attibute2)
        {
            if (File.Exists(attibute2) == false) return;
            bool exist = false;
            using (StreamReader sr = new StreamReader(attibute2))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.IndexOf("PrimarySite|weight") >= 0)
                    {
                        exist = true;
                        break;
                    }
                }
            }
            if (!exist)
            {
                File.AppendAllLines(attibute2, new string[] { "PrimarySite|weight|0|", "PrimarySite|best_value|100|", "PrimarySite|worst_value|0|" });
            }
        }

        public static void AddOwnedCatalog(string path)
        {
            if (Directory.Exists(path) == false) return;

            string product_stat = Path.Combine(path, "product_stats.txt");
            string products = Path.Combine(path, "products.txt");
            string productsTmp = Path.Combine(path, string.Format("products{0}.txt", DateTime.Now.ToString("mmssfff")));

            if (File.Exists(product_stat) == false) return;
            if (File.Exists(products) == false) return;

            HashSet<string> toAddPid = new HashSet<string>();
            toAddPid.Add("106546");
            toAddPid.Add("106547");
            toAddPid.Add("106561");
            toAddPid.Add("106569");
            toAddPid.Add("106605");
            toAddPid.Add("228004");
            toAddPid.Add("229215");
            toAddPid.Add("229279");
            toAddPid.Add("229297");
            toAddPid.Add("23335");
            toAddPid.Add("237572");
            toAddPid.Add("237610");
            toAddPid.Add("237934");

            using (StreamWriter sw = new StreamWriter(product_stat, true))
            {
                int total = (toAddPid.Count - 1) / 2;
                foreach (string pid in toAddPid)
                {
                    string statLine = string.Format("{0}|owning_catalog|1||", pid);
                    sw.WriteLine(statLine);
                    if (total-- <= 0) break;
                }
            }

            File.Move(products, productsTmp);

            using (StreamWriter sw = new StreamWriter(products))
            {
                using (StreamReader sr = new StreamReader(productsTmp))
                {
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        int index = line.IndexOf('|');
                        if (index > 0)
                        {
                            string pid = line.Substring(0, index);
                            if (toAddPid.Contains(pid))
                            {
                                int insert = line.LastIndexOf("||||||");
                                if (insert > 1) line = line.Insert(insert, ", OwnedCatalogTest");
                            }
                        }
                        sw.WriteLine(line);
                    }
                }
            }
            File.Delete(productsTmp);
        }
    }

    class PrimarySiteFilter
    {
        public static void FilterFlatFile(string path)
        {
            if (Directory.Exists(path) == false) return;

            var di = new DirectoryInfo(path);
            string newPath = di.FullName + "\\" + di.Name + "-filter";
            if (Directory.Exists(newPath))
            {
                Directory.Delete(newPath, true);
            }
            Directory.CreateDirectory(newPath);

            string product = path + "\\products.txt";

            var hs = FilterProduct(product, newPath);
            if (hs == null || hs.Count == 0) return;

            FilterProductAttributeOrState(path + "\\product_stats.txt", newPath, hs);
            FilterProductAttributeOrState(path + "\\product_attributes.txt", newPath, hs);
        }

        private static HashSet<string> FilterProduct(string productFile, string newPath)
        {
            if (File.Exists(productFile) == false) return null;
            string newProductFile = newPath + "\\" + (new FileInfo(productFile)).Name;

            HashSet<string> hs = new HashSet<string>();
            using (StreamWriter sw = new StreamWriter(newProductFile))
            {
                using (StreamReader sr = new StreamReader(productFile))
                {
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.IndexOf("primarysitetest", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            hs.Add(line.Split('|')[0]);
                            sw.WriteLine(line);
                        }
                    }
                }
            }
            return hs;
        }

        private static void FilterProductAttributeOrState(string productAttributeStatFile, string newPath, HashSet<string> productIds)
        {
            if (File.Exists(productAttributeStatFile) == false) return;
            string newpaFile = newPath + "\\" + new FileInfo(productAttributeStatFile).Name;

            using (StreamWriter sw = new StreamWriter(newpaFile))
            {
                using (StreamReader sr = new StreamReader(productAttributeStatFile))
                {
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (productIds.Contains(line.Split('|')[0]))
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
            }
        }
    }

    class PrimarySiteCompare
    {
        public static void Comare(string path, string catalogCode)
        {
            if (Directory.Exists(path) == false) return;

            string product_stat = Path.Combine(path, "product_stats.txt");
            string product_attr = Path.Combine(path, "product_attribute.txt");

            if (File.Exists(product_stat) == false) return;
            if (File.Exists(product_attr) == false) return;

            HashSet<string> statPid = GetPids(product_stat, line => { return line.IndexOf("owning_catalog", StringComparison.OrdinalIgnoreCase) > 0; });
            HashSet<string> attrPid = GetPids(product_attr, line =>
            {
                return line.IndexOf("PrimarySite", StringComparison.OrdinalIgnoreCase) > 0
                    && line.IndexOf(catalogCode, StringComparison.OrdinalIgnoreCase) > 0;
            });

            string compareReulst = Path.Combine(path, "results.txt");
            using (StreamWriter sw = new StreamWriter(compareReulst))
            {
                sw.WriteLine("======attr not in stat========");
                int count = 0;
                foreach (string attr in attrPid)
                {
                    if (statPid.Contains(attr) == false)
                    {
                        count++;
                        sw.WriteLine(attr);
                    }
                }
                sw.WriteLine("====  " + count + "  ====");

                sw.WriteLine();
                sw.WriteLine("======stat not in attr========");
                count = 0;
                foreach (string stat in statPid)
                {
                    if (attrPid.Contains(stat) == false)
                    {
                        count++;
                        sw.WriteLine(stat);
                    }
                }
                sw.WriteLine("====  " + count + "  ====");
            }
        }

        private static HashSet<string> GetPids(string file, Func<string, bool> toResolve)
        {
            HashSet<string> statPid = new HashSet<string>();
            using (StreamReader sr = new StreamReader(file))
            {
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (toResolve(line))
                    {
                        statPid.Add(line.Split('|')[0].Trim());
                    }
                }
            }
            return statPid;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MySharper.Index
{
    class JunctionIndexer : IIndexer
    {
        public void Index(List<string> args)
        {
            IndexerPaths = args;
        }

        private static List<string> IndexerPaths;
        private static Dictionary<string, string> DicReparse_Origin;
        private static void Index()
        {
            if (DicReparse_Origin != null) return;
            DicReparse_Origin = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (IndexerPaths == null || IndexerPaths.Count == 0) return;
            IndexerPaths.ForEach(path =>
                                     {
                                         var junctionFiles = GetJunctionConfigFile(path);
                                         if (junctionFiles != null)
                                         {
                                             junctionFiles.ForEach(IndexJunction);
                                         }
                                     }
                );
        }

        private static void IndexJunction(string junctionFile)
        {
            if (!string.IsNullOrEmpty(junctionFile))
            {
                var dic = BuildRelated(junctionFile);
                if (dic != null)
                {
                    foreach (string key in dic.Keys)
                    {
                        if (DicReparse_Origin.ContainsKey(key) == false)
                        {
                            DicReparse_Origin.Add(key, dic[key]);
                        }
                    }
                }
            }
        }

        private static List<string> GetJunctionConfigFile(string path)
        {
            if (path == null || path.LastIndexOf("WebSite.sln", StringComparison.OrdinalIgnoreCase) < 0) return null;

            //ContentGenerationModule\PrettyJunction.txt
            //QuidsiWebSite\PrettyJunction.txt
            List<string> list = new List<string>();
            (new List<string> { "QuidsiWebSite", "ContentGenerationModule" }).ForEach(directory =>
                                         {
                                             string config = GetJunctionConfigFile(path, directory);
                                             if (!string.IsNullOrEmpty(config)) list.Add(config);
                                         });

            return list;
        }

        private static string GetJunctionConfigFile(string path, string directory)
        {
            FileInfo fi = new FileInfo(path);
            if (fi.Exists == false) return null;
            DirectoryInfo di = fi.Directory;
            DirectoryInfo[] quidsiWebSites = null;
            while (di != null && (quidsiWebSites = di.GetDirectories(directory, SearchOption.TopDirectoryOnly)).Length == 0)
            {
                di = di.Parent;
            }

            if (quidsiWebSites == null || quidsiWebSites.Length == 0) return null;
            string file = Path.Combine(quidsiWebSites[0].FullName, "PrettyJunction.txt");
            if (File.Exists(file)) return file;
            return null;
        }

        private static Dictionary<string, string> BuildRelated(string junctionConfig)
        {
            if (File.Exists(junctionConfig) == false) return null;
            string[] lines = File.ReadAllLines(junctionConfig);

            string baseDir = new FileInfo(junctionConfig).Directory.FullName;

            var variables = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith("#")) continue;
                if (line.StartsWith("@:"))
                {
                    GetVariable(line, variables);
                    continue;
                }
                var kvp = AnalyseJunction(baseDir, line);
                result = MergeToResult(result, kvp);
            }
            result = MergeVariable(result, variables);
            return result;
        }

        private static void GetVariable(string line, Dictionary<string, List<string>> variables)
        {
            string[] results = line.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (results.Length < 2) return;

            string variableName = "{" + results[0].Replace("@:", string.Empty) + "}";

            var values = results[1].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            if (variables.ContainsKey(variableName))
            {
                variables[variableName].AddRange(values);
            }
            else
            {
                variables.Add(variableName, values);
            }
        }

        private static KeyValuePair<string, string> AnalyseJunction(string baseDir, string line)
        {
            string[] results = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (results.Length < 2) return new KeyValuePair<string, string>();

            string destination = CommonFunc.CombinePath(baseDir, results[0]);
            string source = CommonFunc.CombinePath(baseDir, results[1]);

            return new KeyValuePair<string, string>(destination, source);
        }

        private static Dictionary<string, string> MergeToResult(Dictionary<string, string> dest, KeyValuePair<string, string> kvp)
        {
            if (kvp.Key == null) return dest;
            var result = new Dictionary<string, string>(dest.Count + 1);
            foreach (string dd in dest.Keys)
            {
                if (dd.StartsWith(kvp.Value, StringComparison.OrdinalIgnoreCase))
                {
                    string newDestKey = dd.Replace(kvp.Value, kvp.Key);
                    if (result.ContainsKey(newDestKey) == false) result.Add(newDestKey, dest[dd]);
                }
                else
                {
                    if (result.ContainsKey(dd) == false) result.Add(dd, dest[dd]);
                }
            }
            if (result.ContainsKey(kvp.Key) == false) result.Add(kvp.Key, kvp.Value);
            return result;
        }

        private static Dictionary<string, string> MergeVariable(Dictionary<string, string> result, Dictionary<string, List<string>> variables)
        {
            var dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (string destination in result.Keys)
            {
                string source = result[destination];
                foreach (string variable in variables.Keys)
                {
                    foreach (string value in variables[variable])
                    {
                        string dest = destination.Replace(variable, value);
                        if (dic.ContainsKey(dest) == false) dic.Add(dest, source.Replace(variable, value));
                    }
                }
                if (variables.Count == 0)
                {
                    if (dic.ContainsKey(destination) == false) dic.Add(destination, source);
                }
            }
            return dic;
        }

        public static string FindOrigin(string file)
        {
            Index();
            if (file == null || !DicReparse_Origin.ContainsKey(file)) return null;
            return DicReparse_Origin[file];
        }
    }
}

using System;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;

namespace TNSConfig
{
    abstract class OracleInfomation
    {
        protected object FindRegValue(RegistryKey key, string name)
        {
            if (key == null) return null;
            object value = key.GetValue(name);
            if (value != null) return value;

            string[] subKeys = key.GetSubKeyNames();
            if (subKeys == null || subKeys.Length == 0) return null;

            foreach (string sk in subKeys)
            {
                var k = key.OpenSubKey(sk);
                object o = FindRegValue(k, name);
                if (o != null)
                {
                    return o;
                }
            }
            return null;
        }

        protected RegistryKey FindSubKey(RegistryKey parent, string name)
        {
            RegistryKey key = parent.OpenSubKey(name);
            if (key != null) return key;

            name = name.ToUpper();

            List<RegistryKey> levelList = new List<RegistryKey>(100);

            string[] subKeys = parent.GetSubKeyNames();
            if (subKeys == null || subKeys.Length == 0)
            {
                return null;
            }
            foreach (string sub in subKeys)
            {
                RegistryKey k = null;
                try
                {
                    k = parent.OpenSubKey(sub);
                }
                catch (System.Security.SecurityException) { continue; }

                if (k == null) continue;
                levelList.Add(k);

                if (k.Name.ToUpper() == name)
                {
                    return k;
                }
            }
            //广度优先
            while (true)
            {
                if (levelList == null || levelList.Count == 0) break;

                List<RegistryKey> list = new List<RegistryKey>(levelList.Count);
                foreach (RegistryKey k in levelList)
                {
                    string[] subs = k.GetSubKeyNames();
                    foreach (string s in subs)
                    {
                        RegistryKey sk = null;
                        try
                        {
                            sk = k.OpenSubKey(s);
                        }
                        catch (System.Security.SecurityException) { continue; }

                        if (sk == null) continue;
                        if (s.ToUpper() == name) return sk;
                        list.Add(sk);
                    }
                }
                levelList = list;
            }

            return null;
        }

        public abstract bool BackupTnsname(out string backFile, out string err);
        public abstract bool GetOracleHome(out string oracleHome, out string errMsg);
        public abstract bool GetOracleTNSName(out string path, out string err);
        public abstract bool GetOracleVersion(out string version, out string err);
    }
}

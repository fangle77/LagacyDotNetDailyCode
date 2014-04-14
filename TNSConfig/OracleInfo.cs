using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;

namespace TNSConfig
{
    class OracleInfo
    {
        private MatchCollection Match(string tnsInput)
        {
            if (tnsInput == null) return null;
            //string regEx = @"([a-zA-Z0-9]+)(\s*=\s*\(DESCRIPTION\s*=\s*\(\s*ADDRESS\s*=\s*\(PROTOCOL\s*=\s*)([a-zA-Z]+)\s*\)(\s*\(\s*HOST\s*=\s*)([a-zA-Z0-9.]+)(\s*\)\s*\(\s*PORT\s*=\s*)([0-9]+)(\s*\)\s*\)\s*\(CONNECT_DATA\s*=\s*)((\([A-Za-z_]+\s*=\s*[A-Za-z0-9_]+\s*\)\s*){1,3})(\s*\)\s*\)\s*)";
            string regEx = @"(\s{1}[a-zA-Z0-9_.-]+)(\s*=\s*\(DESCRIPTION\s*=\s*)(\(\s*ADDRESS_LIST\s*=\s*){0,1} (\(\s*ADDRESS\s*=\s*\(PROTOCOL\s*=\s*)([a-zA-Z]+)\s*\)(\s*\(\s*HOST\s*=\s*)([a-zA-Z0-9-_.]+)(\s*\)\s*\(\s*PORT\s*=\s*)([0-9]+)(\s*\)\s*\)\s*)(\s*\)\s*){0,1} (\(\s*CONNECT_DATA\s*=\s*)((\([A-Za-z_]+\s*=\s*[A-Za-z0-9_:/.]+\s*\)\s*){1,3})(\s*\)\s*\))";

            var reg = new Regex(regEx);

            var matchs = reg.Matches(tnsInput);
            return matchs;
        }

        public Dictionary<string, string> PickupTNSConfig(string tnsContent)
        {
            var matchs = Match(tnsContent);
            if (matchs == null || matchs.Count == 0) return null;

            Dictionary<string, string> dic = new Dictionary<string, string>(matchs.Count);
            foreach (Match m in matchs)
            {
                if (m.Success == false) continue;
                if (m.Groups.Count > 1)
                {
                    string key = m.Groups[1].Value.Trim().ToUpper();
                    if (dic.ContainsKey(key) == false)
                    {
                        dic.Add(key, m.Value);
                    }
                }
            }
            return dic;
        }

        /// <summary>
        /// 是否安装了完整的oracle 服务端或客户端
        /// </summary>
        private static bool _IsInstalledFullOracle = true;

        private OracleInfomation _OracleInfo;
        private OracleInfomation OracleInfoTool
        {
            get
            {
                if (_OracleInfo == null)
                {
                    if (_IsInstalledFullOracle == true)
                        _OracleInfo = new OracleFullVersion();
                    else
                        _OracleInfo = new OracleSimpleVersion();
                }
                return _OracleInfo;
            }
            set { _OracleInfo = value; }
        }


        public bool GetOracleTNSName(out string path, out string err)
        {
            return OracleInfoTool.GetOracleTNSName(out path, out err);
        }

        public bool GetOracleHome(out string oracleHome, out string errMsg)
        {
            if (_IsInstalledFullOracle)
            {
                bool suc = OracleInfoTool.GetOracleHome(out oracleHome, out errMsg);
                if (suc == false)
                {
                    _IsInstalledFullOracle = false;
                    _OracleInfo = null;
                }
            }
            return OracleInfoTool.GetOracleHome(out oracleHome, out errMsg);
        }

        public bool GetOracleVersion(out string version, out string err)
        {
            return OracleInfoTool.GetOracleVersion(out version, out err);
        }

        public bool BackupTnsname(out string backFile, out string err)
        {
            return OracleInfoTool.BackupTnsname(out backFile, out err);
        }
    }
}

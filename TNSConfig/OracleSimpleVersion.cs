using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace TNSConfig
{
    class OracleSimpleVersion : OracleInfomation
    {
        private string _oraHome;
        private string _oraVersion;
        private string _tnsNameFile;

        #region IOracleInfo 成员

        public override bool BackupTnsname(out string backFile, out string err)
        {
            string orahome;
            backFile = "";
            err = "";
            bool su = GetOracleHome(out orahome, out err);
            if (su == false)
            {
                return false;
            }

            //\\network\\admin\\tnsnames.ora

            string tnsPath = orahome.TrimEnd('\\') + "\\";
            string tnsFile = tnsPath + "tnsnames.ora";
            if (Directory.Exists(tnsPath) == false)
            {
                err = "tnsname路径不存在：" + tnsPath;
                return false;
            }
            if (File.Exists(tnsFile) == false)
            {
                err = "tnsname文件不存在：" + tnsFile;
                return false;
            }

            try
            {
                backFile = tnsPath + "_tnsnames" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
                File.Copy(tnsFile, backFile, true);
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        public override bool GetOracleHome(out string oracleHome, out string errMsg)
        {
            oracleHome = _oraHome;
            errMsg = "";

            if (string.IsNullOrEmpty(_oraHome) == false) return true;


            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.LocalMachine;
            RegistryKey key = FindSubKey(reg, "ORACLE INSTANT CLIENT");

            if (key == null)
            {
                errMsg = "未安装oracle";
                return false;
            }

            object orahome = FindRegValue(key, "UNINSTALLSTRING");

            if (orahome == null)
            {
                errMsg = "未能找到ORACLE主目录";
                return false;
            }

            oracleHome = orahome.ToString();

            int index = oracleHome.LastIndexOf('\\');
            if (index >= 0)
            {
                oracleHome = oracleHome.Substring(0, index);
            }
            _oraHome = oracleHome;
            return true;
        }

        public override bool GetOracleTNSName(out string path, out string err)
        {
            path = _tnsNameFile;
            err = "";
            if (string.IsNullOrEmpty(_tnsNameFile) == false) return true;

            string oraHome;
            bool suc = GetOracleHome(out oraHome, out err);
            if (suc == false) return false;

            path = oraHome.TrimEnd('\\') + "\\tnsnames.ora";

            _tnsNameFile = path;
            return true;
        }

        public override bool GetOracleVersion(out string version, out string err)
        {
            version = _oraVersion;
            err = "";
            if (string.IsNullOrEmpty(_oraVersion) == false) return true;

            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.LocalMachine;
            RegistryKey key = FindSubKey(reg, "ORACLE INSTANT CLIENT");

            if (key == null)
            {
                err = "未安装oracle";
                return false;
            }

            object o = FindRegValue(key, "DISPLAYVERSION");

            if (o == null)
            {
                err = "未能找到ORACLE版本";
                return false;
            }

            version = o.ToString();
            _oraVersion = version;
            return true;
        }

        #endregion
    }
}

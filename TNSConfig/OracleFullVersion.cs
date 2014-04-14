using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace TNSConfig
{
    class OracleFullVersion : OracleInfomation
    {
        public override bool GetOracleTNSName(out string path, out string err)
        {
            path = "";
            err = "";
            bool suc = GetOracleHome(out path, out err);

            if (suc == false)
            {
                return false;
            }

            path = path + "\\network\\admin\\tnsnames.ora";
            return true;
        }

        public override bool GetOracleHome(out string oracleHome, out string errMsg)
        {
            oracleHome = "";
            errMsg = "";

            string orahomekeyName;

            Microsoft.Win32.RegistryKey oracleKeyNode = null;
            bool suc = GetORACLEKeyNode(out errMsg, out oracleKeyNode);
            if (suc == false) return false;

            suc = GetOracleHomeKey(oracleKeyNode, out orahomekeyName, out errMsg);
            if (suc == false) return false;

            //SOFTWARE\ORACLE\KEY_OraDb10g_home1
            int index = orahomekeyName.IndexOf("ORACLE\\");
            if (index >= 0)
            {
                orahomekeyName = orahomekeyName.Substring(index + 7);
            }

            Microsoft.Win32.RegistryKey reg = oracleKeyNode;

            object orahome = null;
            try
            {
                Microsoft.Win32.RegistryKey oraHomeKey = reg.OpenSubKey(orahomekeyName);
                orahome = oraHomeKey.GetValue("ORACLE_HOME");
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
            if (orahome == null)
            {
                errMsg = "未能找到ORACLE主目录";
                return false;
            }

            oracleHome = orahome.ToString();
            return true;
        }

        public override bool GetOracleVersion(out string version, out string err)
        {
            version = "";
            err = "";

            string orahome;
            bool suc = GetOracleHome(out orahome, out err);

            if (suc == false)
            {
                return false;
            }

            string ociFile = orahome + "\\bin\\oci.dll";
            if (File.Exists(ociFile) == false)
            {
                err = "未能找到oci.dll";
                return false;
            }
            System.Diagnostics.FileVersionInfo fi = System.Diagnostics.FileVersionInfo.GetVersionInfo(ociFile);
            version = fi.FileVersion;
            return true;
        }

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

            string tnsPath = orahome + "\\network\\admin\\";
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

        private bool GetOracleHomeKey(RegistryKey oracleKeyNode, out string oraHomeKey, out string errMsg)
        {
            oraHomeKey = "";
            errMsg = "";

            object orahome = FindRegValue(oracleKeyNode, "ORACLE_HOME_KEY");

            if (orahome == null)
            {
                errMsg = "未能找到ORACLE主目录的项";
                return false;
            }

            oraHomeKey = orahome.ToString();
            return true;
        }

        private bool GetOracleHomeKey(out string oraHomeKey, out string errMsg)
        {
            oraHomeKey = "";
            errMsg = "";

            RegistryKey oracleKeyNode = null;
            bool suc = GetORACLEKeyNode(out errMsg, out oracleKeyNode);
            if (suc == false) return false;

            object orahome = FindRegValue(oracleKeyNode, "ORACLE_HOME_KEY");

            if (orahome == null)
            {
                errMsg = "未能找到ORACLE主目录的项";
                return false;
            }

            oraHomeKey = orahome.ToString();
            return true;
        }

        private bool GetORACLEKeyNode(out string errMsg, out RegistryKey key)
        {
            errMsg = "";
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.LocalMachine;
            key = FindSubKey(reg, "ORACLE");

            if (key == null)
            {
                errMsg = "未安装oracle";
                return false;
            }
            return true;
        }
    }
}

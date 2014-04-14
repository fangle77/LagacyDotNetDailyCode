using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;

namespace yltl.Tools
{
    public partial class FrmOraTnsname : Form
    {
        public FrmOraTnsname()
        {
            InitializeComponent();
        }

        private void btnEnvironmentPath_Click(object sender, EventArgs e)
        {
            string s = Environment.GetEnvironmentVariable("path");
            rtxtMsg.Text = s;
        }

        private void btnOraclePath_Click(object sender, EventArgs e)
        {
            //network\admin\tnsnames.ora
            string orahome;
            string err;
            bool suc = GetOracleHome(out orahome, out err);

            if (suc == false)
            {
                MessageBox.Show(err);
                return;
            }

            rtxtMsg.Text = orahome;
        }

        private void btnTNSName_Click(object sender, EventArgs e)
        {
            //network\admin\tnsnames.ora
            string orahome;
            string err;
            bool suc = GetOracleTNSName(out orahome, out err);

            if (suc == false)
            {
                MessageBox.Show(err);
                return;
            }

            rtxtMsg.Text = orahome;
        }

        private object FindRegValue(RegistryKey key, string name)
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

        private void btnConfigFormat_Click(object sender, EventArgs e)
        {
            //BIGBOBO =
            //(DESCRIPTION =
            //  (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))
            //  (CONNECT_DATA =
            //    (SERVER = DEDICATED)
            //    (SERVICE_NAME = BIGBOBO)
            //  )
            //)

            //正则
            //([a-zA-Z0-9]+)(\s*=\s*\(DESCRIPTION\s*=\s*\(\s*ADDRESS\s*=\s*\(PROTOCOL\s*=\s*)([a-zA-Z]+)\s*\)(\s*\(\s*HOST\s*=\s*)([a-zA-Z0-9.]+)(\s*\)\s*\(\s*PORT\s*=\s*)([0-9]+)(\s*\)\s*\)\s*\(CONNECT_DATA\s*=\s*)((\([A-Za-z_]+\s*=\s*[A-Za-z0-9_]+\s*\)\s*){1,3})(\s*\)\s*\)\s*)
            //匹配结果


            string tnsFile;
            string err;
            bool suc = GetOracleTNSName(out tnsFile, out err);
            if (suc == false)
            {
                MessageBox.Show(err);
                return;
            }

            if (File.Exists(tnsFile) == false)
            {
                MessageBox.Show("文件" + tnsFile + "不存在");
                return;
            }

            string tns = File.ReadAllText(tnsFile);
            rtxtMsg.Text = tns;
        }

        private MatchCollection Match(string tnsInput)
        {
            if (tnsInput == null) return null;
            //string regEx = @"([a-zA-Z0-9]+)(\s*=\s*\(DESCRIPTION\s*=\s*\(\s*ADDRESS\s*=\s*\(PROTOCOL\s*=\s*)([a-zA-Z]+)\s*\)(\s*\(\s*HOST\s*=\s*)([a-zA-Z0-9.]+)(\s*\)\s*\(\s*PORT\s*=\s*)([0-9]+)(\s*\)\s*\)\s*\(CONNECT_DATA\s*=\s*)((\([A-Za-z_]+\s*=\s*[A-Za-z0-9_]+\s*\)\s*){1,3})(\s*\)\s*\)\s*)";
            string regEx = @"(\s{1}[a-zA-Z0-9_.-]+)(\s*=\s*\(DESCRIPTION\s*=\s*)(\(\s*ADDRESS_LIST\s*=\s*){0,1} (\(\s*ADDRESS\s*=\s*\(PROTOCOL\s*=\s*)([a-zA-Z]+)\s*\)(\s*\(\s*HOST\s*=\s*)([a-zA-Z0-9-_.]+)(\s*\)\s*\(\s*PORT\s*=\s*)([0-9]+)(\s*\)\s*\)\s*)(\s*\)\s*){0,1} (\(\s*CONNECT_DATA\s*=\s*)((\([A-Za-z_]+\s*=\s*[A-Za-z0-9_:/.]+\s*\)\s*){1,3})(\s*\)\s*\))";

            var reg = new Regex(regEx);

            var matchs = reg.Matches(tnsInput);
            return matchs;
        }

        private Dictionary<string, string> PickupTNSConfig(string tnsContent)
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

        private void btnOraVersion_Click(object sender, EventArgs e)
        {
            string version;
            string err;
            bool suc = GetOracleVersion(out version, out err);

            if (suc == false)
            {
                MessageBox.Show(err);
                return;
            }
            rtxtMsg.Text = "ORACLE版本：" + version;
        }

        private bool GetOracleTNSName(out string path, out string err)
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

        private bool GetOracleHome(out string oracleHome, out string errMsg)
        {
            oracleHome = "";
            errMsg = "";

            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.LocalMachine;
            RegistryKey key = reg.OpenSubKey("SOFTWARE");
            key = key.OpenSubKey("ORACLE");

            if (key == null)
            {
                errMsg = "未安装oracle";
                return false;
            }

            object orahome = FindRegValue(key, "ORACLE_HOME");

            if (orahome == null)
            {
                errMsg = "未能找到ORACLE主目录";
                return false;
            }

            oracleHome = orahome.ToString();
            return true;
        }

        private bool GetOracleVersion(out string version, out string err)
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

        private void btnBackupTNSName_Click(object sender, EventArgs e)
        {
            string err;
            string bak;
            bool suc = BackupTnsname(out bak, out err);
            if (suc == false)
            {
                MessageBox.Show(err);
                return;
            }
            else
            {
                rtxtMsg.Text = "已备份到:" + bak;
            }
        }

        private bool BackupTnsname(out string backFile, out string err)
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

        private void btnUpdateTNSName_Click(object sender, EventArgs e)
        {
            //0、取值判断
            string alisName = txtAlisName.Text.Trim().ToUpper();
            string ip = txtIP.Text.Trim();
            string port = txtPort.Text.Trim();
            string sid = txtSID.Text.Trim();
            string user = txtUser.Text.Trim();
            string pwd = txtPwd.Text.Trim();

            if (string.IsNullOrEmpty(alisName) || string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(port)
                || string.IsNullOrEmpty(sid))
            {
                MessageBox.Show("有必填项未填写");
                return;
            }


            //1、备份tns
            string bak;
            string err;
            bool suc = BackupTnsname(out bak, out err);
            if (suc == false)
            {
                MessageBox.Show(err);
                return;
            }

            //2、获取oracle版本
            string version;
            suc = GetOracleVersion(out version, out err);
            if (suc == false)
            {
                MessageBox.Show(err);
                return;
            }

            //3、根据oracle版本生成配置串
            string config = BuildTNSConfig(version, alisName, ip, port, sid);

            //4、读取tnsname，分析其中的配置
            string tnsFile;
            suc = GetOracleTNSName(out tnsFile, out err);
            if (suc == false)
            {
                MessageBox.Show(err);
                return;
            }
            string tnsContent = File.ReadAllText(tnsFile);
            var tnsNameConfigDic = PickupTNSConfig(tnsContent);

            string afterConfig = tnsContent;
            //5、如果有该配置节，则替换;如果不存在则增加配置节
            if (tnsNameConfigDic != null && tnsNameConfigDic.ContainsKey(alisName))
            {
                string value = tnsNameConfigDic[alisName];
                if (value.StartsWith("\n") && tnsContent.IndexOf("\r" + value) >= 0)
                {
                    afterConfig = tnsContent.Replace("\r" + value, config);
                }
                else
                {
                    afterConfig = tnsContent.Replace(value, config);
                }
            }
            else
            {
                afterConfig = tnsContent  + config;
            }

            //7、将修改结果保存
            File.WriteAllText(tnsFile, afterConfig);

            //8、完成
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pwd))
            {
                TestConnct(user, pwd, alisName);
            }
            else
            {
                rtxtMsg.Text = "更新TNSName配置成功";
            }
        }

        private void btnSplitTNSName_Click(object sender, EventArgs e)
        {
            string tnsFile;
            string err;
            bool suc = GetOracleTNSName(out tnsFile, out err);
            if (suc == false)
            {
                MessageBox.Show(err);
                return;
            }
            string tnsContent = File.ReadAllText(tnsFile);

            MatchCollection matchs = Match(tnsContent);
            if (matchs == null || matchs.Count == 0)
            {
                rtxtMsg.Text = "无法匹配项";
                return;
            }

            StringBuilder sb = new StringBuilder(tnsContent.Length);
            foreach (Match m in matchs)
            {
                if (!m.Success) continue;
                if (m.Groups != null && m.Groups.Count > 1)
                {
                    sb.AppendLine("AlisName:\t" + m.Groups[1].Value);
                }
                sb.AppendLine("V:\t" + m.Value);
            }
            rtxtMsg.Text = sb.ToString();
        }

        private string BuildTNSConfig(string version, string alisName, string host, string port, string SID)
        {
            string config = string.Format(@"
{0} =
  (DESCRIPTION =
    (ADDRESS_LIST =
      (ADDRESS = (PROTOCOL = TCP)(HOST = {1})(PORT = {2}))
    )
    (CONNECT_DATA =
      (SERVICE_NAME = {3})
    )
  )
", alisName, host, port, SID);
            return config;
        }

        private void TestConnct(string user, string pwd, string serviceName)
        {
            //Data Source=lan_189;User ID=user_ahdy_shi;Password=user_ahdy_shi
            string conn = string.Format("Data Source={0};User ID={1};Password={2}", serviceName, user, pwd);
            var dbo = yltl.DBUtility.DBFactory.Create(yltl.DBUtility.eDBType.Oracle, conn);
            string err = "";
            if (dbo.TestConnection(out err))
            {
                rtxtMsg.Text = "配置成功，测试连接成功";
            }
            else
            {
                rtxtMsg.Text = "配置成功，" + err;
            }
        }
    }
}

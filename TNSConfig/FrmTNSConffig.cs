using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace TNSConfig
{
    public partial class FrmTNSConfig : Form
    {
        public FrmTNSConfig()
        {
            InitializeComponent();
        }

        OracleInfo _oraInfo = new OracleInfo();

        private void FrmTNSConfig_Load(object sender, EventArgs e)
        {
            //txtIP.Text = "10.208.1.248";
            //txtPort.Text = "1521";
            //txtSID.Text = "sxdy";
            //txtAlisName.Text = "USER_SXDY_SHENG";

            //bool checkServiceName = true;
            //rdbSERVICE_NAME.Checked = checkServiceName;
            //rdbSID.Checked = !checkServiceName;
            //chkDedicated.Checked = false;

            //txtUser.Text = "USER_SXDY_SHENG";
            //txtPwd.Text = "USER_SXDY_SHENG";

            btnLoadOraInfo_Click(sender, e);
        }

        private void LoadOraInfo()
        {
            string info;
            string err;
            ClearMessage();
            AddMessage("\r\n");
            AddMessage("**************ORACLE信息***********************\r\n");
            AddMessage("获取ORACLE信息……\r\n");
            bool suc = _oraInfo.GetOracleHome(out info, out err);
            UpdateMessage("ORACLE路径：\t" + info, err, suc);
            if (suc == false) return;

            //AddMessage("获取ORACLE版本……");
            suc = _oraInfo.GetOracleVersion(out info, out err);
            UpdateMessage("ORACLE版本：\t" + info, err, suc);
            if (suc == false) return;

            //AddMessage("获取TNSName.ora文件路径……");
            suc = _oraInfo.GetOracleTNSName(out info, out err);
            UpdateMessage("TNSName路径：\t" + info, err, suc);
            if (suc)
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    lnkTNS.Text = info;
                }));
            }

            AddMessage("\r\n***********************************************\r\n");
        }

        private void UpdateMessage(string info, string err, bool suc)
        {
            if (suc == false)
            {
                AddMessage(err);
            }
            else
            {
                AddMessage(info);
            }
        }

        private void AddMessage(string msg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    rtxtMsg.AppendText(msg + "\r\n");
                }));
            }
            else
            {
                rtxtMsg.AppendText(msg + "\r\n");
            }
        }

        private void ClearMessage()
        {
            BeginInvoke(new MethodInvoker(() => { rtxtMsg.Clear(); }));
        }

        private void btnUpdateConfig_Click(object sender, EventArgs e)
        {
            try
            {
                //0、取值判断
                string alisName = txtAlisName.Text.Trim().ToUpper();
                string ip = txtIP.Text.Trim();
                string port = txtPort.Text.Trim();
                string sid = txtSID.Text.Trim();

                if (string.IsNullOrEmpty(alisName) || string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(port)
                    || string.IsNullOrEmpty(sid))
                {
                    MessageBox.Show("有必填项未填写");
                    return;
                }

                //1、备份tns
                string bak;
                string err;
                bool suc = _oraInfo.BackupTnsname(out bak, out err);
                if (suc == false)
                {
                    MessageBox.Show(err);
                    return;
                }

                //2、获取oracle版本
                string version;
                suc = _oraInfo.GetOracleVersion(out version, out err);
                if (suc == false)
                {
                    MessageBox.Show(err);
                    return;
                }

                //4、读取tnsname，分析其中的配置
                string tnsFile;
                suc = _oraInfo.GetOracleTNSName(out tnsFile, out err);
                if (suc == false)
                {
                    MessageBox.Show(err);
                    return;
                }
                string tnsContent = File.ReadAllText(tnsFile, Encoding.Default);
                var tnsNameConfigDic = _oraInfo.PickupTNSConfig(tnsContent);


                //3、根据oracle版本生成配置串
                string defaultDomain = Sqlnetora.GetDefaultDomain(tnsFile);//根据sqlnet获取默认域
                if (!string.IsNullOrEmpty(defaultDomain))
                {
                    alisName = alisName + "." + defaultDomain;

                    AddMessage("默认域：" + defaultDomain);
                }

                TNSConfig tc = new TNSConfig(version, ip, port, "TCP", sid, alisName);
                if (rdbSERVICE_NAME.Checked) tc.ConnectData = eConnectData.SERVICE_NAME;
                else if (rdbSID.Checked) tc.ConnectData = eConnectData.SID;

                string config = tc.ToTNSString(chkDedicated.Checked);

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
                    afterConfig = tnsContent + "\r\n" + config;
                }

                //7、将修改结果保存
                File.WriteAllText(tnsFile, afterConfig, Encoding.Default);

                AddMessage("更新配置成功");

                btnTestConnect_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLoadOraInfo_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(() =>
            {
                try
                {
                    EnableControl(btnLoadOraInfo, false);
                    LoadOraInfo();
                    EnableControl(btnLoadOraInfo, true);
                }
                catch (Exception ex)
                {
                    BeginInvoke(new MethodInvoker(() => { rtxtMsg.Text = "exception：" + ex.Message; }));
                    EnableControl(btnLoadOraInfo, true);
                }
            }));
            th.Start();
        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pwd = txtPwd.Text;

            bool suc = false;

            AddMessage("\r\n======测试配置是否正确======");
            string serviceName = txtAlisName.Text.Trim();
            if (string.IsNullOrEmpty(serviceName))
            {
                AddMessage("配置服务名为空");
                return;
            }

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pwd))
            {
                AddMessage("测试用户或密码为空");
                return;
            }

            suc = TestConnect(user, pwd, suc, serviceName);
        }

        private bool TestConnect(string user, string pwd, bool suc, string serviceName)
        {
            var th = new Thread(new ThreadStart(() =>
            {
                string msg;

                //测试网络连通性

                AddMessage("开始测试网络连通性……");
                string ip = txtIP.Text.Trim();
                string port = txtPort.Text.Trim();
                suc = Network.SocketConnectTo(ip, port, out msg);

                if (suc == false)
                {
                    AddMessage(msg);
                    AddMessage(string.Format("请检查您的机器是否能够连接{0}:{1}", ip, port));
                    return;
                }
                else
                {
                    AddMessage(string.Format("连接{0}:{1}成功", ip, port));
                }
                AddMessage("");
                //测试oracle连接
                AddMessage("开始测试Oracle连接……");
                suc = OraTestConnect.TestConnect(user, pwd, serviceName, out msg);

                AddMessage(msg);
            }));
            th.Start();
            return suc;
        }

        private void EnableControl(Control ctrl, bool enable)
        {
            BeginInvoke(new MethodInvoker(() => { ctrl.Enabled = enable; }));
        }

        private void lnkTNS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(lnkTNS.Text);
            }
            catch (Exception ex)
            {
                if (File.Exists(lnkTNS.Text))
                {
                    SelectFile(lnkTNS.Text);
                    return;
                }

                MessageBox.Show(ex.Message);
            }
        }

        private void SelectFile(string file)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("Explorer.exe");
                psi.Arguments = " /select," + file;
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lnkClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rtxtMsg.Clear();
            AddMessage("\r\n");
        }

        private void txtIP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

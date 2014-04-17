using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;

namespace WebServerManager
{
    public partial class UCSite : UserControl
    {
        public UCSite()
        {
            InitializeComponent();
        }

        private string BuildResults = string.Empty;

        private Site _site;

        public Site WebSite
        {
            get
            {
                if (_site == null) _site = new Site();
                _site.Name = txtName.Text.Trim();
                _site.Port = txtPort.Text.Trim().ToInt32();
                _site.Path = txtPath.Text.Trim();
                _site.Solution = txtSolution.Text.Trim();
                return _site;
            }
            set
            {
                _site = value;
                if (_site == null) _site = new Site();
                txtName.Text = _site.Name;
                txtPort.Text = _site.Port.ToString();
                txtPath.Text = _site.Path;
                txtSolution.Text = _site.Solution;

                RefreshStopState();
            }
        }

        public void RefreshStopState()
        {
            ThreadPool.QueueUserWorkItem(InitStopState);
        }

        public Action<UCSite> DeleteHandler;

        #region ControlEvent

        private void btnExcute_Click(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;
            contextMenuStrip1.Show(control, control.PointToClient(MousePosition));
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (DeleteHandler != null)
            {
                var dr = MessageBox.Show("确定删除这个服务吗？", "删除确认框", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK) DeleteHandler(this);
            }
        }

        private void txtPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            var dr = fbd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtPath.Text = fbd.SelectedPath;
            }
        }

        private void txtSolution_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.sln|*.sln";
            ofd.InitialDirectory = "E:\\DEV\\WWW";
            var dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtSolution.Text = ofd.FileName;
            }
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        #endregion

        private void UCSite_Load(object sender, EventArgs e)
        {

        }

        private void InitStopState(object o)
        {
            var p = AppProcessor.GetProcessByNameAndPort(WebDevServerBuilder.WebDev_WebServer_Exe.Replace(".exe", ""), this.WebSite.Port);
            BeginInvoke(new MethodInvoker(() =>
                            {
                                btnStop.Enabled = p != null;
                            }));
        }

        private void UpdateMessage(string msg)
        {
            if (this.InvokeRequired)
            {
                lblState.BeginInvoke(new MethodInvoker(() => lblState.Text = msg));
            }
            else
            {
                lblState.Text = msg;
            }
        }

        #region method

        private void Build()
        {
            if (btnStop.Enabled) StopServer();
            UpdateMessage("sln building...");
            BuildResults = string.Empty;
            if (DevenvCommand.MSBuildFileFullPath.LastIndexOf("devenv", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                DevenvCommand cmd = new DevenvCommand();
                string results = cmd.BuildSolution(WebSite.Solution);
                BuildResults = results;
                Regex reg = new Regex(@"====([ \w,-:]+)====");
                string final = (from Match m in reg.Matches(results) where m.Success where m.Groups.Count > 1 select m.Groups[1].Value).FirstOrDefault();
                UpdateMessage(final ?? "sln build finish!");
            }
            else
            {
                MSBuildCommand cmd = new MSBuildCommand();
                string results = cmd.BuildSolution(WebSite.Solution);
                BuildResults = results;
                string suc = "Build succeeded.";
                Regex timeElapseReg = new Regex("Time Elapsed [0-9:.]+");
                string time = timeElapseReg.Match(results).Value;
                if (results.IndexOf(suc) >= 0)
                {
                    UpdateMessage(suc + "  " + time);
                }
                else
                {
                    Regex errorReg = new Regex(@"[0-9]+\s+Error");
                    string error = errorReg.Match(results).Value;
                    UpdateMessage("Build Failed" + "  (" + error + ")  " + time);
                }
            }
        }

        private void StartWebServer()
        {
            //UpdateMessage("webserver starting...");
            WebDevServerBuilder.Start(WebSite);
            btnStop.Enabled = true;
            //UpdateMessage("webserver running");
        }

        private void StartWeb()
        {
            //UpdateMessage("web starting...");
            System.Diagnostics.Process.Start(string.Format("http://localhost:{0}/index.qs", WebSite.Port));
            //UpdateMessage("web starting...");
        }

        private void StopServer()
        {
            AppProcessor.CloseProcessByNameAndPort(WebDevServerBuilder.WebDev_WebServer_Exe.Replace(".exe", ""), this.WebSite.Port);
            btnStop.Enabled = false;
        }
        #endregion

        #region  stripMenu

        void tsmiRun_Click(object sender, EventArgs e)
        {
            StartWeb();
        }

        void tsmiStart_Click(object sender, EventArgs e)
        {
            StartWebServer();
        }

        private void 编译ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
                                             {
                                                 Build();
                                             });
        }

        private void 编译并启动WebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
                                             {
                                                 Build();
                                                 StartWebServer();
                                             });
        }

        private void 启动ServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartWebServer();
            StartWeb();
        }

        private void 编译启动ServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                Build();
                StartWebServer();
                StartWeb();
            });
        }
        #endregion

        private void lnkSolution_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string sln = WebSite.Solution;
            if (string.IsNullOrWhiteSpace(sln)) return;
            if (File.Exists(sln))
            {
                System.Diagnostics.Process.Start(sln);
            }
            else
            {
                MessageBox.Show("The file doesn't exist:" + sln);
            }
        }

        #region messageBox

        private Form _MessageForm;
        private Form MessageForm
        {
            get
            {
                if (_MessageForm == null)
                {
                    _MessageForm = new Form();
                    _MessageForm.Text = string.Format("MSBuild Message: {0}", WebSite.Solution);
                    _MessageForm.Deactivate += (o, e) => _MessageForm.Hide();
                    _MessageForm.FormClosing += (o, e) => { _MessageForm.Hide(); e.Cancel = true; };
                    _MessageForm.StartPosition = FormStartPosition.CenterScreen;
                    _MessageForm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                    _MessageForm.Size = new Size((int)(Screen.PrimaryScreen.WorkingArea.Width * 0.8), (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.7));
                    var rtxt = new RichTextBox() { ReadOnly = true, Dock = DockStyle.Fill, BorderStyle = BorderStyle.None, Font = new Font(FontFamily.GenericMonospace, 12) };
                    _MessageForm.Controls.Add(rtxt);
                }
                return _MessageForm;
            }
        }

        private void ShowMessage(string message)
        {
            MessageForm.Controls[0].Text = message;
            MessageForm.Show();
        }

        #endregion

        private void lblState_Click(object sender, EventArgs e)
        {
            ShowMessage(BuildResults);
        }
    }
}

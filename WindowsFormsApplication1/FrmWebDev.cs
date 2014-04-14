using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WebServerManager
{
    public partial class FrmWebDev : Form
    {
        private FrmOpenFile frmOpenFile = null;

        public FrmWebDev()
        {
            InitializeComponent();
            this.Closing += new CancelEventHandler(FrmWebDev_Closing);
            this.Text = this.Text + " (v2.0)";
        }

        void FrmWebDev_Closing(object sender, CancelEventArgs e)
        {
            SaveSites();
        }

        private void SaveSites()
        {
            SiteList.Clear();
            foreach (var uc in panSites.Controls)
            {
                var us = uc as UCSite;
                if (us != null) SiteList.Add(us.WebSite);
            }
            SiteSerialize.Serialize(SiteList);
        }

        private WebDevServerBuilder WEBServerBuilder = new WebDevServerBuilder();

        private List<Site> SiteList = null;

        private void FrmWebDev_Load(object sender, EventArgs e)
        {
            WebDevInfo info = WebDevBuilderSerializer.Deserialize();
            WebDevServerBuilder.WebDev_WebServer_Exe = info.ExeName;
            WebDevServerBuilder.WebDev_WebServer_Path = info.Path;
            DevenvCommand.MSBuildFileFullPath = info.VSDevenvFileFullPath;
            txtWebDev.Text = info.GetFullName();
            txtVSDev.Text = info.VSDevenvFileFullPath.Trim('"');

            SiteList = SiteSerialize.Deserialize();
            BindSites();
        }

        private void BindSites()
        {
            if (SiteList == null || SiteList.Count == 0)
            {
                SiteList = new List<Site>(10);
            }
            foreach (var site in SiteList)
            {
                AddSite(site);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddSite(null);
        }

        private void AddSite(Site site)
        {
            UCSite uc = new UCSite();
            uc.WebSite = site;
            uc.Dock = DockStyle.Top;
            uc.DeleteHandler = DeleteUCSite;
            uc.Show();
            panSites.Controls.Add(uc);
        }

        private void DeleteUCSite(UCSite uc)
        {
            if (panSites.Controls.Contains(uc))
                panSites.Controls.Remove(uc);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            WebDevInfo info = new WebDevInfo();
            info.Path = WebDevServerBuilder.WebDev_WebServer_Path;
            info.ExeName = WebDevServerBuilder.WebDev_WebServer_Exe;


            info.SetFullName(txtWebDev.Text.Trim());
            info.VSDevenvFileFullPath = txtVSDev.Text.Trim();
            WebDevBuilderSerializer.Serialize(info);
            WebDevServerBuilder.WebDev_WebServer_Exe = info.ExeName;
            WebDevServerBuilder.WebDev_WebServer_Path = info.Path;
            DevenvCommand.MSBuildFileFullPath = info.VSDevenvFileFullPath;
            MSBuildCommand.MSBuildFileFullPath = info.VSDevenvFileFullPath;
        }

        private void txtWebDev_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var txt = sender as TextBox;
            if (txt == null) return;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.exe|*.exe";
            var dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txt.Text = ofd.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSites();
            MessageBox.Show("保存完成");
        }

        private void btnFreshState_Click(object sender, EventArgs e)
        {
            foreach (var uc in panSites.Controls)
            {
                var us = uc as UCSite;
                if (us != null) us.RefreshStopState();
            }
        }

        private void btnFileForm_Click(object sender, EventArgs e)
        {
            if (frmOpenFile == null)
            {
                frmOpenFile = new FrmOpenFile();
            }
            frmOpenFile.Show();
            frmOpenFile.BringToFront();
        }

        private void txtWebDev_TextChanged(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            ThreadPool.QueueUserWorkItem((o) => BeginInvoke(new MethodInvoker(() =>
                                                                                  {
                                                                                      if(!File.Exists(txt.Text.Trim()))
                                                                                      {
                                                                                          txt.ForeColor = Color.Red;
                                                                                      }
                                                                                      else
                                                                                      {
                                                                                          txt.ForeColor = Color.Black;
                                                                                      }
                                                                                  })));
        }
    }
}

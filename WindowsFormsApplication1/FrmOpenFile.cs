using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WebServerManager
{
    public partial class FrmOpenFile : Form
    {
        public FrmOpenFile()
        {
            InitializeComponent();
            this.Closing += new CancelEventHandler(FrmOpenFile_Closing);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            txtOraginalFile.Focus();
        }

        void FrmOpenFile_Closing(object sender, CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string file = txtOraginalFile.Text.Trim();
            if (file == string.Empty) return;

            var files = SiteFile.GetFilesBySites(file);
            if (files == null || files.Count == 0)
            {
                files = new List<FileInfo>();
                files.Add(new FileInfo(file));
            }
            var links = BuildLinkLabel(files);
            AddFileToPanel(file, links);
        }

        private void AddFileToPanel(string file, List<LinkLabel> linkList)
        {
            if (linkList == null || linkList.Count == 0) return;
            FileInfo fi = new FileInfo(file);
            if (fi.Exists == false) return;

            TabPage page = null;
            foreach (TabPage tp in tabControl1.TabPages)
            {
                if (fi.FullName == tp.Name)
                {
                    page = tp;
                }
            }

            if (page == null)
            {
                page = new TabPage(fi.Name);
                page.Name = fi.FullName;
                tabControl1.TabPages.Add(page);
            }

            tabControl1.SelectedTab = page;
            page.Controls.Clear();

            foreach (var link in linkList)
            {
                page.Controls.Add(link);
            }
        }

        private List<LinkLabel> BuildLinkLabel(List<FileInfo> fileInfos)
        {
            if (fileInfos == null || fileInfos.Count == 0) return null;

            var linkList = new List<LinkLabel>(fileInfos.Count);

            int linkCount = 0;
            foreach (var fi in fileInfos)
            {
                if (fi.Exists)
                {
                    linkCount++;

                    var link = new LinkLabel();
                    link.Text = fi.FullName;
                    link.Click += LinkLabel_Click;
                    //link.Dock = DockStyle.Top;
                    link.AutoSize = true;
                    link.Top = (link.Height + 2) * linkCount;
                    linkList.Add(link);
                }
            }

            return linkList;
        }

        private void LinkLabel_Click(object sender, EventArgs e)
        {
            LinkLabel link = sender as LinkLabel;
            if (link == null) return;
            DevenvCommand cmd = new DevenvCommand();
            cmd.EditFile(link.Text);
            link.BackColor = link.BackColor == Color.Gold ? Color.Beige : Color.Gold;
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            btnTop.BackColor = this.TopMost ? Color.Aqua : Color.Azure;
        }

        private void txtOraginalFile_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(txtOraginalFile.Text)) btnSearch_Click(sender, e);
        }

        private void lnkCloseTabPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (tabControl1.TabPages.Count > 0 && tabControl1.SelectedTab != null)
            {
                var tabPage = tabControl1.SelectedTab;
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                tabPage.Dispose();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace yltl.Tools
{
    public partial class FormClear : Form
    {
        public FormClear()
        {
            InitializeComponent();
        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            var dr = fbd.ShowDialog();
            if (dr == DialogResult.OK)
                textBox1.Text = fbd.SelectedPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _stop = false;

            richTextBox1.Clear();

            string path = textBox1.Text.Trim();
            if (Directory.Exists(path) == false)
            {
                AddMsg("路径不存在");
                return;
            }

            List<string> folders = textBox2.Text.Trim().Split(',').ToList();
            List<string> fileTypes = textBox3.Text.Trim().Split(',').ToList();
            folders.ForEach(s => { s = s.Trim(); });
            fileTypes.ForEach(s => { s = s.Trim(); });
            fileTypes.RemoveAll(s => { return string.IsNullOrEmpty(s); });
            folders.RemoveAll(s => { return string.IsNullOrEmpty(s); });

            var th = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                string pre = _RealDelete ? "正式" : "模拟";
                AddMsg(pre + "清理开始");
                Clear(path, folders, fileTypes);
                AddMsg(pre + "清理完成");
            }));
            th.Start();
        }

        private bool _stop = false;
        private bool _RealDelete = false;

        private void button2_Click(object sender, EventArgs e)
        {
            _stop = true;
            AddMsg("停止");
        }

        private void AddMsg(string msg)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                richTextBox1.AppendText(string.Format("{0}\t{1}\r\n", DateTime.Now.ToString("HH:mm:ss"), msg));
                richTextBox1.ScrollToCaret();
            }));
        }

        private void Clear(string path, List<string> folderNames, List<string> fileTypes)
        {
            if (_stop) return;
            DirectoryInfo di = new DirectoryInfo(path);
            try
            {

                Delay(10);
                bool deleteAll = (folderNames == null || folderNames.Count == 0)
                    && (fileTypes != null && fileTypes.Count > 0);

                if (deleteAll)
                {
                    AddMsg("删除:" + di.FullName);
                    if (_RealDelete) di.Delete(true);
                    return;
                }

                bool isCurrentFolderIn = false;
                if (folderNames != null && folderNames.Count > 0)
                {
                    string curentFolder = di.FullName;
                    foreach (string folder in folderNames)
                    {
                        string f = folder.Trim('\\');
                        string f1 = f + "\\";
                        string f2 = "\\" + f;
                        Delay(10);
                        if ((curentFolder.IndexOf(f1, StringComparison.CurrentCultureIgnoreCase) >= 0)
                            || (curentFolder.IndexOf(f2, StringComparison.CurrentCultureIgnoreCase) >= 0))
                        {
                            isCurrentFolderIn = true;
                            break;
                        }
                    }
                }

                if (isCurrentFolderIn)//当前文件夹在清理的文件夹列表里
                {
                    if (fileTypes != null && fileTypes.Count > 0)//如果有指定清理的文件类型
                    {
                        FileInfo[] fis = di.GetFiles();
                        foreach (var fi in fis)
                        {
                            foreach (string t in fileTypes)
                            {
                                Delay(10);
                                if (fi.FullName.EndsWith(t, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    AddMsg("删除:" + fi.FullName);
                                    if (_RealDelete) File.Delete(fi.FullName);
                                }
                            }
                            if (_stop) break;
                        }
                    }
                    else
                    {
                        //未指定类型，删除当前文件夹
                        AddMsg("删除:" + di.FullName);
                        if (_RealDelete) di.Delete(true);
                        Delay(10);
                    }
                }
            }
            catch (Exception ex)
            {
                AddMsg("===错误===：" + ex.Message);
            }

            if (di.Exists)
            {
                DirectoryInfo[] dis = di.GetDirectories();

                foreach (var d in dis)
                {
                    Delay(10);
                    Clear(d.FullName, folderNames, fileTypes);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            _RealDelete = !checkBox1.Checked;
        }

        private void Delay(int ms)
        {
            int i = System.Environment.TickCount;
            while (System.Environment.TickCount - i > ms)
            {
                Application.DoEvents();
            }
        }
    }
}

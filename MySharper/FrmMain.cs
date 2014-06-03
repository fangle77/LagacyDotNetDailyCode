using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySharper.Find;
using MySharper.Model;
using MySharper.Util;

namespace MySharper
{
    public partial class FrmMain : Form
    {
        public static List<string> Solutions = new List<string>();
        private static bool IsShowOnTopMost = false;

        private int InitialWidth = 300;
        private int InitialHeight = 200;
        private int HalfScreenWidth = 600;
        private int HalfScreenHeight = 400;

        private System.Windows.Forms.Timer AutoCloseTimer;
        private readonly List<Label> ResultLabels = new List<Label>();
        private Label SelectedLabel;

        public FrmMain()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Deactivate += FrmMain_Deactivate;

            AutoCloseTimer = new System.Windows.Forms.Timer();
            AutoCloseTimer.Interval = 60000;
            AutoCloseTimer.Tick += AutoCloseTimer_Tick;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.InitialWidth = this.Width;
            this.InitialHeight = this.Height;
            HalfScreenWidth = Screen.PrimaryScreen.WorkingArea.Width / 2;
            HalfScreenHeight = Screen.PrimaryScreen.WorkingArea.Height / 2;
            this.Location = new Point(HalfScreenWidth - 250, HalfScreenHeight - 150);

            Index();

            ResetAutoClose();
        }

        void AutoCloseTimer_Tick(object sender, EventArgs e)
        {
            CloseProgram();
        }

        private void Index()
        {
            DateTime time1 = DateTime.Now;
#if DEBUG
            if (Solutions == null || Solutions.Count == 0)
            {
                Solutions = new List<string> { @"E:\Code\WWW\DEV\Diapers\DiapersWebSite.sln" };
            }
#endif
            Indexer.StartIndex(Solutions);
            lblElapseTime.Text = DateTime.Now.Subtract(time1).TotalMilliseconds.ToString("0.0");
        }

        private void Search()
        {
            DateTime time1 = DateTime.Now;
            string keyword = txtKeyWord.Text.Trim();

            List<FileItem> items = Finder.Find(keyword);

            DateTime time2 = DateTime.Now;
            lblElapseTime.Text = string.Format("{0} - {1}", items.Count, time2.Subtract(time1).TotalMilliseconds.ToString("0.0"));
            UpdateResult(items);
            ResetAutoClose();
        }

        private void CloseProgram()
        {
            if (!IsShowOnTopMost)
            {
                this.Close();
                Application.Exit();
            }
        }

        private void ResetAutoClose()
        {
            AutoCloseTimer.Enabled = false;
            AutoCloseTimer.Enabled = true;
        }

        private void UpdateResult(List<FileItem> results)
        {
            results = results == null ? null : results.GetRange(0, Math.Min(results.Count, 20));

            panelResult.Controls.Clear();
            AdjustMaxSize(results);
            SelectedLabel = null;
            if (results == null || results.Count == 0) return;

            int resultCount = results.Count;
            if (ResultLabels.Count < resultCount)
            {
                int count = resultCount - ResultLabels.Count;
                for (int i = 0; i < count; i++)
                {
                    Label l = new Label();
                    l.MouseEnter += l_MouseEnter;
                    l.Dock = DockStyle.Top;
                    l.AutoSize = true;
                    l.AutoEllipsis = true;
                    l.Padding = new Padding(0, 5, 5, 5);
                    l.Click += label_Click;
                    ResultLabels.Add(l);
                }
            }

            Label[] currentResults = new Label[resultCount];
            int index = resultCount - 1;
            foreach (var item in results)
            {
                var label = ResultLabels[index];
                label.Text = item.DisplayText;
                label.Tag = item;
                currentResults[index--] = label;
            }


            panelResult.Controls.AddRange(currentResults);
        }

        private void AdjustMaxSize(List<FileItem> results)
        {
            int maxLength = results.Count == 0 ? 0 : results.Max(s => s.DisplayText.Length);

            var maxLengthItem = maxLength <= 0 ? null : results.Find(s => s.DisplayText.Length == maxLength);
            var maxSize = maxLengthItem == null ? new Size(0, 0) : TextRenderer.MeasureText(maxLengthItem.DisplayText, this.Font);

            int maxWidth = maxSize.Width + 17;
            maxWidth = Math.Max(InitialWidth, maxWidth);
            maxWidth = Math.Min(HalfScreenWidth, maxWidth);

            int maxHeight = (maxSize.Height + 10) * results.Count + 50;
            maxHeight = Math.Max(InitialHeight, maxHeight);
            maxHeight = Math.Min(HalfScreenHeight, maxHeight);

            this.Width = maxWidth;
            this.Height = maxHeight;
            panelResult.Width = maxWidth - 5;
            panelResult.Height = maxHeight - 5;

            //panelResult.AutoScroll = true;
            //panelResult.AutoScrollMinSize = new Size(panelResult.Width, panelResult.Height);
        }

        private void SelectLabel(int op)
        {
            ClearLabelStyle(SelectedLabel);
            if (panelResult.Controls.Count == 0) return;
            if (SelectedLabel == null)
            {
                if (op < 0) SelectedLabel = (Label)panelResult.Controls[panelResult.Controls.Count - 1];
                else SelectedLabel = (Label)panelResult.Controls[0];
            }
            else
            {
                int currentIndex = SelectedLabel.TabIndex + op;
                if (currentIndex < 0 || currentIndex > panelResult.Controls.Count - 1)
                {
                    SelectedLabel = null;
                }
                else
                {
                    SelectedLabel = (Label)panelResult.Controls[currentIndex];
                }
            }

            SelectLabelStyle(SelectedLabel);
            ResetAutoClose();
        }
        private void ClearLabelStyle(Label label)
        {
            if (label == null) return;
            label.BackColor = Color.Transparent;
        }
        private void SelectLabelStyle(Label label)
        {
            if (label == null)
            {
                txtKeyWord.Select();
            }
            else
            {
                label.BackColor = Color.SpringGreen;
                panelResult.ScrollControlIntoView(label);
            }
            ResetAutoClose();
        }

        private void OpenSelectedLabel()
        {
            if (panelResult.Controls.Count == 0) return;
            if (SelectedLabel == null) SelectedLabel = (Label)panelResult.Controls[panelResult.Controls.Count - 1];
            label_Click(SelectedLabel, null);
        }

        void l_MouseEnter(object sender, EventArgs e)
        {
            ClearLabelStyle(SelectedLabel);
            SelectedLabel = sender as Label;
            SelectLabelStyle(SelectedLabel);
        }

        void label_Click(object sender, EventArgs e)
        {
            OpenFile((sender as Label).Tag as FileItem);
            CloseProgram();
        }

        private void OpenFile(FileItem fileItem)
        {
            if (fileItem == null) return;
            if (fileItem.OpenFileLocation)
            {
                Process.Start("explorer.exe", "/select, \"" + fileItem.FullPath + "\"");
            }
            else if (fileItem.FullPath.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
            {
                Process.Start(fileItem.FullPath);
            }
            else
            {
                VSIDE.EditFile(fileItem.FullPath);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            IsShowOnTopMost = false;
            CloseProgram();
        }

        private void FrmMain_Deactivate(object sender, EventArgs e)
        {
            CloseProgram();
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter: OpenSelectedLabel(); break;
                case Keys.Up: SelectLabel(1); break;
                case Keys.Down: SelectLabel(-1); break;
                default: break;
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            IsShowOnTopMost = !IsShowOnTopMost;
            //btnTop.BackColor = IsShowOnTopMost ? Color.Chartreuse : Color.Transparent;
            btnTop.Image = IsShowOnTopMost ? global::MySharper.Properties.Resources.Anchor : global::MySharper.Properties.Resources.Bluepin;
            this.TopMost = IsShowOnTopMost;
        }

        private void txtKeyWord_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //supress key up/down change the cursor position of textbox
                case Keys.Up:
                case Keys.Down:
                    e.Handled = true;
                    break;
            }
        }
    }
}


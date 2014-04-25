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

namespace MySharper
{
    public partial class FrmMain : Form
    {
        private System.Windows.Forms.Timer AutoCloseTimer;
        private static bool IsShowOnTopMost = false;

        private int InitialWidth = 300;
        private int InitialHeight = 200;
        private int HalfScreenWidth = 600;
        private int HalfScreenHeight = 400;

        public static List<string> Solutions = new List<string>();
        private Label SelectedLabel;

        public FrmMain()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Deactivate += FrmMain_Deactivate;

            AutoCloseTimer = new System.Windows.Forms.Timer();
            AutoCloseTimer.Interval = 20000;
            AutoCloseTimer.Tick += AutoCloseTimer_Tick;
        }

        void AutoCloseTimer_Tick(object sender, EventArgs e)
        {

        }

        private void ClosePrograme()
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

        void FrmMain_Deactivate(object sender, EventArgs e)
        {
            ClosePrograme();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.InitialWidth = this.Width;
            this.InitialHeight = this.Height;
            HalfScreenWidth = Screen.PrimaryScreen.WorkingArea.Width / 2 - 40;
            HalfScreenHeight = Screen.PrimaryScreen.WorkingArea.Height / 2 - 50;

            DateTime time1 = DateTime.Now;
            //Indexer.StartInitial(@"E:\OwenProject\REST\REST.sln");
            Indexer.StartInitial(Solutions);

#if DEBUG
            if (Solutions == null || Solutions.Count == 0)
            {
                //Indexer.StartInitial(@"E:\Code\WWW\DEV\Diapers\DiapersWebSite.sln");
                Indexer.StartInitial(new List<string> { @"E:\Code\Intranet\DEV\1800DiapersIntranet2010.sln" });
            }
#endif

            lblElapseTime.Text = DateTime.Now.Subtract(time1).TotalMilliseconds.ToString();

            ResetAutoClose();
        }

        private void txtKeyWord_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            DateTime time1 = DateTime.Now;
            string keyword = txtKeyWord.Text.Trim();

            List<FileItem> items = null;
            if (keyword.Length > 10)
            {
                items = SiteSpecFinder.FindItems(keyword);
            }
            if (items == null || items.Count == 0)
            {
                items = FileItemContainer.FindItems(keyword);
            }
            DateTime time2 = DateTime.Now;
            lblElapseTime.Text = items.Count + "," + time2.Subtract(time1).TotalMilliseconds.ToString();
            UpdateResult(items);
            ResetAutoClose();
        }

        private readonly List<Label> ResultLabels = new List<Label>();

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
                    l.Click += label_Click;
                    ResultLabels.Add(l);
                }
            }

            Label[] currentResults = new Label[resultCount];
            for (int i = resultCount - 1; i >= 0; )
            {
                foreach (var item in results)
                {
                    var label = ResultLabels[i];
                    label.Text = item.DisplayText;
                    label.Tag = item.FullPath;
                    currentResults[i] = label;
                    i--;
                }
            }

            panelResult.Controls.AddRange(currentResults);
        }

        void l_MouseEnter(object sender, EventArgs e)
        {
            ClearLabelStyle(SelectedLabel);
            SelectedLabel = sender as Label;
            SelectLabelStyle(SelectedLabel);
        }

        void label_Click(object sender, EventArgs e)
        {
            string file = (sender as Label).Tag.ToString();

            if (file.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
            {
                Process.Start(file);
            }
            else
            {
                VSIDE.EditFile(file);
            }

            ClosePrograme();
        }

        private void AdjustMaxSize(List<FileItem> results)
        {
            int maxLength = results.Count == 0 ? 0 : results.Max(s => { return s.DisplayText.Length; });

            int maxWidth = (int)(maxLength * 5) + 10;
            maxWidth = Math.Max(InitialWidth, maxWidth);
            maxWidth = Math.Min(HalfScreenWidth, maxWidth);

            int maxHeight = results.Count * 20 + 60;
            maxHeight = Math.Max(InitialHeight, maxHeight);
            maxHeight = Math.Min(HalfScreenHeight, maxHeight);

            this.Width = maxWidth;
            this.Height = maxHeight;
            //panelResult.Width = maxWidth - 50;
            //panelResult.Height = maxHeight - 200;
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
            }
            ResetAutoClose();
        }

        private void OpenSelectedLabel()
        {
            if (panelResult.Controls.Count == 0) return;
            if (SelectedLabel == null) SelectedLabel = (Label)panelResult.Controls[panelResult.Controls.Count - 1];
            label_Click(SelectedLabel, null);
        }

        private void lblElapseTime_Click(object sender, EventArgs e)
        {
            ClosePrograme();
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
            btnTop.BackColor = IsShowOnTopMost ? Color.Chartreuse : Color.Transparent;
            this.TopMost = IsShowOnTopMost;
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoveYourEye
{
    public partial class FrmMain : Form
    {
        private Timer _GlobalTimer;
        private Timer _CountDownTimer;
        private DateTime _LastInvokeTime = DateTime.Now;

        private List<int> _WorkHour = new List<int> { 9, 13 };

        int _InvokeSecond = 60 * 60;
        int _ContinueSecond = 60;

        public FrmMain()
        {
            InitializeComponent();
            InitSylte();
            InitLabel();


            InitSize();
#if DEBUG
            this.Hide();
#else
            this.Hide();
#endif

            InitTimer();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Hide();
        }

        private void InitTimer()
        {
            _GlobalTimer = new Timer();
            _GlobalTimer.Interval = 10000;
            _GlobalTimer.Tick += _GlobalTimer_Tick;
            _GlobalTimer.Enabled = true;

            _CountDownTimer = new Timer();
            _CountDownTimer.Interval = 1000;
            _CountDownTimer.Tick += _CountDownTimer_Tick;
        }

        void _GlobalTimer_Tick(object sender, EventArgs e)
        {
            _GlobalTimer.Enabled = false;

            var time = DateTime.Now;
            if (_WorkHour.Contains(time.Hour))
            {
                _LastInvokeTime = time.Date.AddHours(time.Hour).AddSeconds(-20);
            }

            if (time.Subtract(_LastInvokeTime).TotalSeconds >= _InvokeSecond)
            {
                this.Show();
                _LastInvokeTime = time;
                _CountDownTimer.Enabled = true;
            }
            else
            {
                _GlobalTimer.Enabled = true;
            }
        }

        void _CountDownTimer_Tick(object sender, EventArgs e)
        {
            _CountDownTimer.Enabled = false;
            if (DateTime.Now.Subtract(_LastInvokeTime).TotalSeconds >= _ContinueSecond)
            {
                //btnClose_Click(null, null);
                this.Hide();
                _GlobalTimer.Enabled = true;
                lblCountDown.Text = _ContinueSecond.ToString();
            }
            else
            {
                _CountDownTimer.Enabled = true;
                lblCountDown.Text = (_ContinueSecond - DateTime.Now.Subtract(_LastInvokeTime).TotalSeconds).ToString("0");
            }
        }

        private void InitSylte()
        {
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.BackColor = Color.DarkTurquoise;
        }

        private void InitLabel()
        {
            lblMessage.Text = "LOVE YOUR EYES!";
            lblMessage.Font = new Font(FontFamily.GenericSerif, 72, FontStyle.Bold);
            lblMessage.ForeColor = Color.White;

            lblCountDown.Text = lblCountDown.Text = _ContinueSecond.ToString();
            lblCountDown.Font = new Font(FontFamily.GenericMonospace, 18, FontStyle.Regular);
            lblCountDown.ForeColor = Color.AliceBlue;
        }

        private void InitSize()
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            int width = screenWidth;
            int height = screenHeight;

            this.Width = width + 100;
            this.Height = height + 100;

            this.Location = new Point(-50, -50);

            InitControlLocation(width, height);
        }

        private void InitControlLocation(int width, int height)
        {
            SetControlLocation(btnClose, width, height, 0.9);
            SetControlLocation(lblMessage, width, height, 0.5);
            SetControlLocation(lblCountDown, width, height, 0.8);
        }

        private void SetControlLocation(Control control, int width, int height, double heightRate)
        {
            int x = (width - control.Width) / 2;
            int y = (int)((height - control.Height) * heightRate);
            control.Location = new Point(x, y);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
#if DEBUG
            this.Close();
#else
            this.Hide();
#endif
        }
    }
}

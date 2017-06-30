using System;
using System.Configuration;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Updater.Properties;
using Timer = System.Windows.Forms.Timer;

namespace Updater
{
    public partial class WaitForm : Form
    {
        public WaitForm()
            : this(Resources._70)
        {
        }

        private WaitForm(Image image)
        {
            InitializeComponent();
            ClientSize = new Size(image.Width, image.Height);

            AllowTransparency = true;
            BackColor = Color.Empty;
            TransparencyKey = BackColor;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;

            pictureBox1.Image = image;
        }

        public override sealed Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        private readonly Timer _oppacityTimer = new Timer();

        public void FadingClose()
        {
            _oppacityTimer.Tick -= timer_Tick;
            _oppacityTimer.Tick += timer_Tick;
            _oppacityTimer.Interval = 10;
            _oppacityTimer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Opacity -= 0.01;

            if (Opacity <= 0)
            {
                _oppacityTimer.Stop();
                Close();
            }
        }
    }
}

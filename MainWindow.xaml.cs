using System;
using System.Drawing;
using System.Windows.Forms;
using NodaTime;
using NodaTime.Extensions;

namespace GlobalClock
{
    public partial class MainWindow
    {
        private readonly NotifyIcon _notifyIcon;

        public MainWindow()
        {
            InitializeComponent();

            _notifyIcon = new NotifyIcon
            {
                Icon = new Icon("Earth-icon.ico"),
                Visible = true,
                ContextMenuStrip = new ContextMenuStrip()
            };

            _notifyIcon.ContextMenuStrip.Items.Add("Exit", null, (sender, args) => Close());

            var timer = new Timer
            {
                Interval = 1000
            };

            timer.Tick += OnTimerTick;
            timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            // List of timezones: https://gist.github.com/jrolstad/5ca7d78dbfe182d7c1be
            string sfo = GetTimeForTimeZone("America/Los_Angeles");
            string nyc = GetTimeForTimeZone("America/New_York");
            string lon = GetTimeForTimeZone("Europe/London");
            string cpt = GetTimeForTimeZone("Africa/Johannesburg");
            string tky = GetTimeForTimeZone("Asia/Tokyo");

            // ReSharper disable once LocalizableElement
            _notifyIcon.Text = $"SFO {sfo} | NYC {nyc} | LON {lon} | TKY {tky}";
        }

        private static string GetTimeForTimeZone(string timezone)
        {
            return SystemClock.Instance
                .InZone(DateTimeZoneProviders.Tzdb[timezone])
                .GetCurrentTimeOfDay()
                .ToString("HH:mm", null);
        }
    }
}
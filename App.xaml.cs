using System.IO;
using Forms = System.Windows.Forms;

namespace GlobalClock;

public partial class App
{
    private NotifyIcon? _notifyIcon;
    private Forms.Timer? _timer;
    private ContextMenuStrip? _contextMenu;

    protected override void OnStartup(System.Windows.StartupEventArgs e)
    {
        base.OnStartup(e);

        _contextMenu = new ContextMenuStrip();
        _contextMenu.Items.Add(new ToolStripMenuItem("Exit", null, (_, _) => Shutdown()));

        _notifyIcon = new NotifyIcon
        {
            Icon = new Icon(Path.Combine(AppContext.BaseDirectory, "Earth-icon.ico")),
            Visible = true,
            ContextMenuStrip = _contextMenu
        };

        _timer = new Forms.Timer { Interval = 1000 };
        _timer.Tick += OnTimerTick;
        _timer.Start();

        UpdateTooltip();
    }

    protected override void OnExit(System.Windows.ExitEventArgs e)
    {
        if (_timer is not null)
        {
            _timer.Stop();
            _timer.Tick -= OnTimerTick;
            _timer.Dispose();
            _timer = null;
        }

        if (_notifyIcon is not null)
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }

        if (_contextMenu is not null)
        {
            _contextMenu.Dispose();
            _contextMenu = null;
        }

        base.OnExit(e);
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        UpdateTooltip();
    }

    private void UpdateTooltip()
    {
        if (_notifyIcon is null)
        {
            return;
        }

        string sfo = GetTimeForTimeZone("America/Los_Angeles");
        string nyc = GetTimeForTimeZone("America/New_York");
        string lon = GetTimeForTimeZone("Europe/London");
        string tky = GetTimeForTimeZone("Asia/Tokyo");

        _notifyIcon.Text = $"SFO {sfo} | NYC {nyc} | LON {lon} | TKY {tky}";
    }

    private static string GetTimeForTimeZone(string timezoneId)
    {
        string resolvedId = timezoneId;

        if (TimeZoneInfo.TryConvertIanaIdToWindowsId(timezoneId, out string? windowsId))
        {
            resolvedId = windowsId;
        }

        var timezone = TimeZoneInfo.FindSystemTimeZoneById(resolvedId);
        var localTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, timezone);

        return localTime.ToString("HH:mm");
    }
}

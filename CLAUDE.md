# GlobalClock

Windows system tray application that displays multiple timezone clocks. No main window — runs entirely as a notification area icon with a tooltip showing current times.

## Tech Stack

- **Language:** C#
- **Framework:** .NET 10 (WPF + WinForms NotifyIcon), SDK-style project
- **Key dependency:** NodaTime 3.x (timezone handling via TZDB)
- **Output type:** WinExe (no console)

## Project Structure

Single-project solution. No test projects.

| File | Purpose |
|------|---------|
| `MainWindow.xaml.cs` | All core logic: NotifyIcon setup, 1-second timer, timezone display |
| `MainWindow.xaml` | Hidden window (ShowInTaskbar=False, Visibility=Hidden) |
| `Earth-icon.ico` | System tray icon |

## How It Works

- `MainWindow` creates a `System.Windows.Forms.NotifyIcon` in the system tray
- A 1-second timer updates the tooltip with formatted times: `SFO 07:23 | NYC 10:23 | LON 15:23 | CPT 17:23 | TKY 00:23`
- Uses NodaTime `SystemClock.Instance.InZone(DateTimeZoneProviders.Tzdb[tz])` for timezone conversion
- Right-click context menu has an "Exit" option

## Build

```powershell
dotnet build
dotnet publish -c Release
```

## Notes

- Timezones are hardcoded in `MainWindow.xaml.cs` (SFO, NYC, LON, CPT, TKY)

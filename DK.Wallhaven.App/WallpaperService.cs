namespace DK.Wallhaven.App {

  using System.Runtime.InteropServices;
  using Microsoft.Win32;

  enum WallpaperStyle {
    Fill,
    Fit,
    Stretch,
    Tile,
    Center,

    /// <summary>
    /// Windows 8+ only!
    /// </summary>
    Span,
  }

  static class WallpaperService {

    readonly static object locker = new object();

    public static void SetWallpaper(string path, WallpaperStyle style) {

      var wallpaperStyle
        = style == WallpaperStyle.Fill    ? "10"
        : style == WallpaperStyle.Fit     ? "6"
        : style == WallpaperStyle.Span    ? "22"
        : style == WallpaperStyle.Stretch ? "2"
        : style == WallpaperStyle.Tile    ? "0"
        : style == WallpaperStyle.Center  ? "0"
        : throw new System.ArgumentException(nameof(style));

      var tileWallpaper
        = style == WallpaperStyle.Tile    ? "1"
        : style == WallpaperStyle.Fill    ? "0"
        : style == WallpaperStyle.Fit     ? "0"
        : style == WallpaperStyle.Span    ? "0"
        : style == WallpaperStyle.Stretch ? "0"
        : style == WallpaperStyle.Center  ? "0"
        : throw new System.ArgumentException(nameof(style));

      lock(locker) {
        using (var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true)) {
          key.SetValue(@"WallpaperStyle", wallpaperStyle);
          key.SetValue(@"TileWallpaper", tileWallpaper);
        }

        SystemParametersInfo(
          SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
      }

    }

    const int SPI_SETDESKWALLPAPER = 20;
    const int SPIF_UPDATEINIFILE = 0x01;
    const int SPIF_SENDWININICHANGE = 0x02;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

  }

}
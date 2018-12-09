namespace DK.Wallhaven.App {

  using System;
  using System.Threading.Tasks;
  using System.Windows;

  class SetDesktopBackgroundCommand : Command<int> {

    readonly Func<int, Task<string>> downloadWallpaper;

    public SetDesktopBackgroundCommand(Func<int, Task<string>> downloadWallpaper) =>
      this.downloadWallpaper=downloadWallpaper;

    public override async void Execute(int parameter) {
      try {
        var path = await this.downloadWallpaper(parameter);
        WallpaperService.SetWallpaper(path, WallpaperStyle.Stretch);
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Oops!");
      }
    }

  }

}
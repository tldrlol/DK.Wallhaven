namespace DK.Wallhaven.App {

  using System;
  using System.IO;
  using System.Net.Http;
  using System.Windows;

  public partial class App : Application {

    protected override void OnStartup(StartupEventArgs e) {
      base.OnStartup(e);

      var config = Config.Generate(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

      EnsureDirectoriesExist(config);

      var wallhavenClient = new Client(new HttpClient());

      var downloadThumbnail = Function.Memoize((string s) =>
        wallhavenClient.Download(config.thumbnailDirectory, s));

      var downloadWallpaper = Function.Memoize(async (int id) => {
        var result = await wallhavenClient.Wallpaper(id).ConfigureAwait(false);
        return await wallhavenClient.Download(config.wallpaperDirectory, result.src).ConfigureAwait(false);
      });

      new MainWindow {
        DataContext = new MainWindowViewModel {
          SearchCommand = new SearchCommand(
            wallhavenClient,
            downloadThumbnail,
            new SetDesktopBackgroundCommand(downloadWallpaper))
        },
      }.Show();
    }

    static void EnsureDirectoriesExist(Config config) {
      foreach (var d in new[] { config.thumbnailDirectory, config.wallpaperDirectory })
        Directory.CreateDirectory(d);
    }

  }

}

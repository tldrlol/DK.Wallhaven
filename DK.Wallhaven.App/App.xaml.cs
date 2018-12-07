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
      var thumbnailManager = new ImageManager(config.thumbnailDirectory, wallhavenClient.Thumbnail);

      new MainWindow {
        DataContext = new MainViewModel(wallhavenClient, thumbnailManager),
      }.Show();
    }

    static void EnsureDirectoriesExist(Config config) {
      foreach (var d in new[] { config.thumbnailDirectory, config.wallpaperDirectory })
        Directory.CreateDirectory(d);
    }

  }

}

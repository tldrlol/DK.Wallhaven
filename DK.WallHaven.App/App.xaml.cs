namespace DK.WallHaven.App {

  using System.Windows;

  public partial class App : Application {

    protected override void OnStartup(StartupEventArgs e) {
      base.OnStartup(e);

      var wallhavenClient = new Client(new System.Net.Http.HttpClient());

      new MainWindow {
        DataContext = new MainViewModel(wallhavenClient),
      }.Show();
    }

  }

}

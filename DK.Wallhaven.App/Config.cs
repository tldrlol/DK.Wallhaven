namespace DK.Wallhaven.App {

  using System.IO;

  class Config {

    public readonly string thumbnailDirectory;
    public readonly string wallpaperDirectory;

    public Config(string thumbnailDirectory, string wallpaperDirectory) =>
      (this.thumbnailDirectory, this.wallpaperDirectory) = (thumbnailDirectory, wallpaperDirectory);

    public static Config Generate(string rootDirectory) =>
      new Config
        ( thumbnailDirectory: Path.Combine(rootDirectory, "DK.Wallhaven", "Thumbnails")
        , wallpaperDirectory: Path.Combine(rootDirectory, "DK.Wallhaven", "Wallpapers")
        );
  }

}

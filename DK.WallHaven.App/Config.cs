namespace DK.WallHaven.App {

  using System.IO;

  class Config {

    public readonly string thumbnailDirectory;
    public readonly string wallpaperDirectory;

    public Config(string thumbnailDirectory, string wallpaperDirectory) =>
      (this.thumbnailDirectory, this.wallpaperDirectory) = (thumbnailDirectory, wallpaperDirectory);

    public static Config Generate(string rootDirectory) {
      return new Config
        ( thumbnailDirectory: Path.Combine(rootDirectory, "DK.WallHaven", "Thumbnails")
        , wallpaperDirectory: Path.Combine(rootDirectory, "DK.WallHaven", "Wallpapers")
        );
    }
  }

}

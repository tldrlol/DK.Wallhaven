namespace DK.Wallhaven {

  using System;
  using System.IO;
  using System.Threading.Tasks;
  
  public interface IImageManager {

    /// <summary>
    /// Returns a URI to a file containing the given image
    /// </summary>
    Task<string> GetImage(int id);

  }

  public class ImageManager : IImageManager {

    readonly string directory;
    readonly Func<int, Task<Stream>> getImage;

    public ImageManager(string directory, Func<int, Task<Stream>> getImage) =>
      (this.directory, this.getImage) = (directory, getImage);

    public async Task<string> GetImage(int id) {

      var path = Path.Combine(this.directory, $"{id}.jpg");

      if (File.Exists(path))
        return path;

      using (var input = await this.getImage(id)) {
        var output = File.Create(path);
        try {
          await input.CopyToAsync(output);
        }
        catch {
          File.Delete(path);
          throw;
        }
        finally {
          output.Dispose();
        }
      }

      return path;

    }

  }

}
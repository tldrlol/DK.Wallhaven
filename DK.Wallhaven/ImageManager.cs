namespace DK.Wallhaven {

  using System;
  using System.Collections.Concurrent;
  using System.IO;
  using System.Threading.Tasks;

  public class ImageManager : IImageManager {

    readonly string directory;
    readonly Func<int, Task<Stream>> getImage;

    readonly ConcurrentDictionary<int, Task<string>> requests =
      new ConcurrentDictionary<int, Task<string>>();

    public ImageManager(string directory, Func<int, Task<Stream>> getImage) =>
      (this.directory, this.getImage) = (directory, getImage);

    public Task<string> Get(int id) =>
      this.requests.GetOrAdd(id, this.GetImpl);

    async Task<string> GetImpl(int id) {

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
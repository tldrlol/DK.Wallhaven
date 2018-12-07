namespace DK.Wallhaven {
  using System.Threading.Tasks;

  public interface IImageManager {

    /// <summary>
    /// Returns a URI to a file containing the given image
    /// </summary>
    Task<string> Get(int id);

  }

}
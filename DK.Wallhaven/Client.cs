namespace DK.Wallhaven {

  using System.IO;
  using System.Linq;
  using System.Net.Http;
  using System.Threading.Tasks;
  using HtmlAgilityPack;

  public class Client : IClient {

    readonly HttpClient httpClient;

    public Client(HttpClient httpClient) =>
      this.httpClient = httpClient;

    async Task<HtmlDocument> LoadHtmlAsync(string url) {
      var html = await this.httpClient.GetStringAsync(url).ConfigureAwait(false);
      var doc = new HtmlDocument();
      doc.LoadHtml(html);
      return doc;
    }

    public async Task<SearchResult[]> Search(SearchParameters parameters) {
      var url = ApiFormat.SearchUrl(parameters);
      var doc = await this.LoadHtmlAsync(url).ConfigureAwait(false);

      return doc.DocumentNode.Descendants()
        .Where(x => {
          var classes = x.GetClasses();
          return classes.Contains("thumb")
              && classes.Contains("thumb-nsfw") == false;
        })
        .Select(x => {
          var id = int.Parse(x.Attributes["data-wallpaper-id"].Value);
          var thumbnailSrc = x.Descendants().Where(d => d.Name == "img").First().Attributes["data-src"].Value;
          return new SearchResult(id: id, thumbnailSrc: thumbnailSrc);
        })
        .ToArray();
    }

    public async Task<WallpaperResult> Wallpaper(int id) {
      var doc = await this.LoadHtmlAsync($"https://alpha.wallhaven.cc/wallpaper/{id}").ConfigureAwait(false);

      var wallpaper = doc.DocumentNode.Descendants()
        .Single(x => x.Id == "wallpaper" && x.Attributes["src"] != null);

      var src    = "https:" + wallpaper.Attributes["src"].Value;
      var width  = int.Parse(wallpaper.Attributes["data-wallpaper-width"].Value);
      var height = int.Parse(wallpaper.Attributes["data-wallpaper-height"].Value);

      return new WallpaperResult(src, width, height);
    }

    public Task<Stream> Thumbnail(int id) =>
      this.httpClient.GetStreamAsync($"https://alpha.wallhaven.cc/wallpapers/thumb/small/th-{id}.jpg");

    public async Task<string> Download(string directory, string url) {
      var filename = url.Split('/').Last();
      var path = Path.Combine(directory, filename);

      if (File.Exists(path) == false) {
        using (var input = await this.httpClient.GetStreamAsync(url).ConfigureAwait(false)) {
          var output = File.Create(path);
          try {
            await input.CopyToAsync(output).ConfigureAwait(false);
          }
          catch {
            File.Delete(path);
            throw;
          }
          finally {
            output.Dispose();
          }
        }
      }

      return path;
    }

  }

}
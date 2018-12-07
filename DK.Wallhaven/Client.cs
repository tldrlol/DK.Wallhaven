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
      var html = await this.httpClient.GetStringAsync(url);
      var doc = new HtmlDocument();
      doc.LoadHtml(html);
      return doc;
    }

    public async Task<SearchResult[]> Search(SearchParameters parameters) {
      var url = ApiFormat.SearchUrl(parameters);
      var doc = await this.LoadHtmlAsync(url);

      return (
        from n in doc.DocumentNode.Descendants()
        where n.GetClasses().Contains("thumb")
        let id = int.Parse(n.Attributes["data-wallpaper-id"].Value)
        select new SearchResult(id: id)
      ).ToArray();
    }

    public async Task<Stream> Wallpaper(int id) {
      var doc = await LoadHtmlAsync($"https://alpha.wallhaven.cc/wallpaper/{id}");
      return await this.httpClient.GetStreamAsync((
        from n in doc.DocumentNode.Descendants()
        where n.Id == "wallpaper"
        select n.Attributes["src"].Value
      ).First());
    }

    public Task<Stream> Thumbnail(int id) =>
      this.httpClient.GetStreamAsync($"https://alpha.wallhaven.cc/wallpapers/thumb/small/th-{id}.jpg");

  }

}
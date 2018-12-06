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
      var doc = await LoadHtmlAsync(ApiFormat.SearchUrl(parameters));

      return (
        from n in doc.DocumentNode.Descendants()
        where n.GetClasses().Contains("thumb")
        select new SearchResult(
          id: int.Parse(n.Attributes["data-wallpaper-id"].Value),
          thumbnailSrc: n.ChildNodes.Single(x => x.Name == "img").Attributes["data-src"].Value
        )).ToArray();
    }

    public async Task<Stream> Wallpaper(int id) {
      var doc = await LoadHtmlAsync($"https://alpha.wallhaven.cc/wallpaper/{id}");
      return await this.httpClient.GetStreamAsync((
        from n in doc.DocumentNode.Descendants()
        where n.Id == "wallpaper"
        select n.Attributes["src"].Value
      ).First());
    }

  }

}
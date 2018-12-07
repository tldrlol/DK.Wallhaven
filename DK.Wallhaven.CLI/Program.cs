namespace DK.Wallhaven.CLI {

  using System;
  using System.Net.Http;
  using System.Threading.Tasks;

  static class Program {

    static async Task Main() {
      var client = new Client(new HttpClient());
      var parameters = new SearchParameters();
      var results = await client.Search(parameters);
      foreach (var r in results)
        Console.WriteLine(r.id);
    }

  }
}

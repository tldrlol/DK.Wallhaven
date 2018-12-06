namespace DK.WallHaven {

  using System;
  using System.Linq;

  public static class ApiFormat {

    public static string SearchUrl(SearchParameters p) =>
      "https://alpha.wallhaven.cc/search?" + string.Join("&", new[] {
        FormatParameter(p.query,    "q",          x => x),
        FormatParameter(p.page,     "page",       x => x.ToString()),
        FormatParameter(p.category, "categories", x => Binary((int)x)),
        FormatParameter(p.purity,   "purity",     x => Binary((int)x)),
        FormatParameter(p.sorting,  "sorting",    x => Sorting((Sorting)x)),
        FormatParameter(p.order,    "order",      x => Order((Order)x)),
      }.Where(x => x != null));

    static string Binary(int x) =>
      Convert.ToString(x, 2).PadLeft(3, '0');

    static string Sorting(Sorting x) {
      switch (x) {
        case WallHaven.Sorting.Relevance: return "relevance";
        case WallHaven.Sorting.Random:    return "random";
        case WallHaven.Sorting.DateAdded: return "date_added";
        case WallHaven.Sorting.Views:     return "views";
        case WallHaven.Sorting.Favorites: return "favorites";
        case WallHaven.Sorting.TopList:   return "toplist";
        default: throw new ArgumentException(nameof(x));
      }
    }

    static string Order(Order x) {
      switch (x) {
        case WallHaven.Order.Ascending:  return "asc";
        case WallHaven.Order.Descending: return "desc";
        default: throw new ArgumentException(nameof(x));
      }
    }

    static string FormatParameter<T>(T x, string name, Func<T, string> fmt) =>
      x == null ? null : $"{name}={fmt(x)}";

  }

}
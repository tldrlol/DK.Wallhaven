namespace DK.Wallhaven {

  using System;
  using System.Linq;

  public static class ApiFormat {

    public static string SearchUrl(SearchParameters p) =>
      "https://alpha.Wallhaven.cc/search?" + string.Join("&", new[] {
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
        case Wallhaven.Sorting.Relevance: return "relevance";
        case Wallhaven.Sorting.Random:    return "random";
        case Wallhaven.Sorting.DateAdded: return "date_added";
        case Wallhaven.Sorting.Views:     return "views";
        case Wallhaven.Sorting.Favorites: return "favorites";
        case Wallhaven.Sorting.TopList:   return "toplist";
        default: throw new ArgumentException(nameof(x));
      }
    }

    static string Order(Order x) {
      switch (x) {
        case Wallhaven.Order.Ascending:  return "asc";
        case Wallhaven.Order.Descending: return "desc";
        default: throw new ArgumentException(nameof(x));
      }
    }

    static string FormatParameter<T>(T x, string name, Func<T, string> fmt) =>
      x == null ? null : $"{name}={fmt(x)}";

  }

}
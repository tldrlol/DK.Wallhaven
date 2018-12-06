namespace DK.Wallhaven.App {

  public class SortingDisplayConverter : ValueConverter<Sorting, string> {

    public override string Convert(Sorting value) {
      switch (value) {
        case Sorting.Relevance: return "Relevance";
        case Sorting.Random:    return "Random";
        case Sorting.DateAdded: return "Date Added";
        case Sorting.Views:     return "Views";
        case Sorting.Favorites: return "Favorites";
        case Sorting.TopList:   return "Top List";
        default:                return "";
      }
    }

  }

}
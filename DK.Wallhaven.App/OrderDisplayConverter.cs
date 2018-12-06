namespace DK.Wallhaven.App {

  public class OrderDisplayConverter : ValueConverter<Order, string> {

    public override string Convert(Order value) {
      switch (value) {
        case Order.Ascending:  return "Ascending";
        case Order.Descending: return "Descending";
        default:               return "";
      }
    }

  }

}
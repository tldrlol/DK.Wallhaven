namespace DK.Wallhaven.App {

  using System;
  using System.Globalization;
  using System.Windows.Data;

  public abstract class ValueConverter<T, U> : IValueConverter {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
      this.Convert((T)value);

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
      this.ConvertBack((U)value);

    public abstract U Convert(T value);
    public virtual  T ConvertBack(U value) => throw new NotSupportedException();
  }

}
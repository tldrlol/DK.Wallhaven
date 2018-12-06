namespace DK.Wallhaven.App {

  using System.ComponentModel;
  using System.Runtime.CompilerServices;

  public class ViewModelBase : INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;

    protected void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
      if (Equals(field, value))
        return;

      field = value;
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

  }

}
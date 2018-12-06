namespace DK.Wallhaven.App {

  using System.ComponentModel;
  using System.Runtime.CompilerServices;

  public class ViewModelBase : INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;

    protected void CascadingSet<T>(ref T field, T value, string[] cascadingProps, [CallerMemberName] string propertyName = null) {
      if (this.Set(ref field, value))
        foreach (var prop in cascadingProps)
          this.RaisePropertyChanged(prop);
    }

    protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null) {
      if (Equals(field, value))
        return false;

      field = value;
      this.RaisePropertyChanged(propertyName);

      return true;
    }

    void RaisePropertyChanged(string propertyName) =>
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

  }

}
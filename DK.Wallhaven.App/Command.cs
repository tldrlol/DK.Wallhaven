namespace DK.Wallhaven.App {

  using System;

  public abstract class Command<T> : ICommand<T> {

    //The event 'Command<T>.CanExecuteChanged' is never used
    #pragma warning disable 67

    public event EventHandler CanExecuteChanged;

    #pragma warning restore 67

    public abstract bool CanExecute(T parameter);
    public abstract void Execute(T parameter);

    public bool CanExecute(object parameter) => CanExecute((T)parameter);
    public void Execute(object parameter) => Execute((T)parameter);
  }

}
namespace DK.Wallhaven.App {

  using System;
  using System.Windows.Input;

  public abstract class Command<T> : ICommand {

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter) => CanExecute((T)parameter);
    public void Execute(object parameter) => Execute((T)parameter);

    public void RaiseCanExecuteChanged() =>
      this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public virtual bool CanExecute(T parameter) => true;
    public abstract void Execute(T parameter);

  }

}
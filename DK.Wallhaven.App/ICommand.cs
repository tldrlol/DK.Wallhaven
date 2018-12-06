namespace DK.Wallhaven.App {

  using System.Windows.Input;

  interface ICommand<T> : ICommand {
    bool CanExecute(T parameter);
    void Execute(T parameter);
  }

}
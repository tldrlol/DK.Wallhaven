namespace DK.WallHaven.App {

  using System;

  public class RelayCommand<T> : Command<T> {

    readonly Action<T>     execute;
    readonly Func<T, bool> canExecute;

    public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null) =>
      (this.canExecute, this.execute) = (canExecute, execute);

    public override bool CanExecute(T parameter) =>
      this.canExecute?.Invoke(parameter) ?? true;

    public override void Execute(T parameter) =>
      this.execute(parameter);

  }

  public class RelayCommand : RelayCommand<object> {

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
      : base(execute, canExecute) { }

  }

}
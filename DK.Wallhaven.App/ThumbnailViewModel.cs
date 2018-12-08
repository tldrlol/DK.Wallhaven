namespace DK.Wallhaven.App {

  public class ThumbnailViewModel : ViewModelBase {

    public ThumbnailViewModel(int id) =>
      this.Id = id;

    int id;
    public int Id {
      get => this.id;
      set => this.Set(ref this.id, value);
    }

    string source = "";
    public string Source {
      get => this.source;
      set => this.Set(ref this.source, value);
    }

    Command<ThumbnailViewModel> setDesktopBackgroundCommand;
    public Command<ThumbnailViewModel> SetDesktopBackgroundCommand {
      get => this.setDesktopBackgroundCommand;
      set => this.Set(ref this.setDesktopBackgroundCommand, value);
    }

  }

}
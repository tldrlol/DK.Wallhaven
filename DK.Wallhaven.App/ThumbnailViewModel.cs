namespace DK.Wallhaven.App {

  public class ThumbnailViewModel : ViewModelBase {

    int id;
    public int Id {
      get => this.id;
      set => this.Set(ref this.id, value);
    }

    string thumbnailSrc = "";
    public string ThumbnailSrc {
      get => this.thumbnailSrc;
      set => this.Set(ref this.thumbnailSrc, value);
    }

    string thumbnailPath = "";
    public string ThumbnailPath {
      get => this.thumbnailPath;
      set => this.Set(ref this.thumbnailPath, value);
    }

    Command<int> setDesktopBackgroundCommand;
    public Command<int> SetDesktopBackgroundCommand {
      get => this.setDesktopBackgroundCommand;
      set => this.Set(ref this.setDesktopBackgroundCommand, value);
    }

  }

}
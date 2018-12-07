namespace DK.Wallhaven.App {

  public class ThumbnailViewModel : ViewModelBase {

    public ThumbnailViewModel(IImageManager thumbnailManager, int id) {
      this.Id = id;
      LoadImage(thumbnailManager);
    }

    async void LoadImage(IImageManager thumbnailManager) {
      try {
        this.Source = await thumbnailManager.GetImage(this.Id);
      }
      catch {
        // If it fails... what can we do?
      }
    }

    int id;
    public int Id {
      get => this.id;
      set => this.Set(ref this.id, value);
    }

    string source;
    public string Source {
      get => this.source;
      set => this.Set(ref this.source, value);
    }

  }

}
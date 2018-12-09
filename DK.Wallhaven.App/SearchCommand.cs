namespace DK.Wallhaven.App {

  using System;
  using System.Collections.ObjectModel;
  using System.Linq;
  using System.Threading.Tasks;
  using System.Windows;

  class SearchCommand : Command<MainWindowViewModel> {

    readonly IClient wallhavenClient;
    readonly Func<string, Task<string>> downloadThumbnail;
    readonly SetDesktopBackgroundCommand setDesktopBackgroundCommand;

    bool isSearching = false;
    public bool IsSearching {
      get => this.isSearching;
      set {
        if (Equals(this.isSearching, value))
          return;

        this.isSearching = value;
        this.RaiseCanExecuteChanged();
      }
    }

    public SearchCommand(
      IClient wallhavenClient,
      Func<string, Task<string>> downloadThumbnail,
      SetDesktopBackgroundCommand setDesktopBackgroundCommand)
    {
      this.wallhavenClient = wallhavenClient;
      this.downloadThumbnail = downloadThumbnail;
      this.setDesktopBackgroundCommand = setDesktopBackgroundCommand;
    }

    public override bool CanExecute(MainWindowViewModel parameter) =>
     this.IsSearching == false;

    public override async void Execute(MainWindowViewModel parameter) {
      try {
        this.IsSearching = true;

        var results = await this.wallhavenClient.Search(new SearchParameters {
          query    = parameter.SearchQuery,
          category = parameter.Categories,
          order    = parameter.Order,
          purity   = parameter.Purity,
          sorting  = parameter.SortBy,
        });

        async void loadThumbnail(ThumbnailViewModel vm) {
          try {
            vm.ThumbnailPath = await this.downloadThumbnail(vm.ThumbnailSrc);
          }
          catch {
            // TODO: The image component should reflect an error had occurred.
          }
        }

        var thumbnailsViewModels = results
          .Select(x => new ThumbnailViewModel {
            Id                          = x.id,
            ThumbnailSrc                = x.thumbnailSrc,
            SetDesktopBackgroundCommand = this.setDesktopBackgroundCommand
          })
          .ToList();

        foreach (var vm in thumbnailsViewModels)
          loadThumbnail(vm);

        parameter.Thumbnails = new ObservableCollection<ThumbnailViewModel>(thumbnailsViewModels);
      }
      catch (Exception e) {
        MessageBox.Show(e.Message, "Oops!");
      }
      finally {
        this.IsSearching = false;
      }
    }

  }

}
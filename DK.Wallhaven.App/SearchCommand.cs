namespace DK.Wallhaven.App {

  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Threading.Tasks;
  using System.Windows;

  class SearchCommand : Command<MainWindowViewModel> {

    readonly IClient wallhavenClient;
    readonly Func<string, Task<string>> downloadThumbnail;

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

    public SearchCommand(IClient wallhavenClient, Func<string, Task<string>> downloadThumbnail) =>
      (this.wallhavenClient, this.downloadThumbnail) = (wallhavenClient, downloadThumbnail);

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

        async void loadThumbnail(ThumbnailViewModel vm, string src) {
          try {
            vm.Source = await this.downloadThumbnail(src);
          }
          catch {
            // TODO: The image component should reflect an error had occurred.
          }
        }

        var thumbnailsViewModels = new List<ThumbnailViewModel>();
        foreach (var x in results) {
          var vm = new ThumbnailViewModel(x.id);
          thumbnailsViewModels.Add(vm);
          loadThumbnail(vm, x.thumbnailSrc);
        }

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
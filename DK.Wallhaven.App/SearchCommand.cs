namespace DK.Wallhaven.App {

  using System;
  using System.Collections.ObjectModel;
  using System.Linq;
  using System.Windows;

  class SearchCommand : Command<MainWindowViewModel> {

    readonly IClient wallhavenClient;
    readonly IImageManager thumbnailManager;

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

    public SearchCommand(IClient wallhavenClient, IImageManager thumbnailManager) =>
      (this.wallhavenClient, this.thumbnailManager) = (wallhavenClient, thumbnailManager);

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

        parameter.Thumbnails = new ObservableCollection<ThumbnailViewModel>(
          from r in results select new ThumbnailViewModel(this.thumbnailManager, r.id));
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
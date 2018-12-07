namespace DK.Wallhaven.App {

  using System;
  using System.Collections.ObjectModel;
  using System.Linq;
  using System.Windows;

  class MainViewModel : MainViewModelBase {

    public MainViewModel(IClient wallhavenClient, IImageManager thumbnailManager) {

      var isSearchCommandEnabled = true;
      RelayCommand searchCommand = null;

      this.SearchCommand = searchCommand = new RelayCommand(
        async _ => {
          try {
            isSearchCommandEnabled = false;
            searchCommand.RaiseCanExecuteChanged();

            var results = await wallhavenClient.Search(new SearchParameters {
              query    = this.SearchQuery,
              category = this.Categories,
              order    = this.Order,
              purity   = this.Purity,
              sorting  = this.SortBy,
            });

            var thumbnailViewModels = results.Select(x => new ThumbnailViewModel(thumbnailManager, x.id));

            this.Thumbnails = new ObservableCollection<ThumbnailViewModel>(thumbnailViewModels);
          }
          catch (Exception e) {
            MessageBox.Show(e.Message, "Oops!");
            return;
          }
          finally {
            isSearchCommandEnabled = true;
            searchCommand.RaiseCanExecuteChanged();
          }

        },
        _ => isSearchCommandEnabled);

    }

  }

}
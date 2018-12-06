namespace DK.Wallhaven.App {

  using System;
  using System.Collections.ObjectModel;
  using System.Windows;

  class MainViewModel : MainViewModelBase {

    public MainViewModel(IClient wallhavenClient) {

      var isSearchCommandEnabled = true;
      RelayCommand searchCommand = null;

      this.SearchCommand = searchCommand = new RelayCommand(
        async _ => {
          try {
            isSearchCommandEnabled = false;
            searchCommand.RaiseCanExecuteChanged();

            this.SearchResults = new ObservableCollection<SearchResult>(
              await wallhavenClient.Search(new SearchParameters {
                query    = this.SearchQuery,
                category = this.Categories,
                order    = this.Order,
                purity   = this.Purity,
                sorting  = this.SortBy,
            }));
          }
          catch (Exception e) {
            MessageBox.Show(e.Message);
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
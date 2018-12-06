namespace DK.Wallhaven.App {

  using System;
  using System.Collections.ObjectModel;
  using System.Windows;

  class MainViewModel : MainViewModelBase {

    public MainViewModel(IClient wallhavenClient) =>
      this.SearchCommand = new RelayCommand<string>(async q => {
        try {
          var results = await wallhavenClient.Search(new SearchParameters { query = q });
          this.SearchResults = new ObservableCollection<SearchResult>(results);
        }
        catch (Exception e) {
          MessageBox.Show(e.Message);
        }
      });

  }

}
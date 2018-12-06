namespace DK.WallHaven.App {

  using System.Collections.ObjectModel;
  using System.Windows.Input;

  public class MainViewModelBase : ViewModelBase {

    string searchQuery;
    public string SearchQuery {
      get => this.searchQuery;
      set => this.Set(ref this.searchQuery, value);
    }

    ObservableCollection<SearchResult> searchResults = new ObservableCollection<SearchResult>();
    public ObservableCollection<SearchResult> SearchResults {
      get => this.searchResults;
      set => this.Set(ref this.searchResults, value);
    }

    ICommand searchCommand;
    public ICommand SearchCommand {
      get => this.searchCommand;
      set => this.Set(ref this.searchCommand, value);
    }

  }

}
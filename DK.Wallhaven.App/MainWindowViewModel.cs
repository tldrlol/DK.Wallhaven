namespace DK.Wallhaven.App {

  using System.Collections.ObjectModel;

  public class MainWindowViewModel : ViewModelBase {

    string searchQuery;
    public string SearchQuery {
      get => this.searchQuery;
      set => this.Set(ref this.searchQuery, value);
    }

    public Category Categories
      => (this.General ? Category.General : 0)
       | (this.Anime   ? Category.Anime   : 0)
       | (this.People  ? Category.People  : 0);

    bool general = true;
    public bool General {
      get => this.general;
      set => this.CascadingSet(ref this.general, value, new[] { nameof(this.Categories) } );
    }

    bool anime = true;
    public bool Anime {
      get => this.anime;
      set => this.CascadingSet(ref this.anime, value, new[] { nameof(this.Categories) } );
    }

    bool people = true;
    public bool People {
      get => this.people;
      set => this.CascadingSet(ref this.people, value, new[] { nameof(this.Categories) } );
    }

    public Purity Purity
      => (this.SFW     ? Purity.SFW     : 0)
       | (this.Sketchy ? Purity.Sketchy : 0)
       | (this.NSFW    ? Purity.NSFW    : 0);

    bool sfw = true;
    public bool SFW {
      get => this.sfw;
      set => this.CascadingSet(ref this.sfw, value, new[] { nameof(this.Purity) });
    }

    bool sketchy;
    public bool Sketchy {
      get => this.sketchy;
      set => this.CascadingSet(ref this.sketchy, value, new[] { nameof(this.Purity) });
    }

    bool nsfw;
    public bool NSFW {
      get => this.nsfw;
      set => this.CascadingSet(ref this.nsfw, value, new[] { nameof(this.Purity) });
    }

    Sorting sortBy = Sorting.Relevance;
    public Sorting SortBy {
      get => this.sortBy;
      set => this.Set(ref this.sortBy, value);
    }

    Order order = Order.Descending;
    public Order Order {
      get => this.order;
      set => this.Set(ref this.order, value);
    }

    Command<MainWindowViewModel> searchCommand;
    public Command<MainWindowViewModel> SearchCommand {
      get => this.searchCommand;
      set => this.Set(ref this.searchCommand, value);
    }

    ObservableCollection<ThumbnailViewModel> thumbnails;
    public ObservableCollection<ThumbnailViewModel> Thumbnails {
      get => this.thumbnails;
      set => this.Set(ref this.thumbnails, value);
    }

  }

}
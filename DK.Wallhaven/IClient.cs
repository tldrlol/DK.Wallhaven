﻿namespace DK.Wallhaven {

  using System;
  using System.IO;
  using System.Threading.Tasks;

  [Flags]
  public enum Category {
    General = 0b100,
    Anime   = 0b010,
    People  = 0b001,
  }

  [Flags]
  public enum Purity {
    SFW     = 0b100,
    Sketchy = 0b010,
    NSFW    = 0b001,
  }

  public enum Sorting {
    Relevance,
    Random,
    DateAdded,
    Views,
    Favorites,
    TopList,
  }

  public enum Order {
    Ascending,
    Descending,
  }

  public class SearchResult {
    public readonly int id;

    public SearchResult(int id) =>
      this.id = id;
  }

  public class SearchParameters {
    public int?      page;
    public string    query;
    public Category? category;
    public Purity?   purity;
    public Sorting?  sorting;
    public Order?    order;
  }

  public interface IClient {
    Task<SearchResult[]> Search(SearchParameters parameters);
    Task<Stream> Wallpaper(int id);
    Task<Stream> Thumbnail(int id);
  }

}
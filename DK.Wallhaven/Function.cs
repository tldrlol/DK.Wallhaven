namespace DK.Wallhaven {

  using System;
  using System.Collections.Concurrent;

  public static class Function {

    public static Func<T, R> Memoize<T, R>(Func<T, R> f) {
      var dict = new ConcurrentDictionary<T, R>();
      return t => dict.GetOrAdd(t, f);
    }

  }

}
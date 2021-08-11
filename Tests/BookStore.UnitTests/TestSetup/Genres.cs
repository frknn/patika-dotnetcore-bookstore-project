using System;
using BookStore.DBOperations;
using BookStore.Entities;

namespace TestSetup
{
  public static class Genres
  {
    public static void AddGenres(this BookStoreDbContext context)
    {
      context.Genres.AddRange(
        new Genre { Name = "Science - Fiction" },
        new Genre { Name = "Personel Growth" },
        new Genre { Name = "Novel" }
      );
    }
  }
}
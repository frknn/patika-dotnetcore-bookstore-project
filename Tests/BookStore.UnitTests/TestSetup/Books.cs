using System;
using BookStore.DBOperations;
using BookStore.Entities;

namespace TestSetup
{
  public static class Books
  {
    public static void AddBooks(this BookStoreDbContext context)
    {
      context.Books.AddRange(
        new Book { Title = "Lean Startup", AuthorId = 1, GenreId = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 12) },
        new Book { Title = "Herland", AuthorId = 2, GenreId = 2, PageCount = 250, PublishDate = new DateTime(2011, 05, 24) },
        new Book { Title = "Dune", AuthorId = 3, GenreId = 2, PageCount = 540, PublishDate = new DateTime(2002, 03, 18) }
      );
    }
  }
}
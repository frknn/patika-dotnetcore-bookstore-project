using System;
using System.Linq;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.DBOperations
{
  public class DataGenerator
  {
    public static void Initialize(IServiceProvider serviceProvider)
    {
      using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
      {
        if (context.Books.Any())
        {
          return;
        }

        context.Authors.AddRange(
          new Author { FirstName = "Yaşar", LastName = "Kemal", BirthDate = new DateTime(1940, 05, 28) },
          new Author { FirstName = "Franz", LastName = "Kafka", BirthDate = new DateTime(1899, 11, 12) },
          new Author { FirstName = "Charles", LastName = "Dickens", BirthDate = new DateTime(1812, 02, 07) }
        );

        context.Genres.AddRange(
          new Genre { Name = "Science - Fiction" },
          new Genre { Name = "Personel Growth" },
          new Genre { Name = "Novel" }
        );

        context.Books.AddRange(
          new Book { Title = "Lean Startup", AuthorId = 1, GenreId = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 12) },
          new Book { Title = "Herland", AuthorId = 2, GenreId = 2, PageCount = 250, PublishDate = new DateTime(2011, 05, 24) },
          new Book { Title = "Dune", AuthorId = 3, GenreId = 2, PageCount = 540, PublishDate = new DateTime(2002, 03, 18) }
        );

        context.SaveChanges();
      }
    }
  }
}
using System;
using BookStore.DBOperations;
using BookStore.Entities;

namespace TestSetup
{
  public static class Authors
  {
    public static void AddAuthors(this BookStoreDbContext context)
    {
      context.Authors.AddRange(
        new Author { FirstName = "Yaşar", LastName = "Kemal", BirthDate = new DateTime(1940, 05, 28) },
          new Author { FirstName = "Franz", LastName = "Kafka", BirthDate = new DateTime(1899, 11, 12) },
          new Author { FirstName = "Charles", LastName = "Dickens", BirthDate = new DateTime(1812, 02, 07) }
      );
    }
  }
}
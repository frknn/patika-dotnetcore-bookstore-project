using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace TestSetup
{
  public class CommonTestFixture
  {
    public BookStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public CommonTestFixture()
    {
      var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
      Context = new BookStoreDbContext(options);
      Context.Database.EnsureCreated();
      Context.AddAuthors();
      Context.AddGenres();
      Context.AddBooks();
      Context.SaveChanges();

      Mapper = new MapperConfiguration(config => { config.AddProfile<MappingProfile>(); }).CreateMapper();
    }

  }
}
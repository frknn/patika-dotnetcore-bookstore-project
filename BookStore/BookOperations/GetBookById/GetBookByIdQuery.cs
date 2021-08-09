using System;
using System.Linq;
using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BookOperations.GetBookById
{
  public class GetBookByIdQuery
  {
    public int Id { get; set; }
    private readonly BookStoreDbContext _dbContext;

    public GetBookByIdQuery(BookStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public GetBookByIdViewModel Handle()
    {
      Book book = _dbContext.Books.Where(book => book.Id == Id).SingleOrDefault();
      if (book is null)
      {
        throw new InvalidOperationException("Kitap bulunamadı.");
      }
      GetBookByIdViewModel vm = new GetBookByIdViewModel()
      {
        Title = book.Title,
        Genre = ((GenreEnum)book.GenreId).ToString(),
        PageCount = book.PageCount,
        PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")

      };
      return vm;
    }
  }

  public class GetBookByIdViewModel
  {
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
  }
}
using System.Collections.Generic;
using System.Linq;
using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BookOperations.GetBooks
{
  public class GetBooksQuery
  {
    private readonly BookStoreDbContext _dbContext;

    public GetBooksQuery(BookStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public List<BooksViewModel> Handle()
    {
      var books = _dbContext.Books.OrderBy(book => book.Id).ToList<Book>();
      List<BooksViewModel> booksVM = new List<BooksViewModel>();
      foreach (Book book in books)
      {
        booksVM.Add(
          new BooksViewModel(){
            Title = book.Title,
            Genre = ((GenreEnum)book.GenreId).ToString(),
            PageCount = book.PageCount,
            PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
          }
        );
      }
      return booksVM;
    }
  }

  public class BooksViewModel
  {
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
  }
}
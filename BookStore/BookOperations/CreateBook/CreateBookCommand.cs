using System;
using System.Linq;
using BookStore.DBOperations;

namespace BookStore.BookOperations.CreateBook
{
  public class CreateBookCommand
  {
    public CreateBookModel Model { get; set; }

    private readonly BookStoreDbContext _dbContext;
    public CreateBookCommand(BookStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Handle()
    {
      Book book = _dbContext.Books.SingleOrDefault(book => book.Title == Model.Title);
      if (book != null)
      {
        throw new InvalidOperationException("Kitap zaten mevcut.");
      }

      book = new Book();
      book.Title = Model.Title;
      book.PageCount = Model.PageCount;
      book.GenreId = Model.GenreId;
      book.PublishDate = Model.PublishDate;

      _dbContext.Books.Add(book);
      _dbContext.SaveChanges();
    }
  }
  public class CreateBookModel
  {
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
  }
}
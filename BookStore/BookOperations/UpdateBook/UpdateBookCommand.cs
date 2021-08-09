using System;
using System.Linq;
using BookStore.DBOperations;

namespace BookStore.BookOperations.UpdateBook
{
  public class UpdateBookCommand
  {
    public int Id { get; set; }
    public UpdateBookModel Model { get; set; }
    private readonly BookStoreDbContext _dbContext;
    public UpdateBookCommand(BookStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Handle()
    {
      Book book = _dbContext.Books.SingleOrDefault(book => book.Id == Id);
      if (book == null)
      {
        throw new InvalidOperationException("Kitap bulunamadı.");
      }

      book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
      book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
      book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
      book.Title = Model.Title != default ? Model.Title : book.Title;

      _dbContext.SaveChanges();
    }
  }

  public class UpdateBookModel
  {
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
  }
}
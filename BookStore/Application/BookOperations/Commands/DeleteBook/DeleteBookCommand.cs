using System;
using System.Linq;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.BookOperations.Commands.DeleteBook
{
  public class DeleteBookCommand
  {
    public int Id { get; set; }
    private readonly BookStoreDbContext _dbContext;

    public DeleteBookCommand(BookStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Handle()
    {
      Book book = _dbContext.Books.SingleOrDefault(book => book.Id == Id);
      if (book is null)
      {
        throw new InvalidOperationException("Kitap bulunamadı.");
      }

      _dbContext.Books.Remove(book);
      _dbContext.SaveChanges();
    }
  }
}
using System;
using System.Linq;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor
{
  public class DeleteAuthorCommand
  {
    public int Id { get; set; }
    private readonly BookStoreDbContext _dbContext;

    public DeleteAuthorCommand(BookStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Handle()
    {
      Author author = _dbContext.Authors.SingleOrDefault(author => author.Id == Id);
      if (author is null)
      {
        throw new InvalidOperationException("Yazar bulunamadı.");
      }
      if (_dbContext.Books.Any(book => book.AuthorId == author.Id))
      {
        throw new InvalidOperationException("Kitabı kayıtlı olan bir yazarı silemezsiniz.");
      }

      _dbContext.Authors.Remove(author);
      _dbContext.SaveChanges();
    }
  }
}
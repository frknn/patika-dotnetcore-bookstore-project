using System;
using System.Linq;
using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
  public class UpdateAuthorCommand
  {
    public int Id { get; set; }
    public UpdateAuthorModel Model { get; set; }
    private readonly BookStoreDbContext _dbContext;
    public UpdateAuthorCommand(BookStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Handle()
    {
      Author author = _dbContext.Authors.SingleOrDefault(author => author.Id == Id);
      if (author == null)
      {
        throw new InvalidOperationException("Yazar bulunamadı.");
      }

      author.FirstName = string.IsNullOrEmpty(Model.FirstName) ? author.FirstName : Model.FirstName;
      author.LastName = string.IsNullOrEmpty(Model.LastName) ? author.LastName : Model.LastName;

      _dbContext.SaveChanges();
    }
  }
  public class UpdateAuthorModel
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}
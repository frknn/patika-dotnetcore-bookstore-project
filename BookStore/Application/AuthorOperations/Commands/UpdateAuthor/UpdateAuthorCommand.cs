using System;
using System.Linq;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
  public class UpdateAuthorCommand
  {
    public int Id { get; set; }
    public UpdateAuthorModel Model { get; set; }
    private readonly IBookStoreDbContext _dbContext;
    public UpdateAuthorCommand(IBookStoreDbContext dbContext)
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
    private string firstName;
    public string FirstName
    {
      get { return firstName; }
      set { firstName = value.Trim(); }
    }
    private string lastName;
    public string LastName
    {
      get { return lastName; }
      set { lastName = value.Trim(); }
    }
  }
}
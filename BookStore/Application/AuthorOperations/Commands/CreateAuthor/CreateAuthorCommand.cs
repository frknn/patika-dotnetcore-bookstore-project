using System;
using System.Linq;
using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor
{
  public class CreateAuthorCommand
  {
    public CreateAuthorModel Model { get; set; }
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public void Handle()
    {
      Author author = _dbContext.Authors.SingleOrDefault(author => ((author.FirstName.ToLower() + " " + author.LastName.ToLower()) == (Model.FirstName.ToLower() + " " + Model.LastName.ToLower())));
      if (author is not null)
      {
        throw new InvalidOperationException("Yazar zaten mevcut.");
      }

      author = _mapper.Map<Author>(Model);

      _dbContext.Authors.Add(author);
      _dbContext.SaveChanges();
    }
  }

  public class CreateAuthorModel
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
  }
}

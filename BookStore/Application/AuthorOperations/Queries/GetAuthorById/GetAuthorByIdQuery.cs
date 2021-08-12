using System;
using System.Linq;
using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthorById
{
  public class GetAuthorByIdQuery
  {
    public int Id { get; set; }
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorByIdQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public GetAuthorByIdViewModel Handle()
    {
      Author author = _dbContext.Authors.SingleOrDefault(author => author.Id == Id);
      if (author is null)
      {
        throw new InvalidOperationException("Yazar bulunamadı.");
      }
      GetAuthorByIdViewModel authorVM = _mapper.Map<GetAuthorByIdViewModel>(author);
      return authorVM;
    }
  }

  public class GetAuthorByIdViewModel
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BirthDate { get; set; }
  }
}
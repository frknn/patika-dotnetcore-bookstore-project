using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthors
{
  public class GetAuthorsQuery
  {
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorsQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public List<AuthorsViewModel> Handle()
    {
      var authors = _dbContext.Authors.OrderBy(author => author.Id).ToList<Author>();
      List<AuthorsViewModel> authorsVM = _mapper.Map<List<AuthorsViewModel>>(authors);
      return authorsVM;
    }
  }

  public class AuthorsViewModel
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BirthDate { get; set; }
  }

}
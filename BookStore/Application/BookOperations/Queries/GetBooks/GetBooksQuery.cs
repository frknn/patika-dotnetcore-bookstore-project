using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BookOperations.Queries.GetBooks
{
  public class GetBooksQuery
  {
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
      var books = _dbContext.Books.Include(book => book.Genre).Include(book => book.Author).OrderBy(book => book.Id).ToList<Book>();
      List<BooksViewModel> booksVM = _mapper.Map<List<BooksViewModel>>(books);
      return booksVM;
    }
  }

  public class BooksViewModel
  {
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Author { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
  }
}
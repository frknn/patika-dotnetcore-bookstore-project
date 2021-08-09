using System;
using System.Linq;
using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BookOperations.GetBookById
{
  public class GetBookByIdQuery
  {
    public int Id { get; set; }
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;


    public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public GetBookByIdViewModel Handle()
    {
      Book book = _dbContext.Books.Where(book => book.Id == Id).SingleOrDefault();
      if (book is null)
      {
        throw new InvalidOperationException("Kitap bulunamadı.");
      }
      GetBookByIdViewModel vm = _mapper.Map<GetBookByIdViewModel>(book);
      return vm;
    }
  }

  public class GetBookByIdViewModel
  {
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
  }
}
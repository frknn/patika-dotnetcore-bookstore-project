using System;
using System.Linq;
using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.BookOperations.Commands.CreateBook
{
  public class CreateBookCommand
  {
    public CreateBookModel Model { get; set; }

    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public void Handle()
    {
      Book book = _dbContext.Books.SingleOrDefault(book => book.Title == Model.Title);
      if (book != null)
      {
        throw new InvalidOperationException("Kitap zaten mevcut.");
      }

      book = _mapper.Map<Book>(Model);

      _dbContext.Books.Add(book);
      _dbContext.SaveChanges();
    }
  }
  public class CreateBookModel
  {
    private string title;
    public string Title
    {
      get { return title; }
      set { title = value.Trim(); }
    }
    public int GenreId { get; set; }
    public int AuthorId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
  }
}
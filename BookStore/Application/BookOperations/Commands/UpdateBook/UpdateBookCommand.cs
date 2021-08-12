using System;
using System.Linq;
using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.BookOperations.Commands.UpdateBook
{
  public class UpdateBookCommand
  {
    public int Id { get; set; }
    public UpdateBookModel Model { get; set; }
    private readonly IBookStoreDbContext _dbContext;
    public UpdateBookCommand(IBookStoreDbContext dbContext)
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

      book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
      book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;
      book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
      book.Title = string.IsNullOrEmpty(Model.Title) ? book.Title : Model.Title.Trim();

      _dbContext.SaveChanges();
    }
  }

  public class UpdateBookModel
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
  }
}
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
    private readonly BookStoreDbContext _dbContext;
    public UpdateBookCommand(BookStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Handle()
    {
      Book book = _dbContext.Books.SingleOrDefault(book => book.Id == Id);
      if (book == null)
      {
        throw new InvalidOperationException("Kitap bulunamadı.");
      }
      if (Model.AuthorId > 0 && _dbContext.Authors.SingleOrDefault(author => author.Id == Model.AuthorId) == null)
      {
        throw new InvalidOperationException("Girdiğiniz ID'ye sahip bir yazar bulunamadı. Kitap güncelleyebilmek için geçerli bir yazar ID'si girmeniz gerekmektedir.");
      }

      book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
      book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;
      book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
      book.Title = string.IsNullOrEmpty(Model.Title) ? book.Title : Model.Title;

      _dbContext.SaveChanges();
    }
  }

  public class UpdateBookModel
  {
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int AuthorId { get; set; }
    public int PageCount { get; set; }
  }
}
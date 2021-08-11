using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
  public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistingBookTitleIsGiven_Handle_ThrowsInvalidOperationException()
    {
      // Arrange
      Book book = new Book() { Title = "WhenAlreadyExistingBookTitleIsGiven_Handle_ThrowsInvalidOperationException", PageCount = 100, PublishDate = new System.DateTime(1990, 01, 10), GenreId = 1, AuthorId = 1 };
      _context.Books.Add(book);
      _context.SaveChanges();

      CreateBookCommand command = new CreateBookCommand(_context, _mapper);
      command.Model = new CreateBookModel() { Title = book.Title };

      // Act & Assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Kitap zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
      // arrange
      CreateBookCommand command = new CreateBookCommand(_context, _mapper);
      CreateBookModel model = new CreateBookModel()
      {
        Title = "Hobbit",
        PageCount = 1000,
        GenreId = 1,
        AuthorId = 1,
        PublishDate = DateTime.Now.Date.AddYears(-10)
      };
      command.Model = model;

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Book book = _context.Books.SingleOrDefault(book => book.Title == model.Title);

      book.Should().NotBeNull();
      book.Title.Should().Be(model.Title);
      book.PageCount.Should().Be(model.PageCount);
      book.GenreId.Should().Be(model.GenreId);
      book.AuthorId.Should().Be(model.AuthorId);
      book.PublishDate.Should().Be(model.PublishDate);
    }
  }
}
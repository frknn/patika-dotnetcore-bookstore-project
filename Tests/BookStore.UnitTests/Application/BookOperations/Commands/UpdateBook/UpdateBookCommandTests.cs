using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
  public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    public UpdateBookCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenBookIsNotFound_Handle_ThrowsInvalidOperationException()
    {
      // Arrange
      UpdateBookCommand command = new UpdateBookCommand(_context);
      command.Id = 999;
      command.Model = new UpdateBookModel() { Title = "Updated Book Name" };

      // Act & Assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Kitap bulunamadı.");
    }

    [Fact]
    public void WhenDefaultInputsAreGiven_Book_ShouldNotChange()
    {
      // arrange
      UpdateBookCommand command = new UpdateBookCommand(_context);
      command.Id = 1;
      UpdateBookModel model = new UpdateBookModel()
      {
        Title = "",
        PageCount = 0,
        GenreId = 0,
        AuthorId = 0,
      };
      command.Model = model;

      Book bookBeforeUpdate = _context.Books.SingleOrDefault(book => book.Id == command.Id);

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Book bookAfterUpdate = _context.Books.SingleOrDefault(book => book.Id == command.Id);

      bookAfterUpdate.Should().NotBeNull();
      bookAfterUpdate.Title.Should().Be(bookBeforeUpdate.Title);
      bookAfterUpdate.PageCount.Should().Be(bookBeforeUpdate.PageCount);
      bookAfterUpdate.GenreId.Should().Be(bookBeforeUpdate.GenreId);
      bookAfterUpdate.AuthorId.Should().Be(bookBeforeUpdate.AuthorId);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
    {
      // arrange
      UpdateBookCommand command = new UpdateBookCommand(_context);
      command.Id = 1;
      UpdateBookModel model = new UpdateBookModel()
      {
        Title = "Hobbit",
        PageCount = 1000,
        GenreId = 1,
        AuthorId = 1,
      };
      command.Model = model;

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Book book = _context.Books.SingleOrDefault(book => book.Id == command.Id);

      book.Should().NotBeNull();
      book.Title.Should().Be(model.Title);
      book.PageCount.Should().Be(model.PageCount);
      book.GenreId.Should().Be(model.GenreId);
      book.AuthorId.Should().Be(model.AuthorId);
    }
  }
}
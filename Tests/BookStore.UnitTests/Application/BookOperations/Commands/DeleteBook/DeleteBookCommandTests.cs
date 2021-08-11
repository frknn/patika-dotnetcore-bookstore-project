using System;
using System.Linq;
using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
  public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenBookIsNotFound_Handle_ThrowsInvalidOperationException()
    {
      // arrange
      DeleteBookCommand command = new DeleteBookCommand(_context);
      command.Id = 999;

      // act & assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Kitap bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
    {
      // arrange
      DeleteBookCommand command = new DeleteBookCommand(_context);
      command.Id = 1;

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Book book = _context.Books.SingleOrDefault(book => book.Id == command.Id);

      book.Should().BeNull();
    }
  }
}

using System;
using System.Linq;
using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
  public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIsNotFound_Handle_ThrowsInvalidOperationException()
    {
      // arrange
      DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
      command.Id = 999;

      // act & assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Yazar bulunamadı.");
    }

    [Fact]
    public void WhenGivenAuthorHasPublishedBooks_Handle_ThrowsInvalidOperationException()
    {
      // arrange
      DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
      command.Id = 2;

      // act & assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Kitabı kayıtlı olan bir yazarı silemezsiniz.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
    {
      // arrange
      Author authorWithNoBooks = new Author()
      {
        FirstName = "New",
        LastName = "Author",
        BirthDate = DateTime.Now.Date.AddYears(-100)
      };

      _context.Authors.Add(authorWithNoBooks);
      _context.SaveChanges();

      Author createdAuthor = _context.Authors.SingleOrDefault(author => ((author.FirstName.ToLower() + " " + author.LastName.ToLower()) == (authorWithNoBooks.FirstName.ToLower() + " " + authorWithNoBooks.LastName.ToLower())));

      DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
      command.Id = createdAuthor.Id;

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Author author = _context.Authors.SingleOrDefault(author => author.Id == command.Id);

      author.Should().BeNull();
    }
  }
}


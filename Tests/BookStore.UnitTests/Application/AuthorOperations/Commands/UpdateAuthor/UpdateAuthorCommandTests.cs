using System;
using System.Linq;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
  public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    public UpdateAuthorCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIsNotFound_Handle_ThrowsInvalidOperationException()
    {
      // Arrange
      UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
      command.Id = 999;
      command.Model = new UpdateAuthorModel() { FirstName = "Yaşar", LastName = "Kemal" };

      // Act & Assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Yazar bulunamadı.");
    }

    [Fact]
    public void WhenDefaultInputsAreGiven_Author_ShouldNotBeChanged()
    {
      // arrange
      UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
      command.Id = 3;
      UpdateAuthorModel model = new UpdateAuthorModel()
      {
        FirstName = "",
        LastName = ""
      };
      command.Model = model;

      Author authorBeforeUpdate = _context.Authors.SingleOrDefault(author => author.Id == command.Id);

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Author authorAfterUpdate = _context.Authors.SingleOrDefault(author => author.Id == command.Id);

      authorAfterUpdate.Should().NotBeNull();
      authorAfterUpdate.FirstName.Should().Be(authorBeforeUpdate.FirstName);
      authorAfterUpdate.LastName.Should().Be(authorBeforeUpdate.LastName);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
    {
      // arrange
      UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
      command.Id = 3;
      UpdateAuthorModel model = new UpdateAuthorModel()
      {
        FirstName = "Albert",
        LastName = "Camus",

      };
      command.Model = model;

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Author author = _context.Authors.SingleOrDefault(author => author.Id == command.Id);

      author.Should().NotBeNull();
      author.FirstName.Should().Be(model.FirstName);
      author.LastName.Should().Be(model.LastName);
    }
  }
}
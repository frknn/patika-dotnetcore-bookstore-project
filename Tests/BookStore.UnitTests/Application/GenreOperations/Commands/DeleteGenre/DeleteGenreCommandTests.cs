using System;
using System.Linq;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
  public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenGenreIsNotFound_Handle_ThrowsInvalidOperationException()
    {
      // arrange
      DeleteGenreCommand command = new DeleteGenreCommand(_context);
      command.Id = 999;

      // act & assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Genre bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
    {
      // arrange
      DeleteGenreCommand command = new DeleteGenreCommand(_context);
      command.Id = 1;

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Genre genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.Id);

      genre.Should().BeNull();
    }
  }
}

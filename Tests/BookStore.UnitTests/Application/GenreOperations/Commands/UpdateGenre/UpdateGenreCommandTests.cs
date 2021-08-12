using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
  public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenGenreIsNotFound_Handle_ThrowsInvalidOperationException()
    {
      // Arrange
      UpdateGenreCommand command = new UpdateGenreCommand(_context);
      command.Id = 999;
      command.Model = new UpdateGenreModel() { Name = "Updated Genre Name" };

      // Act & Assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Genre bulunamadı.");
    }

    [Fact]
    public void WhenGivenGenreNameAlreadyExistsWithADiffrentId_Handle_ThrowsInvalidOperationException()
    {
      // Arrange
      UpdateGenreCommand command = new UpdateGenreCommand(_context);
      command.Id = 3;
      command.Model = new UpdateGenreModel() { Name = "Personal Growth" };

      // Act & Assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Bu isimde bir Genre zaten var.");
    }

    [Fact]
    public void WhenDefaultGenreNameIsGiven_GenreName_ShouldNotBeChanged()
    {
      // arrange
      UpdateGenreCommand command = new UpdateGenreCommand(_context);
      command.Id = 3;
      UpdateGenreModel model = new UpdateGenreModel()
      {
        Name = "",
        IsActive = true
      };
      command.Model = model;

      Genre genreBeforeUpdate = _context.Genres.SingleOrDefault(genre => genre.Id == command.Id);

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Genre genreAfterUpdate = _context.Genres.SingleOrDefault(genre => genre.Id == command.Id);

      genreAfterUpdate.Should().NotBeNull();
      genreAfterUpdate.Name.Should().Be(genreBeforeUpdate.Name);
      genreAfterUpdate.IsActive.Should().Be(genreAfterUpdate.IsActive);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
    {
      // arrange
      UpdateGenreCommand command = new UpdateGenreCommand(_context);
      command.Id = 3;
      UpdateGenreModel model = new UpdateGenreModel()
      {
        Name = "Updated Genre Name",
        IsActive = false
      };
      command.Model = model;

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Genre genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.Id);

      genre.Should().NotBeNull();
      genre.Name.Should().Be(model.Name);
      genre.IsActive.Should().Be(model.IsActive);

    }
  }
}
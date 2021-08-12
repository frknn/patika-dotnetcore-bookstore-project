using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperatons.Commands.CreateGenre
{
  public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistingGenreNameIsGiven_Handle_ThrowsInvalidOperationException()
    {
      // arrange
      Genre genre = new Genre() { Name = "WhenAlreadyExistingGenreNameIsGiven_Handle_ThrowsInvalidOperationException" };
      _context.Genres.Add(genre);
      _context.SaveChanges();

      CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
      command.Model = new CreateGenreModel() { Name = genre.Name };

      // act & assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Genre zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
    {
      // arrange
      CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
      CreateGenreModel model = new CreateGenreModel()
      {
        Name = "New Genre",

      };
      command.Model = model;

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Genre genre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name);

      genre.Should().NotBeNull();
    }

  }

}

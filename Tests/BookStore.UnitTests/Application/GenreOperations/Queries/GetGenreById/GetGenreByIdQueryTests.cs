using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenreById;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenreById
{
  public class GetGenreByIdQueryTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetGenreByIdQueryTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenGenreIsNotFound_Handle_ThrowsInvalidOperationException()
    {
      // arrange
      GetGenreByIdQuery query = new GetGenreByIdQuery(_context, _mapper);
      query.Id = 999;

      // act & assert
      FluentActions
        .Invoking(() => query.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Genre bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeReturned()
    {
      // arrange
      GetGenreByIdQuery query = new GetGenreByIdQuery(_context, _mapper);
      query.Id = 2;

      // act
      FluentActions.Invoking(() => query.Handle()).Invoke();

      // assert
      Genre genre = _context.Genres.SingleOrDefault(genre => genre.Id == query.Id);

      genre.Should().NotBeNull();
    }
  }
}

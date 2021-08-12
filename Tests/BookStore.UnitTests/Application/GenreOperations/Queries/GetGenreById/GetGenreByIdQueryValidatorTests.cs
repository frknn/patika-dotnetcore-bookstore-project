using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.Application.BookOperations.Queries.GetBookById;
using BookStore.Application.GenreOperations.Queries.GetGenreById;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Queries.GetGenreById
{
  public class GetGenreByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
  {
    [Fact]
    public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
    {
      // arrange
      GetGenreByIdQuery query = new GetGenreByIdQuery(null, null);
      query.Id = 0;

      // act
      GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
      var validationResult = validator.Validate(query);

      // // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      GetGenreByIdQuery query = new GetGenreByIdQuery(null, null);
      query.Id = 1;

      // act
      GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
      var validationResult = validator.Validate(query);

      // // assert
      validationResult.Errors.Count.Should().Be(0);
    }

  }

}
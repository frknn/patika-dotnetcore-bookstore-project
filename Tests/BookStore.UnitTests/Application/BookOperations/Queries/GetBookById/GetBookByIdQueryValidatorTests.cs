using BookStore.Application.BookOperations.Queries.GetBookById;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Queries.GetBookById
{
  public class GetBookByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
  {
    [Fact]
    public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
    {
      // arrange
      GetBookByIdQuery query = new GetBookByIdQuery(null, null);
      query.Id = 0;

      // act
      GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
      var validationResult = validator.Validate(query);

      // // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      GetBookByIdQuery query = new GetBookByIdQuery(null, null);
      query.Id = 1;

      // act
      GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
      var validationResult = validator.Validate(query);

      // // assert
      validationResult.Errors.Count.Should().Be(0);
    }

  }

}
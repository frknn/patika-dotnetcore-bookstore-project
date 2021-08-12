using BookStore.Application.AuthorOperations.Queries.GetAuthorById;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorById
{
  public class GetAuthorByIdQueryValidatorTests : IClassFixture<CommonTestFixture>
  {
    [Fact]
    public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
    {
      // arrange
      GetAuthorByIdQuery query = new GetAuthorByIdQuery(null, null);
      query.Id = 0;

      // act
      GetAuthorByIdQueryValidator validator = new GetAuthorByIdQueryValidator();
      var validationResult = validator.Validate(query);

      // // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      GetAuthorByIdQuery query = new GetAuthorByIdQuery(null, null);
      query.Id = 1;

      // act
      GetAuthorByIdQueryValidator validator = new GetAuthorByIdQueryValidator();
      var validationResult = validator.Validate(query);

      // // assert
      validationResult.Errors.Count.Should().Be(0);
    }

  }

}
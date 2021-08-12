using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
  public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
  {
    [Fact]
    public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
    {
      // arrange
      DeleteGenreCommand command = new DeleteGenreCommand(null);
      command.Id = 0;

      // act
      DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
      var validationResult = validator.Validate(command);

      // // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      DeleteGenreCommand command = new DeleteGenreCommand(null);
      command.Id = 1;

      // act
      DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);
    }

  }
}

using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperations.Commands.UpdateGenre
{
  public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
  {

    [Theory]
    // name - id - isActive
    // [InlineData("Science - Fiction", 1)] - Valid
    [InlineData("   Sci   ", 1)]
    [InlineData("Sci", 1)]
    [InlineData("Science - Fiction", 0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name, int id)
    {
      // arrange
      UpdateGenreCommand command = new UpdateGenreCommand(null);
      command.Id = id;
      command.Model = new UpdateGenreModel()
      {
        Name = name,
        IsActive = true,
      };

      // act
      UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }


    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      UpdateGenreCommand command = new UpdateGenreCommand(null);
      command.Id = 3;
      command.Model = new UpdateGenreModel()
      {
        Name = "Valid Genre Name",
        IsActive = true
      };

      // act
      UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);

    }
  }

}
using System;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.GenreOperatons.Commands.CreateGenre
{
  public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
  {

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData("    ")]
    [InlineData("   Sci   ")]
    [InlineData("Sci")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
    {
      // arrange
      CreateGenreCommand command = new CreateGenreCommand(null, null);
      command.Model = new CreateGenreModel()
      {
        Name = name
      };

      // act
      CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }


    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      CreateGenreCommand command = new CreateGenreCommand(null, null);
      command.Model = new CreateGenreModel()
      {
        Name = "Valid Genre Name",
      };

      // act
      CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);
    }
  }
}
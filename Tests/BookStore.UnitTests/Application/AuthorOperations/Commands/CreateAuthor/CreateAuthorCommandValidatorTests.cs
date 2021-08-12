using System;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.BookOperations.Commands.CreateBook;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
  public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
  {

    [Theory]
    // firstName - lastName
    // [InlineData("Yaşar","Kemal")] - Valid
    [InlineData("Yaşar", "")]
    [InlineData("Yaşar", " ")]
    [InlineData("Yaşar", "  ")]
    [InlineData("Yaşar", "   ")]
    [InlineData("", "Kemal")]
    [InlineData(" ", "Kemal")]
    [InlineData("   ", "Kemal")]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData("    ", "    ")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string firstName, string lastName)
    {
      // arrange
      CreateAuthorCommand command = new CreateAuthorCommand(null, null);
      command.Model = new CreateAuthorModel()
      {
        FirstName = firstName,
        LastName = lastName,
        BirthDate = DateTime.Now.Date.AddYears(-100)
      };

      // act
      CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenDateTimeIsEqualToNow_Validator_ShouldReturnError()
    {
      // arrange
      CreateAuthorCommand command = new CreateAuthorCommand(null, null);
      command.Model = new CreateAuthorModel()
      {
        FirstName = "Yaşar",
        LastName = "Kemal",
        BirthDate = DateTime.Now.Date
      };

      // act
      CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenDateTimeIsGreaterThanNow_Validator_ShouldReturnError()
    {
      // arrange
      CreateAuthorCommand command = new CreateAuthorCommand(null, null);
      command.Model = new CreateAuthorModel()
      {
        FirstName = "Yaşar",
        LastName = "Kemal",
        BirthDate = DateTime.Now.Date.AddMilliseconds(1)
      };

      // act
      CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      CreateAuthorCommand command = new CreateAuthorCommand(null, null);
      command.Model = new CreateAuthorModel()
      {
        FirstName = "Yaşar",
        LastName = "Kemal",
        BirthDate = DateTime.Now.Date.AddYears(-100)
      };

      // act
      CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);
    }
  }
}
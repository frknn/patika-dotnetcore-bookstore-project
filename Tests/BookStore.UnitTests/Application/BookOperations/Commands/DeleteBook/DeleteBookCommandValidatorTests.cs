using BookStore.Application.BookOperations.Commands.DeleteBook;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
  public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
  {
    [Fact]
    public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
    {
      // arrange
      DeleteBookCommand command = new DeleteBookCommand(null);
      command.Id = 0;

      // act
      DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
      var validationResult = validator.Validate(command);

      // // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      DeleteBookCommand command = new DeleteBookCommand(null);
      command.Id = 1;

      // act
      DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);
    }

  }
}

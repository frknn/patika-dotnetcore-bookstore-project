using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.Application.BookOperations.Commands.DeleteBook;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
  public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
  {
    [Fact]
    public void WhenNonPositiveIdIsGiven_Validator_ShouldReturnError()
    {
      // arrange
      DeleteAuthorCommand command = new DeleteAuthorCommand(null);
      command.Id = 0;

      // act
      DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
      var validationResult = validator.Validate(command);

      // // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenPositiveIdIsGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      DeleteAuthorCommand command = new DeleteAuthorCommand(null);
      command.Id = 1;

      // act
      DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);
    }

  }
}

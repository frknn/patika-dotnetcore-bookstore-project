using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
  public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
  {
    /* 
      Boş ya da 1 karakterden uzun her string 
      valid olduğu için invalid case yok.
    */

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      UpdateAuthorCommand command = new UpdateAuthorCommand(null);
      command.Id = 1;
      command.Model = new UpdateAuthorModel()
      {
        FirstName = "Yaşar",
        LastName = "Kemal"
      };

      // act
      UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);

    }
  }

}
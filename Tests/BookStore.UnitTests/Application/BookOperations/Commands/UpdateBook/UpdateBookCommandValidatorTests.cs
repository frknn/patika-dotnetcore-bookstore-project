using BookStore.Application.BookOperations.Commands.UpdateBook;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
  public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
  {
    [Theory]
    // title - authorId - genreId - pageCount - id
    // [InlineData("Lord Of The Rings", 0, 0, 0, 1)] - Valid
    [InlineData("Lord Of The Rings", 0, 0, 0, 0)]
    [InlineData("Lord Of The Rings", 0, 0, -1, 1)]
    [InlineData("Lord Of The Rings", 0, -1, 0, 1)]
    [InlineData("Lord Of The Rings", -1, 0, 0, 1)]
    [InlineData("Lor", 0, 0, 0, 1)]
    [InlineData("   Lor   ", 0, 0, 0, 1)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string title, int authorId, int genreId, int pageCount, int id)
    {
      // arrange
      UpdateBookCommand command = new UpdateBookCommand(null);
      command.Id = id;
      command.Model = new UpdateBookModel()
      {
        Title = title,
        AuthorId = authorId,
        GenreId = genreId,
        PageCount = pageCount
      };

      // act
      UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      UpdateBookCommand command = new UpdateBookCommand(null);
      command.Id = 1;
      command.Model = new UpdateBookModel()
      {
        Title = "Lord of The Ring",
        AuthorId = 1,
        GenreId = 1,
        PageCount = 250
      };

      // act
      UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);

    }
  }

}
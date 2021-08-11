using System;
using BookStore.Application.BookOperations.Commands.CreateBook;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
  public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
  {

    [Theory]
    [InlineData("Lord Of The Rings", 1, 1, 0)]
    [InlineData("Lord Of The Rings", 1, 0, 1)]
    [InlineData("Lord Of The Rings", 0, 1, 1)]
    [InlineData("Lor", 1, 1, 1)]
    [InlineData("", 1, 1, 1)]
    [InlineData(" ", 1, 1, 1)]
    [InlineData("     ", 1, 1, 1)]
    [InlineData("", 0, 0, 0)]
    [InlineData(" ", 0, 0, 0)]
    [InlineData("Lor", 0, 0, 0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string title, int authorId, int genreId, int pageCount)
    {
      // arrange
      CreateBookCommand command = new CreateBookCommand(null, null);
      command.Model = new CreateBookModel()
      {
        Title = title,
        AuthorId = authorId,
        GenreId = genreId,
        PageCount = pageCount,
        PublishDate = DateTime.Now.Date.AddYears(-1)
      };

      // act
      CreateBookCommandValidator validator = new CreateBookCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenDateTimeIsEqualToNow_Validator_ShouldReturnError()
    {
      // arrange
      CreateBookCommand command = new CreateBookCommand(null, null);
      command.Model = new CreateBookModel()
      {
        Title = "Lord of The Rings",
        AuthorId = 1,
        GenreId = 1,
        PageCount = 250,
        PublishDate = DateTime.Now.Date
      };

      // act
      CreateBookCommandValidator validator = new CreateBookCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenGivenDateTimeIsGreaterThanNow_Validator_ShouldReturnError()
    {
      // arrange
      CreateBookCommand command = new CreateBookCommand(null, null);
      command.Model = new CreateBookModel()
      {
        Title = "Lord of The Rings",
        AuthorId = 1,
        GenreId = 1,
        PageCount = 250,
        PublishDate = DateTime.Now.Date.AddMilliseconds(1)
      };

      // act
      CreateBookCommandValidator validator = new CreateBookCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
      // arrange
      CreateBookCommand command = new CreateBookCommand(null, null);
      command.Model = new CreateBookModel()
      {
        Title = "Lord of The Rings",
        AuthorId = 1,
        GenreId = 1,
        PageCount = 250,
        PublishDate = DateTime.Now.Date.AddYears(-2)
      };

      // act
      CreateBookCommandValidator validator = new CreateBookCommandValidator();
      var validationResult = validator.Validate(command);

      // assert
      validationResult.Errors.Count.Should().Be(0);
    }
  }
}
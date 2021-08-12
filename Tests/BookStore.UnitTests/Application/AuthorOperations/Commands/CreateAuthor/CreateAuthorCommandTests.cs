using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
  public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public CreateAuthorCommandTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistingAuthorNameIsGiven_Handle_ThrowsInvalidOperationException()
    {
      // Arrange
      Author author = new Author()
      {
        FirstName = "Yakup Kadri",
        LastName = "Karaosmanoğlu",
        BirthDate = new DateTime(1900, 01, 10)
      };
      _context.Authors.Add(author);
      _context.SaveChanges();

      CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
      command.Model = new CreateAuthorModel()
      {
        FirstName = author.FirstName,
        LastName = author.LastName
      };

      // Act & Assert
      FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Yazar zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
    {
      // arrange
      CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
      CreateAuthorModel model = new CreateAuthorModel()
      {
        FirstName = "Orhan Veli",
        LastName = "Kanık",
        BirthDate = new DateTime(1912, 11, 27)
      };
      command.Model = model;

      // act
      FluentActions.Invoking(() => command.Handle()).Invoke();

      // assert
      Author author = _context.Authors.SingleOrDefault(author => ((author.FirstName.ToLower() + " " + author.LastName.ToLower()) == (model.FirstName.ToLower() + " " + model.LastName.ToLower())));

      author.Should().NotBeNull();
      author.FirstName.Should().Be(model.FirstName);
      author.LastName.Should().Be(model.LastName);
      author.BirthDate.Should().Be(model.BirthDate);
    }
  }
}
using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.AuthorOperations.Queries.GetAuthorById;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorById
{
  public class GetAuthorByIdQueryTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetAuthorByIdQueryTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenAuthorIsNotFound_Handle_ThrowsInvalidOperationException()
    {
      // arrange
      GetAuthorByIdQuery query = new GetAuthorByIdQuery(_context, _mapper);
      query.Id = 999;

      // act & assert
      FluentActions
        .Invoking(() => query.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Yazar bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeReturned()
    {
      // arrange
      GetAuthorByIdQuery query = new GetAuthorByIdQuery(_context, _mapper);
      query.Id = 2;

      // act
      FluentActions.Invoking(() => query.Handle()).Invoke();

      // assert
      Author author = _context.Authors.SingleOrDefault(author => author.Id == query.Id);

      author.Should().NotBeNull();
    }
  }
}

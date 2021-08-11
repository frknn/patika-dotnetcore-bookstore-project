using System;
using System.Linq;
using AutoMapper;
using BookStore.Application.BookOperations.Queries.GetBookById;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Queries.GetBookById
{
  public class GetBookByIdQueryTests : IClassFixture<CommonTestFixture>
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetBookByIdQueryTests(CommonTestFixture testFixture)
    {
      _context = testFixture.Context;
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenBookIsNotFound_Handle_ThrowsInvalidOperationException()
    {
      // arrange
      GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
      query.Id = 999;

      // act & assert
      FluentActions
        .Invoking(() => query.Handle())
        .Should().Throw<InvalidOperationException>()
        .And
        .Message.Should().Be("Kitap bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeReturned()
    {
      // arrange
      GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
      query.Id = 2;

      // act
      FluentActions.Invoking(() => query.Handle()).Invoke();

      // assert
      Book book = _context.Books.SingleOrDefault(book => book.Id == query.Id);

      book.Should().NotBeNull();
    }
  }
}

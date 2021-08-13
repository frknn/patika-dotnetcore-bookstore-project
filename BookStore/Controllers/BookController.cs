using System.Collections.Generic;
using AutoMapper;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.Application.BookOperations.Queries.GetBookById;
using BookStore.Application.BookOperations.Queries.GetBooks;
using BookStore.Application.BookOperations.Commands.UpdateBook;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[Controller]s")]
  public class BookController : ControllerBase
  {
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    public BookController(IBookStoreDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
      GetBooksQuery query = new GetBooksQuery(_context, _mapper);
      List<BooksViewModel> result = query.Handle();
      return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetBookById(int id)
    {
      GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
      query.Id = id;

      GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
      validator.ValidateAndThrow(query);

      GetBookByIdViewModel result = query.Handle();

      return Ok(result);
    }

    [HttpPost]
    public IActionResult CreateBook([FromBody] CreateBookModel newBook)
    {
      CreateBookCommand command = new CreateBookCommand(_context, _mapper);
      command.Model = newBook;

      CreateBookCommandValidator validator = new CreateBookCommandValidator();
      validator.ValidateAndThrow(command);

      command.Handle();

      return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
      UpdateBookCommand command = new UpdateBookCommand(_context);

      command.Id = id;
      command.Model = updatedBook;
      UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
      validator.ValidateAndThrow(command);
      command.Handle();

      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
      DeleteBookCommand command = new DeleteBookCommand(_context);

      command.Id = id;
      DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
      validator.ValidateAndThrow(command);
      command.Handle();

      return Ok();
    }
  }
}
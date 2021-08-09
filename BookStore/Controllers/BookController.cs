using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.GetBookById;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore
{
  [ApiController]
  [Route("[Controller]s")]
  public class BookController : ControllerBase
  {
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public BookController(BookStoreDbContext context, IMapper mapper)
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
      GetBookByIdViewModel result;
      try
      {
        query.Id = id;
        GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
        validator.ValidateAndThrow(query);
        result = query.Handle();
      }
      catch (Exception ex)
      {
        return NotFound(ex.Message);
      }
      return Ok(result);
    }

    [HttpPost]
    public IActionResult CreateBook([FromBody] CreateBookModel newBook)
    {
      CreateBookCommand command = new CreateBookCommand(_context, _mapper);
      try
      {
        command.Model = newBook;
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
      return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
      UpdateBookCommand command = new UpdateBookCommand(_context);
      try
      {
        command.Id = id;
        command.Model = updatedBook;
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
      DeleteBookCommand command = new DeleteBookCommand(_context);
      try
      {
        command.Id = id;
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
      return Ok();
    }
  }
}
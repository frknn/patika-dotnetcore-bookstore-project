using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBookById;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;

namespace BookStore
{
  [ApiController]
  [Route("[Controller]s")]
  public class BookController : ControllerBase
  {
    private readonly BookStoreDbContext _context;
    public BookController(BookStoreDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
      GetBooksQuery query = new GetBooksQuery(_context);
      List<BooksViewModel> result = query.Handle();
      return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetBookById(int id)
    {
      GetBookByIdQuery query = new GetBookByIdQuery(_context);
      GetBookByIdViewModel result;
      try
      {
        query.Id = id;
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
      CreateBookCommand command = new CreateBookCommand(_context);
      try
      {
        command.Model = newBook;
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
      Book book = _context.Books.SingleOrDefault(book => book.Id == id);
      if (book == null)
      {
        return BadRequest();
      }

      _context.Books.Remove(book);
      _context.SaveChanges();
      return Ok();
    }
  }
}
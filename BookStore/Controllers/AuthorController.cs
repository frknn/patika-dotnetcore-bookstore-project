using System.Collections.Generic;
using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthorById;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
  [ApiController]
  [Route("[Controller]s")]
  public class AuthorController : ControllerBase
  {
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public AuthorController(IBookStoreDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
      GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
      List<AuthorsViewModel> result = query.Handle();
      return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetAuthorById(int id)
    {
      GetAuthorByIdQuery query = new GetAuthorByIdQuery(_context, _mapper);
      query.Id = id;

      GetAuthorByIdQueryValidator validator = new GetAuthorByIdQueryValidator();
      validator.ValidateAndThrow(query);

      GetAuthorByIdViewModel result = query.Handle();

      return Ok(result);
    }

    [HttpPost]
    public IActionResult CreateAuthor([FromBody] CreateAuthorModel newAuthor)
    {
      CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
      command.Model = newAuthor;

      CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
      validator.ValidateAndThrow(command);

      command.Handle();

      return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
    {
      UpdateAuthorCommand command = new UpdateAuthorCommand(_context);

      command.Id = id;
      command.Model = updatedAuthor;
      UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
      validator.ValidateAndThrow(command);
      command.Handle();

      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
      DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
      command.Id = id;

      DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
      validator.ValidateAndThrow(command);
      command.Handle();

      return Ok();
    }
  }
}
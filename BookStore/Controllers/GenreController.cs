using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenreById;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.DBOperations;
using BookStore.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
  [ApiController]
  [Route("[Controller]s")]
  public class GenreController : ControllerBase
  {
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GenreController(IBookStoreDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetGenres()
    {
      GetGenresQuery query = new GetGenresQuery(_context, _mapper);
      List<GenresViewModel> result = query.Handle();
      return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetGenreById(int id)
    {
      GetGenreByIdQuery query = new GetGenreByIdQuery(_context, _mapper);
      query.Id = id;

      GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
      validator.ValidateAndThrow(query);

      GetGenreByIdViewModel result = query.Handle();

      return Ok(result);
    }

    [HttpPost]
    public IActionResult CreateGenre([FromBody] CreateGenreModel newGenre)
    {
      CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
      command.Model = newGenre;

      CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
      validator.ValidateAndThrow(command);

      command.Handle();

      return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedGenre)
    {
      UpdateGenreCommand command = new UpdateGenreCommand(_context);
      command.Id = id;
      command.Model = updatedGenre;

      UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
      validator.ValidateAndThrow(command);

      command.Handle();
      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGenre(int id)
    {
      DeleteGenreCommand command = new DeleteGenreCommand(_context);
      command.Id = id;

      DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
      validator.ValidateAndThrow(command);

      command.Handle();

      return Ok();
    }
  }
}
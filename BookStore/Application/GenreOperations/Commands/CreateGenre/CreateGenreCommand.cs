using System;
using System.Linq;
using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.GenreOperations.Commands.CreateGenre
{
  public class CreateGenreCommand
  {
    public CreateGenreModel Model { get; set; }
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateGenreCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public void Handle()
    {
      Genre genre = _dbContext.Genres.SingleOrDefault(genre => genre.Name == Model.Name);
      if (genre is not null)
      {
        throw new InvalidOperationException("Genre zaten mevcut.");
      }

      genre = _mapper.Map<Genre>(Model);

      _dbContext.Genres.Add(genre);
      _dbContext.SaveChanges();
    }
  }

  public class CreateGenreModel
  {
    private string name;
    public string Name
    {
      get { return name; }
      set { name = value.Trim(); }
    }
  }
}

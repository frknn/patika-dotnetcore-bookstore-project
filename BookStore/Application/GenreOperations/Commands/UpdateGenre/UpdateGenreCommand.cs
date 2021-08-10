using System;
using System.Linq;
using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre
{
  public class UpdateGenreCommand
  {
    public int Id { get; set; }
    public UpdateGenreModel Model { get; set; }
    private readonly BookStoreDbContext _dbContext;
    public UpdateGenreCommand(BookStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public void Handle()
    {
      Genre genre = _dbContext.Genres.SingleOrDefault(genre => genre.Id == Id);
      if (genre is null)
      {
        throw new InvalidOperationException("Genre bulunamadı.");
      }
      if (_dbContext.Genres.Any(genre => genre.Name.ToLower() == Model.Name.ToLower() && genre.Id != Id))
      {
        throw new InvalidOperationException("Bu isimde bir Genre zaten var.");
      }

      genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
      genre.IsActive = Model.IsActive;
      _dbContext.SaveChanges();
    }
  }

  public class UpdateGenreModel
  {
    public string Name { get; set; }
    public bool IsActive { get; set; }
  }
}
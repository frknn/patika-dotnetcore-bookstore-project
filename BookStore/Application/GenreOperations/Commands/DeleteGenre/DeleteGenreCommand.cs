using System;
using System.Linq;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.GenreOperations.Commands.DeleteGenre
{
  public class DeleteGenreCommand
  {
    public int Id { get; set; }
    private readonly IBookStoreDbContext _dbContext;

    public DeleteGenreCommand(IBookStoreDbContext dbContext)
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

      _dbContext.Genres.Remove(genre);
      _dbContext.SaveChanges();
    }
  }
}
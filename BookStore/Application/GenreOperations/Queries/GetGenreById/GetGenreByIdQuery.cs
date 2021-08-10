using System;
using System.Linq;
using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.GenreOperations.Queries.GetGenreById
{
  public class GetGenreByIdQuery
  {
    public int Id { get; set; }
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenreByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public GetGenreByIdViewModel Handle()
    {
      Genre genre = _dbContext.Genres.SingleOrDefault(genre => genre.IsActive && genre.Id == Id);
      if (genre is null)
      {
        throw new InvalidOperationException("Genre bulunamadı.");
      }
      GetGenreByIdViewModel genreVM = _mapper.Map<GetGenreByIdViewModel>(genre);
      return genreVM;
    }
  }

  public class GetGenreByIdViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}
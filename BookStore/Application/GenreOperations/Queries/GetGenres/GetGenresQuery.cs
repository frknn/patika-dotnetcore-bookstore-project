using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.GenreOperations.Queries.GetGenres
{
  public class GetGenresQuery
  {
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenresQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public List<GenresViewModel> Handle()
    {
      var genres = _dbContext.Genres.Where(genre => genre.IsActive).OrderBy(genre => genre.Id).ToList<Genre>();
      List<GenresViewModel> genresVM = _mapper.Map<List<GenresViewModel>>(genres);
      return genresVM;
    }
  }

  public class GenresViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}

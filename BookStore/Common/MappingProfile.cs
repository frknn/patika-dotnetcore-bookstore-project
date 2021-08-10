using AutoMapper;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.BookOperations.Queries.GetBookById;
using BookStore.Application.BookOperations.Queries.GetBooks;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenreById;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.Entities;

namespace BookStore.Common
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<CreateBookModel, Book>();
      CreateMap<Book, GetBookByIdViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
      CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
      CreateMap<CreateGenreModel, Genre>();
      CreateMap<Genre, GetGenreByIdViewModel>();
      CreateMap<Genre, GenresViewModel>();
    }
  }
}
using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthorById;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;
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
      CreateMap<Book, GetBookByIdViewModel>()
        .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
        .ForMember(dest => dest.Author, opt => opt.MapFrom(src => (src.Author.FirstName + " " + src.Author.LastName)));

      CreateMap<Book, BooksViewModel>()
        .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
        .ForMember(dest => dest.Author, opt => opt.MapFrom(src => (src.Author.FirstName + " " + src.Author.LastName)));
      CreateMap<CreateGenreModel, Genre>();
      CreateMap<Genre, GetGenreByIdViewModel>();
      CreateMap<Genre, GenresViewModel>();
      CreateMap<Author, AuthorsViewModel>();
      CreateMap<Author, GetAuthorByIdViewModel>();
      CreateMap<CreateAuthorModel, Author>();
    }
  }
}
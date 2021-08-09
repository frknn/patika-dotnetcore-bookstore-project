using AutoMapper;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBookById;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBook;

namespace BookStore.Common
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<CreateBookModel, Book>();
      CreateMap<Book, GetBookByIdViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
      CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
    }
  }
}
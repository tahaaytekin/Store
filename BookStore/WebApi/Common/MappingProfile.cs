using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.Queries.GetGenres;
using WebApi.Application.Queries.GetGenresDetail;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
            public MappingProfile()
            {
                CreateMap<CreateBookModel,Book>();
                CreateMap<Book,BookDetailViewModel>().ForMember(dest => dest.Genre,opt => opt.MapFrom(src =>src.Genre.Name));
                CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre,opt => opt.MapFrom(src =>src.Genre.Name));
                CreateMap<Genre,GenresViewModel>();
                CreateMap<Genre,GenreDetailViewModel>();
            }
    }
}
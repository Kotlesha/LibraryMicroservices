using AutoMapper;
using Book.Application.DTOs.RequestDTOs;
using Book.Application.DTOs.ResponseDTOs;
using Book.Domain.Extensions;

namespace Book.Application.Profiles;

using Book = Domain.Entities.Book;

internal class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookRequestDTO, Book>()
            .ConstructUsing(dto => Book.Create(
                dto.Title,
                dto.Description,
                dto.Price,
                dto.Pages,
                dto.AgeRating,
                dto.ISBN,
                dto.CategoryId,
                true));

        CreateMap<Book, BookResponseDTO>()
            .ForMember(dest => dest.AuthorsIds,
                       opt => opt.MapFrom(src => src.Authors.Select(a => a.Id)))
            .ForMember(dest => dest.AgeRating, opt => opt.MapFrom(src => src.AgeRating.ToFormattedString()))
            .ForMember(dest => dest.GenresIds,
                       opt => opt.MapFrom(src => src.Genres.Select(g => g.Id)));
    }
}
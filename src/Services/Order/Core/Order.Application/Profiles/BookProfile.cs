using AutoMapper;
using Order.Application.DTOs.RequestDTOs;
using Order.Application.DTOs.ResponseDTOs;

namespace Order.Application.Profiles;

using Book = Domain.Entities.Book;

internal class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookRequestDTO, Book>()
            .ConstructUsing(dto => Book.Create(
                dto.Title,
                dto.Price,
                true));

        CreateMap<Book, BookResponseDTO>();
    }
}
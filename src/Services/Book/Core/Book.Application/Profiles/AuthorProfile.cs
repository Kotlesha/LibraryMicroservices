using AutoMapper;
using Book.Application.DTOs.RequestDTOs;
using Book.Application.DTOs.ResponseDTOs;
using Book.Domain.Entities;

namespace Book.Application.Profiles;

internal class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<AuthorRequestDTO, Author>()
            .ConstructUsing(dto => Author.Create(
                dto.Surname,
                dto.Name));

        CreateMap<Author, AuthorResponseDTO>();
    }
}
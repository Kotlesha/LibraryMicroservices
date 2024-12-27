using AutoMapper;
using Book.Application.DTOs.RequestDTOs;
using Book.Application.DTOs.ResponseDTOs;
using Book.Domain.Entities;

namespace Book.Application.Profiles;

internal class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<GenreRequestDTO, Genre>()
            .ConstructUsing(dto => Genre.Create(
                dto.Name));

        CreateMap<Genre, GenreResponseDTO>();
    }
}
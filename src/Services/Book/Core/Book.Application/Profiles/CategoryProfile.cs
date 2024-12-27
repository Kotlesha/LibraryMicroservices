using AutoMapper;
using Book.Application.DTOs.RequestDTOs;
using Book.Application.DTOs.ResponseDTOs;
using Book.Domain.Entities;

namespace Book.Application.Profiles;

internal class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryRequestDTO, Category>()
            .ConstructUsing(dto => Category.Create(
                dto.Name));

        CreateMap<Category, CategoryResponseDTO>();
    }
}
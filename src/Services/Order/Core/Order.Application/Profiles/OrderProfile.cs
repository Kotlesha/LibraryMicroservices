using AutoMapper;
using Order.Application.DTOs.ResponseDTOs;

namespace Order.Application.Profiles;

using Order = Domain.Entities.Order;

internal class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponseDTO>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
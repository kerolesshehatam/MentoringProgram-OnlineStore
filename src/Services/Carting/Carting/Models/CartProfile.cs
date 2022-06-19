using AutoMapper;
using Carting.Entities;
using OlineStore.CartingService.Models.Requests;
using OlineStore.CartingService.Models.Responses;

namespace OlineStore.CartingService.Models
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartItemRequest, CartItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ItemId)
                ).ReverseMap();
            CreateMap<CartItem, CartItemResponse>()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.Id)
                ).ReverseMap();
        }
    }
}

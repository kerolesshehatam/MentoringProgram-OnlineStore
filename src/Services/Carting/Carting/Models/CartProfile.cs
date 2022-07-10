using AutoMapper;
using OnlineStore.CartingService.Entities;
using OnlineStore.CartingService.Models.Requests;
using OnlineStore.CartingService.Models.Responses;

namespace OnlineStore.CartingService.Models
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartResponse>()
                .ForMember(dest => dest.CartKey, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ReverseMap();

            CreateMap<CartItem, CartItemResponse>().ReverseMap();
            CreateMap<CartItemRequest, CartItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ItemId))
                .ReverseMap();

        }
    }
}

using AutoMapper;
using OnlineStore.Catalog.Application.Models.Requests;
using OnlineStore.Catalog.Application.Models.Responses;
using OnlineStore.Catalog.Domain.Entities;

namespace OnlineStore.Catalog.Application.Models
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<CategoryItemRequest, CategoryItem>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ReverseMap();

            CreateMap<CategoryItem, CategoryItemResponse>()
                .ForMember(dest => dest.CategoryDetails, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();

            CreateMap<CategoryRequest, Category>().ReverseMap();

            CreateMap<Category, CategoryResponse>().ReverseMap();
        }
    }
}

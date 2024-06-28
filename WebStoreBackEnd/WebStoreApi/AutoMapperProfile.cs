using AutoMapper;
using WebStoreApi.Models;
using WebStoreApi.Models.Dto;

namespace AuthenticationWebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<UserCartChange, UserCartChangeDto>();
            CreateMap<UserCartChangeDto, UserCartChange>();
        }
    }
}
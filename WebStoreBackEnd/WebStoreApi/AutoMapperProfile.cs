using AutoMapper;
using WebStoreApi.Models;
using WebStoreApi.Models.Dto;

namespace AuthenticationWebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductResponse, Product>();
            CreateMap<UserCartChange, UserCartChangeRequest>();
            CreateMap<UserCartChangeRequest, UserCartChange>();
        }
    }
}
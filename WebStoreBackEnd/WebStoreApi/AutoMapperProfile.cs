using AutoMapper;
using WebStoreApi.Domain.Dtos;
using WebStoreApi.Domain.Entities;
using WebStoreApi.Domain.Models;

namespace WebStoreApi
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
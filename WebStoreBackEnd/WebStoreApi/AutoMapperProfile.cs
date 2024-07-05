using AutoMapper;
using WebStoreApi.Dtos.ControllerDtos;
using WebStoreApi.Dtos.ServiceDtos;
using WebStoreApi.Entities;

namespace AuthenticationWebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductResponse, Product>();
            CreateMap<UserCartChangeServiceRequest, UserCartChangeRequest>();
            CreateMap<UserCartChangeRequest, UserCartChangeServiceRequest>();
        }
    }
}
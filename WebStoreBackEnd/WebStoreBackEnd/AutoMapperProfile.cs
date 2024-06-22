using AutoMapper;
using WebStoreBackEnd.Models;
using WebStoreBackEnd.Models.Dto;

namespace HorseBets
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuthenticationModel, AuthenticationModelDto>();
            CreateMap<AuthenticationModelDto, AuthenticationModel>();
        }
    }
}
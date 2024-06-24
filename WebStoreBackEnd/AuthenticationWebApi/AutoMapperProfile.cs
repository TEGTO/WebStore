using AuthenticationWebApi.Models;
using AuthenticationWebApi.Models.Dto;
using AutoMapper;

namespace AuthenticationWebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserRegistrationDto>();
            CreateMap<UserRegistrationDto, User>().ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
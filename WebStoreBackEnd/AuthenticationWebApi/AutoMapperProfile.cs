using AuthenticationManager.Models;
using AuthenticationWebApi.Dtos.ControllerDtos;
using AuthenticationWebApi.Dtos.ServiceDtos;
using AuthenticationWebApi.Entities;
using AutoMapper;

namespace AuthenticationWebApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserRegistrationRequest>();
            CreateMap<UserRegistrationRequest, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<AccessTokenData, AccessTokenDto>();
            CreateMap<AccessTokenDto, AccessTokenData>();
            CreateMap<UserUpdateDataRequest, UserUpdateServiceRequest>();

        }
    }
}
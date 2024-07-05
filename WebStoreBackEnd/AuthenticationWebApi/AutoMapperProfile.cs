using AuthenticationManager.Models;
using AuthenticationWebApi.Domain.Dtos;
using AuthenticationWebApi.Domain.Entities;
using AuthenticationWebApi.Domain.Models;
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
            CreateMap<UserUpdateDataRequest, UserUpdateData>();

        }
    }
}
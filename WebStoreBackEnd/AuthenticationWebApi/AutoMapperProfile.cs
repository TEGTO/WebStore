using AuthenticationManager.Models;
using AuthenticationWebApi.Models;
using AuthenticationWebApi.Models.Dto;
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
            CreateMap<UserUpdateDataRequest, UserDataUpdate>();
            CreateMap<UserDataUpdate, UserUpdateDataRequest>();
        }
    }
}
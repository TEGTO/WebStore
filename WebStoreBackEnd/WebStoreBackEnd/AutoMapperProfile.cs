using AutoMapper;
using WebStoreBackEnd.Models;
using WebStoreBackEnd.Models.Dto;

namespace HorseBets
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
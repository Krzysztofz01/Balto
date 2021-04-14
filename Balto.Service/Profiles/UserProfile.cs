using AutoMapper;
using Balto.Domain;
using Balto.Service.Dto;

namespace Balto.Service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using Balto.Service.Dto;
using Balto.Web.ViewModels;

namespace Balto.Web.Profiles
{
    public class UserViewProfile : Profile
    {
        public UserViewProfile()
        {
            CreateMap<UserDto, UserGetView>().ReverseMap();
        }
    }
}

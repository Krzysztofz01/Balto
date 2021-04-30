using AutoMapper;
using Balto.Service.Dto;
using Balto.Web.ViewModels;

namespace Balto.Web.Profiles
{
    public class TeamViewProfile : Profile
    {
        public TeamViewProfile()
        {
            CreateMap<TeamDto, TeamPostView>().ReverseMap();
            CreateMap<TeamDto, TeamPatchView>().ReverseMap();
            CreateMap<TeamDto, TeamGetView>().ReverseMap();
        }
    }
}

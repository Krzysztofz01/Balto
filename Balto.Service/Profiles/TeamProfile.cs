using AutoMapper;
using Balto.Domain;
using Balto.Service.Dto;

namespace Balto.Service.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamDto>().ReverseMap();
        }
    }
}

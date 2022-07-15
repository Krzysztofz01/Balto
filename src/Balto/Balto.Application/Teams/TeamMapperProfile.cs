using AutoMapper;
using Balto.Domain.Team;
using Balto.Domain.Team.TeamMembers;
using static Balto.Application.Teams.Dto;

namespace Balto.Application.Teams
{
    public class TeamMapperProfile : Profile
    {
        public TeamMapperProfile()
        {
            CreateMap<Team, TeamSimple>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name.Value))
                .ForMember(d => d.Color, m => m.MapFrom(s => s.Color.Value));

            CreateMap<Team, TeamDetails>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name.Value))
                .ForMember(d => d.Color, m => m.MapFrom(s => s.Color.Value))
                .ForMember(d => d.Members, m => m.MapFrom(s => s.Members));

            CreateMap<TeamMember, MemberDetails>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.IdentityId.Value));
        }
    }
}

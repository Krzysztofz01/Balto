using AutoMapper;

namespace Balto.Application.Aggregates.Team
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Aggregates.Team.Team, Dto.V1.TeamDetails>()
                .ForMember(d => d.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(d => d.Name, s => s.MapFrom(e => e.Name.Value))
                .ForMember(d => d.Color, s => s.MapFrom(e => e.Color.Value));

            CreateMap<Domain.Aggregates.Team.Team, Dto.V1.TeamSimple>()
                .ForMember(d => d.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(d => d.Name, s => s.MapFrom(e => e.Name.Value))
                .ForMember(d => d.Color, s => s.MapFrom(e => e.Color.Value));
        }
    }
}

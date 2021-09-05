using AutoMapper;

namespace Balto.Application.Aggregates.User
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Aggregates.User.User, Dto.V1.UserDetails>()
                .ForMember(d => d.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(d => d.Name, s => s.MapFrom(e => e.Name.Value))
                .ForMember(d => d.Email, s => s.MapFrom(e => e.Email.Value))
                .ForMember(d => d.TeamId, s => s.MapFrom(e => e.TeamId.Value))
                .ForMember(d => d.Color, s => s.MapFrom(e => e.Color.Value))
                .ForMember(d => d.LastLoginIp, s => s.MapFrom(e => e.LastLogin.IpAddress))
                .ForMember(d => d.LastLoginDate, s => s.MapFrom(e => e.LastLogin.Date))
                .ForMember(d => d.IsLeader, s => s.MapFrom(e => e.IsLeader))
                .ForMember(d => d.IsActivated, s => s.MapFrom(e => e.IsActivated));

            CreateMap<Domain.Aggregates.User.User, Dto.V1.UserSimple>()
                .ForMember(d => d.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(d => d.Name, s => s.MapFrom(e => e.Name.Value))
                .ForMember(d => d.Email, s => s.MapFrom(e => e.Email.Value))
                .ForMember(d => d.TeamId, s => s.MapFrom(e => e.TeamId.Value))
                .ForMember(d => d.Color, s => s.MapFrom(e => e.Color.Value));
        }
    }
}

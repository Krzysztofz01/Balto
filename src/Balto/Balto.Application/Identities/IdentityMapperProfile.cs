using AutoMapper;
using Balto.Domain.Identities;
using static Balto.Application.Identities.Dto;

namespace Balto.Application.Identities
{
    public class IdentityMapperProfile : Profile
    {
        public IdentityMapperProfile()
        {
            CreateMap<Identity, IdentitySimple>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name.Value))
                .ForMember(d => d.Role, m => m.MapFrom(s => s.Role))
                .ForMember(d => d.Color, m => m.MapFrom(s => s.Color.Value));

            CreateMap<Identity, IdentityDetails>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Name, m => m.MapFrom(s => s.Name.Value))
                .ForMember(d => d.Email, m => m.MapFrom(s => s.Email.Value))
                .ForMember(d => d.LastLoginIp, m => m.MapFrom(s => s.LastLogin.IpAddress))
                .ForMember(d => d.LastLoginDate, m => m.MapFrom(s => s.LastLogin.Date))
                .ForMember(d => d.Role, m => m.MapFrom(s => s.Role))
                .ForMember(d => d.Activation, m => m.MapFrom(s => s.Activation.Value))
                .ForMember(d => d.Color, m => m.MapFrom(s => s.Color.Value));
        }
    }
}

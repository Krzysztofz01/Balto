using AutoMapper;
using Balto.Domain;
using Balto.Service.Dto;
using System.Linq;

namespace Balto.Service.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(s => s.ReadWriteUsers, m => m.MapFrom(t => t.ReadWriteUsers.Select(x => x.User)))
                .ReverseMap()
                .ForMember(s => s.Owner, t => t.Ignore())
                .ForMember(s => s.ReadWriteUsers, t => t.Ignore());
        }
    }
}

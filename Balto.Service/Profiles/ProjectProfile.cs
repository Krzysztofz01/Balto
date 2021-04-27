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
                .ForMember(s => s.OwnerEmail, m => m.MapFrom(t => t.Owner.Email))
                .ForMember(s => s.ReadWriteUsersEmails, m => m.MapFrom(t => t.ReadWriteUsers.Select(u => u.User.Email)))
                .ReverseMap()
                .ForMember(s => s.Owner, t => t.Ignore());
        }
    }
}

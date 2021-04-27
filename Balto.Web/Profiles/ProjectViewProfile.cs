using AutoMapper;
using Balto.Service.Dto;
using Balto.Web.ViewModels;

namespace Balto.Web.Profiles
{
    public class ProjectViewProfile : Profile
    {
        public ProjectViewProfile()
        {
            CreateMap<ProjectDto, ProjectPostView>().ReverseMap();
            
            CreateMap<ProjectDto, ProjectGetView>()
                .ForMember(s => s.OwnerEmail, m => m.MapFrom(t => t.OwnerEmail))
                .ReverseMap();
        }
    }
}

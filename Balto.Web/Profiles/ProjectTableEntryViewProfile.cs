using AutoMapper;
using Balto.Service.Dto;
using Balto.Web.ViewModels;

namespace Balto.Web.Profiles
{
    public class ProjectTableEntryViewProfile : Profile
    {
        public ProjectTableEntryViewProfile()
        {
            CreateMap<ProjectTableEntryDto, ProjectTableEntryPostView>().ReverseMap();
            CreateMap<ProjectTableEntryDto, ProjectTableEntryPatchView>().ReverseMap();
            CreateMap<ProjectTableEntryDto, ProjectTableEntryGetView>().ReverseMap();
        }
    }
}

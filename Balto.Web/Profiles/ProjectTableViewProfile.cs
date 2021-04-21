using AutoMapper;
using Balto.Service.Dto;
using Balto.Web.ViewModels;

namespace Balto.Web.Profiles
{
    public class ProjectTableViewProfile : Profile
    {
        public ProjectTableViewProfile()
        {
            CreateMap<ProjectTableDto, ProjectTablePostView>().ReverseMap();
            CreateMap<ProjectTableDto, ProjectTableGetView>().ReverseMap();
        }
    }
}

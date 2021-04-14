using AutoMapper;
using Balto.Domain;
using Balto.Service.Dto;

namespace Balto.Service.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
        }
    }
}

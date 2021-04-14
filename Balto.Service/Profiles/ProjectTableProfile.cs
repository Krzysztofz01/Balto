using AutoMapper;
using Balto.Domain;
using Balto.Service.Dto;

namespace Balto.Service.Profiles
{
    public class ProjectTableProfile: Profile
    {
        public ProjectTableProfile()
        {
            CreateMap<ProjectTable, ProjectTableDto>().ReverseMap();
        }
    }
}

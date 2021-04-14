using AutoMapper;
using Balto.Domain;
using Balto.Service.Dto;

namespace Balto.Service.Profiles
{
    public class ProjectTableEntryProfile : Profile
    {
        public ProjectTableEntryProfile()
        {
            CreateMap<ProjectTableEntry, ProjectTableEntryDto>().ReverseMap();
        }
    }
}

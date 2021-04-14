using AutoMapper;
using Balto.Domain;
using Balto.Service.Dto;

namespace Balto.Service.Profiles
{
    public class ObjectiveProfile : Profile
    {
        public ObjectiveProfile()
        {
            CreateMap<Objective, ObjectiveDto>().ReverseMap();
        }
    }
}

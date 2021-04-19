using AutoMapper;
using Balto.Service.Dto;
using Balto.Web.ViewModels;

namespace Balto.Web.Profiles
{
    public class ObjectiveViewProfile : Profile
    {
        public ObjectiveViewProfile()
        {
            CreateMap<ObjectiveDto, ObjectivePostView>().ReverseMap();
            CreateMap<ObjectiveDto, ObjectiveGetView>().ReverseMap();
        }
    }
}

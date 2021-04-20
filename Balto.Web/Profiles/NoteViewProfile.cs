using AutoMapper;
using Balto.Service.Dto;
using Balto.Web.ViewModels;

namespace Balto.Web.Profiles
{
    public class NoteViewProfile : Profile
    {
        public NoteViewProfile()
        {
            CreateMap<NoteDto, NotePostView>().ReverseMap();
            CreateMap<NoteDto, NoteGetView>().ReverseMap();
        }
    }
}

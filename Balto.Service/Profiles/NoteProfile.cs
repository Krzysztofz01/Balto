using AutoMapper;
using Balto.Domain;
using Balto.Service.Dto;
using System.Linq;

namespace Balto.Service.Profiles
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteDto>()
                .ForMember(s => s.ReadWriteUsers, m => m.MapFrom(t => t.ReadWriteUsers.Select(x => x.User)))
                .ReverseMap()
                .ForMember(s => s.Owner, t => t.Ignore())
                .ForMember(s => s.ReadWriteUsers, t => t.Ignore());
        }
    }
}

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
                .ForMember(s => s.OwnerEmail, m => m.MapFrom(t => t.Owner.Email))
                .ForMember(s => s.ReadWriteUsersEmails, m => m.MapFrom(t => t.ReadWriteUsers.Select(x => x.User.Email)))
                .ReverseMap()
                .ForMember(s => s.Owner, t => t.Ignore());
        }
    }
}

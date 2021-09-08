using AutoMapper;
using System.Linq;

namespace Balto.Application.Aggregates.Note
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Aggregates.Note.Note, Dto.V1.NoteDetails>()
                .ForMember(d => d.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(d => d.Title, s => s.MapFrom(e => e.Title.Value))
                .ForMember(d => d.Content, s => s.MapFrom(e => e.Content.Value))
                .ForMember(d => d.Public, s => s.MapFrom(e => e.Public))
                .ForMember(d => d.OwnerId, s => s.MapFrom(e => e.OwnerId.Value))
                .ForMember(d => d.CollaboratorsIds, s => s.MapFrom(e => e.Contributors.Select(c => c.Id.Value)));

            CreateMap<Domain.Aggregates.Note.Note, Dto.V1.NoteSimple>()
                .ForMember(d => d.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(d => d.Title, s => s.MapFrom(e => e.Title.Value))
                .ForMember(d => d.OwnerId, s => s.MapFrom(e => e.OwnerId.Value));
        }
    }
}

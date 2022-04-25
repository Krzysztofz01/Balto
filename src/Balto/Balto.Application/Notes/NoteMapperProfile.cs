using AutoMapper;
using Balto.Domain.Notes;
using Balto.Domain.Notes.NoteContributors;
using Balto.Domain.Notes.NoteSnapshots;
using System.Linq;
using static Balto.Application.Notes.Dto;

namespace Balto.Application.Notes
{
    public class NoteMapperProfile : Profile
    {
        public NoteMapperProfile()
        {
            CreateMap<Note, NoteSimple>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.OwnerId, m => m.MapFrom(s => s.OwnerId.Value))
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Title.Value))
                .ForMember(d => d.TagIds, m => m.MapFrom(s => s.Tags.Select(t => t.TagId.Value)));

            CreateMap<Note, NoteDetails>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.OwnerId, m => m.MapFrom(s => s.OwnerId.Value))
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Title.Value))
                .ForMember(d => d.Content, m => m.MapFrom(s => s.Content.Value))
                .ForMember(d => d.Contributors, m => m.MapFrom(s => s.Contributors))
                .ForMember(d => d.Snapshots, m => m.MapFrom(s => s.Snapshots))
                .ForMember(d => d.TagIds, m => m.MapFrom(s => s.Tags.Select(t => t.TagId.Value)));

            CreateMap<NoteContributor, ContributorDetails>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.IdentityId.Value))
                .ForMember(d => d.AccessRole, m => m.MapFrom(s => s.AccessRole.Value));

            CreateMap<NoteSnapshot, SnapshotDetails>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Content, m => m.MapFrom(s => s.Content.Value))
                .ForMember(d => d.CreationDate, m => m.MapFrom(s => s.CreationDate.Value));
        }
    }
}

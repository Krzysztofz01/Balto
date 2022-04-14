using AutoMapper;
using Balto.Domain.Tags;
using static Balto.Application.Tags.Dto;

namespace Balto.Application.Tags
{
    public class TagMapperProfile : Profile
    {
        public TagMapperProfile()
        {
            CreateMap<Tag, TagSimple>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Title.Value))
                .ForMember(d => d.Color, m => m.MapFrom(s => s.Color.Value));

            CreateMap<Tag, TagDetails>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Title.Value))
                .ForMember(d => d.Color, m => m.MapFrom(s => s.Color.Value));
        }
    }
}

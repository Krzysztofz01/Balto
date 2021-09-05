using AutoMapper;

namespace Balto.Application.Aggregates.Objectives
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Aggregates.Objective.Objective, Dto.V1.ObjectiveDetails>()
                .ForMember(d => d.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(d => d.OwnerId, s => s.MapFrom(e => e.OwnerId.Value))
                .ForMember(d => d.Title, s => s.MapFrom(e => e.Title.Value))
                .ForMember(d => d.Description, s => s.MapFrom(e => e.Description.Value))
                .ForMember(d => d.Priority, s => s.MapFrom(e => e.Priority.Value))
                .ForMember(d => d.Periodicity, s => s.MapFrom(e => e.Periodicity.Value))
                .ForMember(d => d.StartingDate, s => s.MapFrom(e => e.StartingDate.Value))
                .ForMember(d => d.EndingDate, s => s.MapFrom(e => e.EndingDate.Value))
                .ForMember(d => d.Finished, s => s.MapFrom(e => e.Finished))
                .ForMember(d => d.FinishDate, s => s.MapFrom(e => e.FinishDate));

            CreateMap<Domain.Aggregates.Objective.Objective, Dto.V1.ObjectiveSimple>()
                .ForMember(d => d.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(d => d.OwnerId, s => s.MapFrom(e => e.OwnerId.Value))
                .ForMember(d => d.Title, s => s.MapFrom(e => e.Title.Value))
                .ForMember(d => d.Priority, s => s.MapFrom(e => e.Priority.Value))
                .ForMember(d => d.Periodicity, s => s.MapFrom(e => e.Periodicity.Value))
                .ForMember(d => d.StartingDate, s => s.MapFrom(e => e.StartingDate.Value))
                .ForMember(d => d.EndingDate, s => s.MapFrom(e => e.EndingDate.Value))
                .ForMember(d => d.Finished, s => s.MapFrom(e => e.Finished));
        }
    }
}

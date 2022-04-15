using AutoMapper;
using Balto.Domain.Goals;
using System.Linq;
using static Balto.Application.Goals.Dto;

namespace Balto.Application.Goals
{
    public class GoalMapperProfile : Profile
    {
        public GoalMapperProfile()
        {
            CreateMap<Goal, GoalSimple>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Title.Value))
                .ForMember(d => d.Priority, m => m.MapFrom(s => s.Priority.Value))
                .ForMember(d => d.Color, m => m.MapFrom(s => s.Color.Value))
                .ForMember(d => d.Deadline, m => m.MapFrom(s => s.Deadline.Value))
                .ForMember(d => d.IsRecurring, m => m.MapFrom(s => s.IsRecurring.Value))
                .ForMember(d => d.Finished, m => m.MapFrom(s => s.Status.Finished))
                .ForMember(d => d.TagIds, m => m.MapFrom(s => s.Tags.Select(t => t.TagId.Value)));

            CreateMap<Goal, GoalDetails>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.OwnerId, m => m.MapFrom(s => s.OwnerId.Value))
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Title.Value))
                .ForMember(d => d.Description, m => m.MapFrom(s => s.Description.Value))
                .ForMember(d => d.Priority, m => m.MapFrom(s => s.Priority.Value))
                .ForMember(d => d.Color, m => m.MapFrom(s => s.Color.Value))
                .ForMember(d => d.StartingDate, m => m.MapFrom(s => s.StartingDate.Value))
                .ForMember(d => d.Deadline, m => m.MapFrom(s => s.Deadline.Value))
                .ForMember(d => d.IsRecurring, m => m.MapFrom(s => s.IsRecurring.Value))
                .ForMember(d => d.Finished, m => m.MapFrom(s => s.Status.Finished))
                .ForMember(d => d.FinishDate, m => m.MapFrom(s => s.Status.FinishDate))
                .ForMember(d => d.TagIds, m => m.MapFrom(s => s.Tags.Select(t => t.TagId.Value)));
        }
    }
}

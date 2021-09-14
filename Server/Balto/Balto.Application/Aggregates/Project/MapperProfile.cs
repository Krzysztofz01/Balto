using AutoMapper;
using System.Linq;

namespace Balto.Application.Aggregates.Project
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Domain.Aggregates.Project.Project, Dto.V1.ProjectDetails>()
                .ForMember(d => d.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(d => d.Title, s => s.MapFrom(e => e.Title.Value))
                .ForMember(d => d.OwnerId, s => s.MapFrom(e => e.OwnerId.Value))
                .ForMember(d => d.TicketToken, s => s.MapFrom(e => e.TicketToken.Value))
                .ForMember(d => d.ContributorsIds, s => s.MapFrom(e => e.Contributors.Select(c => c.Id.Value)))
                .ForMember(d => d.Tables, s => s.MapFrom(e => e.Tables));

            CreateMap<Domain.Aggregates.Project.Project, Dto.V1.ProjectSimple>()
                .ForMember(d => d.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(d => d.Title, s => s.MapFrom(e => e.Title.Value));

            CreateMap<Domain.Aggregates.Project.Table.ProjectTable, Dto.V1.TableDetails>()
                .ForMember(e => e.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(e => e.Title, s => s.MapFrom(e => e.Title.Value))
                .ForMember(e => e.Color, s => s.MapFrom(e => e.Color.Value))
                .ForMember(e => e.Cards, s => s.MapFrom(e => e.Cards));

            CreateMap<Domain.Aggregates.Project.Table.ProjectTable, Dto.V1.TableSimple>()
                .ForMember(e => e.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(e => e.Title, s => s.MapFrom(e => e.Title.Value))
                .ForMember(e => e.Color, s => s.MapFrom(e => e.Color.Value));

            CreateMap<Domain.Aggregates.Project.Card.ProjectTableCard, Dto.V1.CardDetails>()
                .ForMember(e => e.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(e => e.Title, s => s.MapFrom(e => e.Title.Value))
                .ForMember(e => e.Content, s => s.MapFrom(e => e.Content.Value))
                .ForMember(e => e.Color, s => s.MapFrom(e => e.Color.Value))
                .ForMember(e => e.CreatorId, s => s.MapFrom(e => e.CreatorId.Value))
                .ForMember(e => e.StartingDate, s => s.MapFrom(e => e.StartingDate.Value))
                .ForMember(e => e.DeadlineDate, s => s.MapFrom(e => e.Deadline.Date))
                .ForMember(e => e.AssignedUserId, s => s.MapFrom(e => e.Deadline.UserId))
                .ForMember(e => e.Notify, s => s.MapFrom(e => e.Deadline.Notify))
                .ForMember(e => e.Finished, s => s.MapFrom(e => e.Finished.Finished))
                .ForMember(e => e.FinishedByUserId, s => s.MapFrom(e => e.Finished.FinishedBy))
                .ForMember(e => e.FinishDate, s => s.MapFrom(e => e.Finished.FinishedOn))
                .ForMember(e => e.Priority, s => s.MapFrom(e => e.Priority))
                .ForMember(e => e.OrdinalNumber, s => s.MapFrom(e => e.OrdinalNumber))
                .ForMember(e => e.Comments, s => s.MapFrom(e => e.Comments));

            CreateMap<Domain.Aggregates.Project.Card.ProjectTableCard, Dto.V1.CardSimple>()
                .ForMember(e => e.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(e => e.Title, s => s.MapFrom(e => e.Title.Value))
                .ForMember(e => e.Color, s => s.MapFrom(e => e.Color.Value))
                .ForMember(e => e.CreatorId, s => s.MapFrom(e => e.CreatorId.Value))
                .ForMember(e => e.DeadlineDate, s => s.MapFrom(e => e.Deadline.Date))
                .ForMember(e => e.AssignedUserId, s => s.MapFrom(e => e.Deadline.UserId))
                .ForMember(e => e.Finished, s => s.MapFrom(e => e.Finished.Finished))
                .ForMember(e => e.FinishedByUserId, s => s.MapFrom(e => e.Finished.FinishedBy))
                .ForMember(e => e.Priority, s => s.MapFrom(e => e.Priority))
                .ForMember(e => e.OrdinalNumber, s => s.MapFrom(e => e.OrdinalNumber));

            CreateMap<Domain.Aggregates.Project.Card.ProjectTableCardComment, Dto.V1.CommentDetails>()
                .ForMember(e => e.Id, s => s.MapFrom(e => e.Id.Value))
                .ForMember(e => e.Content, s => s.MapFrom(e => e.Content.Value))
                .ForMember(e => e.UserId, s => s.MapFrom(e => e.CreatorId.Value))
                .ForMember(e => e.CreateDate, s => s.MapFrom(e => e.CreateDate));
        }
    }
}

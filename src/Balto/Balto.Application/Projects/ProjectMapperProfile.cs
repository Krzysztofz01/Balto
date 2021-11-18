using AutoMapper;
using Balto.Domain.Projects;
using Balto.Domain.Projects.ProjectContributors;
using Balto.Domain.Projects.ProjectTables;
using Balto.Domain.Projects.ProjectTasks;
using static Balto.Application.Projects.Dto;

namespace Balto.Application.Projects
{
    public class ProjectMapperProfile : Profile
    {
        public ProjectMapperProfile()
        {
            CreateMap<Project, ProjectSimple>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.OwnerId, m => m.MapFrom(s => s.OwnerId.Value))
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Title.Value));

            CreateMap<Project, ProjectDetails>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.OwnerId, m => m.MapFrom(s => s.OwnerId.Value))
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Title.Value))
                .ForMember(d => d.TicketToken, m => m.MapFrom(s => s.TicketToken.Value))
                .ForMember(d => d.Contributors, m => m.MapFrom(s => s.Contributors))
                .ForMember(d => d.Tables, m => m.MapFrom(s => s.Tables));

            CreateMap<ProjectContributor, ContributorDetails>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.IdentityId.Value))
                .ForMember(d => d.Role, m => m.MapFrom(s => s.Role.Value));

            CreateMap<ProjectTable, TableSimple>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Title.Value))
                .ForMember(d => d.Color, m => m.MapFrom(s => s.Color.Value));

            CreateMap<ProjectTable, TableDetails>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Title.Value))
                .ForMember(d => d.Color, m => m.MapFrom(s => s.Color.Value))
                .ForMember(d => d.Tasks, m => m.MapFrom(s => s.Tasks));

            CreateMap<ProjectTask, TaskSimple>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Title.Value))
                .ForMember(d => d.Color, m => m.MapFrom(s => s.Color.Value))
                .ForMember(d => d.AssignedContributorId, m => m.MapFrom(s => s.AssignedContributorId.Value))
                .ForMember(d => d.Deadline, m => m.MapFrom(s => s.Deadline.Value))
                .ForMember(d => d.Finished, m => m.MapFrom(s => s.Status.Finished))
                .ForMember(d => d.Priority, m => m.MapFrom(s => s.Priority))
                .ForMember(d => d.OrdinalNumber, m => m.MapFrom(s => s.OrdinalNumber.Value));

            CreateMap<ProjectTask, TaskDetails>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Title.Value))
                .ForMember(d => d.Content, m => m.MapFrom(s => s.Content.Value))
                .ForMember(d => d.Color, m => m.MapFrom(s => s.Color.Value))
                .ForMember(d => d.CreatorId, m => m.MapFrom(s => s.CreatorId.Value))
                .ForMember(d => d.AssignedContributorId, m => m.MapFrom(s => s.AssignedContributorId.Value))
                .ForMember(d => d.StartingDate, m => m.MapFrom(s => s.StartingDate.Value))
                .ForMember(d => d.Deadline, m => m.MapFrom(s => s.Deadline.Value))
                .ForMember(d => d.Finished, m => m.MapFrom(s => s.Status.Finished))
                .ForMember(d => d.FinishDate, m => m.MapFrom(s => s.Status.FinishDate))
                .ForMember(d => d.FinishedBy, m => m.MapFrom(s => s.Status.FinishedBy))
                .ForMember(d => d.Priority, m => m.MapFrom(s => s.Priority))
                .ForMember(d => d.OrdinalNumber, m => m.MapFrom(s => s.OrdinalNumber.Value));
        }   
    }
}

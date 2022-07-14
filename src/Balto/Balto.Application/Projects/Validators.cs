using FluentValidation;
using static Balto.Application.Projects.Commands.V1;

namespace Balto.Application.Projects
{
    public static class Validators
    {
        public static class V1
        {
            public class CreateValidator : AbstractValidator<Create>
            {
                public CreateValidator()
                {
                    RuleFor(c => c.Title).NotEmpty();
                }
            }

            public class UpdateValidator : AbstractValidator<Update>
            {
                public UpdateValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.Title).NotEmpty();
                }
            }

            public class DeleteValidator : AbstractValidator<Delete>
            {
                public DeleteValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                }
            }

            public class PushTicketValidator : AbstractValidator<PushTicket>
            {
                public PushTicketValidator()
                {
                    RuleFor(c => c.TicketToken).NotEmpty();
                    RuleFor(c => c.Title).NotEmpty();
                    RuleFor(c => c.Content).NotNull();
                }
            }

            public class AddContributorValidator : AbstractValidator<AddContributor>
            {
                public AddContributorValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.UserId).NotEmpty();
                }
            }

            public class DeleteContributorValidator : AbstractValidator<DeleteContributor>
            {
                public DeleteContributorValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.UserId).NotEmpty();
                }
            }

            public class UpdateContributorValidator : AbstractValidator<UpdateContributor>
            {
                public UpdateContributorValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.UserId).NotEmpty();
                    RuleFor(c => c.Role).NotNull();
                }
            }

            public class LeaveAsContributorValidator : AbstractValidator<LeaveAsContributor>
            {
                public LeaveAsContributorValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                }
            }

            public class AddTableValidator : AbstractValidator<AddTable>
            {
                public AddTableValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.Title).NotEmpty();
                }
            }

            public class UpdateTableValidator : AbstractValidator<UpdateTable>
            {
                public UpdateTableValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.TableId).NotEmpty();
                    RuleFor(c => c.Title).NotEmpty();
                    RuleFor(c => c.Color).NotEmpty();
                }
            }

            public class DeleteTableValidator : AbstractValidator<DeleteTable>
            {
                public DeleteTableValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.TableId).NotEmpty();
                }
            }

            public class TableOrdinalNumberChangedValidator : AbstractValidator<TableOrdinalNumbersChanged>
            {
                public TableOrdinalNumberChangedValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.TableId).NotEmpty();
                    RuleFor(c => c.IdOrdinalNumberPairs).NotNull();
                }
            }

            public class AddTaskValidator : AbstractValidator<AddTask>
            {
                public AddTaskValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.TableId).NotEmpty();
                    RuleFor(c => c.Title).NotEmpty();
                }
            }

            public class UpdateTaskValidator : AbstractValidator<UpdateTask>
            {
                public UpdateTaskValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.TableId).NotEmpty();
                    RuleFor(c => c.TaskId).NotEmpty();
                    RuleFor(c => c.Title).NotEmpty();
                    RuleFor(c => c.Content).NotNull();
                    RuleFor(c => c.StartingDate).NotNull();
                    RuleFor(c => c.Priority).NotNull();
                }
            }

            public class DeleteTaskValidator : AbstractValidator<DeleteTask>
            {
                public DeleteTaskValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.TableId).NotEmpty();
                    RuleFor(c => c.TaskId).NotEmpty();
                }
            }

            public class ChangeTaskStatusValidator : AbstractValidator<ChangeTaskStatus>
            {
                public ChangeTaskStatusValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.TableId).NotEmpty();
                    RuleFor(c => c.TaskId).NotEmpty();
                    RuleFor(c => c.Status).NotNull();
                }
            }

            public class TaskTagAssignValidator : AbstractValidator<TaskTagAssign>
            {
                public TaskTagAssignValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.TableId).NotEmpty();
                    RuleFor(c => c.TaskId).NotEmpty();
                    RuleFor(c => c.TagId).NotEmpty();
                }
            }

            public class TaskTagUnassignValidator : AbstractValidator<TaskTagUnassign>
            {
                public TaskTagUnassignValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.TableId).NotEmpty();
                    RuleFor(c => c.TaskId).NotEmpty();
                    RuleFor(c => c.TagId).NotEmpty();
                }
            }
        }
    }
}

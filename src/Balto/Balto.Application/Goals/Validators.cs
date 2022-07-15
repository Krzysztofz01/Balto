using FluentValidation;
using static Balto.Application.Goals.Commands.V1;

namespace Balto.Application.Goals
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

            public class DeleteValidator : AbstractValidator<Delete>
            {
                public DeleteValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                }
            }

            public class UpdateValidator : AbstractValidator<Update>
            {
                public UpdateValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.Title).NotEmpty();
                    RuleFor(c => c.Description).NotNull();
                    RuleFor(c => c.Priority).NotNull();
                    RuleFor(c => c.Color).NotEmpty();
                    RuleFor(c => c.StartingDate).NotNull();
                    RuleFor(c => c.IsRecurring).NotNull();
                }
            }

            public class StatusChangeValidator : AbstractValidator<StatusChange>
            {
                public StatusChangeValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.State).NotNull();
                }
            }

            public class TagAssignValidator : AbstractValidator<TagAssign>
            {
                public TagAssignValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.TagId).NotEmpty();
                }
            }

            public class TagUnassignValidator : AbstractValidator<TagUnassign>
            {
                public TagUnassignValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.TagId).NotEmpty();
                }
            }
        }
    }
}

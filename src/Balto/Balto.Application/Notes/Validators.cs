using FluentValidation;
using static Balto.Application.Notes.Commands.V1;

namespace Balto.Application.Notes
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
                    RuleFor(c => c.Content).NotNull();
                }
            }

            public class DeleteValidator : AbstractValidator<Delete>
            {
                public DeleteValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
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

            public class SnapshotCreateValidator : AbstractValidator<SnapshotCreate>
            {
                public SnapshotCreateValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                }
            }

            public class SnapshotDeleteValidator : AbstractValidator<SnapshotDelete>
            {
                public SnapshotDeleteValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.SnapshotId).NotEmpty();
                }
            }
        }
    }
}

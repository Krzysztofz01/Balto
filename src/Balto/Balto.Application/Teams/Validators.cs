using FluentValidation;
using static Balto.Application.Teams.Commands.V1;

namespace Balto.Application.Teams
{
    public static class Validators
    {
        public static class V1
        {
            public class CreateValidator : AbstractValidator<Create>
            {
                public CreateValidator()
                {
                    RuleFor(c => c.Name).NotEmpty();
                    RuleFor(c => c.Color).NotEmpty();
                }
            }

            public class DeleteValidator : AbstractValidator<Delete>
            {
                public DeleteValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                }
            }

            public class AddMemberValidator : AbstractValidator<AddMember>
            {
                public AddMemberValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.UserId).NotEmpty();
                }
            }

            public class DeleteMemberValidator : AbstractValidator<DeleteMember>
            {
                public DeleteMemberValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.UserId).NotEmpty();
                }
            }
        }
    }
}

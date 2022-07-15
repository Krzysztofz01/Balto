using FluentValidation;
using static Balto.Application.Identities.Commands.V1;

namespace Balto.Application.Identities
{
    public static class Validators
    {
        public static class V1
        {
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
                    RuleFor(c => c.Color).NotNull();
                }
            }

            public class ActivationValidator : AbstractValidator<Activation>
            {
                public ActivationValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.Activated).NotNull();
                }
            }

            public class RoleChangeValidator : AbstractValidator<RoleChange>
            {
                public RoleChangeValidator()
                {
                    RuleFor(c => c.Id).NotEmpty();
                    RuleFor(c => c.Role).NotNull();
                }
            }
        }
    }
}

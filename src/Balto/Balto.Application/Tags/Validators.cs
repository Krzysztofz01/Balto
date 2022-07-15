using FluentValidation;
using static Balto.Application.Tags.Commands.V1;

namespace Balto.Application.Tags
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
        }
    }
}

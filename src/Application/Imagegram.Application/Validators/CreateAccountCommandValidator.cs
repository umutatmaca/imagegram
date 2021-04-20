using FluentValidation;
using Imagegram.Application.Requests;

namespace Imagegram.Application.Validators
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountRequest>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty();
        }
    }
}

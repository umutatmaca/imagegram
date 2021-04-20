using FluentValidation;
using Imagegram.Application.Requests;

namespace Imagegram.Application.Validators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentRequest>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(command => command.CreatorId).NotEmpty();
            RuleFor(command => command.PostId).NotEmpty();
            RuleFor(command => command.Content).NotEmpty();
        }
    }
}

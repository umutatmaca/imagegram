using FluentValidation;
using Imagegram.Application.Requests;

namespace Imagegram.Application.Validators
{
    public class GetPostCommentsByPostIdQueryValidator : AbstractValidator<GetPostCommentsByPostIdRequest>
    {
        public GetPostCommentsByPostIdQueryValidator()
        {
            RuleFor(command => command.PostId).NotEmpty();
            RuleFor(command => command.PageSize).GreaterThan(0);
            RuleFor(command => command.PageNumber).GreaterThan(0);
        }
    }
}

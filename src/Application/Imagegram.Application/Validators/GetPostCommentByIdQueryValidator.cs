using FluentValidation;
using Imagegram.Application.Requests;

namespace Imagegram.Application.Validators
{
    public class GetPostCommentByIdQueryValidator : AbstractValidator<GetPostCommentByIdRequest>
    {
        public GetPostCommentByIdQueryValidator()
        {
            RuleFor(command => command.PostId).NotEmpty();
            RuleFor(command => command.CommentId).NotEmpty();
        }
    }
}

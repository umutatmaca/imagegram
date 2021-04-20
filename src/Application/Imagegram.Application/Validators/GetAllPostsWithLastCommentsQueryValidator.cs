using FluentValidation;
using Imagegram.Application.Requests;

namespace Imagegram.Application.Validators
{
    public class GetAllPostsWithLastCommentsQueryValidator : AbstractValidator<GetAllPostsWithLastCommentsRequest>
    {
        public GetAllPostsWithLastCommentsQueryValidator()
        {
            RuleFor(command => command.PageSize).GreaterThan(0);
            RuleFor(command => command.PageNumber).GreaterThan(0);
        }
    }
}

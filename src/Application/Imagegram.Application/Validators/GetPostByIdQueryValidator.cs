using FluentValidation;
using Imagegram.Application.Requests;

namespace Imagegram.Application.Validators
{
    public class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdRequest>
    {
        public GetPostByIdQueryValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}

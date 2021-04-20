using FluentValidation;
using Imagegram.Application.Requests;

namespace Imagegram.Application.Validators
{
    public class GetAccountByIdQueryValidator : AbstractValidator<GetAccountByIdRequest>
    {
        public GetAccountByIdQueryValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}

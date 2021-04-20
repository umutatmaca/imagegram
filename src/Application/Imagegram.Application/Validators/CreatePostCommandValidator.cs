using FluentValidation;
using Imagegram.Application.Requests;
using System.Collections.Generic;

namespace Imagegram.Application.Validators
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostRequest>
    {
        private static readonly List<string> allowedTypes = new List<string>
        {
            "image/png",
            "image/jpg",
            "image/bmp"
        };
        public CreatePostCommandValidator()
        {
            RuleFor(command => command.AccountId).NotEmpty();
            RuleFor(command => command.ImageFile).NotNull();
            RuleFor(command => command.ImageFile.ContentType)
                .Must(type => allowedTypes.Contains(type))
                .WithMessage("invalid image type");
        }
    }
}

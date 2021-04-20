using FluentValidation;
using Imagegram.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Text;

namespace Imagegram.Web.API.Problems
{
    public class BadRequestProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public BadRequestProblemDetails(FormatException exception)
        {
            Title = exception.Message;
            Status = StatusCodes.Status400BadRequest;
        }

        public BadRequestProblemDetails(InvalidCommandException exception)
        {
            Title = exception.Message;
            Status = StatusCodes.Status400BadRequest;
            Detail = exception.Details;
            Type = "https://somedomain/validation-error";
        }

        public BadRequestProblemDetails(ValidationException exception)
        {
            var errorBuilder = new StringBuilder();
            var errors = exception.Errors.Where(error => error != null);
            errorBuilder.Append("Invalid command, reason: ");
            foreach (var error in errors)
            {
                errorBuilder.Append($"{error.ErrorMessage}, ");
            }

            Title = exception.Message;
            Status = StatusCodes.Status400BadRequest;
            Detail = errorBuilder.ToString();
            Type = "https://somedomain/validation-error";
        }
    }
}

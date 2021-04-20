using Imagegram.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Imagegram.Web.API.Problems
{
    public class ResourceNotFoundProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public ResourceNotFoundProblemDetails(NotFoundException exception)
        {
            this.Title = exception.Message;
            this.Status = StatusCodes.Status404NotFound;
            this.Detail = exception.ResourceName;
            this.Type = "https://somedomain/validation-error";
        }
    }
}

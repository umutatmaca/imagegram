using Imagegram.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Imagegram.Web.API.Problems
{
    public class UnauthorizedRequestProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public UnauthorizedRequestProblemDetails(AuthenticationException exception)
        {
            this.Title = exception.Message;
            this.Status = StatusCodes.Status401Unauthorized;
            this.Detail = exception.Reason;
        }
    }
}

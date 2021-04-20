using Imagegram.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Imagegram.Web.API.Problems
{
    public class DomainRuleFailedProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public DomainRuleFailedProblemDetails(DomainRuleFailedException exception)
        {
            this.Title = "Business rule validation error";
            this.Status = StatusCodes.Status409Conflict;
            this.Detail = $"{exception.FailedRule.GetType().Name} failed";
            this.Type = "https://somedomain/domain-rule-validation-error";
        }

    }
}

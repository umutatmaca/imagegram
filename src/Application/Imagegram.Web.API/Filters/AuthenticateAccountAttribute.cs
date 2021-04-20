using Imagegram.Core.Exceptions;
using Imagegram.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Imagegram.Web.API.Filters
{
    public class AuthenticateAccountAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null && controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
            {
                return;
            }

            var accountRepo = context.HttpContext.RequestServices.GetService(typeof(IAccountRepository)) as IAccountRepository;
            if (accountRepo == null)
            {
                return;
            }

            if (!context.HttpContext.Request.Headers.TryGetValue("X-Account-Id", out var accountIdValue))
            {
                throw new AuthenticationException("Account header is required.");
            }

            accountIdValue = accountIdValue.ToString().Trim();
            if (string.IsNullOrEmpty(accountIdValue))
            {
                throw new AuthenticationException("Account header is required.");
            }

            Guid accountId = new Guid(accountIdValue);
            var account = await accountRepo.GetByIdAsync(accountId);
            if (account == null)
            {
                throw new AuthenticationException("Account is not authenticated.");
            }
        }
    }
}

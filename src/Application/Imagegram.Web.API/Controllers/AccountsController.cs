using Imagegram.Application.Requests;
using Imagegram.Application.Responses;
using Imagegram.Web.API.Problems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Imagegram.Web.API.Controllers
{
    /// <summary>
    /// account management endpoints
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// create account
        /// </summary>
        /// <returns>newly created account</returns>
        /// <response code="201">newly created account</response>
        /// <response code="400">if name is empty</response>
        /// <response code="500">if unhandled exception occurs</response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(CreateAccountResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BadRequestProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateAccount([FromBody] string name)
        {
            CreateAccountResponse response = await mediator.Send(new CreateAccountRequest(name));

            return CreatedAtAction(nameof(GetAccountById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Get account by id.
        /// </summary>
        /// <returns>specific account</returns>
        /// <response code="200">account</response>
        /// <response code="400">if account id is empty</response>
        /// <response code="401">if account header is not present or invalid</response>
        /// <response code="404">if account is not found</response>
        /// <response code="500">if unhandled exception occurs</response>
        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(GetAccountByIdResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResourceNotFoundProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedRequestProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAccountById([FromRoute] Guid id)
        {
            GetAccountByIdResponse response = await mediator.Send(new GetAccountByIdRequest(id));

            return Ok(response);
        }

        /// <summary>
        /// Delete account.
        /// </summary>
        /// <returns>specific account</returns>
        /// <response code="200">deleted account</response>
        /// <response code="204">if account is already deleted</response>
        /// <response code="401">if account header is not present or invalid</response>
        /// <response code="500">if unhandled exception occurs</response>
        [HttpDelete]
        [ProducesResponseType(typeof(DeleteAccountResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResourceNotFoundProblemDetails), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedRequestProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> DeleteAccount([FromHeader(Name = "X-Account-Id")] Guid id)
        {
            DeleteAccountResponse response = await mediator.Send(new DeleteAccountRequest(id));
            if (response.Id == Guid.Empty)
            {
                return NoContent();
            }
            return Ok(response);
        }
    }
}

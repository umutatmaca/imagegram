using Imagegram.Application.Requests;
using Imagegram.Application.Responses;
using Imagegram.Web.API.Problems;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Imagegram.Web.API.Controllers
{
    /// <summary>
    /// post comments management endpoints
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public CommentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// create comments on posts with given creator account
        /// </summary>
        /// <returns>newly created comment account</returns>
        /// <response code="201">newly created comment</response>
        /// <response code="400">if creatorId, postId or content is empty</response>
        /// <response code="401">if account header is not present or invalid</response>
        /// <response code="404">if creator account or post is not found</response>
        /// <response code="500">if unhandled exception occurs</response>
        [HttpPost("~/api/posts/{postId}/comments")]
        [ProducesResponseType(typeof(CreateCommentResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BadRequestProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResourceNotFoundProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedRequestProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> CreateComment([FromHeader(Name = "X-Account-Id")] Guid creatorId, [FromRoute] Guid postId, [FromBody] string content)
        {
            CreateCommentResponse response = await mediator.Send(new CreateCommentRequest(creatorId, postId, content));

            return CreatedAtAction(nameof(GetCommentById), new { postId = postId, commentId = response.Id }, response);
        }

        /// <summary>
        /// get comment with given comment id for given post id.
        /// </summary>
        /// <returns>specific comment belonging to a post</returns>
        /// <response code="200">newly created comment</response>
        /// <response code="400">if postId or commentId is empty</response>
        /// <response code="401">if account header is not present or invalid</response>
        /// <response code="404">if comment is not found</response>
        /// <response code="500">if unhandled exception occurs</response>
        [HttpGet, Route("~/api/posts/{postId}/comments/{commentId}")]
        [ProducesResponseType(typeof(GetPostCommentByIdResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResourceNotFoundProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedRequestProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetCommentById([FromRoute] Guid postId, [FromRoute] Guid commentId)
        {
            GetPostCommentByIdResponse response = await mediator.Send(new GetPostCommentByIdRequest(postId, commentId));

            return Ok(response);
        }

        /// <summary>
        /// get comments on a post with given postId
        /// </summary>
        /// <returns>comments under a post</returns>
        /// <response code="200">comments uncer a post</response>
        /// <response code="400">if postId is empty, pageNumber or pageSize is less than or equal to zero</response>
        /// <response code="401">if account header is not present or invalid</response>
        /// <response code="500">if unhandled exception occurs</response>
        [HttpGet("~/api/posts/{postId}/comments")]
        [ProducesResponseType(typeof(GetPostCommentsByPostIdResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedRequestProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetPostComments([FromRoute] Guid postId, [FromQuery] int pageSize = 50, [FromQuery] int pageNumber = 1)
        {
            GetPostCommentsByPostIdResponse response = await mediator.Send(new GetPostCommentsByPostIdRequest(postId, pageSize, pageNumber));

            return Ok(response);
        }
    }
}

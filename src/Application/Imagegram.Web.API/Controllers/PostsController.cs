using Imagegram.Application.Requests;
using Imagegram.Application.Responses;
using Imagegram.Web.API.Problems;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Imagegram.Web.API.Controllers
{
    /// <summary>
    /// posts management endpoints
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PostsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PostsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// create posts for given account
        /// </summary>
        /// <returns></returns>
        /// <returns>newly created post</returns>
        /// <response code="201">newly created post</response>
        /// <response code="400">if creatorId or image empty or image type is not valid</response>
        /// <response code="401">if account header is not present or invalid</response>
        /// <response code="404">if creator account is not found</response>
        /// <response code="500">if unhandled exception occurs</response>
        [HttpPost, DisableRequestSizeLimit]
        [ProducesResponseType(typeof(CreatePostResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BadRequestProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResourceNotFoundProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedRequestProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreatePost([FromHeader(Name = "X-Account-Id")] Guid creatorId, [FromForm] IFormFile imageFile)
        {
            var form = await Request.ReadFormAsync();
            CreatePostResponse response = await mediator.Send(new CreatePostRequest(creatorId, form.Files[0]));
            return CreatedAtAction(nameof(GetPostById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Gets the post by id.
        /// </summary>
        /// <returns>specific post</returns>
        /// <response code="200">specific post</response>
        /// <response code="400">if post id is empty</response>
        /// <response code="401">if account header is not present or invalid</response>
        /// <response code="404">if post is not found</response>
        /// <response code="500">if unhandled exception occurs</response>
        [HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(GetPostByIdResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResourceNotFoundProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedRequestProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetPostById([FromRoute] Guid id)
        {
            GetPostByIdResponse response = await mediator.Send(new GetPostByIdRequest(id));

            return Ok(response);
        }

        /// <summary>
        /// get all posts with last 3 comments
        /// </summary>
        /// <returns>all posts with last 3 comments</returns>
        /// <response code="200">all posts</response>
        /// <response code="400">if pageNumber or pageSize is less than or equal to zero</response>
        /// <response code="401">if account header is not present or invalid</response>
        /// <response code="500">if unhandled exception occurs</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetAllPostsWithLastCommentsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedRequestProblemDetails), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetPosts([FromQuery] int pageSize = 50, [FromQuery] int pageNumber = 1)
        {
            GetAllPostsWithLastCommentsResponse response = await mediator.Send(new GetAllPostsWithLastCommentsRequest(pageSize, pageNumber));
            return Ok(response);
        }
    }
}

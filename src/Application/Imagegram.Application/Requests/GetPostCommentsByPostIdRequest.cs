using Imagegram.Application.Responses;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Imagegram.Application.Requests
{
    /// <summary>
    /// fetch post comments by id query
    /// </summary>
    public class GetPostCommentsByPostIdRequest : IRequest<GetPostCommentsByPostIdResponse>
    {
        /// <summary>
        /// post id to fetch comments of
        /// </summary>
        [Required]
        public Guid PostId { get; }

        /// <summary>
        /// page size
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// page number
        /// </summary>
        public int PageNumber { get; }
        public GetPostCommentsByPostIdRequest(Guid postId, int pageSize = 50, int pageNumber = 1)
        {
            PostId = postId;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}

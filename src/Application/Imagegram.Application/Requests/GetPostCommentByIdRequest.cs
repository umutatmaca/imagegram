using Imagegram.Application.Responses;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Imagegram.Application.Requests
{
    /// <summary>
    /// fetch comments by id query
    /// </summary>
    public class GetPostCommentByIdRequest : IRequest<GetPostCommentByIdResponse>
    {
        /// <summary>
        /// post id to fetch comments for
        /// </summary>
        [Required]
        public Guid PostId { get; }

        /// <summary>
        /// comment id to fetch exactly
        /// </summary>
        [Required]
        public Guid CommentId { get; }
        public GetPostCommentByIdRequest(Guid postId, Guid commentId)
        {
            PostId = postId;
            CommentId = commentId;
        }
    }
}

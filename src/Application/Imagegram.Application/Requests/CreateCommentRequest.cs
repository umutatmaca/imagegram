using Imagegram.Application.Responses;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Imagegram.Application.Requests
{
    /// <summary>
    /// comment creation command
    /// </summary>
    public class CreateCommentRequest : IRequest<CreateCommentResponse>
    {
        /// <summary>
        /// creator account id
        /// </summary>
        [Required]
        public Guid CreatorId { get; }

        /// <summary>
        /// post id to create comment for
        /// </summary>
        [Required]
        public Guid PostId { get; }

        /// <summary>
        /// comment content
        /// </summary>
        [Required]
        public string Content { get; }
        public CreateCommentRequest(Guid creatorId, Guid postId, string content)
        {
            CreatorId = creatorId;
            PostId = postId;
            Content = content;
        }
    }
}

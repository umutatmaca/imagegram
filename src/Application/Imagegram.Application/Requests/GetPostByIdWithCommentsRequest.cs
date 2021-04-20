using Imagegram.Application.Responses;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Imagegram.Application.Requests
{
    /// <summary>
    /// fetch post by id with comments query
    /// </summary>
    public class GetPostByIdWithCommentsRequest : IRequest<GetPostByIdResponse>
    {
        /// <summary>
        /// post id to fetch
        /// </summary>
        [Required]
        public Guid Id { get; }
        public GetPostByIdWithCommentsRequest(Guid id)
        {
            Id = id;
        }
    }
}

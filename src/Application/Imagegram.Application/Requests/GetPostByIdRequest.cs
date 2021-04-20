using Imagegram.Application.Responses;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Imagegram.Application.Requests
{
    /// <summary>
    /// fetch post by id query
    /// </summary>
    public class GetPostByIdRequest : IRequest<GetPostByIdResponse>
    {
        /// <summary>
        /// post id to fetch
        /// </summary>
        [Required]
        public Guid Id { get; }
        public GetPostByIdRequest(Guid id)
        {
            Id = id;
        }
    }
}

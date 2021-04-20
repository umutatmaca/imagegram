using Imagegram.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Imagegram.Application.Requests
{
    /// <summary>
    /// post creation command
    /// </summary>
    public class CreatePostRequest : IRequest<CreatePostResponse>
    {
        /// <summary>
        /// account which is creating the post
        /// </summary>
        [Required]
        public Guid AccountId { get; }

        /// <summary>
        /// image file coming along with the request
        /// </summary>
        [Required]
        public IFormFile ImageFile { get; }
        public CreatePostRequest(Guid accountId, IFormFile imageFile)
        {
            AccountId = accountId;
            ImageFile = imageFile;
        }
    }
}

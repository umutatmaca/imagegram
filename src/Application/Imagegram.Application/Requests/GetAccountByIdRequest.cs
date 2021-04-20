using Imagegram.Application.Responses;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Imagegram.Application.Requests
{
    /// <summary>
    /// account fetch by id query
    /// </summary>
    public class GetAccountByIdRequest : IRequest<GetAccountByIdResponse>
    {
        /// <summary>
        /// account id to fetch the info for
        /// </summary>
        [Required]
        public Guid Id { get; }
        public GetAccountByIdRequest(Guid id)
        {
            Id = id;
        }
    }
}

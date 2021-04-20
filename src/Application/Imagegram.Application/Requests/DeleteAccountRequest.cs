using Imagegram.Application.Responses;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Imagegram.Application.Requests
{
    /// <summary>
    /// account deletion command
    /// </summary>
    public class DeleteAccountRequest : IRequest<DeleteAccountResponse>
    {
        /// <summary>
        /// account id subject to delete operation
        /// </summary>
        [Required]
        public Guid Id { get; }
        public DeleteAccountRequest(Guid id)
        {
            Id = id;
        }
    }
}

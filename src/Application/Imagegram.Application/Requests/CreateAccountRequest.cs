using Imagegram.Application.Responses;
using MediatR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Imagegram.Application.Requests
{
    /// <summary>
    /// account creation command
    /// </summary>
    public class CreateAccountRequest : IRequest<CreateAccountResponse>
    {
        /// <summary>
        /// name of the account
        /// </summary>
        [Required]
        [Description("name of the account")]
        public string Name { get; }
        public CreateAccountRequest(string name)
        {
            Name = name;
        }
    }
}
